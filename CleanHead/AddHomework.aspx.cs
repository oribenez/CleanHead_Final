using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AddHomework : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lvl_id"] == null) {
            Response.Redirect("~/Errors/403Error.aspx");
        }
        else if (Convert.ToInt32(Session["lvl_id"]) == 0 || Convert.ToInt32(Session["lvl_id"]) == 2) {
            Response.Redirect("~/Errors/403Error.aspx");
        }

        if (!IsPostBack) {
            if (Convert.ToInt32(Session["lvl_id"]) == 3) { // if עורך בית ספר
                ddlProfessions.Enabled = false;
                ddlTeachers.Enabled = false;
                ddlLessons.Enabled = false;
            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 4) { // if מנהל תוכנה ומעלה
                ddlLayers.Enabled = false;
                ddlProfessions.Enabled = false;
                ddlTeachers.Enabled = false;
                ddlLessons.Enabled = false;
            }
        }
    }
    protected void ValidateDate_ServerValidate(object source, ServerValidateEventArgs args) {
        DateTime dt;
        if (DateTime.TryParse(args.Value, out dt)) {
            args.IsValid = true;
        }
        else {
            args.IsValid = false;
        }
    }
    protected void ddlSchools_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            //DDL Schools
            DataSet dsSchools = ch_schoolsSvc.GetSchools();

            ddlSchools.DataSource = dsSchools;
            ddlSchools.DataValueField = "sc_id";
            dsSchools.Tables[0].Columns.Add("school", typeof(string), "sc_name + ' - ' + cty_name");
            ddlSchools.DataTextField = "school";
            ddlSchools.DataBind();

            ddlSchools.Items.Add(new ListItem("-בחר בית ספר-", "-1"));
            ddlSchools.SelectedIndex = ddlSchools.Items.Count - 1;
        }
    }
    protected void ddlSchools_SelectedIndexChanged(object sender, EventArgs e) {
        if (ddlSchools.SelectedValue != "-1") {
            ddlLayers.Enabled = true;
            ddlLayers.Items.Clear();
            //DDL layers
            string[] layers = ch_roomsSvc.GetClassesLayers(Convert.ToInt32(ddlSchools.SelectedValue));
            for (int i = 0; i < layers.Length; i++) {
                ddlLayers.Items.Add(layers[i]);
            }

            ddlLayers.Items.Add(new ListItem("-בחר שכבה-", "-1"));
            ddlLayers.SelectedIndex = ddlLayers.Items.Count - 1;

            if (ddlLayers.SelectedValue != "-1") {
                ddlLessons.Enabled = true;
            }
            else {
                ddlLessons.Enabled = false;
            }
        }
        else {
            ddlLayers.Enabled = false;
        }
    }
    protected void ddlLayers_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            if (Convert.ToInt32(Session["lvl_id"]) == 3) {
                //DDL layers
                string[] layers = ch_roomsSvc.GetClassesLayers(Convert.ToInt32(Session["sc_id"]));
                for (int i = 0; i < layers.Length; i++) {
                    ddlLayers.Items.Add(layers[i]);
                }

                ddlLayers.Items.Add(new ListItem("-בחר שכבה-", "-1"));
                ddlLayers.SelectedIndex = ddlLayers.Items.Count - 1;
            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
                //DDL layers
                string[] layers = ch_roomsSvc.GetClassesLayers(Convert.ToInt32(ddlSchools.SelectedValue));
                for (int i = 0; i < layers.Length; i++) {
                    ddlLayers.Items.Add(layers[i]);
                }

                ddlLayers.Items.Add(new ListItem("-בחר שכבה-", "-1"));
                ddlLayers.SelectedIndex = ddlLayers.Items.Count - 1;
            }

        }
    }
    protected void ddlLayers_SelectedIndexChanged(object sender, EventArgs e) {
        if (ddlLayers.SelectedValue == "-1") { // אם לא נבחרה שכבה
            // איפוס
            ddlProfessions.Items.Clear();
            ddlTeachers.Items.Clear();
            ddlLessons.Items.Clear();

            ddlProfessions.Enabled = false;
            ddlTeachers.Enabled = false;
            ddlLessons.Enabled = false;
        }
        else { // אם נבחרה שכבה
            //data bind professions
            DataSet dsProfessions = ch_professionsSvc.GetLayerProfessions(ddlLayers.SelectedItem.Text);

            ddlProfessions.DataSource = dsProfessions;
            ddlProfessions.DataValueField = "pro_id";
            ddlProfessions.DataTextField = "pro_name";
            ddlProfessions.DataBind();

            ddlProfessions.Items.Add(new ListItem("-בחר מקצוע-", "-1"));
            ddlProfessions.SelectedIndex = ddlProfessions.Items.Count - 1;

            ddlProfessions.Enabled = true;
        }
    }
    protected void ddlProfessions_Load(object sender, EventArgs e) {

    }
    protected void ddlProfessions_SelectedIndexChanged(object sender, EventArgs e) {
        if (ddlLayers.SelectedValue == "-1") { // אם לא נבחר מקצוע
            // איפוס
            ddlTeachers.Items.Clear();
            ddlLessons.Items.Clear();

            ddlTeachers.Enabled = false;
            ddlLessons.Enabled = false;
        }
        else { // אם נבחר מקצוע
            ddlTeachers.Enabled = true;
            ddlLessons.Enabled = true;
            //data bind teachers
            int sc_id = Convert.ToInt32(Session["sc_id"]);
            int pro_id = Convert.ToInt32(ddlProfessions.SelectedValue);
            DataSet dsTeachers = ch_teachers_professionsSvc.GetTeachersByPro(pro_id);

            ddlTeachers.DataSource = dsTeachers;
            ddlTeachers.DataValueField = "usr.usr_id";
            dsTeachers.Tables[0].Columns.Add("usr_fullname", typeof(string), "usr_first_name + ' ' + usr_last_name");
            ddlTeachers.DataTextField = "usr_fullname";
            ddlTeachers.DataBind();

            ddlTeachers.Items.Add(new ListItem("-כל המורים-", "-1"));
            ddlTeachers.SelectedIndex = ddlTeachers.Items.Count - 1;


            // מציג את כל השיעורים לפי: בית ספר + מקצוע + שכבה
            //data bind lessons
            string layer = ddlLayers.SelectedItem.Text;
            DataSet dsSchoolLayerLessons = ch_lessonsSvc.GetLessons(sc_id, layer, pro_id);

            ddlLessons.DataSource = dsSchoolLayerLessons;
            ddlLessons.DataValueField = "מזהה השיעור";
            ddlLessons.DataTextField = "les_name";
            ddlLessons.DataBind();

            ddlLessons.Items.Add(new ListItem("-בחר שיעור-", "-1"));
            ddlLessons.SelectedIndex = ddlLessons.Items.Count - 1;
        }
    }
    protected void ddlTeachers_Load(object sender, EventArgs e) {

    }
    protected void ddlTeachers_SelectedIndexChanged(object sender, EventArgs e) {
        if (ddlTeachers.SelectedValue == "-1") { // אם לא נבחר מורה
            // מציג את כל השיעורים לפי: בית ספר + מקצוע + שכבה
            //data bind lessons
            int sc_id = int.Parse(Session["sc_id"].ToString());
            string layer = ddlLayers.SelectedItem.Text;
            int pro_id = Convert.ToInt32(ddlProfessions.SelectedValue);
            DataSet dsSchoolLayerLessons = ch_lessonsSvc.GetLessons(sc_id, layer, pro_id);

            ddlLessons.DataSource = dsSchoolLayerLessons;
            ddlLessons.DataValueField = "מזהה השיעור";
            ddlLessons.DataTextField = "les_name";
            ddlLessons.DataBind();

            ddlLessons.Items.Add(new ListItem("-בחר שיעור-", "-1"));
            ddlLessons.SelectedIndex = ddlLessons.Items.Count - 1;
        }
        else { // אם נבחר מורה
            // מציג את כל השיעורים לפי: בית ספר + מקצוע + שכבה + מורה
            //data bind lessons
            int sc_id = int.Parse(Session["sc_id"].ToString());
            string layer = ddlLayers.SelectedItem.Text;
            int pro_id = Convert.ToInt32(ddlProfessions.SelectedValue);
            int tch_id = Convert.ToInt32(ddlTeachers.SelectedValue);
            DataSet dsSchoolLayerLessons = ch_lessonsSvc.GetLessons(sc_id, layer, pro_id, tch_id);

            ddlLessons.DataSource = dsSchoolLayerLessons;
            ddlLessons.DataValueField = "מזהה השיעור";
            ddlLessons.DataTextField = "les_name";
            ddlLessons.DataBind();

            ddlLessons.Items.Add(new ListItem("-בחר שיעור-", "-1"));
            ddlLessons.SelectedIndex = ddlLessons.Items.Count - 1;
        }
    }
    protected void ddlLessons_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            if (Convert.ToInt32(Session["lvl_id"]) == 0) { // if סטודנט
                //DDL Lessons
                int stu_id = int.Parse(Session["usr_id"].ToString());
                DataSet dsStudentLessons = ch_lessonsSvc.GetStudentLessons(stu_id);

                ddlLessons.DataSource = dsStudentLessons;
                ddlLessons.DataValueField = "מזהה השיעור";
                ddlLessons.DataTextField = "שם השיעור";
                ddlLessons.DataBind();

                ddlLessons.Items.Add(new ListItem("-כל השיעורים-", "-1"));
                ddlLessons.SelectedIndex = ddlLessons.Items.Count - 1;
            }
            else if (Convert.ToInt32(Session["lvl_id"]) == 1) { // if מורה
                //DDL Lessons
                int tch_id = int.Parse(Session["usr_id"].ToString());
                DataSet dsTeacherLessons = ch_lessonsSvc.GetTeacherLessons(tch_id);

                ddlLessons.DataSource = dsTeacherLessons;
                ddlLessons.DataValueField = "מזהה השיעור";
                ddlLessons.DataTextField = "שם השיעור";
                ddlLessons.DataBind();

                ddlLessons.Items.Add(new ListItem("-בחר שיעור-", "-1"));
                ddlLessons.SelectedValue = "-1";
            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 4) { // if מנהל תוכנה ומעלה
                //DDL Lessons
                int tch_id = int.Parse(Session["usr_id"].ToString());
                DataSet dsTeacherLessons = ch_lessonsSvc.GetTeacherLessons(tch_id);

                ddlLessons.DataSource = dsTeacherLessons;
                ddlLessons.DataValueField = "מזהה השיעור";
                ddlLessons.DataTextField = "שם השיעור";
                ddlLessons.DataBind();

                ddlLessons.Items.Add(new ListItem("-בחר שיעור-", "-1"));
                ddlLessons.SelectedIndex = ddlLessons.Items.Count - 1;

                ddlLessons.Enabled = false;
            }
        }
    }
    protected void ddlLessons_SelectedIndexChanged(object sender, EventArgs e) {

    }
    protected void ddlDeadlineHr_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            //DDL Lessons
            int sc_id = Convert.ToInt32(Session["sc_id"]);
            DataSet dsHours = ch_hoursSvc.GetHours(sc_id);

            ddlDeadlineHr.DataSource = dsHours;
            ddlDeadlineHr.DataValueField = "hr_id";
            ddlDeadlineHr.DataTextField = "hr_name";
            ddlDeadlineHr.DataBind();

            ddlDeadlineHr.Items.Add(new ListItem("-בחר שעה-", "-1"));
            ddlDeadlineHr.SelectedIndex = ddlDeadlineHr.Items.Count - 1;
        }
    }
    protected void btnAddHw_Click(object sender, EventArgs e) {
        //check validation
        if (!ValidatePage()) { return; }

        int les_id = Convert.ToInt32(ddlLessons.SelectedValue);
        int hr_id = Convert.ToInt32(ddlDeadlineHr.SelectedValue);

        ch_homework hw = new ch_homework(les_id, txtHw.Text, txtDeadlineDate.Text, hr_id);

        lblErr.Text = ch_homeworkSvc.AddHomework(hw);

        if (lblErr.Text == "") {
            Response.Redirect("Homework.aspx");
        }

    }
    protected bool ValidatePage() {
        if (ddlLessons.SelectedValue == "-1" || ddlLessons.Items.Count == 0) {
            lblErr.Text = "בחר שיעור";
            return false;
        }
        return true;
    }
}