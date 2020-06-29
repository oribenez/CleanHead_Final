using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class LessonsData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lvl_id"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if (Convert.ToInt32(Session["lvl_id"]) != 1 && Convert.ToInt32(Session["lvl_id"]) < 3) {
            Response.Redirect("~/Errors/403Error.aspx");
        }
        if (!IsPostBack) {
            if (Convert.ToInt32(Session["lvl_id"]) == 3) { // if עורך בית ספר
                ddlProfessions.Enabled = false;
                ddlTeachers.Enabled = false;
            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 4) { // if מנהל תוכנה ומעלה
                ddlLayers.Enabled = false;
                ddlProfessions.Enabled = false;
                ddlTeachers.Enabled = false;
            }
        }
    }
    protected void gvLessons_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            int sc_id = Convert.ToInt32(Session["sc_id"]);

            if (Convert.ToInt32(Session["lvl_id"]) == 1) {//if teacher
                //Bind data to GridView
                int usr_id = Convert.ToInt32(Session["usr_id"]);
                DataSet dsLessons = ch_lessonsSvc.GetLessonsByTeacher(usr_id, sc_id);

                for (int i = 0; i < dsLessons.Tables[0].Rows.Count; i += 2) {
                    DataRow details = dsLessons.Tables[0].NewRow();
                    dsLessons.Tables[0].Rows.InsertAt(details, i + 1);
                }
                GridViewSvc.GVBind(dsLessons, gvLessons);
            }
        }
    }
    protected void gvLessons_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex % 2 != 0)
            {
                int temp = e.Row.Cells.Count;
                for (int i = 0; i < temp - 1; i++) {
                    e.Row.Cells.RemoveAt(1);
                }
                e.Row.Cells[0].ColumnSpan = temp;
                e.Row.Cells[0].Text = "";

                string les_id = gvLessons.Rows[e.Row.RowIndex - 1].Cells[0].Text;
                string pro_name = gvLessons.Rows[e.Row.RowIndex - 1].Cells[1].Text;
                string les_name = gvLessons.Rows[e.Row.RowIndex - 1].Cells[2].Text;

                string tch_ids = gvLessons.Rows[e.Row.RowIndex - 1].Cells[3].Text;
                int[] tchArr = Array.ConvertAll(tch_ids.Split(','), Convert.ToInt32);//convert string array to int

                //convert from "3,7,6" to "אורי בן עזרא, ליאור דביר, עמית גור"
                string tch_names = "";
                if (tchArr.Length == 1)
                {
                    tch_names = ch_usersSvc.GetUserFullname(tchArr[0]);
                }
                else
	            {
                    for (int i = 0; i < tchArr.Length; i++)
                    {
                        tch_names += ch_usersSvc.GetUserFullname(tchArr[i]);
                        if (i == tchArr.Length)
                        {
                            tch_names += ", ";
                        }
                    }
	            }
                //-----------
                gvLessons.Rows[e.Row.RowIndex - 1].Cells[3].Text = tch_names;

                string stu_ids = gvLessons.Rows[e.Row.RowIndex - 1].Cells[4].Text;
                int[] stuArr = null;
                if (stu_ids != "&nbsp;") { // אם אין תלמידים
                    stuArr = Array.ConvertAll(stu_ids.Split(','), Convert.ToInt32);//convert string array to int
                }

                Panel wrap_block1 = new Panel();
                wrap_block1.CssClass = "wrap_block";

                Panel header_block1 = new Panel();
                header_block1.CssClass = "header_block";

                Panel content_block1 = new Panel();
                content_block1.CssClass = "content_block";

                e.Row.Cells[0].Controls.Add(wrap_block1);
                wrap_block1.Controls.Add(header_block1);
                wrap_block1.Controls.Add(content_block1);

                //title
                header_block1.Controls.Add(new LiteralControl("פרטי השיעור"));

                //---------------------------------

                //les_id
                Label lblsubLes_Id = new Label();
                lblsubLes_Id.Text = "מזהה שיעור: ";
                lblsubLes_Id.CssClass = "lbl_subject";

                Panel pnlcontentLes_Id = new Panel();
                pnlcontentLes_Id.CssClass = "pnl_content";

                Label lblcontentLes_Id = new Label();
                lblcontentLes_Id.Text = les_id;
                lblcontentLes_Id.ID = "lblcontentLes_Id";
                lblcontentLes_Id.CssClass = "lbl_content";

                //add
                content_block1.Controls.Add(lblsubLes_Id);
                content_block1.Controls.Add(pnlcontentLes_Id);

                Panel pnl_all_Les_Id = new Panel();
                pnl_all_Les_Id.CssClass = "pnl_all_content";

                pnl_all_Les_Id.Controls.Add(lblsubLes_Id);
                pnl_all_Les_Id.Controls.Add(pnlcontentLes_Id);

                content_block1.Controls.Add(pnl_all_Les_Id);
                pnlcontentLes_Id.Controls.Add(lblcontentLes_Id);

                //---------------------------------

                //pro_name
                Label lblsubPro_Name = new Label();
                lblsubPro_Name.Text = "מקצוע: ";
                lblsubPro_Name.CssClass = "lbl_subject";

                Panel pnlcontentPro_Name = new Panel();
                pnlcontentPro_Name.CssClass = "pnl_content";

                Label lblcontentPro_Name = new Label();
                lblcontentPro_Name.Text = pro_name;
                lblcontentPro_Name.ID = "lblcontentPro_Name";
                lblcontentPro_Name.CssClass = "lbl_content";

                //DropDownList ddlPro = new DropDownList();
                //ddlPro.ID = "ddlPro";
                //DataSet dsPro = ch_professionsSvc.GetProfessions();
                //ddlPro.DataSource = dsPro;
                //ddlPro.DataValueField = "pro_id";
                //ddlPro.DataTextField = "pro_name";
                //ddlPro.DataBind();

                //ddlPro.Items.Add("-בחר מקצוע-");
                //ddlPro.SelectedValue = ch_professionsSvc.GetIdByProName(pro_name).ToString();
                //ddlPro.Visible = false;

                //pnlcontentPro_Name.Controls.Add(ddlPro);

                //add
                Panel pnl_all_Pro_Name = new Panel();
                pnl_all_Pro_Name.CssClass = "pnl_all_content";

                pnl_all_Pro_Name.Controls.Add(lblsubPro_Name);
                pnl_all_Pro_Name.Controls.Add(pnlcontentPro_Name);

                content_block1.Controls.Add(pnl_all_Pro_Name);

                pnlcontentPro_Name.Controls.Add(lblcontentPro_Name);

                //---------------------------------

                //les_name
                Label lblsubLes_Name = new Label();
                lblsubLes_Name.Text = "שם שיעור: ";
                lblsubLes_Name.CssClass = "lbl_subject";

                Panel pnlcontentLes_Name = new Panel();
                pnlcontentLes_Name.CssClass = "pnl_content";

                Label lblcontentLes_Name = new Label();
                lblcontentLes_Name.Text = les_name;
                lblcontentLes_Name.ID = "lblcontentLes_Name";
                lblcontentLes_Name.CssClass = "lbl_content";

                //TextBox txt_les_name = new TextBox();
                //txt_les_name.Text = les_name;
                //txt_les_name.ID = "txt_les_name";
                //txt_les_name.Visible = false;
                //pnlcontentLes_Name.Controls.Add(txt_les_name);

                //add
                Panel pnl_all_Les_Name = new Panel();
                pnl_all_Les_Name.CssClass = "pnl_all_content";

                pnl_all_Les_Name.Controls.Add(lblsubLes_Name);
                pnl_all_Les_Name.Controls.Add(pnlcontentLes_Name);

                content_block1.Controls.Add(pnl_all_Les_Name);

                pnlcontentLes_Name.Controls.Add(lblcontentLes_Name);


                //---------------------------------

                //tch_name
                Label lblsubTch_Name = new Label();
                lblsubTch_Name.Text = "המורים המלמדים: ";
                lblsubTch_Name.CssClass = "lbl_subject";

                Panel pnlcontentTch_Name = new Panel();
                pnlcontentTch_Name.CssClass = "pnl_content";

                Label lblcontentTch_Name = new Label();
                lblcontentTch_Name.Text = tch_names;
                lblcontentTch_Name.ID = "lblcontentTch_Name";
                lblcontentTch_Name.CssClass = "lbl_content";

                //TextBox txt_tch_name = new TextBox();
                //txt_tch_name.Text = tch_names;
                //txt_tch_name.ID = "txt_les_name";
                //txt_tch_name.Visible = false;
                //pnlcontentTch_Name.Controls.Add(txt_tch_name);

                //add
                Panel pnl_all_Tch_Name = new Panel();
                pnl_all_Tch_Name.CssClass = "pnl_all_content";

                pnl_all_Tch_Name.Controls.Add(lblsubTch_Name);
                pnl_all_Tch_Name.Controls.Add(pnlcontentTch_Name);

                content_block1.Controls.Add(pnl_all_Tch_Name);

                pnlcontentTch_Name.Controls.Add(lblcontentTch_Name);

                //*/*/*/*/*/*/*/*/*/*/*/*/*/*/*


                Panel wrap_block2 = new Panel();
                wrap_block2.CssClass = "wrap_block2";

                Panel header_block2 = new Panel();
                header_block2.CssClass = "header_block";

                Panel content_block2 = new Panel();
                content_block2.CssClass = "content_block";

                e.Row.Cells[0].Controls.Add(wrap_block2);
                wrap_block2.Controls.Add(header_block2);
                wrap_block2.Controls.Add(content_block2);

                //title
                header_block2.Controls.Add(new LiteralControl("תלמידים"));

                //---------------------------------

                if (stuArr != null) {
                    for (int i = 0; i < stuArr.Length; i++) {
                        Panel pnl_name = new Panel();
                        pnl_name.CssClass = "pnl_name";

                        HyperLink hl_profile = new HyperLink();
                        hl_profile.NavigateUrl = "UsrProfile.aspx?usr_id=" + stuArr[i];

                        content_block2.Controls.Add(hl_profile);
                        hl_profile.Controls.Add(pnl_name);
                        pnl_name.Controls.Add(new LiteralControl(ch_usersSvc.GetUserFullname(stuArr[i])));
                    }
                    //בחירת התלמידים מתוך כיתה(checkbox)(dropdownlist)
                }

                ///*//*/*//*/*/*/**/*/*/*/*/*/*
                
                ImageButton btnEdit = new ImageButton();
                btnEdit.ID = "btnEdit";
                btnEdit.ImageUrl = "~/Images/ic_edit_24px.png";
                btnEdit.ToolTip = "ערוך פרטים";
                btnEdit.Click += btnEdit_Click;
                btnEdit.CausesValidation = false;

                ImageButton btnDelete = new ImageButton();
                btnDelete.ID = "btnDelete";
                btnDelete.ImageUrl = "~/Images/ic_delete_24px.png";
                btnDelete.ToolTip = "מחק שיעור";
                btnDelete.Click += btnDelete_Click;
                btnDelete.OnClientClick += "return ConfirmDelete()";
                btnDelete.CausesValidation = false;

                e.Row.Cells[0].Controls.Add(btnEdit);
                e.Row.Cells[0].Controls.Add(btnDelete);
            }
        } 
    }
    private void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        Label lblcontentLes_Id = (Label)gvr.FindControl("lblcontentLes_Id");
        int les_id = Convert.ToInt32(lblcontentLes_Id.Text);

        ch_lessonsSvc.DeleteLesson(les_id);

        int sc_id = Convert.ToInt32(Session["sc_id"]);
        //Bind data to GridView
        DataSet dsLessons = ch_lessonsSvc.GetLessons(sc_id);
        for (int i = 0; i < dsLessons.Tables[0].Rows.Count; i += 2)
        {
            DataRow details = dsLessons.Tables[0].NewRow();
            dsLessons.Tables[0].Rows.InsertAt(details, i + 1);
        }
        GridViewSvc.GVBind(dsLessons, gvLessons);
    }

    private void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        int les_Id = Convert.ToInt32(((Label)gvr.FindControl("lblcontentLes_Id")).Text);
        Response.Redirect("UpdateLesson.aspx?les_id=" + les_Id);
    }
    protected void gvLessons_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count > 4)
            e.Row.Cells[4].Visible = false;
    }
    protected void gvLessons_PageIndexChanging(object sender, GridViewPageEventArgs e) {
        gvLessons.PageIndex = e.NewPageIndex;
        
        int sc_id = (ddlSchools == null) ? Convert.ToInt32(Session["sc_id"]) : Convert.ToInt32(ddlSchools.SelectedValue);
        string layer = ddlLayers.SelectedItem.Text;
        int pro_id = Convert.ToInt32(ddlProfessions.SelectedValue);
        int tch_id = Convert.ToInt32(ddlTeachers.SelectedValue);

        gvLessons.Visible = true;
        if (ddlTeachers.SelectedValue == "-1") { // אם לא נבחר מורה
            // מציג את כל השיעורים לפי: בית ספר + שכבה + מקצוע
            //data bind lessons
            DataSet dsSchoolLayerLessons = ch_lessonsSvc.GetLessonsGV(sc_id, layer, pro_id);

            for (int i = 0; i < dsSchoolLayerLessons.Tables[0].Rows.Count; i += 2) {
                DataRow details = dsSchoolLayerLessons.Tables[0].NewRow();
                dsSchoolLayerLessons.Tables[0].Rows.InsertAt(details, i + 1);
            }
            GridViewSvc.GVBind(dsSchoolLayerLessons, gvLessons);
        }
        else { // אם נבחר מורה

            // מציג את כל השיעורים לפי: בית ספר + שכבה + מקצוע + מורה
            //data bind lessons
            DataSet dsSchoolLayerLessons = ch_lessonsSvc.GetLessonsGV(sc_id, layer, pro_id, tch_id);

            for (int i = 0; i < dsSchoolLayerLessons.Tables[0].Rows.Count; i += 2) {
                DataRow details = dsSchoolLayerLessons.Tables[0].NewRow();
                dsSchoolLayerLessons.Tables[0].Rows.InsertAt(details, i + 1);
            }
            GridViewSvc.GVBind(dsSchoolLayerLessons, gvLessons);
        }
    }
    protected void btnInsert_Click(object sender, EventArgs e) {
        Response.Redirect("AddLesson.aspx");
    }
    protected void ddlSchools_Load(object sender, EventArgs e) { // Only: lvl >= 4
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
    protected void ddlSchools_SelectedIndexChanged(object sender, EventArgs e) { // Only: lvl >= 4
        
        if (ddlSchools.SelectedValue != "-1") {
            int sc_id = Convert.ToInt32(ddlSchools.SelectedValue);

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
        }
        else {
            ddlLayers.Enabled = false;
            ddlProfessions.Enabled = false;
            ddlTeachers.Enabled = false;

            gvLessons.Visible = false;
        }
    }
    protected void ddlLayers_Load(object sender, EventArgs e) {
        // Only: lvl == 3
        if (!IsPostBack) {
            if (Convert.ToInt32(Session["lvl_id"]) == 3) {
                int sc_id = Convert.ToInt32(Session["sc_id"]);

                //DDL layers
                string[] layers = ch_roomsSvc.GetClassesLayers(sc_id);
                for (int i = 0; i < layers.Length; i++) {
                    ddlLayers.Items.Add(layers[i]);
                }

                ddlLayers.Items.Add(new ListItem("-בחר שכבה-", "-1"));
                ddlLayers.SelectedIndex = ddlLayers.Items.Count - 1;
            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
                int sc_id = Convert.ToInt32(ddlSchools.SelectedValue);
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

            ddlLayers.Enabled = true;
            ddlProfessions.Enabled = false;
            ddlTeachers.Enabled = false;
        }
        else { // אם נבחרה שכבה
            //data bind professions
            // בית ספר + שכבה
            int sc_id;
            if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
                sc_id = Convert.ToInt32(ddlSchools.SelectedValue);
            }
            else
	        {
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
        }
    }
    protected void ddlProfessions_Load(object sender, EventArgs e) {

    }
    protected void ddlProfessions_SelectedIndexChanged(object sender, EventArgs e) {
        if (ddlLayers.SelectedValue == "-1") { // אם לא נבחר מקצוע
            // איפוס
            ddlTeachers.Items.Clear();

            ddlLayers.Enabled = true;
            ddlProfessions.Enabled = true;
            ddlTeachers.Enabled = false;
        }
        else { // אם נבחר מקצוע
            ddlLayers.Enabled = true;
            ddlProfessions.Enabled = true;
            ddlTeachers.Enabled = true;
            //בית ספר + שכבה + מקצוע
            int sc_id;
            if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
                sc_id = Convert.ToInt32(ddlSchools.SelectedValue);
            }
            else {
                sc_id = Convert.ToInt32(Session["sc_id"]);
            } 
            string layer = ddlLayers.SelectedItem.Text;
            int pro_id = Convert.ToInt32(ddlProfessions.SelectedValue);

            //data bind teachers
            DataSet dsTeachers = ch_teachers_professionsSvc.GetTeachersByProAndSchool(sc_id, layer, pro_id);

            ddlTeachers.DataSource = dsTeachers;
            ddlTeachers.DataValueField = "usr_id";
            ddlTeachers.DataTextField = "usr_fullname";
            ddlTeachers.DataBind();

            ddlTeachers.Items.Add(new ListItem("-כל המורים-", "-1"));
            ddlTeachers.SelectedIndex = ddlTeachers.Items.Count - 1;
            ddlTeachers.Enabled = true;

            // מציג את כל השיעורים לפי: בית ספר + שכבה + מקצוע
            //data bind lessons
            gvLessons.Visible = true;
            DataSet dsSchoolLayerLessons = ch_lessonsSvc.GetLessonsGV(sc_id, layer, pro_id);

            for (int i = 0; i < dsSchoolLayerLessons.Tables[0].Rows.Count; i += 2) {
                DataRow details = dsSchoolLayerLessons.Tables[0].NewRow();
                dsSchoolLayerLessons.Tables[0].Rows.InsertAt(details, i + 1);
            }
            GridViewSvc.GVBind(dsSchoolLayerLessons, gvLessons);
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

        gvLessons.Visible = true;
        if (ddlTeachers.SelectedValue == "-1") { // אם לא נבחר מורה
            // מציג את כל השיעורים לפי: בית ספר + שכבה + מקצוע
            //data bind lessons
            DataSet dsSchoolLayerLessons = ch_lessonsSvc.GetLessonsGV(sc_id, layer, pro_id);

            for (int i = 0; i < dsSchoolLayerLessons.Tables[0].Rows.Count; i += 2) {
                DataRow details = dsSchoolLayerLessons.Tables[0].NewRow();
                dsSchoolLayerLessons.Tables[0].Rows.InsertAt(details, i + 1);
            }
            GridViewSvc.GVBind(dsSchoolLayerLessons, gvLessons);
        }
        else { // אם נבחר מורה

            // מציג את כל השיעורים לפי: בית ספר + שכבה + מקצוע + מורה
            //data bind lessons
            DataSet dsSchoolLayerLessons = ch_lessonsSvc.GetLessonsGV(sc_id, layer, pro_id, tch_id);

            for (int i = 0; i < dsSchoolLayerLessons.Tables[0].Rows.Count; i += 2) {
                DataRow details = dsSchoolLayerLessons.Tables[0].NewRow();
                dsSchoolLayerLessons.Tables[0].Rows.InsertAt(details, i + 1);
            }
            GridViewSvc.GVBind(dsSchoolLayerLessons, gvLessons);
        }
    }
}