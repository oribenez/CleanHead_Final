using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Behaviors : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["lvl_id"]) == 2) { // if crew
            Response.Redirect("~/Errors/403Error.aspx");
        }
        if (!IsPostBack) {
            if (Convert.ToInt32(Session["lvl_id"]) == 1) { // if מורה
                ddlProfessions.Enabled = false;
                ddlTeachers.Enabled = false;
                ddlLessons.Enabled = false;
                lbStudents.Enabled = false;
            }
            if (Convert.ToInt32(Session["lvl_id"]) == 3) { // if עורך בית ספר
                ddlClasses.Enabled = false;
                lbStudents.Enabled = false;
            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 4) { // if מנהל תוכנה ומעלה
                ddlLayers.Enabled = false;
                ddlClasses.Enabled = false;
                lbStudents.Enabled = false;
            }
        }
    }
    protected void gvBhv_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            if (Convert.ToInt32(Session["lvl_id"]) == 0) { // אם סטודנט
                //Bind data to GridView
                int usr_id = Convert.ToInt32(Session["usr_id"]);
                DataSet dsBhv = ch_students_behaviorsSvc.GetStuBehaviors(usr_id);
                GridViewSvc.GVBind(dsBhv, gvBhv);

                lblSumPoints.Text = "סה\"כ נקודות החודש(" + GetMonth() + "): " + ch_students_behaviorsSvc.GetStuBehaviorsPointsByMonth(usr_id);
            }    
        }
    }

    static string GetMonth() {
        switch (DateTime.Now.Month) {
            case 1:
                return "ינואר";
            case 2:
                return "פברואר";
            case 3:
                return "מרץ";
            case 4:
                return "אפריל";
            case 5:
                return "מאי";
            case 6:
                return "יוני";
            case 7:
                return "יולי";
            case 8:
                return "אוגוסט";
            case 9:
                return "ספטמבר";
            case 10:
                return "אוקטובר";
            case 11:
                return "נובמבר";
            case 12:
                return "דצמבר";
            default:
                return "";
        }
    }

    protected void gvBhv_PageIndexChanging(object sender, GridViewPageEventArgs e) {
        gvBhv.PageIndex = e.NewPageIndex;

        if (Convert.ToInt32(Session["lvl_id"]) == 0) { // אם סטודנט
            //Bind data to GridView
            int usr_id = Convert.ToInt32(Session["usr_id"]);
            DataSet dsBhv = ch_students_behaviorsSvc.GetStuBehaviors(usr_id);
            GridViewSvc.GVBind(dsBhv, gvBhv);

            lblSumPoints.Text = "סה\"כ נקודות החודש(" + GetMonth() + "): " + ch_students_behaviorsSvc.GetStuBehaviorsPointsByMonth(usr_id);
        }
        if (lbStudents.SelectedIndex == -1) {
            pnlBhv.Visible = false;
        }
        else {
            if (Convert.ToInt32(Session["lvl_id"]) == 1) { // אם מורה
                //Bind data to GridView
                pnlBhv.Visible = true;

                int stu_id = Convert.ToInt32(lbStudents.SelectedValue);
                int tch_id = Convert.ToInt32(Session["usr_id"]);

                DataSet dsBhv = ch_students_behaviorsSvc.GetStuBehaviors(stu_id);

                lblSumPoints.Text = "סה\"כ נקודות החודש(" + GetMonth() + "): " + ch_students_behaviorsSvc.GetStuBehaviorsPointsByMonth(stu_id);

                //bind
                GridViewSvc.GVBind(dsBhv, gvBhv);
            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 3) { //אם עורך בית ספר ומעלה
                //Bind data to GridView
                pnlBhv.Visible = true;

                int stu_id = Convert.ToInt32(lbStudents.SelectedValue);
                DataSet dsBhv = ch_students_behaviorsSvc.GetStuBehaviors(stu_id);

                lblSumPoints.Text = "סה\"כ נקודות החודש(" + GetMonth() + "): " + ch_students_behaviorsSvc.GetStuBehaviorsPointsByMonth(stu_id);

                //bind
                GridViewSvc.GVBind(dsBhv, gvBhv);
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

            pnlBhv.Visible = false;

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
            if (Convert.ToInt32(Session["lvl_id"]) == 1 || Convert.ToInt32(Session["lvl_id"]) == 3) {
                //DDL layers
                string[] layers = ch_roomsSvc.GetClassesLayers(Convert.ToInt32(Session["sc_id"]));
                for (int i = 0; i < layers.Length; i++) {
                    ddlLayers.Items.Add(layers[i]);
                }

                ddlLayers.Items.Add(new ListItem("-בחר שכבה-", "-1"));
                ddlLayers.SelectedIndex = ddlLayers.Items.Count - 1;

                pnlBhv.Visible = false;
            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
                //DDL layers
                string[] layers = ch_roomsSvc.GetClassesLayers(Convert.ToInt32(ddlSchools.SelectedValue));
                for (int i = 0; i < layers.Length; i++) {
                    ddlLayers.Items.Add(layers[i]);
                }

                ddlLayers.Items.Add(new ListItem("-בחר שכבה-", "-1"));
                ddlLayers.SelectedIndex = ddlLayers.Items.Count - 1;

                pnlBhv.Visible = false;
            }

        }
    }
    protected void ddlLayers_SelectedIndexChanged(object sender, EventArgs e) {
        if (ddlLayers.SelectedValue == "-1") { // אם לא נבחרה שכבה
            // איפוס
            ddlProfessions.Items.Clear();
            ddlTeachers.Items.Clear();
            ddlLessons.Items.Clear();
            ddlClasses.Items.Clear();
            lbStudents.Items.Clear();

            ddlProfessions.Enabled = false;
            ddlTeachers.Enabled = false;
            ddlLessons.Enabled = false;
            ddlClasses.Enabled = false;
            lbStudents.Enabled = false;
        }
        else { // אם נבחרה שכבה
            //data bind professions
            if (Convert.ToInt32(Session["lvl_id"]) == 1) {
                //bind pro
                DataSet dsProfessions = ch_professionsSvc.GetLayerProfessions(ddlLayers.SelectedItem.Text);

                ddlProfessions.DataSource = dsProfessions;
                ddlProfessions.DataValueField = "pro_id";
                ddlProfessions.DataTextField = "pro_name";
                ddlProfessions.DataBind();

                ddlProfessions.Items.Add(new ListItem("-בחר מקצוע-", "-1"));
                ddlProfessions.SelectedIndex = ddlProfessions.Items.Count - 1;

                ddlProfessions.Enabled = true;
            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 3) {
                //bind class
                DataSet dsClasses = ch_roomsSvc.GetStuPrimaryClasses(Convert.ToInt32(Session["sc_id"]), ddlLayers.SelectedItem.Text);

                ddlClasses.DataSource = dsClasses;
                ddlClasses.DataValueField = "rm_id";
                ddlClasses.DataTextField = "rm_name";
                ddlClasses.DataBind();

                ddlClasses.Items.Add(new ListItem("-בחר כיתה-", "-1"));
                ddlClasses.SelectedIndex = ddlClasses.Items.Count - 1;

                ddlClasses.Enabled = true;
            }
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
    }
    protected void ddlLessons_SelectedIndexChanged(object sender, EventArgs e) {
        if (ddlLessons.SelectedValue == "-1") { // אם לא נבחר שיעור
            // איפוס
            lbStudents.Items.Clear();

            lbStudents.Enabled = false;
        }
        else { // אם נבחר מורה
            // מציג את כל התלמידים לפי: בית ספר + מקצוע + שכבה + מורה + שיעור
            //data bind lessons
            lbStudents.Enabled = true;

            int sc_id = int.Parse(Session["sc_id"].ToString());
            string layer = ddlLayers.SelectedItem.Text;
            int pro_id = Convert.ToInt32(ddlProfessions.SelectedValue);
            int tch_id = Convert.ToInt32(ddlTeachers.SelectedValue);
            int les_id = Convert.ToInt32(ddlLessons.SelectedValue);
            DataSet dsStudents = ch_students_lessonsSvc.GetStudentsLesson(les_id);

            lbStudents.DataSource = dsStudents;
            lbStudents.DataValueField = "usr_id";
            lbStudents.DataTextField = "usr_fullname";
            lbStudents.DataBind();
        }
    }
    protected void ddlClasses_Load(object sender, EventArgs e) {
    }
    protected void ddlClasses_SelectedIndexChanged(object sender, EventArgs e) {
        if (ddlClasses.SelectedValue == "-1") {
            lbStudents.Items.Clear();

            lbStudents.Enabled = false;
        }
        else {
            if (Convert.ToInt32(Session["lvl_id"]) == 3) {
                lbStudents.Enabled = true;

                int sc_id = int.Parse(Session["sc_id"].ToString());
                int rm_id = Convert.ToInt32(ddlClasses.SelectedValue);
                DataSet dsStudents = ch_studentsSvc.GetStudents(sc_id, rm_id);
                dsStudents.Tables[0].Columns.Add("usr_fullname", typeof(string), "usr_first_name + ' ' + usr_last_name");

                lbStudents.DataSource = dsStudents;
                lbStudents.DataValueField = "usr.usr_id";
                lbStudents.DataTextField = "usr_fullname";
                lbStudents.DataBind();
            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
                lbStudents.Enabled = true;

                int sc_id = int.Parse(ddlSchools.SelectedValue);
                int rm_id = Convert.ToInt32(ddlClasses.SelectedValue);
                DataSet dsStudents = ch_studentsSvc.GetStudents(sc_id, rm_id);
                dsStudents.Tables[0].Columns.Add("usr_fullname", typeof(string), "usr_first_name + ' ' + usr_last_name");

                lbStudents.DataSource = dsStudents;
                lbStudents.DataValueField = "usr.usr_id";
                lbStudents.DataTextField = "usr_fullname";
                lbStudents.DataBind();
            }
        }
    }
    protected void lbStudents_SelectedIndexChanged(object sender, EventArgs e) {
        if (lbStudents.SelectedIndex == -1) {
            pnlBhv.Visible = false;
        }
        else {
            if (Convert.ToInt32(Session["lvl_id"]) == 1) { // אם מורה
                //Bind data to GridView
                pnlBhv.Visible = true;

                int stu_id = Convert.ToInt32(lbStudents.SelectedValue);
                int tch_id = Convert.ToInt32(Session["usr_id"]);

                DataSet dsBhv = ch_students_behaviorsSvc.GetStuBehaviors(stu_id);

                lblSumPoints.Text = "סה\"כ נקודות החודש(" + GetMonth() + "): " + ch_students_behaviorsSvc.GetStuBehaviorsPointsByMonth(stu_id);

                //bind
                GridViewSvc.GVBind(dsBhv, gvBhv);
            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 3) { //אם עורך בית ספר ומעלה
                //Bind data to GridView
                pnlBhv.Visible = true;

                int stu_id = Convert.ToInt32(lbStudents.SelectedValue);
                DataSet dsBhv = ch_students_behaviorsSvc.GetStuBehaviors(stu_id);

                lblSumPoints.Text = "סה\"כ נקודות החודש(" + GetMonth() + "): " + ch_students_behaviorsSvc.GetStuBehaviorsPointsByMonth(stu_id);

                //bind
                GridViewSvc.GVBind(dsBhv, gvBhv);
            }
        }
    }
    protected void btnInsert_Click(object sender, EventArgs e) {
        Response.Redirect("AddBehaviors.aspx");
    }
    protected void btn_delete_bhv_Click(object sender, ImageClickEventArgs e) {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        //Delete selected row
        Label lblBhvDate = (Label)gvr.FindControl("lblBhvDate");
        Label lblBhvName = (Label)gvr.FindControl("lblBhvName");
        Label lblLesName = (Label)gvr.FindControl("lblLesName");
        Label lblHrName = (Label)gvr.FindControl("lblHrName");

        ch_students_behaviors stu_bhv = new ch_students_behaviors();
        stu_bhv.stu_bhv_date = Convert.ToDateTime(lblBhvDate.Text);
        stu_bhv.bhv_id = ch_behaviorsSvc.GetIdByBhvName(lblBhvName.Text);
        stu_bhv.les_id = ch_lessonsSvc.GetIdByLesName(lblLesName.Text);
        stu_bhv.stu_id = Convert.ToInt32(lbStudents.SelectedValue);
        int sc_id = Convert.ToInt32(Session["lvl_id"]) < 4 ? Convert.ToInt32(Session["sc_id"]) : Convert.ToInt32(ddlSchools.SelectedValue);
        stu_bhv.hr_id = ch_hoursSvc.GetId(sc_id, lblHrName.Text);
        //Response.Write("stu_bhv_date=" + stu_bhv.stu_bhv_date + "&bhv_id=" + stu_bhv.bhv_id + "&les_id=" + stu_bhv.les_id + "&stu_id=" + stu_bhv.stu_id + "&hr_id=" + stu_bhv.hr_id);
        ch_students_behaviorsSvc.DeleteStuBehavior(stu_bhv);

        if (Convert.ToInt32(Session["lvl_id"]) == 1) { // אם מורה
            //Bind data to GridView
            pnlBhv.Visible = true;

            int stu_id = Convert.ToInt32(lbStudents.SelectedValue);
            int tch_id = Convert.ToInt32(Session["usr_id"]);

            DataSet dsBhv = ch_students_behaviorsSvc.GetStuBehaviors(stu_id);

            lblSumPoints.Text = "סה\"כ נקודות החודש(" + GetMonth() + "): " + ch_students_behaviorsSvc.GetStuBehaviorsPointsByMonth(stu_id);

            //bind
            GridViewSvc.GVBind(dsBhv, gvBhv);
        }
        else if (Convert.ToInt32(Session["lvl_id"]) >= 3) { //אם עורך בית ספר ומעלה
            //Bind data to GridView
            pnlBhv.Visible = true;

            int stu_id = Convert.ToInt32(lbStudents.SelectedValue);
            DataSet dsBhv = ch_students_behaviorsSvc.GetStuBehaviors(stu_id);

            lblSumPoints.Text = "סה\"כ נקודות החודש(" + GetMonth() + "): " + ch_students_behaviorsSvc.GetStuBehaviorsPointsByMonth(stu_id);

            //bind
            GridViewSvc.GVBind(dsBhv, gvBhv);
        }
    }
    protected void gvBhv_RowCreated(object sender, GridViewRowEventArgs e) {
        if (Convert.ToInt32(Session["lvl_id"]) == 0) {
            if (e.Row.Cells.Count >= 7) {
                e.Row.Cells[7].Visible = false;
            }
        }
    }
}