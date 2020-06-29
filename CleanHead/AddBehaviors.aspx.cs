using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Collections;

public partial class AddBehaviors : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["lvl_id"]) == 0) { // if Not teacher
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
                ddlHours.Enabled = false;
            }
            pnlBhv.Visible = true;

            
            //הצגה ראשונית לפני בחירת שיעור
            DataTable dt = ch_students_behaviorsSvc.GetStudentsBehaviorsToInsert(-1);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            GridViewSvc.GVBind(ds, gvBhv);
        }
        else {
            if (ddlLessons.Items.Count > 0) {
                if (ddlLessons.SelectedValue == "-1") { // אם לא נבחר שיעור
                    pnlBhv.Visible = false;
                }
                else { // אם נבחר מורה
                    pnlBhv.Visible = true;

                    int les_id = Convert.ToInt32(ddlLessons.SelectedValue);
                    DataTable dt = ch_students_behaviorsSvc.GetStudentsBehaviorsToInsert(les_id);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    GridViewSvc.GVBind(ds, gvBhv);
                }
            }
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
            int sc_id = Convert.ToInt32(ddlSchools.SelectedValue);

            //DDL Lessons
            ddlHours.Enabled = true;
            ddlHours.Items.Clear();
            DataSet dsHours = ch_hoursSvc.GetHours(sc_id);

            ddlHours.DataSource = dsHours;
            ddlHours.DataValueField = "hr_id";
            ddlHours.DataTextField = "hr_name";
            ddlHours.DataBind();

            ddlHours.Items.Add(new ListItem("-בחר שעה-", "-1"));
            ddlHours.SelectedIndex = ddlHours.Items.Count - 1;
            
            ddlLayers.Items.Clear();
            //DDL layers
            string[] layers = ch_roomsSvc.GetClassesLayers(sc_id);
            for (int i = 0; i < layers.Length; i++) {
                ddlLayers.Items.Add(layers[i]);
            }

            ddlLayers.Items.Add(new ListItem("-בחר שכבה-", "-1"));
            ddlLayers.SelectedIndex = ddlLayers.Items.Count - 1;

            ddlLayers.Enabled = true;
            ddlProfessions.Enabled = false;
            ddlTeachers.Enabled = false;
            ddlLessons.Enabled = false;
        }
        else {
            ddlHours.Enabled = false;
            ddlLayers.Enabled = false;
            ddlProfessions.Enabled = false;
            ddlTeachers.Enabled = false;
            ddlLessons.Enabled = false;
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

            ddlLayers.Enabled = true;
            ddlProfessions.Enabled = false;
            ddlTeachers.Enabled = false;
            ddlLessons.Enabled = false;
        }
        else { // אם נבחרה שכבה
            //data bind professions
            int sc_id;
            if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
                sc_id = Convert.ToInt32(ddlSchools.SelectedValue);
            }
            else {
                sc_id = Convert.ToInt32(Session["sc_id"]);
            } 
            
            string layer = ddlLayers.SelectedItem.Text;
            DataSet dsProfessions = ch_professionsSvc.GetLayerProfessions(sc_id, layer);

            ddlProfessions.DataSource = dsProfessions;
            ddlProfessions.DataValueField = "pro_id";
            ddlProfessions.DataTextField = "pro_name";
            ddlProfessions.DataBind();

            ddlProfessions.Items.Add(new ListItem("-בחר מקצוע-", "-1"));
            ddlProfessions.SelectedIndex = ddlProfessions.Items.Count - 1;

            ddlLayers.Enabled = true;
            ddlProfessions.Enabled = true;
            ddlTeachers.Enabled = false;
            ddlLessons.Enabled = false;
        }
    }
    protected void ddlProfessions_Load(object sender, EventArgs e) {

    }
    protected void ddlProfessions_SelectedIndexChanged(object sender, EventArgs e) {
        if (ddlLayers.SelectedValue == "-1") { // אם לא נבחר מקצוע
            // איפוס
            ddlTeachers.Items.Clear();
            ddlLessons.Items.Clear();

            ddlLayers.Enabled = true;
            ddlProfessions.Enabled = true;
            ddlTeachers.Enabled = false;
            ddlLessons.Enabled = false;
        }
        else { // אם נבחר מקצוע
            ddlLayers.Enabled = true;
            ddlProfessions.Enabled = true;
            ddlTeachers.Enabled = true;
            ddlLessons.Enabled = true;

            //data bind teachers
            int sc_id;
            if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
                sc_id = Convert.ToInt32(ddlSchools.SelectedValue);
            }
            else {
                sc_id = Convert.ToInt32(Session["sc_id"]);
            } 
            string layer = ddlLayers.SelectedItem.Text;
            int pro_id = Convert.ToInt32(ddlProfessions.SelectedValue);

            DataSet dsTeachers = ch_teachers_professionsSvc.GetTeachersByProAndSchool(sc_id, layer, pro_id);

            ddlTeachers.DataSource = dsTeachers;
            ddlTeachers.DataValueField = "usr_id";
            ddlTeachers.DataTextField = "usr_fullname";
            ddlTeachers.DataBind();

            ddlTeachers.Items.Add(new ListItem("-כל המורים-", "-1"));
            ddlTeachers.SelectedIndex = ddlTeachers.Items.Count - 1;


            // מציג את כל השיעורים לפי: בית ספר + מקצוע + שכבה
            //data bind lessons
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
        int sc_id;
        if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
            sc_id = Convert.ToInt32(ddlSchools.SelectedValue);
        }
        else {
            sc_id = Convert.ToInt32(Session["sc_id"]);
        } 
        string layer = ddlLayers.SelectedItem.Text;
        int pro_id = Convert.ToInt32(ddlProfessions.SelectedValue);
        int tch_id = Convert.ToInt32(ddlTeachers.SelectedValue);

        if (ddlTeachers.SelectedValue == "-1") { // אם לא נבחר מורה
            // מציג את כל השיעורים לפי: בית ספר + מקצוע + שכבה
            //data bind lessons

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
            if (Convert.ToInt32(Session["lvl_id"]) == 1) { // if מורה
                //DDL Lessons
                int tch_id = int.Parse(Session["usr_id"].ToString());
                DataSet dsTeacherLessons = ch_lessonsSvc.GetTeacherLessons(tch_id);

                ddlLessons.DataSource = dsTeacherLessons;
                ddlLessons.DataValueField = "מזהה השיעור";
                ddlLessons.DataTextField = "שם השיעור";
                ddlLessons.DataBind();

                ddlLessons.Items.Add(new ListItem("-בחר שיעור-", "-1"));
                ddlLessons.SelectedIndex = ddlLessons.Items.Count - 1;
            }
            
        }
    }
    protected void ddlLessons_SelectedIndexChanged(object sender, EventArgs e) {
        if (ddlLessons.SelectedValue == "-1") { // אם לא נבחר שיעור
            pnlBhv.Visible = false;
        }
        else { // אם נבחר מורה
            pnlBhv.Visible = true;

            int les_id = Convert.ToInt32(ddlLessons.SelectedValue);
            DataTable dt = ch_students_behaviorsSvc.GetStudentsBehaviorsToInsert(les_id);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            GridViewSvc.GVBind(ds, gvBhv);
        }
    }
    protected void gvBhv_RowCreated(object sender, GridViewRowEventArgs e) {
        //unvisible usr_id
        e.Row.Cells[0].Visible = false;
    }
    protected void gvBhv_RowDataBound(object sender, GridViewRowEventArgs e) {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            for (int i = 2; i < e.Row.Cells.Count; i++) {
                CheckBox ck = new CheckBox();
                ck.ID = "ckStuBhv_" + i;
                ck.CausesValidation = false;
                e.Row.Cells[i].Controls.Add(ck);
                Label lbl = new Label();
                lbl.Text = "";
                lbl.AssociatedControlID = "ckStuBhv_" + i;
                e.Row.Cells[i].Controls.Add(lbl);
            }
        }
    }
    protected void ValidateDate_ServerValidate(object source, ServerValidateEventArgs args) {
        DateTime dt;
        if (DateTime.TryParseExact(txtLesDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)) {
            if (dt <= DateTime.Now) {
                if (dt.Month <= 7 && (dt.Year == DateTime.Now.Year || dt.Year == DateTime.Now.Year - 1)) { // אם התאריך המבוקש נמצא בשנת הלימודים הנוכחית אבל לפני השנה הלועזית החדשה
                    args.IsValid = true;
                }
                else if (dt.Month >= 9 && dt.Year == DateTime.Now.Year) { // אם התאריך המבוקש נמצא בשנת הלימודים הנוכחית אבל אחרי השנה הלועזית החדשה
                    args.IsValid = true;
                }
            }
        }
        else {
            args.IsValid = false;
        }
    }
    protected void ddlHours_Load(object sender, EventArgs e) {
        if (Convert.ToInt32(Session["lvl_id"]) <= 3) {
            if (!IsPostBack) {
                //DDL Lessons
                int sc_id = Convert.ToInt32(Session["sc_id"]); ;
                DataSet dsHours = ch_hoursSvc.GetHours(sc_id);

                ddlHours.DataSource = dsHours;
                ddlHours.DataValueField = "hr_id";
                ddlHours.DataTextField = "hr_name";
                ddlHours.DataBind();

                ddlHours.Items.Add(new ListItem("-בחר שעה-", "-1"));
                ddlHours.SelectedIndex = ddlHours.Items.Count - 1;
            }
        }
    }
    protected bool ValidatePage() {
        if (ddlLessons.SelectedValue == "-1") {
            lblErrGV.Text = "בחר שיעור";
            return false;
        }

        if (txtLesDate.Text == "") {
            lblErrGV.Text = "בחר שיעור";
            return false;
        }

        if (ddlHours.SelectedValue == "-1") {
            lblErrGV.Text = "בחר שעת לימוד";
            return false;
        }

        return true;
    }
    protected void btnAddBhv_Click(object sender, EventArgs e) {

        if (!ValidatePage()) {
            return;
        }

        for (int i = 0; i < gvBhv.Rows.Count; i++) {
            for (int j = 2; j < gvBhv.Rows[i].Cells.Count; j++) {
                CheckBox ckCurrent = (CheckBox)gvBhv.Rows[i].FindControl("ckStuBhv_" + j);
                if (ckCurrent.Checked) {
                    ch_students_behaviors stu_bhvCurrent = new ch_students_behaviors();
                    stu_bhvCurrent.bhv_id = ch_behaviorsSvc.GetIdByBhvName(gvBhv.HeaderRow.Cells[j].Text);
                    stu_bhvCurrent.stu_id = Convert.ToInt32(gvBhv.Rows[i].Cells[0].Text);
                    stu_bhvCurrent.les_id = Convert.ToInt32(ddlLessons.SelectedValue);
                    stu_bhvCurrent.stu_bhv_date = DateTime.ParseExact(txtLesDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    stu_bhvCurrent.hr_id = Convert.ToInt32(ddlHours.SelectedValue);

                    ch_students_behaviorsSvc.AddStuBehavior(stu_bhvCurrent);
                }
            }
        }
        Response.Redirect("Behaviors.aspx");
    }
}