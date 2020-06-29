using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Globalization;

public partial class AddGrades : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lvl_id"] == null)
        {
            Response.Redirect("~/Errors/403Error.aspx");
        }
        else if (Convert.ToInt32(Session["lvl_id"]) == 0 || Convert.ToInt32(Session["lvl_id"]) == 2)
        {
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
    protected void ddlLessons_Load(object sender, EventArgs e) {
        if (!IsPostBack)
        {
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
    protected void ValidateDate_ServerValidate(object source, ServerValidateEventArgs args) {
        DateTime dt;
        if (DateTime.TryParseExact(txtGradeDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)) {
            if (dt <= DateTime.Now) {
                if (dt.Month <= 8 && (dt.Year == DateTime.Now.Year || dt.Year == DateTime.Now.Year-1)) { // אם התאריך המבוקש נמצא בשנת הלימודים הנוכחית
                    args.IsValid = true;
                }
                else if (dt.Month >= 9 && dt.Year == DateTime.Now.Year) { // אם התאריך המבוקש נמצא בשנת הלימודים הנוכחית
                    args.IsValid = true;
                }
            }
        }
        else {
            args.IsValid = false;
        }
    }
    protected void ddlLessons_SelectedIndexChanged(object sender, EventArgs e) {
        int les_id = Convert.ToInt32(ddlLessons.SelectedValue);
        if (les_id != -1) {
            lblStudentsGrades.Visible = true;
            DataSet studentsInLesson = ch_students_lessonsSvc.GetStudentsLesson(les_id);
            GridViewSvc.GVBind(studentsInLesson, gvStudents);
        }
        else {
            lblStudentsGrades.Visible = false;
            DataSet studentsInLesson = ch_students_lessonsSvc.GetStudentsLesson(les_id);
            for (int i = 0; i < studentsInLesson.Tables[0].Rows.Count; i++) {
                studentsInLesson.Tables[0].Rows.RemoveAt(i);
            }
            GridViewSvc.GVBind(studentsInLesson, gvStudents);
        }
    }
    protected void gvStudents_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            gvStudents.Columns[0].Visible = false; //visible false the = usr_id
        }
    }
    protected bool ValidatePage() {
        if (ddlLessons.SelectedValue == "-1" || ddlLessons.Items.Count == 0) {
            lblErr.Text = "בחר שיעור";
            return false;
        }
        foreach (GridViewRow gvr in gvStudents.Rows) {
            TextBox txtGrade = (TextBox)gvr.FindControl("txtGrade");
            if (!Regex.IsMatch(txtGrade.Text, "^[0-9]{0,3}$")) {
                lblErr.Text = "מספרים בלבד בהכנסת ציונים";
                return false;
            }
            if (Convert.ToInt32(txtGrade.Text) > 100 || Convert.ToInt32(txtGrade.Text) < 0) {
                lblErr.Text = "ציון חייב להיות בין 0 ל-100";
                return false;
            }
        }
        return true;
    }
    protected void btnAddGrades_Click(object sender, EventArgs e) {
        if (ValidatePage()) {
            string gradeName = txtGradeName.Text;
            string gradeDate = DateTime.ParseExact(txtGradeDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");

            int les_id = Convert.ToInt32(ddlLessons.SelectedValue);

            ch_grades newGrades = new ch_grades(les_id, gradeName, gradeDate);
            lblErr.Text = ch_gradesSvc.AddGrade(newGrades);

            int last_grd_id = ch_gradesSvc.GetMaxId();

            foreach (GridViewRow gvr in gvStudents.Rows) {
                int usr_id = Convert.ToInt32(((Label)gvr.FindControl("lbl_usr_id")).Text);
                int grd_num = Convert.ToInt32(((TextBox)gvr.FindControl("txtGrade")).Text);

                ch_users_grades usr_grd = new ch_users_grades(usr_id, last_grd_id, grd_num);
                ch_users_gradesSvc.AddUsrGrade(usr_grd);
            }
            if (lblErr.Text == "") {
                Response.Redirect("Grades.aspx");
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
            ddlLayers.Enabled = true;
            ddlLayers.Items.Clear();
            //DDL layers
            string[] layers = ch_roomsSvc.GetClassesLayers(Convert.ToInt32(ddlSchools.SelectedValue));
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
            ddlLayers.Enabled = false;
            ddlProfessions.Enabled = false;
            ddlTeachers.Enabled = false;
            ddlLessons.Enabled = false;
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
}