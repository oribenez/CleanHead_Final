using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Homework : System.Web.UI.Page {
    PagedDataSource pager = new PagedDataSource();
    public int currentPage;

    public int CurrentPage {

        get {
            if (this.ViewState["CurrentPage"] == null)
                return 0;
            else
                return Convert.ToInt32(this.ViewState["CurrentPage"].ToString());
        }

        set {
            this.ViewState["CurrentPage"] = value;
        }

    }
    protected void Page_Load(object sender, EventArgs e) {
        if (Convert.ToInt32(Session["lvl_id"]) == 2) { // אם איש צוות
            Response.Redirect("~/Errors/403Error.aspx");
        }
        if (!IsPostBack) {
            popup.Visible = false;

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
    protected void btnClosePopup_Click(object sender, EventArgs e) {
        popup.Visible = false;
    }
    protected void DataListHwBind() {
        DataSet dsHw = (DataSet)ViewState["dsHw"];
        

        if (ViewState["dsHw"] != null) {
            if (dsHw.Tables[0].Columns["hw_deadlinedate-typeof(dt)"] == null)
                dsHw.Tables[0].Columns.Add("hw_deadlinedate-typeof(dt)", typeof(DateTime));

            foreach (DataRow dr in dsHw.Tables[0].Rows) {
                dr["hw_deadlinedate-typeof(dt)"] = Convert.ToDateTime(dr["hw_deadlinedate"]);
            }

            DataView dv = new DataView(dsHw.Tables[0]);
            dv.Sort = "hw_deadlinedate-typeof(dt) DESC";

            pager.DataSource = dv;
            pager.AllowPaging = true;
            pager.PageSize = 8;
            pager.CurrentPageIndex = CurrentPage;
            this.lnkbtnNextPage.Visible = !pager.IsLastPage;
            this.lnkbtnPrevPage.Visible = !pager.IsFirstPage;

            this.dlHw.DataSource = pager;
            this.dlHw.DataBind();

            DoPaging();
        }
    }
    private void DoPaging() {
        DataTable dt = new DataTable();
        dt.Columns.Add("PageIndex");
        dt.Columns.Add("PageText");
        for (int i = 0; i < pager.PageCount; i++) {
            DataRow dr = dt.NewRow();
            dr[0] = i;
            dr[1] = i + 1;
            dt.Rows.Add(dr);
        }

        dlPaging.DataSource = dt;
        dlPaging.DataBind();
    }
    protected void dlPaging_ItemCommand(object source, DataListCommandEventArgs e) {
        if (e.CommandName.Equals("lnkbtnPaging")) {
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            DataListHwBind();
        }
    }
    protected void lnkbtnNextPage_Click(object sender, EventArgs e) {
        CurrentPage += 1;
        DataListHwBind();
    }
    protected void lnkbtnPrevPage_Click(object sender, EventArgs e) {
        CurrentPage -= 1;
        DataListHwBind();
    }
    protected void dlHw_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            //this.currentPage = 0;
            //Session["currentPage"] = 0;

            DataSet dsHw = null;

            //show all homeworks
            if (Convert.ToInt32(Session["lvl_id"]) == 0) { // אם סטודנט
                int les_id = Convert.ToInt32(ddlLessons.SelectedValue);
                if (les_id != -1) { // שיעור מסויים
                    pnlReport.Visible = true;

                    dsHw = ch_homeworkSvc.GetLessonHomeworks(les_id);
                }
                else { //כל השיעורים
                    pnlReport.Visible = true;

                    int usr_id = Convert.ToInt32(Session["usr_id"]);
                    dsHw = ch_homeworkSvc.GetStuHomeworks(usr_id);
                }
            }
            else if (Convert.ToInt32(Session["lvl_id"]) == 1) { // אם מורה
                int les_id = Convert.ToInt32(ddlLessons.SelectedValue);
                pnlReport.Visible = true;

                int usr_id = Convert.ToInt32(Session["usr_id"]);
                dsHw = ch_homeworkSvc.GetLessonHomeworks(les_id);
            }
            ViewState["dsHw"] = dsHw;
            DataListHwBind();
        }
        else {
            //this.currentPage = Convert.ToInt32(Session["currentPage"]);
        }
    }
    protected void dlHw_ItemDataBound(object sender, DataListItemEventArgs e) {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {

            //int hw_id = Convert.ToInt32(((Panel)e.Item.FindControl("hwWrap")).ToolTip.ToString());
            //e.Item.Attributes.Add("onclick", "location.href='Homework.aspx?hw_id=" + hw_id + "'");
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

            pnlReport.Visible = false;

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

                pnlReport.Visible = false;
            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
                //DDL layers
                string[] layers = ch_roomsSvc.GetClassesLayers(Convert.ToInt32(ddlSchools.SelectedValue));
                for (int i = 0; i < layers.Length; i++) {
                    ddlLayers.Items.Add(layers[i]);
                }

                ddlLayers.Items.Add(new ListItem("-בחר שכבה-", "-1"));
                ddlLayers.SelectedIndex = ddlLayers.Items.Count - 1;

                pnlReport.Visible = false;
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
                ddlLessons.SelectedIndex = 0;
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

        DataSet dsHw = null;

        //data bind
        if (Convert.ToInt32(Session["lvl_id"]) == 0) { // if סטודנט
            if (les_id != -1) { // שיעור מסויים
                pnlReport.Visible = true;

                dsHw = ch_homeworkSvc.GetLessonHomeworks(les_id);
            }
            else { //כל השיעורים
                pnlReport.Visible = true;

                int usr_id = Convert.ToInt32(Session["usr_id"]);
                dsHw = ch_homeworkSvc.GetStuHomeworks(usr_id);
            }
        }
        else if (Convert.ToInt32(Session["lvl_id"]) == 1) { // if מורה
            pnlReport.Visible = true;

            dsHw = ch_homeworkSvc.GetLessonHomeworks(les_id);
        }
        else if (Convert.ToInt32(Session["lvl_id"]) >= 3) { // if עורך בית ספר ומעלה
            if (les_id == -1) { // לא נבחר שיעור
                pnlReport.Visible = false;
            }
            else { // נבחר שיעור מסויים
                pnlReport.Visible = true;

                dsHw = ch_homeworkSvc.GetLessonHomeworks(les_id);
            }
        }
        ViewState["dsHw"] = dsHw;
        DataListHwBind();
    }
    protected void btnBiggerHw_Click(object sender, EventArgs e) {
        Button btn = (Button)sender;
        DataListItem dli = (DataListItem)btn.NamingContainer;
        Panel pnl = (Panel)dli.FindControl("hwWrap");

        popup.Visible = true;
        DataRow dr_hw = ch_homeworkSvc.GetHomework(Convert.ToInt32(pnl.ToolTip));
        popupContainer.ToolTip = pnl.ToolTip;
        lbl_pro_name.Text = dr_hw["pro_name"].ToString();
        lbl_les_name.Text = dr_hw["les_name"].ToString();
        lbl_hr_name.Text = dr_hw["hr_name"].ToString();
        lbl_editdate.Text = dr_hw["hw_editdate"].ToString();
        lbl_deadlinedate.Text = dr_hw["hw_deadlinedate"].ToString();
        lbl_txt.Text = dr_hw["hw_txt"].ToString();
        lbl_timeleft.Text = dr_hw["hw_timeleft"].ToString();
    }
    protected void btn_edit_hw_Click(object sender, ImageClickEventArgs e) {
        Response.Redirect("UpdateHomework.aspx?hw_id=" + popupContainer.ToolTip);
    }

    protected void dlPaging_ItemDataBound(object sender, DataListItemEventArgs e) {
        LinkButton lnkbtnPage = (LinkButton)e.Item.FindControl("lnkbtnPaging");
        if (lnkbtnPage.CommandArgument.ToString() == CurrentPage.ToString()) {
            lnkbtnPage.Enabled = false;
            lnkbtnPage.Font.Bold = true;
        }
    }
    protected void btnInsert_Click(object sender, EventArgs e) {
        Response.Redirect("AddHomework.aspx");
    }
}