using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Grades : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["lvl_id"]) == 2) { // if crew
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

    protected void BindGradesReport() {
        int les_id = Convert.ToInt32(ddlLessons.SelectedValue);
        int sc_id = Convert.ToInt32(ddlSchools.SelectedValue);
        DataSet gradesReport = ch_gradesSvc.GradesReport(sc_id, les_id);
        if (gradesReport.Tables[0].Rows.Count == 0) {
            pnlPercents.Visible = false;
        }
        GridViewSvc.GVBind(gradesReport, gvGrades);

        //add footer controls
        DataSet tests = ch_gradesSvc.GetTests(les_id);
        int i = 0;
        foreach (DataRow dr in tests.Tables[0].Rows) {
            int grd_id = Convert.ToInt32(tests.Tables[0].Rows[i]["grd_id"].ToString());

            HyperLink hlEdit = new HyperLink();
            hlEdit.NavigateUrl = "UpdateGrades.aspx?grd_id=" + grd_id;
            hlEdit.ImageUrl = "~/Images/ic_edit_24px.png";

            gvGrades.Rows[gradesReport.Tables[0].Rows.Count - 1].Cells[i + 3].Controls.Add(hlEdit);
            i++;
        }
    }

    protected void ddlLessons_Load(object sender, EventArgs e) {
        if (!IsPostBack)
        {
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
                ddlLessons.SelectedIndex = 0;
            }
            else if (Convert.ToInt32(Session["lvl_id"]) == 3) { // if עורך בית ספר
                ////DDL Lessons
                //int sc_id = int.Parse(Session["sc_id"].ToString());
                //DataSet dsTeacherLessons = ch_lessonsSvc.GetSchoolLessons(sc_id);

                //ddlLessons.DataSource = dsTeacherLessons;
                //ddlLessons.DataValueField = "מזהה השיעור";
                //ddlLessons.DataTextField = "שם השיעור";
                //ddlLessons.DataBind();

                //ddlLessons.Items.Add(new ListItem("-בחר שיעור-", "-1"));
                //ddlLessons.SelectedIndex = ddlLessons.Items.Count - 1;
                //ddlLessons.Enabled = false;
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
        int les_id = Convert.ToInt32(ddlLessons.SelectedValue);
        //data bind
        if (Convert.ToInt32(Session["lvl_id"]) == 0) { // if סטודנט
            if (les_id != -1) { // שיעור מסויים
                pnlReport.Visible = true;

                int usr_id = Convert.ToInt32(Session["usr_id"]);
                DataSet gradesReportForStu = ch_gradesSvc.GradesReportForStu(usr_id, les_id);
                GridViewSvc.GVBind(gradesReportForStu, gvGrades);
            }
            else {
                pnlReport.Visible = true;
                pnlPercents.Visible = false;

                int usr_id = Convert.ToInt32(Session["usr_id"]);
                DataSet gradesReportForStu = ch_gradesSvc.GradesReportForStu(usr_id);
                GridViewSvc.GVBind(gradesReportForStu, gvGrades);
            }
        }
        else if (Convert.ToInt32(Session["lvl_id"]) == 1) { // if מורה
            pnlReport.Visible = true;
            pnlPercents.Visible = true;

            BindGradesReport();

            DataSet tests = ch_gradesSvc.GetTests(les_id);
            GridViewSvc.GVBind(tests, gvPercents);
        }
        else if (Convert.ToInt32(Session["lvl_id"]) >= 3) { // if עורך בית ספר ומעלה
            if (les_id == -1) { // לא נבחר שיעור
                pnlReport.Visible = false;
                pnlPercents.Visible = false;
            }
            else { // נבחר שיעור מסויים
                BindGradesReport();

                DataSet tests = ch_gradesSvc.GetTests(les_id);
                GridViewSvc.GVBind(tests, gvPercents);

                pnlReport.Visible = true;
                pnlPercents.Visible = true;
            } 
        }
    }
    protected void gvGrades_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            //data bind
            if (Convert.ToInt32(Session["lvl_id"]) == 0) { // if סטודנט
                int les_id = Convert.ToInt32(ddlLessons.SelectedValue);
                if (les_id != -1) { // שיעור מסויים
                    pnlReport.Visible = true;

                    int usr_id = Convert.ToInt32(Session["usr_id"]);
                    DataSet gradesReportForStu = ch_gradesSvc.GradesReportForStu(usr_id, les_id);
                    GridViewSvc.GVBind(gradesReportForStu, gvGrades);
                }
                else
	            {
                    pnlReport.Visible = true;
                    pnlPercents.Visible = false;

                    int usr_id = Convert.ToInt32(Session["usr_id"]);
                    DataSet gradesReportForStu = ch_gradesSvc.GradesReportForStu(usr_id);
                    GridViewSvc.GVBind(gradesReportForStu, gvGrades);
	            }
            }
            else if (Convert.ToInt32(Session["lvl_id"]) == 1) { // if מורה
                int les_id = Convert.ToInt32(ddlLessons.SelectedValue);
                pnlReport.Visible = true;
                pnlPercents.Visible = true;

                BindGradesReport();

                DataSet tests = ch_gradesSvc.GetTests(les_id);
                GridViewSvc.GVBind(tests, gvPercents);
            }
        }
    }
    protected void gvGrades_RowCreated(object sender, GridViewRowEventArgs e) {
        if (Convert.ToInt32(Session["lvl_id"]) == 0) {
            if (e.Row.Cells.Count >= 0) {
                e.Row.Cells[0].Visible = true;
            }
            if (e.Row.Cells.Count >= 2) {
                e.Row.Cells[2].Visible = true;
            }
        }
        else {
            if (e.Row.Cells.Count >= 0) {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.Cells.Count >= 2) {
                e.Row.Cells[2].Visible = false;
            }
        }
        
    }
    protected void tblPercents_Load(object sender, EventArgs e) {
        
    }
    protected void btnChangePercents_Click(object sender, EventArgs e) {
        if (ValidatePercents()) {
            int les_id = Convert.ToInt32(ddlLessons.SelectedValue);

            DataSet tests = ch_gradesSvc.GetTests(les_id);

            for (int i = 0; i < gvPercents.Rows.Count; i++) {
                int percents = Convert.ToInt32(((TextBox)gvPercents.Rows[i].Cells[1].FindControl("txt_grd_percents")).Text.Trim());
                int grd_id = Convert.ToInt32(tests.Tables[0].Rows[i]["grd_id"].ToString());
                ch_gradesSvc.PercentsUpdate(grd_id, percents);
            }
            //rebind GradesReport
            BindGradesReport();

            lblErr.Text = "האחוזים התעדכנו בהצלחה";

        }
    }

    private void imgbtnDelete_Click(object sender, ImageClickEventArgs e) {
        throw new NotImplementedException();
    }

    private void imgbtnEdit_Click(object sender, ImageClickEventArgs e) {
        ImageButton imgbtn = (ImageButton)sender;
        int les_id = Convert.ToInt32(ddlLessons.SelectedValue);

        DataSet tests = ch_gradesSvc.GetTests(les_id);
        int grd_id = Convert.ToInt32(tests.Tables[0].Rows[Convert.ToInt32(imgbtn.ToolTip)-2]["grd_id"].ToString());

        Response.Redirect("UpdateGrades.aspx?grd_id=" + grd_id);
    }
    protected bool ValidatePercents() {
        int sum = 0;
        for (int i = 0; i < gvPercents.Rows.Count; i++) {
            string txtPercents = ((TextBox)gvPercents.Rows[i].Cells[1].FindControl("txt_grd_percents")).Text.Trim();

            int numPercents;
            if (!int.TryParse(txtPercents, out numPercents)) {
                lblErr.Text = "הכנס מספרים בלבד";
                return false;
            }
            sum += numPercents;
        }

        if (sum <= 100) {
            
            return true;
        }
        else {
            lblErr.Text = "סך כל האחוזים צריך להיות קטן או שווה ל - 100";
            return false;
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

                pnlReport.Visible = false;
                pnlPercents.Visible = false;
            }
            //else if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
            //    //DDL layers
            //    string[] layers = ch_roomsSvc.GetClassesLayers(Convert.ToInt32(ddlSchools.SelectedValue));
            //    for (int i = 0; i < layers.Length; i++) {
            //        ddlLayers.Items.Add(layers[i]);
            //    }

            //    ddlLayers.Items.Add(new ListItem("-בחר שכבה-", "-1"));
            //    ddlLayers.SelectedIndex = ddlLayers.Items.Count - 1;

            //    pnlReport.Visible = false;
            //    pnlPercents.Visible = false;
            //}
            
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


        ////ddlLayers.SelectedItem.Text
        //ddlLessons.Enabled = true;

        ////DDL Lessons
        //int tch_id = int.Parse(Session["usr_id"].ToString());
        //DataSet dsTeacherLessons = ch_lessonsSvc.GetTeacherLessons(tch_id);

        //ddlLessons.DataSource = dsTeacherLessons;
        //ddlLessons.DataValueField = "מזהה השיעור";
        //ddlLessons.DataTextField = "שם השיעור";
        //ddlLessons.DataBind();

        //ddlLessons.Items.Add(new ListItem("-בחר שיעור-", "-1"));
        //ddlLessons.SelectedIndex = ddlLessons.Items.Count - 1;
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

            pnlReport.Visible = false;
            pnlPercents.Visible = false;

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
        //if (!IsPostBack) {
        //    int sc_id = Convert.ToInt32(Session["sc_id"]);
        //    DataSet dsTeachers = ch_teachers_lessonsSvc.GetTeachersLessonsGroupByUsr_id(sc_id);

        //    ddlTeachers.DataSource = dsTeachers;
        //    ddlTeachers.DataValueField = "usr_id";
        //    dsTeachers.Tables[0].Columns.Add("usr_fullname", typeof(string), "usr_first_name + ' ' + usr_last_name");
        //    ddlTeachers.DataTextField = "usr_fullname";
        //    ddlTeachers.DataBind();

        //    ddlTeachers.Items.Add(new ListItem("-כל המורים-", "-1"));
        //    ddlTeachers.SelectedIndex = ddlTeachers.Items.Count - 1;
        //}
        
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
    protected void gvGrades_PageIndexChanging(object sender, GridViewPageEventArgs e) {
        gvGrades.PageIndex = e.NewPageIndex;

        //data bind
        if (Convert.ToInt32(Session["lvl_id"]) == 0) { // if סטודנט
            int les_id = Convert.ToInt32(ddlLessons.SelectedValue);
            if (les_id != -1) { // שיעור מסויים
                pnlReport.Visible = true;

                int usr_id = Convert.ToInt32(Session["usr_id"]);
                DataSet gradesReportForStu = ch_gradesSvc.GradesReportForStu(usr_id, les_id);
                GridViewSvc.GVBind(gradesReportForStu, gvGrades);
            }
            else {
                pnlReport.Visible = true;
                pnlPercents.Visible = false;

                int usr_id = Convert.ToInt32(Session["usr_id"]);
                DataSet gradesReportForStu = ch_gradesSvc.GradesReportForStu(usr_id);
                GridViewSvc.GVBind(gradesReportForStu, gvGrades);
            }
        }
        else if (Convert.ToInt32(Session["lvl_id"]) == 1) { // if מורה
            int les_id = Convert.ToInt32(ddlLessons.SelectedValue);
            pnlReport.Visible = true;
            pnlPercents.Visible = true;

            BindGradesReport();

            DataSet tests = ch_gradesSvc.GetTests(les_id);
            GridViewSvc.GVBind(tests, gvPercents);
        }
    }
    protected void btnInsert_Click(object sender, EventArgs e) {
        Response.Redirect("AddGrades.aspx");
    }
}