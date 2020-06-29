using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Globalization;
public partial class UpdateGrades : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) {
        if (Session["lvl_id"] == null) {
            Response.Redirect("~/Errors/403Error.aspx");
        }
        else if (Convert.ToInt32(Session["lvl_id"]) == 0 || Convert.ToInt32(Session["lvl_id"]) == 2) {
            Response.Redirect("~/Errors/403Error.aspx");
        }

        if (Request.QueryString["grd_id"] == null) {
            Response.Redirect("Grades.aspx");
        }

        if (!IsPostBack) {
            int grd_id = Convert.ToInt32(Request.QueryString["grd_id"]);

            DataRow drTest = ch_gradesSvc.GetTest(grd_id);
            txtGradeName.Text = drTest["grd_name"].ToString();
            txtGradeDate.Text = Convert.ToDateTime(drTest["grd_date"].ToString()).ToString("dd/MM/yyyy");

            DataSet studentsInLesson = ch_students_lessonsSvc.GetStudentsLesson(Convert.ToInt32(drTest["les_id"].ToString()));
            GridViewSvc.GVBind(studentsInLesson, gvStudents);

            //after gridview bind
            DataSet dsUsersGrades = ch_users_gradesSvc.GetUsersGrades(grd_id);
            int i = 0;
            foreach (DataRow dr in dsUsersGrades.Tables[0].Rows) {
                TextBox txtGrade = (TextBox)gvStudents.Rows[i].FindControl("txtGrade");
                txtGrade.Text = dr["grd_num"].ToString();
                i++;
            }
        }
    }
    //protected void ddlLessons_Load(object sender, EventArgs e) {
    //    if (!IsPostBack) {

            

    //        //DDL Lessons
    //        int tch_id;
    //        if (Convert.ToInt32(Session["lvl_id"]) == 1) {
    //            tch_id = int.Parse(Session["usr_id"].ToString());
    //        }
    //        else {
    //            int grd_id = Convert.ToInt32(Request.QueryString["grd_id"]);
    //            DataRow drTest = ch_gradesSvc.GetTest(grd_id);
    //            DataSet dsTeachers = ch_teachers_lessonsSvc.GetTeachersLesson(Convert.ToInt32(drTest["les_id"].ToString()));
    //            tch_id = dsTeachers.
    //        }
    //        DataSet dsTeacherLessons = ch_lessonsSvc.GetTeacherLessons(tch_id);
    //        if (dsTeacherLessons.Tables[0].Rows.Count == 0) {
    //            pnlPermission.Visible = false;
    //            lblErr.Text = "אין שיעורים להכניס אליהם מבחנים (בקש ממנהל להוסיף שיעור).";
    //        }

    //        ddlLessons.DataSource = dsTeacherLessons;
    //        ddlLessons.DataValueField = "מזהה השיעור";
    //        ddlLessons.DataTextField = "שם השיעור";
    //        ddlLessons.DataBind();
    //    }
    //}
    protected void ValidateDate_ServerValidate(object source, ServerValidateEventArgs args) {
        DateTime dt;
        if (DateTime.TryParseExact(txtGradeDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)) {
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
    //protected void ddlLessons_SelectedIndexChanged(object sender, EventArgs e) {
    //    int les_id = Convert.ToInt32(ddlLessons.SelectedValue);
    //    if (les_id != -1) {
    //        lblStudentsGrades.Visible = true;
    //        DataSet studentsInLesson = ch_students_lessonsSvc.GetStudentsLesson(les_id);
    //        GridViewSvc.GVBind(studentsInLesson, gvStudents);

    //        //after gridview bind
    //        int grd_id = Convert.ToInt32(Request.QueryString["grd_id"]);
    //        DataRow drTest = ch_gradesSvc.GetTest(grd_id);

    //        if (Convert.ToInt32(drTest["les_id"]) == les_id) {
    //            DataSet dsUsersGrades = ch_users_gradesSvc.GetUsersGrades(grd_id);
    //            int i = 0;
    //            foreach (DataRow dr in dsUsersGrades.Tables[0].Rows) {
    //                TextBox txtGrade = (TextBox)gvStudents.Rows[i].FindControl("txtGrade");
    //                txtGrade.Text = dr["grd_num"].ToString();
    //                i++;
    //            }
    //        }
    //    }
    //    else {
    //        lblStudentsGrades.Visible = false;
    //        DataSet studentsInLesson = ch_students_lessonsSvc.GetStudentsLesson(les_id);
    //        for (int i = 0; i < studentsInLesson.Tables[0].Rows.Count; i++) {
    //            studentsInLesson.Tables[0].Rows.RemoveAt(i);
    //        }
    //        GridViewSvc.GVBind(studentsInLesson, gvStudents);
    //    }
    //}
    protected void gvStudents_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            gvStudents.Columns[0].Visible = false; //visible false the = usr_id
        }
    }
    protected bool ValidatePage() {
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
    protected void btnUpdateGrades_Click(object sender, EventArgs e) {
        if (ValidatePage()) {
            string gradeName = txtGradeName.Text;
            string gradeDate = DateTime.ParseExact(txtGradeDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");


            int grd_id = Convert.ToInt32(Request.QueryString["grd_id"]);
            DataRow drTest = ch_gradesSvc.GetTest(grd_id);
            int les_id = Convert.ToInt32(drTest["les_id"].ToString());

            ch_grades newGrades = new ch_grades(les_id, gradeName, gradeDate);
            lblErr.Text = ch_gradesSvc.UpdateGrade(grd_id, newGrades);

            ch_users_gradesSvc.DeleteUsersGradesByTest(grd_id);

            foreach (GridViewRow gvr in gvStudents.Rows) {
                int usr_id = Convert.ToInt32(((Label)gvr.FindControl("lbl_usr_id")).Text);
                int grd_num = Convert.ToInt32(((TextBox)gvr.FindControl("txtGrade")).Text);

                ch_users_grades usr_grd = new ch_users_grades(usr_id, grd_id, grd_num);
                ch_users_gradesSvc.AddUsrGrade(usr_grd);
            }
            if (lblErr.Text == "") {
                Response.Redirect("Grades.aspx");
            }
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e) {
        int grd_id = Convert.ToInt32(Request.QueryString["grd_id"]);
        ch_gradesSvc.DeleteGrade(grd_id);
        Response.Redirect("Grades.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e) {
        Response.Redirect("Grades.aspx");
    }
    protected void lblLesson_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            int grd_id = Convert.ToInt32(Request.QueryString["grd_id"]);
            DataRow drTest = ch_gradesSvc.GetTest(grd_id);
            int les_id = Convert.ToInt32(drTest["les_id"].ToString());
            DataRow drLessons = ch_lessonsSvc.GetLesson(les_id);
            lblLesson.Text = drLessons["les_name"].ToString();

            DataSet studentsInLesson = ch_students_lessonsSvc.GetStudentsLesson(les_id);
            GridViewSvc.GVBind(studentsInLesson, gvStudents);

            //after gridview bind
            if (Convert.ToInt32(drTest["les_id"]) == les_id) {
                DataSet dsUsersGrades = ch_users_gradesSvc.GetUsersGrades(grd_id);
                int i = 0;
                foreach (DataRow dr in dsUsersGrades.Tables[0].Rows) {
                    TextBox txtGrade = (TextBox)gvStudents.Rows[i].FindControl("txtGrade");
                    txtGrade.Text = dr["grd_num"].ToString();
                    i++;
                }
            }
        }
    }
}