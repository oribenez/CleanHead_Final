using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;

public partial class TeachersData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lvl_id"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else if(Convert.ToInt32(Session["lvl_id"]) < 3)
        {
            Response.Redirect("Default.aspx");
        }
    }
    protected void gvBind() {
        if (Request.QueryString["search"] == null) {
            //Bind data to GridView
            DataSet dsTeachers = ch_teachersSvc.GetTeachersGV();
            for (int i = 0; i < dsTeachers.Tables[0].Rows.Count; i += 2) {
                DataRow details = dsTeachers.Tables[0].NewRow();
                dsTeachers.Tables[0].Rows.InsertAt(details, i + 1);
            }

            GridViewSvc.GVBind(dsTeachers, GVTeachers);
        }
        else {
            if (Request.QueryString["search"] == "") {
                //Bind data to GridView
                DataSet dsTeachers = ch_teachersSvc.GetTeachersGV();
                for (int i = 0; i < dsTeachers.Tables[0].Rows.Count; i += 2) {
                    DataRow details = dsTeachers.Tables[0].NewRow();
                    dsTeachers.Tables[0].Rows.InsertAt(details, i + 1);
                }

                GridViewSvc.GVBind(dsTeachers, GVTeachers);
            }
            else{
                txtSearch.Text = Request.QueryString["search"].ToString();

                //Bind data to GridView
                DataSet dsTeachers = ch_teachersSvc.GetTeachersGV(Request.QueryString["search"].ToString());
                for (int i = 0; i < dsTeachers.Tables[0].Rows.Count; i += 2) {
                    DataRow details = dsTeachers.Tables[0].NewRow();
                    dsTeachers.Tables[0].Rows.InsertAt(details, i + 1);
                }

                GridViewSvc.GVBind(dsTeachers, GVTeachers);
            }
        }
    }
    protected void GVTeachers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //Delete selected row
        int usr_id = Convert.ToInt32(GVTeachers.DataKeys[e.RowIndex].Value);
        ch_teachersSvc.DeleteTeacherById(usr_id);
        ch_usersSvc.DeleteUserById(usr_id);

        if (ddlSchools.SelectedValue == "-1") {
            gvBind();
        }
        else {
            if (Convert.ToInt32(Session["lvl_id"]) == 3) { // if editor
                //Bind data to GridView
                int sc_id = Convert.ToInt32(Session["sc_id"]);
                DataSet dsTeachers = ch_teachersSvc.GetTeachersGV(sc_id);
                for (int i = 0; i < dsTeachers.Tables[0].Rows.Count; i += 2) {
                    DataRow details = dsTeachers.Tables[0].NewRow();
                    dsTeachers.Tables[0].Rows.InsertAt(details, i + 1);
                }

                GridViewSvc.GVBind(dsTeachers, GVTeachers);
            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 4) { // if vice_manager
                //Bind data to GridView
                int sc_id = Convert.ToInt32(ddlSchools.SelectedValue);
                DataSet dsTeachers = ch_teachersSvc.GetTeachersGV(sc_id);
                for (int i = 0; i < dsTeachers.Tables[0].Rows.Count; i += 2) {
                    DataRow details = dsTeachers.Tables[0].NewRow();
                    dsTeachers.Tables[0].Rows.InsertAt(details, i + 1);
                }

                GridViewSvc.GVBind(dsTeachers, GVTeachers);
            }
        }
    }
    protected void GVTeachers_Load(object sender, EventArgs e)
    {
        if (ddlSchools.SelectedValue == "-1") {
            gvBind();
        }
        else {
            if (Convert.ToInt32(Session["lvl_id"]) == 3) { // if editor
                //Bind data to GridView
                int sc_id = Convert.ToInt32(Session["sc_id"]);
                DataSet dsTeachers = ch_teachersSvc.GetTeachersGV(sc_id);
                for (int i = 0; i < dsTeachers.Tables[0].Rows.Count; i += 2) {
                    DataRow details = dsTeachers.Tables[0].NewRow();
                    dsTeachers.Tables[0].Rows.InsertAt(details, i + 1);
                }

                GridViewSvc.GVBind(dsTeachers, GVTeachers);
            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 4) { // if vice_manager
                //Bind data to GridView
                int sc_id = Convert.ToInt32(ddlSchools.SelectedValue);
                DataSet dsTeachers = ch_teachersSvc.GetTeachersGV(sc_id);
                for (int i = 0; i < dsTeachers.Tables[0].Rows.Count; i += 2) {
                    DataRow details = dsTeachers.Tables[0].NewRow();
                    dsTeachers.Tables[0].Rows.InsertAt(details, i + 1);
                }

                GridViewSvc.GVBind(dsTeachers, GVTeachers);
            }
        }
    }
    protected void GVTeachers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Change page index
        GVTeachers.PageIndex = e.NewPageIndex;

        if (ddlSchools.SelectedValue == "-1") {
            gvBind();
        }
        else {
            if (Convert.ToInt32(Session["lvl_id"]) == 3) { // if editor
                //Bind data to GridView
                int sc_id = Convert.ToInt32(Session["sc_id"]);
                DataSet dsTeachers = ch_teachersSvc.GetTeachersGV(sc_id);
                for (int i = 0; i < dsTeachers.Tables[0].Rows.Count; i += 2) {
                    DataRow details = dsTeachers.Tables[0].NewRow();
                    dsTeachers.Tables[0].Rows.InsertAt(details, i + 1);
                }

                GridViewSvc.GVBind(dsTeachers, GVTeachers);
            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 4) { // if vice_manager
                //Bind data to GridView
                int sc_id = Convert.ToInt32(ddlSchools.SelectedValue);
                DataSet dsTeachers = ch_teachersSvc.GetTeachersGV(sc_id);
                for (int i = 0; i < dsTeachers.Tables[0].Rows.Count; i += 2) {
                    DataRow details = dsTeachers.Tables[0].NewRow();
                    dsTeachers.Tables[0].Rows.InsertAt(details, i + 1);
                }

                GridViewSvc.GVBind(dsTeachers, GVTeachers);
            }
        }
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        Label lblcontentUsr_Id = (Label)gvr.FindControl("lblcontentUsr_Id");
        int usr_id = Convert.ToInt32(lblcontentUsr_Id.Text);

        ch_usersSvc.DeleteUserById(usr_id);
        ch_teachersSvc.DeleteTeacherById(usr_id);
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        selectedRowIndex.Value = (gvr.RowIndex/2).ToString();

        lblErrGV.Text = "";

        TextBox txtUsr_identity = (TextBox)gvr.FindControl("txt_identity");
        txtUsr_identity.Visible = true;
        Label lblUsr_identity = (Label)gvr.FindControl("lblcontentUsr_Identity");
        lblUsr_identity.Visible = false;

        TextBox txtUsr_first_name = (TextBox)gvr.FindControl("txt_first_name");
        txtUsr_first_name.Visible = true;
        Label lblUsr_first_name = (Label)gvr.FindControl("lblcontentFirst_Name");
        lblUsr_first_name.Visible = false;

        TextBox txtUsr_last_name = (TextBox)gvr.FindControl("txt_last_name");
        txtUsr_last_name.Visible = true;
        Label lblUsr_last_name = (Label)gvr.FindControl("lblcontentLast_Name");
        lblUsr_last_name.Visible = false;

        RadioButtonList rblGender = (RadioButtonList)gvr.FindControl("rblGender");
        rblGender.Visible = true;
        Label lblcontentGender = (Label)gvr.FindControl("lblcontentGender");
        lblcontentGender.Visible = false;

        TextBox editableBirthdate = (TextBox)gvr.FindControl("editableTextBoxBirthday");
        editableBirthdate.Visible = true;
        Label lblcontentBirth_Date = (Label)gvr.FindControl("lblcontentBirth_Date");
        lblcontentBirth_Date.Visible = false;

        DropDownList ddlSchool = (DropDownList)gvr.FindControl("ddlSchool");
        ddlSchool.Visible = true;
        Label lblcontentSc_Name = (Label)gvr.FindControl("lblcontentSc_Name");
        lblcontentSc_Name.Visible = false;


        DropDownList ddlCities = (DropDownList)gvr.FindControl("ddlCities");
        ddlCities.Visible = true;
        Label lblcontentCty_Name = (Label)gvr.FindControl("lblcontentCty_Name");
        lblcontentCty_Name.Visible = false;

        TextBox txt_Usr_Address = (TextBox)gvr.FindControl("txt_Usr_Address");
        txt_Usr_Address.Visible = true;
        Label lblcontentUsr_Address = (Label)gvr.FindControl("lblcontentUsr_Address");
        lblcontentUsr_Address.Visible = false;

        TextBox txt_Home_Phone = (TextBox)gvr.FindControl("txt_Home_Phone");
        txt_Home_Phone.Visible = true;
        Label lblcontentHome_Phone = (Label)gvr.FindControl("lblcontentHome_Phone");
        lblcontentHome_Phone.Visible = false;

        TextBox txt_Cellphone = (TextBox)gvr.FindControl("txt_Cellphone");
        txt_Cellphone.Visible = true;
        Label lblcontentCellphone = (Label)gvr.FindControl("lblcontentCellphone");
        lblcontentCellphone.Visible = false;

        TextBox txt_Email = (TextBox)gvr.FindControl("txt_Email");
        txt_Email.Visible = true;
        Label lblcontentEmail = (Label)gvr.FindControl("lblcontentEmail");
        lblcontentEmail.Visible = false;

        DropDownList ddlLevels = (DropDownList)gvr.FindControl("ddlLevels");
        if (ddlLevels != null) {
            ddlLevels.Visible = true;
            Label lblcontentLvl_Name = (Label)gvr.FindControl("lblcontentLvl_Name");
            lblcontentLvl_Name.Visible = false;
        }

        ListBox lbProfessions = (ListBox)gvr.FindControl("lbProfessions");
        lbProfessions.Visible = true;
        Label lblcontentUsr_Professions = (Label)gvr.FindControl("lblcontentUsr_Professions");
        lblcontentUsr_Professions.Visible = false;


        ImageButton btnEdit = (ImageButton)gvr.FindControl("btnEdit");
        btnEdit.Visible = false;

        ImageButton btnDelete = (ImageButton)gvr.FindControl("btnDelete");
        btnDelete.Visible = false;

        ImageButton btnCancel = (ImageButton)gvr.FindControl("btnCancel");
        btnCancel.Visible = true;

        ImageButton btnUpdate = (ImageButton)gvr.FindControl("btnUpdate");
        btnUpdate.Visible = true;

    }
    protected bool ValidatePage(GridViewRow gvr) {
        TextBox txtUsr_identity = (TextBox)gvr.FindControl("txt_identity");
        TextBox txtUsr_first_name = (TextBox)gvr.FindControl("txt_first_name");
        TextBox txtUsr_last_name = (TextBox)gvr.FindControl("txt_last_name");
        RadioButtonList rblGender = (RadioButtonList)gvr.FindControl("rblGender");
        TextBox editableBirthdate = (TextBox)gvr.FindControl("editableTextBoxBirthday");
        DropDownList ddlSchool = (DropDownList)gvr.FindControl("ddlSchool");
        DropDownList ddlCities = (DropDownList)gvr.FindControl("ddlCities");
        TextBox txt_Usr_Address = (TextBox)gvr.FindControl("txt_Usr_Address");
        TextBox txt_Home_Phone = (TextBox)gvr.FindControl("txt_Home_Phone");
        TextBox txt_Cellphone = (TextBox)gvr.FindControl("txt_Cellphone");
        TextBox txt_Email = (TextBox)gvr.FindControl("txt_Email");
        DropDownList ddlLevels = (DropDownList)gvr.FindControl("ddlLevels");
        ListBox lbProfessions = (ListBox)gvr.FindControl("lbProfessions");

        //identity
        if (txtUsr_identity.Text == "") {
            lblErrGV.Text = "הכנס ת.ז.";
            return false;
        }
        if (txtUsr_identity.Text.Length != 9) {
            lblErrGV.Text = "ת.ז. לא תקינה";
            return false;
        }

        //first name
        if (txtUsr_first_name.Text == "") {
            lblErrGV.Text = "הכנס שם פרטי";
            return false;
        }
        if (!Regex.IsMatch(txtUsr_first_name.Text, "^[a-zA-Zא-ת ]{2,20}$")) {
            lblErrGV.Text = "שם פרטי לא תקין";
            return false;
        }

        //last name
        if (txtUsr_last_name.Text == "") {
            lblErrGV.Text = "הכנס שם משפחה";
            return false;
        }
        if (!Regex.IsMatch(txtUsr_last_name.Text, "^[a-zA-Zא-ת ]{2,20}$")) {
            lblErrGV.Text = "שם משפחה לא תקין";
            return false;
        }

        //rblGender
        if (rblGender.SelectedValue == "-1") {
            lblErrGV.Text = "בחר מין";
            return false;
        }

        //editableBirthdate
        if (editableBirthdate.Text == "") {
            lblErrGV.Text = "הכנס תאריך";
            return false;
        }
        DateTime dt;
        if (!DateTime.TryParse(editableBirthdate.Text,out dt)) {
            lblErrGV.Text = "תאריך לא תקין";
            return false;
        }
        double minAgeYears = 18;
        if ((DateTime.Now - dt).TotalDays <  minAgeYears*365) { // גיל מינימלי למורה 18 שנים
            lblErrGV.Text = "גיל מינימלי להוראה - 18 שנים";
            return false;
        }
        double maxAgeYears = 120;
        if (DateTime.Now.Year - dt.Year > maxAgeYears) { // גיל מקסימלי לאדם הוא 120
            lblErrGV.Text = "גיל מקסימלי להרשמה - 120 שנים";
            return false;
        }

        //ddlSchool
        if (ddlSchool.SelectedValue == "-1") {
            lblErrGV.Text = "בחר בית ספר";
            return false;
        }

        //ddlCities
        if (ddlCities.SelectedValue == "-1") {
            lblErrGV.Text = "בחר עיר";
            return false;
        }

        //txt_Usr_Address
        if (txt_Usr_Address.Text == "") {
            lblErrGV.Text = "הכנס כתובת";
            return false;
        }
        if (txt_Usr_Address.Text.Length > 25) {
            lblErrGV.Text = "הכנס כתובת תקנית מעל 25 תווים";
            return false;
        }

        //txt_Home_Phone
        if (txt_Home_Phone.Text == "") {
            lblErrGV.Text = "הכנס מספר טלפון";
            return false;
        }
        if (!Regex.IsMatch(txt_Home_Phone.Text, @"^0[23489]{1}(\-)?[^0\D]{1}\d{6}$")) {
            lblErrGV.Text = "הכנס מספר טלפון בית תקני בן 7 ספרות";
            return false;
        }

        //txt_Cellphone 
        if (txt_Cellphone.Text == "") {
            lblErrGV.Text = "הכנס מספר טלפון נייד";
            return false;
        }
        if (!Regex.IsMatch(txt_Cellphone.Text, @"^05\d([-]{0,1})\d{7}$")) {
            lblErrGV.Text = "הכנס מספר טלפון נייד תקני בן 10 ספרות";
            return false;
        }

        //txt_Email
        if (txt_Email.Text == "") {
		    lblErrGV.Text = "הכנס אימייל";
            return false;
	    }
        if (!Regex.IsMatch(txt_Email.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase)) {
		    lblErrGV.Text = "הכנס אימייל תקני";
            return false;
	    }

        //Levels
        if (ddlLevels != null) {
            if (ddlLevels.SelectedValue == "-1") {
                lblErrGV.Text = "בחר רמה";
                return false;
            }
        }

        //tch_professions
        bool flag = false;
        foreach (ListItem li in lbProfessions.Items) {
            if (li.Selected) {
                flag = true;
                break;
            }
        }

        if (!flag) {
            lblErrGV.Text = "בחר לפחות הכשרה אחת";
            return false;
        }

        lblErrGV.Text = "";
        return true;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        selectedRowIndex.Value = (gvr.RowIndex/2).ToString();

        lblErrGV.Text = "";

        TextBox txtUsr_identity = (TextBox)gvr.FindControl("txt_identity");
        TextBox txtUsr_first_name = (TextBox)gvr.FindControl("txt_first_name");
        TextBox txtUsr_last_name = (TextBox)gvr.FindControl("txt_last_name");
        RadioButtonList rblGender = (RadioButtonList)gvr.FindControl("rblGender");
        TextBox editableBirthdate = (TextBox)gvr.FindControl("editableTextBoxBirthday");
        DropDownList ddlSchool = (DropDownList)gvr.FindControl("ddlSchool");
        DropDownList ddlCities = (DropDownList)gvr.FindControl("ddlCities");
        TextBox txt_Usr_Address = (TextBox)gvr.FindControl("txt_Usr_Address");
        TextBox txt_Home_Phone = (TextBox)gvr.FindControl("txt_Home_Phone");
        TextBox txt_Cellphone = (TextBox)gvr.FindControl("txt_Cellphone");
        TextBox txt_Email = (TextBox)gvr.FindControl("txt_Email");
        DropDownList ddlLevels = (DropDownList)gvr.FindControl("ddlLevels");
        ListBox lbProfessions = (ListBox)gvr.FindControl("lbProfessions");


        //all vars to one object
        //add cty to db
        ch_cities cty = new ch_cities();
        cty.cty_Name = ddlCities.SelectedItem.Text;

        //validation
        if (!ValidatePage(gvr)) { return; }

        //בדיקה אם קיים בWS
        Cities.Cities ctyWs = new Cities.Cities();
        if (!ctyWs.IsExist(cty.cty_Name)) {
            lblErrGV.Text = "העיר כבר לא קיימת במאגר הנתונים הארצי";
            return;
        }
        ch_citiesSvc.AddCity(cty);

        ch_users usr1 = new ch_users();
        usr1.usr_Identity = txtUsr_identity.Text;
        usr1.usr_First_Name = txtUsr_first_name.Text;
        usr1.usr_Last_Name = txtUsr_last_name.Text;
        usr1.usr_Gender = rblGender.SelectedValue;
        usr1.usr_Birth_Date = editableBirthdate.Text;
        usr1.sc_Id = Convert.ToInt32(ddlSchool.SelectedValue);
        usr1.cty_Id = ch_citiesSvc.GetIdByCtyName(cty.cty_Name);
        usr1.usr_Address = txt_Usr_Address.Text;
        usr1.usr_Home_Phone = txt_Home_Phone.Text;
        usr1.usr_Cellphone = txt_Cellphone.Text;
        usr1.usr_Email = txt_Email.Text;
        usr1.lvl_Id = Convert.ToInt32(ddlLevels.SelectedValue);

        //ch_teachers tch1 = new ch_teachers();

        

        //get usr_id
        Label lblcontentUsr_Id = (Label)gvr.FindControl("lblcontentUsr_Id");
        int usr_id = Convert.ToInt32(lblcontentUsr_Id.Text);

        //update professions
        foreach (ListItem li in lbProfessions.Items) {
            ch_teachers_professions tch_pro = new ch_teachers_professions(Convert.ToInt32(li.Value), usr_id);

            if (!ch_teachers_professionsSvc.IsExist(tch_pro) && li.Selected) {
                ch_teachers_professionsSvc.AddTeacherProfessions(tch_pro);
            }
            else if (ch_teachers_professionsSvc.IsExist(tch_pro) && !li.Selected) {
                ch_teachers_professionsSvc.DeleteTeacherProfessions(tch_pro);
            }
        }

        //update
        string err = ch_usersSvc.UpdateUserById(usr_id, usr1);
        //ch_teachersSvc.UpdateTchById(usr_id, tch1);

        int row_index = gvr.RowIndex;


        if (ddlSchools.SelectedValue == "-1") {
            gvBind();
        }
        else {
            if (Convert.ToInt32(Session["lvl_id"]) == 3) { // if editor
                //Bind data to GridView
                int sc_id = Convert.ToInt32(Session["sc_id"]);
                DataSet dsTeachers = ch_teachersSvc.GetTeachersGV(sc_id);
                for (int i = 0; i < dsTeachers.Tables[0].Rows.Count; i += 2) {
                    DataRow details = dsTeachers.Tables[0].NewRow();
                    dsTeachers.Tables[0].Rows.InsertAt(details, i + 1);
                }

                GridViewSvc.GVBind(dsTeachers, GVTeachers);
            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 4) { // if vice_manager
                //Bind data to GridView
                int sc_id = Convert.ToInt32(ddlSchools.SelectedValue);
                DataSet dsTeachers = ch_teachersSvc.GetTeachersGV(sc_id);
                for (int i = 0; i < dsTeachers.Tables[0].Rows.Count; i += 2) {
                    DataRow details = dsTeachers.Tables[0].NewRow();
                    dsTeachers.Tables[0].Rows.InsertAt(details, i + 1);
                }

                GridViewSvc.GVBind(dsTeachers, GVTeachers);
            }
        }

        GridViewRow gvr2 = GVTeachers.Rows[row_index];

        Label lblError = (Label)gvr2.FindControl("lblError");
        lblError.Text = err;

        if (lblError.Text == "")
            lblError.Text = "המשתמש עודכן בהצלחה";

        //hide all textboxes
        txtUsr_identity.Visible = false;
        Label lblUsr_identity = (Label)gvr.FindControl("lblcontentUsr_Identity");
        lblUsr_identity.Visible = true;

        txtUsr_first_name.Visible = false;
        Label lblUsr_first_name = (Label)gvr.FindControl("lblcontentFirst_Name");
        lblUsr_first_name.Visible = true;

        txtUsr_last_name.Visible = false;
        Label lblUsr_last_name = (Label)gvr.FindControl("lblcontentLast_Name");
        lblUsr_last_name.Visible = true;

        rblGender.Visible = false;
        Label lblcontentGender = (Label)gvr.FindControl("lblcontentGender");
        lblcontentGender.Visible = true;

        editableBirthdate.Visible = false;
        Label lblcontentBirth_Date = (Label)gvr.FindControl("lblcontentBirth_Date");
        lblcontentBirth_Date.Visible = true;

        ddlSchool.Visible = false;
        Label lblcontentSc_Name = (Label)gvr.FindControl("lblcontentSc_Name");
        lblcontentSc_Name.Visible = true;

        ddlCities.Visible = false;
        Label lblcontentCty_Name = (Label)gvr.FindControl("lblcontentCty_Name");
        lblcontentCty_Name.Visible = true;

        txt_Usr_Address.Visible = false;
        Label lblcontentUsr_Address = (Label)gvr.FindControl("lblcontentUsr_Address");
        lblcontentUsr_Address.Visible = true;

        txt_Home_Phone.Visible = false;
        Label lblcontentHome_Phone = (Label)gvr.FindControl("lblcontentHome_Phone");
        lblcontentHome_Phone.Visible = true;

        txt_Cellphone.Visible = false;
        Label lblcontentCellphone = (Label)gvr.FindControl("lblcontentCellphone");
        lblcontentCellphone.Visible = true;

        txt_Email.Visible = false;
        Label lblcontentEmail = (Label)gvr.FindControl("lblcontentEmail");
        lblcontentEmail.Visible = true;

        if (ddlLevels != null) {
            ddlLevels.Visible = false;
            Label lblcontentLvl_Name = (Label)gvr.FindControl("lblcontentLvl_Name");
            lblcontentLvl_Name.Visible = true;
        }

        lbProfessions.Visible = false;
        Label lblcontentUsr_Professions = (Label)gvr.FindControl("lblcontentUsr_Professions");
        lblcontentUsr_Professions.Visible = true;

        ImageButton btnEdit = (ImageButton)gvr.FindControl("btnEdit");
        btnEdit.Visible = true;

        ImageButton btnDelete = (ImageButton)gvr.FindControl("btnDelete");
        btnDelete.Visible = true;

        ImageButton btnCancel = (ImageButton)gvr.FindControl("btnCancel");
        btnCancel.Visible = false;

        ImageButton btnUpdate = (ImageButton)gvr.FindControl("btnUpdate");
        btnUpdate.Visible = false;

    }
    protected void GVTeachers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex % 2 != 0)
            {  
                for (int i = 0; i < 12; i++)
                {
                    e.Row.Cells.RemoveAt(1);
                } 
                e.Row.Cells[0].ColumnSpan = 9;
                string usr_id = GVTeachers.Rows[e.Row.RowIndex - 1].Cells[0].Text;
                string usr_identity = GVTeachers.Rows[e.Row.RowIndex - 1].Cells[1].Text;
                string usr_first_name = GVTeachers.Rows[e.Row.RowIndex - 1].Cells[2].Text;
                string usr_last_name = GVTeachers.Rows[e.Row.RowIndex - 1].Cells[3].Text;
                string birth_date = GVTeachers.Rows[e.Row.RowIndex - 1].Cells[4].Text;
                birth_date = birth_date.Substring(0, birth_date.IndexOf(' '));
                string usr_gender = GVTeachers.Rows[e.Row.RowIndex - 1].Cells[5].Text;
                string cty_name = GVTeachers.Rows[e.Row.RowIndex - 1].Cells[6].Text;
                string usr_address = GVTeachers.Rows[e.Row.RowIndex - 1].Cells[7].Text;
                string usr_home_phone = GVTeachers.Rows[e.Row.RowIndex - 1].Cells[8].Text;
                string usr_cellphone = GVTeachers.Rows[e.Row.RowIndex - 1].Cells[9].Text;
                string sc_name = GVTeachers.Rows[e.Row.RowIndex - 1].Cells[10].Text;
                string sc_cty_name = GVTeachers.Rows[e.Row.RowIndex - 1].Cells[11].Text;
                string usr_email = GVTeachers.Rows[e.Row.RowIndex - 1].Cells[12].Text;
                string lvl_name = GVTeachers.Rows[e.Row.RowIndex - 1].Cells[13].Text;


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
                header_block1.Controls.Add(new LiteralControl("פרטי התלמיד"));

                //---------------------------------

                //usr_id
                Label lblsubUsr_Id = new Label();
                lblsubUsr_Id.Text = "מזהה: ";
                lblsubUsr_Id.CssClass = "lbl_subject";

                Panel pnlcontentUsr_Id = new Panel();
                pnlcontentUsr_Id.CssClass = "pnl_content";

                Label lblcontentUsr_Id = new Label();
                lblcontentUsr_Id.Text = usr_id;
                lblcontentUsr_Id.ID = "lblcontentUsr_Id";
                lblcontentUsr_Id.CssClass = "lbl_content";

                //add
                content_block1.Controls.Add(lblsubUsr_Id);
                content_block1.Controls.Add(pnlcontentUsr_Id);
                pnlcontentUsr_Id.Controls.Add(lblcontentUsr_Id);

                //---------------------------------

                //usr_identity
                Label lblsubUsr_Identity = new Label();
                lblsubUsr_Identity.Text = "ת.ז: ";
                lblsubUsr_Identity.CssClass = "lbl_subject";

                Panel pnlcontentUsr_Identity = new Panel();
                pnlcontentUsr_Identity.CssClass = "pnl_content";

                Label lblcontentUsr_Identity = new Label();
                lblcontentUsr_Identity.Text = usr_identity;
                lblcontentUsr_Identity.ID = "lblcontentUsr_Identity";
                lblcontentUsr_Identity.CssClass = "lbl_content";

                TextBox txt_identity = new TextBox();
                txt_identity.Text = usr_identity;
                txt_identity.ID = "txt_identity";
                txt_identity.Visible = false;
                pnlcontentUsr_Identity.Controls.Add(txt_identity);

                //add
                content_block1.Controls.Add(lblsubUsr_Identity);
                content_block1.Controls.Add(pnlcontentUsr_Identity);
                pnlcontentUsr_Identity.Controls.Add(lblcontentUsr_Identity);

                //---------------------------------

                //firstname
                Label lblsubFirst_Name = new Label();
                lblsubFirst_Name.Text = "שם פרטי: ";
                lblsubFirst_Name.CssClass = "lbl_subject";

                Panel pnlcontentFirst_Name = new Panel();
                pnlcontentFirst_Name.CssClass = "pnl_content";

                Label lblcontentFirst_Name = new Label();
                lblcontentFirst_Name.Text = usr_first_name;
                lblcontentFirst_Name.ID = "lblcontentFirst_Name";
                lblcontentFirst_Name.CssClass = "lbl_content";

                TextBox txt_first_name = new TextBox();
                txt_first_name.Text = usr_first_name;
                txt_first_name.ID = "txt_first_name";
                txt_first_name.Visible = false;
                pnlcontentFirst_Name.Controls.Add(txt_first_name);

                //add
                content_block1.Controls.Add(lblsubFirst_Name);
                content_block1.Controls.Add(pnlcontentFirst_Name);
                pnlcontentFirst_Name.Controls.Add(lblcontentFirst_Name);

                //---------------------------------

                //lastname
                Label lblsubLast_Name = new Label();
                lblsubLast_Name.Text = "שם משפחה: ";
                lblsubLast_Name.CssClass = "lbl_subject";

                Panel pnlcontentLast_Name = new Panel();
                pnlcontentLast_Name.CssClass = "pnl_content";

                Label lblcontentLast_Name = new Label();
                lblcontentLast_Name.Text = usr_last_name;
                lblcontentLast_Name.ID = "lblcontentLast_Name";
                lblcontentLast_Name.CssClass = "lbl_content";

                TextBox txt_last_name = new TextBox();
                txt_last_name.Text = usr_last_name;
                txt_last_name.ID = "txt_last_name";
                txt_last_name.Visible = false;
                pnlcontentLast_Name.Controls.Add(txt_last_name);

                //add
                content_block1.Controls.Add(lblsubLast_Name);
                content_block1.Controls.Add(pnlcontentLast_Name);
                pnlcontentLast_Name.Controls.Add(lblcontentLast_Name);

                //---------------------------------

                //gender
                Label lblsubGender = new Label();
                lblsubGender.Text = "מין: ";
                lblsubGender.CssClass = "lbl_subject";

                Panel pnlcontentGender = new Panel();
                pnlcontentGender.CssClass = "pnl_content";

                Label lblcontentGender = new Label();
                lblcontentGender.Text = usr_gender;
                lblcontentGender.ID = "lblcontentGender";
                lblcontentGender.CssClass = "lbl_content";

                RadioButtonList rblGender = new RadioButtonList();
                rblGender.CssClass = "rblGender";
                rblGender.ID = "rblGender";
                rblGender.Visible = false;
                rblGender.RepeatDirection = RepeatDirection.Horizontal;
                ListItem liMale = new ListItem();
                liMale.Text = "זכר";
                liMale.Value = "זכר";
                ListItem liFemale = new ListItem();
                liFemale.Text = "נקבה";
                liFemale.Value = "נקבה";

                rblGender.Items.Add(liMale);
                rblGender.Items.Add(liFemale);
                rblGender.SelectedValue = usr_gender;
                pnlcontentGender.Controls.Add(rblGender);

                //add
                content_block1.Controls.Add(lblsubGender);
                content_block1.Controls.Add(pnlcontentGender);
                pnlcontentGender.Controls.Add(lblcontentGender);

                //---------------------------------

                //birth_date
                Label lblsubBirth_Date = new Label();
                lblsubBirth_Date.Text = "תאריך לידה: ";
                lblsubBirth_Date.CssClass = "lbl_subject";

                Panel pnlcontentBirth_Date = new Panel();
                pnlcontentBirth_Date.CssClass = "pnl_content";

                Label lblcontentBirth_Date = new Label();
                lblcontentBirth_Date.Text = birth_date;
                lblcontentBirth_Date.ID = "lblcontentBirth_Date";
                lblcontentBirth_Date.CssClass = "lbl_content";

                TextBox editableBirthdate = new TextBox();
                editableBirthdate.ID = "editableTextBoxBirthday";
                editableBirthdate.Text = birth_date;
                editableBirthdate.Visible = false;

                AjaxControlToolkit.CalendarExtender birthdateCalendar = new AjaxControlToolkit.CalendarExtender();
                birthdateCalendar.ID = "calendarExtender" + Guid.NewGuid().ToString();
                birthdateCalendar.TargetControlID = "editableTextBoxBirthday";

                pnlcontentBirth_Date.Controls.Add(editableBirthdate);
                pnlcontentBirth_Date.Controls.Add(birthdateCalendar);

                //add
                content_block1.Controls.Add(lblsubBirth_Date);
                content_block1.Controls.Add(pnlcontentBirth_Date);
                pnlcontentBirth_Date.Controls.Add(lblcontentBirth_Date);


                //---------------------------------

                //school
                Label lblsubSc_Name = new Label();
                lblsubSc_Name.Text = "בית ספר: ";
                lblsubSc_Name.CssClass = "lbl_subject";

                Panel pnlcontentSc_Name = new Panel();
                pnlcontentSc_Name.CssClass = "pnl_content";

                Label lblcontentSc_Name = new Label();
                lblcontentSc_Name.Text = sc_name;
                lblcontentSc_Name.ID = "lblcontentSc_Name";
                lblcontentSc_Name.CssClass = "lbl_content";

                DropDownList ddlSchool = new DropDownList();
                ddlSchool.ID = "ddlSchool";

                if (Convert.ToInt32(Session["lvl_id"]) == 3) {// if editor
                    int sc_id = Convert.ToInt32(Session["sc_id"]);
                    DataSet dsSchool = ch_schoolsSvc.GetSchool(sc_id);
                    ddlSchool.DataSource = dsSchool;
                    ddlSchool.DataValueField = "sc_id";
                    dsSchool.Tables[0].Columns.Add("school", typeof(string), "sc_name + ' - ' + cty_name");
                    ddlSchool.DataTextField = "school";
                    ddlSchool.DataBind();

                    ddlSchool.SelectedValue = ch_schoolsSvc.GetId(sc_name, ch_citiesSvc.GetIdByCtyName(sc_cty_name)).ToString();
                    ddlSchool.Visible = false;
                }
                else if (Convert.ToInt32(Session["lvl_id"]) >= 4) { // if vice_manager and up
                    DataSet dsSchool = ch_schoolsSvc.GetSchools();
                    ddlSchool.DataSource = dsSchool;
                    ddlSchool.DataValueField = "sc_id";
                    dsSchool.Tables[0].Columns.Add("school", typeof(string), "sc_name + ' - ' + cty_name");
                    ddlSchool.DataTextField = "school";
                    ddlSchool.DataBind();

                    ddlSchool.Items.Add("-בחר בית ספר-");
                    ddlSchool.SelectedValue = ch_schoolsSvc.GetId(sc_name, ch_citiesSvc.GetIdByCtyName(sc_cty_name)).ToString();
                    ddlSchool.Visible = false;
                }

                pnlcontentSc_Name.Controls.Add(ddlSchool);

                //add
                content_block1.Controls.Add(lblsubSc_Name);
                content_block1.Controls.Add(pnlcontentSc_Name);
                pnlcontentSc_Name.Controls.Add(lblcontentSc_Name);


                //---------------------------------

                //lvl_name
                Label lblsubLvl_Name = new Label();
                lblsubLvl_Name.Text = "רמת משתמש: ";
                lblsubLvl_Name.CssClass = "lbl_subject";

                Panel pnlcontentLvl_Name = new Panel();
                pnlcontentLvl_Name.CssClass = "pnl_content";

                Label lblcontentLvl_Name = new Label();
                lblcontentLvl_Name.Text = lvl_name;
                lblcontentLvl_Name.ID = "lblcontentLvl_Name";
                lblcontentLvl_Name.CssClass = "lbl_content";

                if (Convert.ToInt32(Session["lvl_id"]) >= 5) { // if manager
                    DropDownList ddlLevels = new DropDownList();
                    ddlLevels.ID = "ddlLevels";
                    DataSet dsLevels = ch_levelsSvc.GetLevels();
                    ddlLevels.DataSource = dsLevels;
                    ddlLevels.DataValueField = "lvl_id";
                    ddlLevels.DataTextField = "lvl_name";
                    ddlLevels.DataBind();

                    ddlLevels.Items.Add(new ListItem("-בחר רמה-", "-1"));
                    ddlLevels.SelectedValue = ch_levelsSvc.GetIdByLevelName(lvl_name).ToString();
                    ddlLevels.Visible = false;

                    pnlcontentLvl_Name.Controls.Add(ddlLevels);
                }

                //add
                content_block1.Controls.Add(lblsubLvl_Name);
                content_block1.Controls.Add(pnlcontentLvl_Name);
                pnlcontentLvl_Name.Controls.Add(lblcontentLvl_Name);


                //*/*/*/*/*/*/*/*/*/*/*/*/*/*/*


                Panel wrap_block2 = new Panel();
                wrap_block2.CssClass = "wrap_block";

                Panel header_block2 = new Panel();
                header_block2.CssClass = "header_block";

                Panel content_block2 = new Panel();
                content_block2.CssClass = "content_block";

                e.Row.Cells[0].Controls.Add(wrap_block2);
                wrap_block2.Controls.Add(header_block2);
                wrap_block2.Controls.Add(content_block2);

                //title
                header_block2.Controls.Add(new LiteralControl("יצירת קשר"));

                //---------------------------------

                //city
                Label lblsubCty_Name = new Label();
                lblsubCty_Name.Text = "עיר: ";
                lblsubCty_Name.CssClass = "lbl_subject";

                Panel pnlcontentCty_Name = new Panel();
                pnlcontentCty_Name.CssClass = "pnl_content";

                Label lblcontentCty_Name = new Label();
                lblcontentCty_Name.Text = cty_name;
                lblcontentCty_Name.ID = "lblcontentCty_Name";
                lblcontentCty_Name.CssClass = "lbl_content";

                //DDL Cities
                DropDownList ddlCities = new DropDownList();
                ddlCities.ID = "ddlCities";
                Cities.Cities cty = new Cities.Cities();
                ddlCities.DataSource = cty.GetCities();
                ddlCities.DataTextField = "שם_ישוב";
                ddlCities.DataBind();

                ddlCities.Items.Add("-בחר עיר-");
                ddlCities.SelectedItem.Text = cty_name;
                ddlCities.Visible = false;

                pnlcontentCty_Name.Controls.Add(ddlCities);

                //add
                content_block2.Controls.Add(lblsubCty_Name);
                content_block2.Controls.Add(pnlcontentCty_Name);
                pnlcontentCty_Name.Controls.Add(lblcontentCty_Name);
                


                //---------------------------------

                //address
                Label lblsubUsr_Address = new Label();
                lblsubUsr_Address.Text = "כתובת: ";
                lblsubUsr_Address.CssClass = "lbl_subject";

                Panel pnlcontentUsr_Address = new Panel();
                pnlcontentUsr_Address.CssClass = "pnl_content";

                Label lblcontentUsr_Address = new Label();
                lblcontentUsr_Address.Text = usr_address;
                lblcontentUsr_Address.ID = "lblcontentUsr_Address";
                lblcontentUsr_Address.CssClass = "lbl_content";

                TextBox txt_Usr_Address = new TextBox();
                txt_Usr_Address.Text = usr_address;
                txt_Usr_Address.ID = "txt_Usr_Address";
                txt_Usr_Address.Visible = false;
                pnlcontentUsr_Address.Controls.Add(txt_Usr_Address);

                //add
                content_block2.Controls.Add(lblsubUsr_Address);
                content_block2.Controls.Add(pnlcontentUsr_Address);
                pnlcontentUsr_Address.Controls.Add(lblcontentUsr_Address);
                


                //---------------------------------

                //home_phone
                Label lblsubHome_Phone = new Label();
                lblsubHome_Phone.Text = "טלפון בבית: ";
                lblsubHome_Phone.CssClass = "lbl_subject";

                Panel pnlcontentHome_Phone = new Panel();
                pnlcontentHome_Phone.CssClass = "pnl_content";

                Label lblcontentHome_Phone = new Label();
                lblcontentHome_Phone.Text = usr_home_phone;
                lblcontentHome_Phone.ID = "lblcontentHome_Phone";
                lblcontentHome_Phone.CssClass = "lbl_content";

                TextBox txt_Home_Phone = new TextBox();
                txt_Home_Phone.Text = usr_home_phone;
                txt_Home_Phone.ID = "txt_Home_Phone";
                txt_Home_Phone.Visible = false;
                pnlcontentHome_Phone.Controls.Add(txt_Home_Phone);

                //add
                content_block2.Controls.Add(lblsubHome_Phone);
                content_block2.Controls.Add(pnlcontentHome_Phone);
                pnlcontentHome_Phone.Controls.Add(lblcontentHome_Phone);
                


                //---------------------------------

                //Cellphone
                Label lblsubCellphone = new Label();
                lblsubCellphone.Text = "פלאפון: ";
                lblsubCellphone.CssClass = "lbl_subject";

                Panel pnlcontentCellphone = new Panel();
                pnlcontentCellphone.CssClass = "pnl_content";

                Label lblcontentCellphone = new Label();
                lblcontentCellphone.Text = usr_cellphone;
                lblcontentCellphone.ID = "lblcontentCellphone";
                lblcontentCellphone.CssClass = "lbl_content";

                TextBox txt_Cellphone = new TextBox();
                txt_Cellphone.Text = usr_cellphone;
                txt_Cellphone.ID = "txt_Cellphone";
                txt_Cellphone.Visible = false;
                pnlcontentCellphone.Controls.Add(txt_Cellphone);

                //add
                content_block2.Controls.Add(lblsubCellphone);
                content_block2.Controls.Add(pnlcontentCellphone);
                pnlcontentCellphone.Controls.Add(lblcontentCellphone);
                


                //---------------------------------

                //email
                Label lblsubEmail = new Label();
                lblsubEmail.Text = "אימייל: ";
                lblsubEmail.CssClass = "lbl_subject";

                Panel pnlcontentEmail = new Panel();
                pnlcontentEmail.CssClass = "pnl_content";

                Label lblcontentEmail = new Label();
                lblcontentEmail.Text = usr_email;
                lblcontentEmail.ID = "lblcontentEmail";
                lblcontentEmail.CssClass = "lbl_content";

                TextBox txt_Email = new TextBox();
                txt_Email.Text = usr_email;
                txt_Email.ID = "txt_Email";
                txt_Email.Visible = false;
                pnlcontentEmail.Controls.Add(txt_Email);

                //add
                content_block2.Controls.Add(lblsubEmail);
                content_block2.Controls.Add(pnlcontentEmail);
                pnlcontentEmail.Controls.Add(lblcontentEmail);


                //*/*/*/*/*/*/*/*/*/*/*/*/*/*/*


                Panel wrap_block3 = new Panel();
                wrap_block3.CssClass = "wrap_block";

                Panel header_block3 = new Panel();
                header_block3.CssClass = "header_block";

                Panel content_block3 = new Panel();
                content_block3.CssClass = "content_block";

                e.Row.Cells[0].Controls.Add(wrap_block3);
                wrap_block3.Controls.Add(header_block3);
                wrap_block3.Controls.Add(content_block3);

                //title
                header_block3.Controls.Add(new LiteralControl("הכשרות"));

                //---------------------------------

                //Professions
                DataSet dsProfessions = ch_teachers_professionsSvc.GetProfessionsByTch(Convert.ToInt32(usr_id));
                string strProfessions = "";
                int k = 0;
                foreach (DataRow dr in dsProfessions.Tables[0].Rows) {
                    if (k == 0) {
                        strProfessions = dr["pro_name"].ToString();
                    }
                    else {
                        strProfessions += ", " + dr["pro_name"].ToString();
                    }

                    k++;
                }

                Label lblsubUsr_Professions = new Label();
                lblsubUsr_Professions.Text = "מוכשר ללמד: ";
                lblsubUsr_Professions.CssClass = "lbl_subject";

                Panel pnlcontentUsr_Professions = new Panel();
                pnlcontentUsr_Professions.CssClass = "pnl_content";

                Label lblcontentUsr_Professions = new Label();
                lblcontentUsr_Professions.Text = strProfessions;
                lblcontentUsr_Professions.ID = "lblcontentUsr_Professions";
                lblcontentUsr_Professions.CssClass = "lbl_content";

                ListBox lbProfessions = new ListBox();
                lbProfessions.ID = "lbProfessions";
                lbProfessions.SelectionMode = ListSelectionMode.Multiple;
                lbProfessions.Height = 300;
                //bind professions
                DataSet pro = ch_professionsSvc.GetProfessions();
                DataSet tch_pro = ch_teachers_professionsSvc.GetProfessionsByTch(Convert.ToInt32(usr_id));

                foreach (DataRow dr_pro in pro.Tables[0].Rows) {
                    ListItem li = new ListItem(dr_pro["pro_name"].ToString(), dr_pro["pro_id"].ToString());
                    lbProfessions.Items.Add(li);

                    foreach (DataRow dr_tch_pro in tch_pro.Tables[0].Rows) {
                        if (dr_tch_pro["pro_id"].ToString() == dr_pro["pro_id"].ToString()) {
                            li.Selected = true;
                        }
                    }
                }
                lbProfessions.Visible = false;
                pnlcontentUsr_Professions.Controls.Add(lbProfessions);

                //add
                content_block3.Controls.Add(lblsubUsr_Professions);
                content_block3.Controls.Add(pnlcontentUsr_Professions);
                pnlcontentUsr_Professions.Controls.Add(lblcontentUsr_Professions);


                //*-*-*-*-*-*-*-*-*-*-*-*-*-**-*

                ImageButton btnEdit = new ImageButton();
                btnEdit.ID = "btnEdit";
                btnEdit.ImageUrl = "~/Images/ic_edit_24px.png";
                btnEdit.ToolTip = "ערוך פרטים";
                btnEdit.Click += new ImageClickEventHandler(btnEdit_Click);
                btnEdit.CausesValidation = false;

                ImageButton btnDelete = new ImageButton();
                btnDelete.ID = "btnDelete";
                btnDelete.ImageUrl = "~/Images/ic_delete_24px.png";
                btnDelete.ToolTip = "מחק תלמיד";
                btnDelete.OnClientClick += "return ConfirmDelete()";
                btnDelete.Click += new ImageClickEventHandler(btnDelete_Click);
                btnDelete.CausesValidation = false;

                ImageButton btnCancel = new ImageButton();
                btnCancel.ID = "btnCancel";
                btnCancel.ImageUrl = "~/Images/cancel.png";
                btnCancel.ToolTip = "בטל עריכה";
                btnCancel.Click += new ImageClickEventHandler(btnCancel_Click);
                btnCancel.CausesValidation = false;
                btnCancel.Visible = false;

                ImageButton btnUpdate = new ImageButton();
                btnUpdate.ID = "btnUpdate";
                btnUpdate.ImageUrl = "~/Images/ok.png";
                btnUpdate.ToolTip = "עדכן פרטים";
                btnUpdate.Click += new ImageClickEventHandler(btnUpdate_Click);
                btnUpdate.CausesValidation = false;
                btnUpdate.Visible = false;

                Label lblError = new Label();
                lblError.Text = "";
                lblError.ID = "lblError";
                lblError.CssClass = "lblError";
                lblError.ForeColor = Color.FromArgb(1, 234, 106, 70);


                e.Row.Cells[0].Controls.Add(btnEdit);
                e.Row.Cells[0].Controls.Add(btnDelete);
                e.Row.Cells[0].Controls.Add(btnCancel);
                e.Row.Cells[0].Controls.Add(btnUpdate);
                e.Row.Cells[0].Controls.Add(new LiteralControl("<br />"));
                e.Row.Cells[0].Controls.Add(lblError);
            }
        } 
    }

    private void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        selectedRowIndex.Value = (gvr.RowIndex/2).ToString();

        TextBox txtUsr_identity = (TextBox)gvr.FindControl("txt_identity");
        txtUsr_identity.Visible = false;
        Label lblUsr_identity = (Label)gvr.FindControl("lblcontentUsr_Identity");
        lblUsr_identity.Visible = true;

        TextBox txtUsr_first_name = (TextBox)gvr.FindControl("txt_first_name");
        txtUsr_first_name.Visible = false;
        Label lblUsr_first_name = (Label)gvr.FindControl("lblcontentFirst_Name");
        lblUsr_first_name.Visible = true;

        TextBox txtUsr_last_name = (TextBox)gvr.FindControl("txt_last_name");
        txtUsr_last_name.Visible = false;
        Label lblUsr_last_name = (Label)gvr.FindControl("lblcontentLast_Name");
        lblUsr_last_name.Visible = true;

        RadioButtonList rblGender = (RadioButtonList)gvr.FindControl("rblGender");
        rblGender.Visible = false;
        Label lblcontentGender = (Label)gvr.FindControl("lblcontentGender");
        lblcontentGender.Visible = true;

        TextBox editableBirthdate = (TextBox)gvr.FindControl("editableTextBoxBirthday");
        editableBirthdate.Visible = false;
        Label lblcontentBirth_Date = (Label)gvr.FindControl("lblcontentBirth_Date");
        lblcontentBirth_Date.Visible = true;

        DropDownList ddlSchool = (DropDownList)gvr.FindControl("ddlSchool");
        ddlSchool.Visible = false;
        Label lblcontentSc_Name = (Label)gvr.FindControl("lblcontentSc_Name");
        lblcontentSc_Name.Visible = true;


        DropDownList ddlCities = (DropDownList)gvr.FindControl("ddlCities");
        ddlCities.Visible = false;
        Label lblcontentCty_Name = (Label)gvr.FindControl("lblcontentCty_Name");
        lblcontentCty_Name.Visible = true;

        TextBox txt_Usr_Address = (TextBox)gvr.FindControl("txt_Usr_Address");
        txt_Usr_Address.Visible = false;
        Label lblcontentUsr_Address = (Label)gvr.FindControl("lblcontentUsr_Address");
        lblcontentUsr_Address.Visible = true;

        TextBox txt_Home_Phone = (TextBox)gvr.FindControl("txt_Home_Phone");
        txt_Home_Phone.Visible = false;
        Label lblcontentHome_Phone = (Label)gvr.FindControl("lblcontentHome_Phone");
        lblcontentHome_Phone.Visible = true;

        TextBox txt_Cellphone = (TextBox)gvr.FindControl("txt_Cellphone");
        txt_Cellphone.Visible = false;
        Label lblcontentCellphone = (Label)gvr.FindControl("lblcontentCellphone");
        lblcontentCellphone.Visible = true;

        TextBox txt_Email = (TextBox)gvr.FindControl("txt_Email");
        txt_Email.Visible = false;
        Label lblcontentEmail = (Label)gvr.FindControl("lblcontentEmail");
        lblcontentEmail.Visible = true;

        DropDownList ddlLevels = (DropDownList)gvr.FindControl("ddlLevels");
        if (ddlLevels != null) {
            ddlLevels.Visible = false;
            Label lblcontentLvl_Name = (Label)gvr.FindControl("lblcontentLvl_Name");
            lblcontentLvl_Name.Visible = true;
        }

        ListBox lbProfessions = (ListBox)gvr.FindControl("lbProfessions");
        lbProfessions.Visible = false;
        Label lblcontentUsr_Professions = (Label)gvr.FindControl("lblcontentUsr_Professions");
        lblcontentUsr_Professions.Visible = true;

        ImageButton btnEdit = (ImageButton)gvr.FindControl("btnEdit");
        btnEdit.Visible = true;

        ImageButton btnDelete = (ImageButton)gvr.FindControl("btnDelete");
        btnDelete.Visible = true;

        ImageButton btnCancel = (ImageButton)gvr.FindControl("btnCancel");
        btnCancel.Visible = false;

        ImageButton btnUpdate = (ImageButton)gvr.FindControl("btnUpdate");
        btnUpdate.Visible = false;
    }
    protected void GVTeachers_PreRender(object sender, EventArgs e)
    {
    }
    protected void GVTeachers_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count > 1)
            e.Row.Cells[1].Visible = false;

        if (e.Row.Cells.Count > 4)
            e.Row.Cells[4].Visible = false;

        if (e.Row.Cells.Count > 10)
            e.Row.Cells[10].Visible = false;

        if (e.Row.Cells.Count > 11)
            e.Row.Cells[11].Visible = false;

        //if (e.Row.Cells.Count > 12)
        //    e.Row.Cells[12].Visible = false;

        if (e.Row.Cells.Count > 13)
            e.Row.Cells[13].Visible = false;
    }
    protected void ddlSchools_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            if (Convert.ToInt32(Session["lvl_id"]) >= 4) { // if vice_manager and up
                //DDL Schools
                DataSet dsSchools = ch_schoolsSvc.GetSchools();

                ddlSchools.DataSource = dsSchools;
                ddlSchools.DataValueField = "sc_id";
                dsSchools.Tables[0].Columns.Add("school", typeof(string), "sc_name + ' - ' + cty_name");
                ddlSchools.DataTextField = "school";
                ddlSchools.DataBind();

                ddlSchools.Items.Add(new ListItem("-כל בתי הספר-", "-1"));
                ddlSchools.SelectedIndex = ddlSchools.Items.Count - 1;
            }
        }
    }
    protected void ddlSchools_SelectedIndexChanged(object sender, EventArgs e) {
        if (ddlSchools.SelectedValue == "-1") {
            gvBind();
        }
        else {
            txtSearch.Text = "";

            //Bind data to GridView
            int sc_id = Convert.ToInt32(ddlSchools.SelectedValue);
            DataSet dsTeachers = ch_teachersSvc.GetTeachersGV(sc_id);
            for (int i = 0; i < dsTeachers.Tables[0].Rows.Count; i += 2) {
                DataRow details = dsTeachers.Tables[0].NewRow();
                dsTeachers.Tables[0].Rows.InsertAt(details, i + 1);
            }

            GridViewSvc.GVBind(dsTeachers, GVTeachers);
        }
    }
    protected void btnInsert_Click(object sender, EventArgs e) {
        Response.Redirect("RegisterTch.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e) {
        Button btn = (Button)sender;
        string currentUrl = Request.Url.AbsolutePath; // get current url eg.  /TESTERS/Default6.aspx
        Response.Redirect(currentUrl + "?search=" + txtSearch.Text);
    }
}