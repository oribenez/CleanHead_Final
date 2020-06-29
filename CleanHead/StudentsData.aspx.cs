using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;

public partial class StudentsData : System.Web.UI.Page
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
            DataSet dsStudents = ch_studentsSvc.GetStudentsGV();
            for (int i = 0; i < dsStudents.Tables[0].Rows.Count; i += 2) {
                DataRow details = dsStudents.Tables[0].NewRow();
                dsStudents.Tables[0].Rows.InsertAt(details, i + 1);
            }

            GridViewSvc.GVBind(dsStudents, GVStudents);
        }
        else {
            if (Request.QueryString["search"] == "") {
                //Bind data to GridView
                DataSet dsStudents = ch_studentsSvc.GetStudentsGV();
                for (int i = 0; i < dsStudents.Tables[0].Rows.Count; i += 2) {
                    DataRow details = dsStudents.Tables[0].NewRow();
                    dsStudents.Tables[0].Rows.InsertAt(details, i + 1);
                }

                GridViewSvc.GVBind(dsStudents, GVStudents);
            }
            else {
                txtSearch.Text = Request.QueryString["search"].ToString();

                //Bind data to GridView
                DataSet dsStudents = ch_studentsSvc.GetStudentsGV(Request.QueryString["search"].ToString());
                for (int i = 0; i < dsStudents.Tables[0].Rows.Count; i += 2) {
                    DataRow details = dsStudents.Tables[0].NewRow();
                    dsStudents.Tables[0].Rows.InsertAt(details, i + 1);
                }

                GridViewSvc.GVBind(dsStudents, GVStudents);
            }
        }
    }
    protected void GVStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //Delete selected row
        int usr_id = Convert.ToInt32(GVStudents.DataKeys[e.RowIndex].Value);
        ch_studentsSvc.DeleteStudentById(usr_id);
        ch_usersSvc.DeleteUserById(usr_id);

        if (ddlSchools.SelectedValue == "-1") {
            gvBind();
        }
        else {
            if (Convert.ToInt32(Session["lvl_id"]) == 3) { // if editor
                //Bind data to GridView
                int sc_id = Convert.ToInt32(Session["sc_id"]);
                DataSet dsStudents = ch_studentsSvc.GetStudentsGV(sc_id);
                for (int i = 0; i < dsStudents.Tables[0].Rows.Count; i += 2) {
                    DataRow details = dsStudents.Tables[0].NewRow();
                    dsStudents.Tables[0].Rows.InsertAt(details, i + 1);
                }

                GridViewSvc.GVBind(dsStudents, GVStudents);
            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 4) { // if vice_manager
                //Bind data to GridView
                int sc_id = Convert.ToInt32(ddlSchools.SelectedValue);
                DataSet dsStudents = ch_studentsSvc.GetStudentsGV(sc_id);
                for (int i = 0; i < dsStudents.Tables[0].Rows.Count; i += 2) {
                    DataRow details = dsStudents.Tables[0].NewRow();
                    dsStudents.Tables[0].Rows.InsertAt(details, i + 1);
                }

                GridViewSvc.GVBind(dsStudents, GVStudents);
            }
        }
    }
    protected void GVStudents_Load(object sender, EventArgs e)
    {
        if (ddlSchools.SelectedValue == "-1") {
            gvBind();
        }
        else {
            if (Convert.ToInt32(Session["lvl_id"]) == 3) { // if editor
                //Bind data to GridView
                int sc_id = Convert.ToInt32(Session["sc_id"]);
                DataSet dsStudents = ch_studentsSvc.GetStudentsGV(sc_id);
                for (int i = 0; i < dsStudents.Tables[0].Rows.Count; i += 2) {
                    DataRow details = dsStudents.Tables[0].NewRow();
                    dsStudents.Tables[0].Rows.InsertAt(details, i + 1);
                }

                GridViewSvc.GVBind(dsStudents, GVStudents);
            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 4) { // if vice_manager
                //Bind data to GridView
                int sc_id = Convert.ToInt32(ddlSchools.SelectedValue);
                DataSet dsStudents = ch_studentsSvc.GetStudentsGV(sc_id);
                for (int i = 0; i < dsStudents.Tables[0].Rows.Count; i += 2) {
                    DataRow details = dsStudents.Tables[0].NewRow();
                    dsStudents.Tables[0].Rows.InsertAt(details, i + 1);
                }

                GridViewSvc.GVBind(dsStudents, GVStudents);
            }
        }
    }
    protected void GVStudents_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Change page index
        GVStudents.PageIndex = e.NewPageIndex;

        if (ddlSchools.SelectedValue == "-1") {
            gvBind();
        }
        else {
            if (Convert.ToInt32(Session["lvl_id"]) == 3) { // if editor
                //Bind data to GridView
                int sc_id = Convert.ToInt32(Session["sc_id"]);
                DataSet dsStudents = ch_studentsSvc.GetStudentsGV(sc_id);
                for (int i = 0; i < dsStudents.Tables[0].Rows.Count; i += 2) {
                    DataRow details = dsStudents.Tables[0].NewRow();
                    dsStudents.Tables[0].Rows.InsertAt(details, i + 1);
                }

                GridViewSvc.GVBind(dsStudents, GVStudents);
            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 4) { // if vice_manager
                //Bind data to GridView
                int sc_id = Convert.ToInt32(ddlSchools.SelectedValue);
                DataSet dsStudents = ch_studentsSvc.GetStudentsGV(sc_id);
                for (int i = 0; i < dsStudents.Tables[0].Rows.Count; i += 2) {
                    DataRow details = dsStudents.Tables[0].NewRow();
                    dsStudents.Tables[0].Rows.InsertAt(details, i + 1);
                }

                GridViewSvc.GVBind(dsStudents, GVStudents);
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
        ch_studentsSvc.DeleteStudentById(usr_id);


    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        selectedRowIndex.Value = (gvr.RowIndex/2).ToString();

        lblErr.Text = "";

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

        DropDownList ddlRoom = (DropDownList)gvr.FindControl("ddlRoom");
        ddlRoom.Visible = true;
        Label lblcontentRm_Name = (Label)gvr.FindControl("lblcontentRm_Name");
        lblcontentRm_Name.Visible = false;

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

        TextBox txt_Mom_Identity = (TextBox)gvr.FindControl("txt_Mom_Identity");
        txt_Mom_Identity.Visible = true;
        Label lblcontentMom_Identity = (Label)gvr.FindControl("lblcontentMom_Identity");
        lblcontentMom_Identity.Visible = false;

        TextBox txt_Mom_First_Name = (TextBox)gvr.FindControl("txt_Mom_First_Name");
        txt_Mom_First_Name.Visible = true;
        Label lblcontentMom_First_Name = (Label)gvr.FindControl("lblcontentMom_First_Name");
        lblcontentMom_First_Name.Visible = false;

        TextBox txt_Mom_Cellphone = (TextBox)gvr.FindControl("txt_Mom_Cellphone");
        txt_Mom_Cellphone.Visible = true;
        Label lblcontentMom_Cellphone = (Label)gvr.FindControl("lblcontentMom_Cellphone");
        lblcontentMom_Cellphone.Visible = false;

        TextBox txt_Dad_Identity = (TextBox)gvr.FindControl("txt_Dad_Identity");
        txt_Dad_Identity.Visible = true;
        Label lblcontentDad_Identity = (Label)gvr.FindControl("lblcontentDad_Identity");
        lblcontentDad_Identity.Visible = false;

        TextBox txt_Dad_First_Name = (TextBox)gvr.FindControl("txt_Dad_First_Name");
        txt_Dad_First_Name.Visible = true;
        Label lblcontentDad_First_Name = (Label)gvr.FindControl("lblcontentDad_First_Name");
        lblcontentDad_First_Name.Visible = false;

        TextBox txt_Dad_Cellphone = (TextBox)gvr.FindControl("txt_Dad_Cellphone");
        txt_Dad_Cellphone.Visible = true;
        Label lblcontentDad_Cellphone = (Label)gvr.FindControl("lblcontentDad_Cellphone");
        lblcontentDad_Cellphone.Visible = false;


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
        DropDownList ddlRoom = (DropDownList)gvr.FindControl("ddlRoom");
        TextBox editableBirthdate = (TextBox)gvr.FindControl("editableTextBoxBirthday");
        DropDownList ddlSchool = (DropDownList)gvr.FindControl("ddlSchool");
        DropDownList ddlCities = (DropDownList)gvr.FindControl("ddlCities");
        TextBox txt_Usr_Address = (TextBox)gvr.FindControl("txt_Usr_Address");
        TextBox txt_Home_Phone = (TextBox)gvr.FindControl("txt_Home_Phone");
        TextBox txt_Cellphone = (TextBox)gvr.FindControl("txt_Cellphone");
        TextBox txt_Email = (TextBox)gvr.FindControl("txt_Email");
        TextBox txt_Mom_Identity = (TextBox)gvr.FindControl("txt_Mom_Identity");
        TextBox txt_Mom_First_Name = (TextBox)gvr.FindControl("txt_Mom_First_Name");
        TextBox txt_Mom_Cellphone = (TextBox)gvr.FindControl("txt_Mom_Cellphone");
        TextBox txt_Dad_Identity = (TextBox)gvr.FindControl("txt_Dad_Identity");
        TextBox txt_Dad_First_Name = (TextBox)gvr.FindControl("txt_Dad_First_Name");
        TextBox txt_Dad_Cellphone = (TextBox)gvr.FindControl("txt_Dad_Cellphone");

        //identity
        if (txtUsr_identity.Text == "") {
            lblErr.Text = "הכנס ת.ז.";
            return false;
        }
        if (txtUsr_identity.Text.Length != 9) {
            lblErr.Text = "ת.ז. לא תקינה";
            return false;
        }

        //first name
        if (txtUsr_first_name.Text == "") {
            lblErr.Text = "הכנס שם פרטי";
            return false;
        }
        if (!Regex.IsMatch(txtUsr_first_name.Text, "^[a-zA-Zא-ת ]{2,20}$")) {
            lblErr.Text = "שם פרטי לא תקין";
            return false;
        }

        //last name
        if (txtUsr_last_name.Text == "") {
            lblErr.Text = "הכנס שם משפחה";
            return false;
        }
        if (!Regex.IsMatch(txtUsr_last_name.Text, "^[a-zA-Zא-ת ]{2,20}$")) {
            lblErr.Text = "שם משפחה לא תקין";
            return false;
        }

        //rblGender
        if (rblGender.SelectedValue == "-1") {
            lblErr.Text = "בחר מין";
            return false;
        }

        //ddlRoom
        if (ddlRoom.SelectedValue == "-1") {
            lblErr.Text = "בחר כיתה";
            return false;
        }

        //editableBirthdate
        if (editableBirthdate.Text == "") {
            lblErr.Text = "הכנס תאריך";
            return false;
        }
        DateTime dt;
        if (!DateTime.TryParse(editableBirthdate.Text,out dt)) {
            lblErr.Text = "תאריך לא תקין";
            return false;
        }
        double minAgeYears = 5;
        if ((DateTime.Now - dt).TotalDays < minAgeYears * 365) { // גיל מינימלי לתלמיד 5 שנים
            lblErr.Text = "גיל מינימלי לתלמיד - 5 שנים";
            return false;
        }
        double maxAgeYears = 120;
        if (DateTime.Now.Year - dt.Year > maxAgeYears) { // גיל מקסימלי לאדם הוא 120
            lblErr.Text = "גיל מקסימלי להרשמה - 120 שנים";
            return false;
        }

        //ddlSchool
        if (ddlSchool.SelectedValue == "-1") {
            lblErr.Text = "בחר בית ספר";
            return false;
        }

        //ddlCities
        if (ddlCities.SelectedValue == "-1") {
            lblErr.Text = "בחר עיר";
            return false;
        }

        //txt_Usr_Address
        if (txt_Usr_Address.Text == "") {
            lblErr.Text = "הכנס כתובת";
            return false;
        }
        if (txt_Usr_Address.Text.Length > 25) {
            lblErr.Text = "הכנס כתובת תקנית מעל 25 תווים";
            return false;
        }

        //txt_Home_Phone
        if (txt_Home_Phone.Text == "") {
            lblErr.Text = "הכנס מספר טלפון";
            return false;
        }
        if (!Regex.IsMatch(txt_Home_Phone.Text, @"^0[23489]{1}(\-)?[^0\D]{1}\d{6}$")) {
            lblErr.Text = "הכנס מספר טלפון בית תקני בן 7 ספרות";
            return false;
        }

        //txt_Cellphone 
        if (txt_Cellphone.Text == "") {
            lblErr.Text = "הכנס מספר טלפון נייד";
            return false;
        }
        if (!Regex.IsMatch(txt_Cellphone.Text, @"^05\d([-]{0,1})\d{7}$")) {
            lblErr.Text = "הכנס מספר טלפון נייד תקני בן 10 ספרות";
            return false;
        }

        //txt_Email
        if (txt_Email.Text == "") {
		    lblErr.Text = "הכנס אימייל";
            return false;
	    }
        if (!Regex.IsMatch(txt_Email.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase)) {
		    lblErr.Text = "הכנס אימייל תקני";
            return false;
	    }

        //txt_Mom_Identity
        if (txt_Mom_Identity.Text == "") {
            lblErr.Text = "הכנס מספר תעודת זהות של האמא";
            return false;
        }
        if (txt_Mom_Identity.Text.Length != 9) {
            lblErr.Text = "הכנס מספר תעודת זהות של האמא תקני בן 9 ספרות";
            return false;
        }

        //txt_Mom_First_Name
        if (txt_Mom_First_Name.Text == "") {
            lblErr.Text = "הכנס את שם האמא";
            return false;
        }
        if (!Regex.IsMatch(txt_Mom_First_Name.Text, "^[a-zA-Zא-ת ]{2,20}$")) {
            lblErr.Text = "הכנס את שם אמא תקני";
            return false;
        }

        //txt_Mom_Cellphone
        if (txt_Mom_Cellphone.Text != "") {
            if (!Regex.IsMatch(txt_Mom_Cellphone.Text, @"^0\d([\d]{0,1})([-]{0,1})\d{7}$")) {
                lblErr.Text = "הכנס מספר טלפון תקני של האמא";
                return false;
            }
         }

        //txt_Dad_Identity
        if (txt_Dad_Identity.Text == "") {
            lblErr.Text = "הכנס מספר תעודת זהות של האבא";
            return false;
        }
        if (txt_Dad_Identity.Text.Length != 9) {
            lblErr.Text = "הכנס מספר תעודת זהות של האבא תקני בן 9 ספרות";
            return false;
        }

        //txt_Dad_First_Name
        if (txt_Dad_First_Name.Text == "") {
            lblErr.Text = "הכנס את שם האבא";
            return false;
        }
        if (!Regex.IsMatch(txt_Dad_First_Name.Text, "^[a-zA-Zא-ת ]{2,20}$")) {
            lblErr.Text = "הכנס את שם אבא תקני";
            return false;
        }

        //txt_Dad_Cellphone
        if (txt_Dad_Cellphone.Text != "") {
            if (!Regex.IsMatch(txt_Dad_Cellphone.Text, @"^0\d([\d]{0,1})([-]{0,1})\d{7}$")) {
                lblErr.Text = "הכנס מספר טלפון תקני של האבא";
                return false;
            }
         }

        lblErr.Text = "";
        return true;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        selectedRowIndex.Value = (gvr.RowIndex/2).ToString();

        lblErr.Text = "";

        TextBox txtUsr_identity = (TextBox)gvr.FindControl("txt_identity");
        TextBox txtUsr_first_name = (TextBox)gvr.FindControl("txt_first_name");
        TextBox txtUsr_last_name = (TextBox)gvr.FindControl("txt_last_name");
        RadioButtonList rblGender = (RadioButtonList)gvr.FindControl("rblGender");
        DropDownList ddlRoom = (DropDownList)gvr.FindControl("ddlRoom");
        TextBox editableBirthdate = (TextBox)gvr.FindControl("editableTextBoxBirthday");
        DropDownList ddlSchool = (DropDownList)gvr.FindControl("ddlSchool");
        DropDownList ddlCities = (DropDownList)gvr.FindControl("ddlCities");
        TextBox txt_Usr_Address = (TextBox)gvr.FindControl("txt_Usr_Address");
        TextBox txt_Home_Phone = (TextBox)gvr.FindControl("txt_Home_Phone");
        TextBox txt_Cellphone = (TextBox)gvr.FindControl("txt_Cellphone");
        TextBox txt_Email = (TextBox)gvr.FindControl("txt_Email");
        TextBox txt_Mom_Identity = (TextBox)gvr.FindControl("txt_Mom_Identity");
        TextBox txt_Mom_First_Name = (TextBox)gvr.FindControl("txt_Mom_First_Name");
        TextBox txt_Mom_Cellphone = (TextBox)gvr.FindControl("txt_Mom_Cellphone");
        TextBox txt_Dad_Identity = (TextBox)gvr.FindControl("txt_Dad_Identity");
        TextBox txt_Dad_First_Name = (TextBox)gvr.FindControl("txt_Dad_First_Name");
        TextBox txt_Dad_Cellphone = (TextBox)gvr.FindControl("txt_Dad_Cellphone");
        
        
        //validation
        if (!ValidatePage(gvr)) { return; }
        
        //all vars to one object
        //add cty to db
        ch_cities cty = new ch_cities();
        cty.cty_Name = ddlCities.SelectedItem.Text;

        //בדיקה אם קיים בWS
        Cities.Cities ctyWs = new Cities.Cities();
        if (!ctyWs.IsExist(cty.cty_Name)) {
            lblErr.Text = "העיר כבר לא קיימת במאגר הנתונים הארצי";
            return;
        }
        ch_citiesSvc.AddCity(cty);

        ch_users usr1 = new ch_users();
        usr1.usr_Identity = txtUsr_identity.Text.Trim();
        usr1.usr_First_Name = txtUsr_first_name.Text.Trim();
        usr1.usr_Last_Name = txtUsr_last_name.Text.Trim();
        usr1.usr_Gender = rblGender.SelectedValue;
        usr1.usr_Birth_Date = editableBirthdate.Text.Trim();
        usr1.sc_Id = Convert.ToInt32(ddlSchool.SelectedValue);
        usr1.cty_Id = ch_citiesSvc.GetIdByCtyName(cty.cty_Name);
        usr1.usr_Address = txt_Usr_Address.Text.Trim();
        usr1.usr_Home_Phone = txt_Home_Phone.Text.Trim();
        usr1.usr_Cellphone = txt_Cellphone.Text.Trim();
        usr1.usr_Email = txt_Email.Text.Trim();
        usr1.lvl_Id = 0;

        ch_students stu1 = new ch_students();
        stu1.rm_Id = Convert.ToInt32(ddlRoom.SelectedValue);
        stu1.stu_Mom_Identity = txt_Mom_Identity.Text.Trim();
        stu1.stu_Mom_First_Name = txt_Mom_First_Name.Text.Trim();
        stu1.stu_Mom_Cellphone = txt_Mom_Cellphone.Text.Trim();
        stu1.stu_Dad_Identity = txt_Dad_Identity.Text.Trim();
        stu1.stu_Dad_First_Name = txt_Dad_First_Name.Text.Trim();
        stu1.stu_Dad_Cellphone = txt_Dad_Cellphone.Text.Trim();

        

        //update
        Label lblcontentUsr_Id = (Label)gvr.FindControl("lblcontentUsr_Id");
        int usr_id = Convert.ToInt32(lblcontentUsr_Id.Text);

        string err = ch_usersSvc.UpdateUserById(usr_id, usr1);
        ch_studentsSvc.UpdateStuById(usr_id, stu1);

        int row_index = gvr.RowIndex;



        if (ddlSchools.SelectedValue == "-1") {
            //Bind data to GridView
            gvBind();
        }
        else {
            if (Convert.ToInt32(Session["lvl_id"]) == 3) { // if editor
                //Bind data to GridView
                int sc_id = Convert.ToInt32(Session["sc_id"]);
                DataSet dsStudents = ch_studentsSvc.GetStudentsGV(sc_id);
                for (int i = 0; i < dsStudents.Tables[0].Rows.Count; i += 2) {
                    DataRow details = dsStudents.Tables[0].NewRow();
                    dsStudents.Tables[0].Rows.InsertAt(details, i + 1);
                }

                GridViewSvc.GVBind(dsStudents, GVStudents);
            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 4) { // if vice_manager
                //Bind data to GridView
                int sc_id = Convert.ToInt32(ddlSchools.SelectedValue);
                DataSet dsStudents = ch_studentsSvc.GetStudentsGV(sc_id);
                for (int i = 0; i < dsStudents.Tables[0].Rows.Count; i += 2) {
                    DataRow details = dsStudents.Tables[0].NewRow();
                    dsStudents.Tables[0].Rows.InsertAt(details, i + 1);
                }

                GridViewSvc.GVBind(dsStudents, GVStudents);
            }
        }

        GridViewRow gvr2 = GVStudents.Rows[row_index];

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

        ddlRoom.Visible = false;
        Label lblcontentRm_Name = (Label)gvr.FindControl("lblcontentRm_Name");
        lblcontentRm_Name.Visible = true;

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

        txt_Mom_Identity.Visible = false;
        Label lblcontentMom_Identity = (Label)gvr.FindControl("lblcontentMom_Identity");
        lblcontentMom_Identity.Visible = true;

        txt_Mom_First_Name.Visible = false;
        Label lblcontentMom_First_Name = (Label)gvr.FindControl("lblcontentMom_First_Name");
        lblcontentMom_First_Name.Visible = true;

        txt_Mom_Cellphone.Visible = false;
        Label lblcontentMom_Cellphone = (Label)gvr.FindControl("lblcontentMom_Cellphone");
        lblcontentMom_Cellphone.Visible = true;

        txt_Dad_Identity.Visible = false;
        Label lblcontentDad_Identity = (Label)gvr.FindControl("lblcontentDad_Identity");
        lblcontentDad_Identity.Visible = true;

        txt_Dad_First_Name.Visible = false;
        Label lblcontentDad_First_Name = (Label)gvr.FindControl("lblcontentDad_First_Name");
        lblcontentDad_First_Name.Visible = true;

        txt_Dad_Cellphone.Visible = false;
        Label lblcontentDad_Cellphone = (Label)gvr.FindControl("lblcontentDad_Cellphone");
        lblcontentDad_Cellphone.Visible = true;


        ImageButton btnEdit = (ImageButton)gvr.FindControl("btnEdit");
        btnEdit.Visible = true;

        ImageButton btnDelete = (ImageButton)gvr.FindControl("btnDelete");
        btnDelete.Visible = true;

        ImageButton btnCancel = (ImageButton)gvr.FindControl("btnCancel");
        btnCancel.Visible = false;

        ImageButton btnUpdate = (ImageButton)gvr.FindControl("btnUpdate");
        btnUpdate.Visible = false;

    }
    protected void GVStudents_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex % 2 != 0)
            {  
                for (int i = 0; i < 15; i++)
                {
                    e.Row.Cells.RemoveAt(1);
                } 
                e.Row.Cells[0].ColumnSpan = 13;
                string usr_id = GVStudents.Rows[e.Row.RowIndex - 1].Cells[0].Text;
                string usr_identity = GVStudents.Rows[e.Row.RowIndex - 1].Cells[1].Text;
                string usr_first_name = GVStudents.Rows[e.Row.RowIndex - 1].Cells[2].Text;
                string usr_last_name = GVStudents.Rows[e.Row.RowIndex - 1].Cells[3].Text;
                string birth_date = GVStudents.Rows[e.Row.RowIndex - 1].Cells[4].Text;
                birth_date = birth_date.Substring(0, birth_date.IndexOf(' '));
                string usr_gender = GVStudents.Rows[e.Row.RowIndex - 1].Cells[5].Text;
                string cty_name = GVStudents.Rows[e.Row.RowIndex - 1].Cells[6].Text;
                string usr_address = GVStudents.Rows[e.Row.RowIndex - 1].Cells[7].Text;
                string usr_home_phone = GVStudents.Rows[e.Row.RowIndex - 1].Cells[8].Text;
                string usr_cellphone = GVStudents.Rows[e.Row.RowIndex - 1].Cells[9].Text;
                string sc_name = GVStudents.Rows[e.Row.RowIndex - 1].Cells[10].Text;
                string sc_cty_name = GVStudents.Rows[e.Row.RowIndex - 1].Cells[11].Text;
                string usr_email = GVStudents.Rows[e.Row.RowIndex - 1].Cells[12].Text;
                string lvl_name = GVStudents.Rows[e.Row.RowIndex - 1].Cells[13].Text;
                string rm_name = GVStudents.Rows[e.Row.RowIndex - 1].Cells[14].Text;
                string mom_identity = GVStudents.Rows[e.Row.RowIndex - 1].Cells[15].Text;
                string mom_first_name = GVStudents.Rows[e.Row.RowIndex - 1].Cells[16].Text;
                string mom_cellphone = GVStudents.Rows[e.Row.RowIndex - 1].Cells[17].Text;
                string dad_identity = GVStudents.Rows[e.Row.RowIndex - 1].Cells[18].Text;
                string dad_first_name = GVStudents.Rows[e.Row.RowIndex - 1].Cells[19].Text;
                string dad_cellphone = GVStudents.Rows[e.Row.RowIndex - 1].Cells[20].Text;


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

                Panel pnl_all_Usr_Id = new Panel();
                pnl_all_Usr_Id.CssClass = "pnl_all_content";

                pnl_all_Usr_Id.Controls.Add(lblsubUsr_Id);
                pnl_all_Usr_Id.Controls.Add(pnlcontentUsr_Id);

                content_block1.Controls.Add(pnl_all_Usr_Id);
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
                Panel pnl_all_Usr_Identity = new Panel();
                pnl_all_Usr_Identity.CssClass = "pnl_all_content";

                pnl_all_Usr_Identity.Controls.Add(lblsubUsr_Identity);
                pnl_all_Usr_Identity.Controls.Add(pnlcontentUsr_Identity);

                content_block1.Controls.Add(pnl_all_Usr_Identity);

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
                Panel pnl_all_First_Name = new Panel();
                pnl_all_First_Name.CssClass = "pnl_all_content";

                pnl_all_First_Name.Controls.Add(lblsubFirst_Name);
                pnl_all_First_Name.Controls.Add(pnlcontentFirst_Name);

                content_block1.Controls.Add(pnl_all_First_Name);

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
                Panel pnl_all_Last_Name = new Panel();
                pnl_all_Last_Name.CssClass = "pnl_all_content";

                pnl_all_Last_Name.Controls.Add(lblsubLast_Name);
                pnl_all_Last_Name.Controls.Add(pnlcontentLast_Name);

                content_block1.Controls.Add(pnl_all_Last_Name);

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
                Panel pnl_all_Gender = new Panel();
                pnl_all_Gender.CssClass = "pnl_all_content";

                pnl_all_Gender.Controls.Add(lblsubGender);
                pnl_all_Gender.Controls.Add(pnlcontentGender);

                content_block1.Controls.Add(pnl_all_Gender);

                pnlcontentGender.Controls.Add(lblcontentGender);

                //---------------------------------

                //class
                Label lblsubRm_Name = new Label();
                lblsubRm_Name.Text = "כיתה: ";
                lblsubRm_Name.CssClass = "lbl_subject";

                Panel pnlcontentRm_Name = new Panel();
                pnlcontentRm_Name.CssClass = "pnl_content";

                Label lblcontentRm_Name = new Label();
                lblcontentRm_Name.Text = rm_name;
                lblcontentRm_Name.ID = "lblcontentRm_Name";
                lblcontentRm_Name.CssClass = "lbl_content";

                DropDownList ddlRoom = new DropDownList();
                ddlRoom.ID = "ddlRoom";
                DataSet dsRoom = ch_roomsSvc.GetRooms(Convert.ToInt32(Session["sc_id"]));
                ddlRoom.DataSource = dsRoom;
                ddlRoom.DataValueField = "rm_id";
                ddlRoom.DataTextField = "rm_name";
                ddlRoom.DataBind();

                ddlRoom.Items.Add(new ListItem("-בחר כיתה-", "-1"));

                ch_rooms rm1 = new ch_rooms();
                rm1.rm_Name = rm_name;
                rm1.sc_Id = Convert.ToInt32(Session["sc_id"]);

                ddlRoom.SelectedValue = ch_roomsSvc.GetId(rm1).ToString();
                ddlRoom.Visible = false;

                pnlcontentRm_Name.Controls.Add(ddlRoom);

                //add
                Panel pnl_all_Rm_Name = new Panel();
                pnl_all_Rm_Name.CssClass = "pnl_all_content";

                pnl_all_Rm_Name.Controls.Add(lblsubRm_Name);
                pnl_all_Rm_Name.Controls.Add(pnlcontentRm_Name);

                content_block1.Controls.Add(pnl_all_Rm_Name);

                pnlcontentRm_Name.Controls.Add(lblcontentRm_Name);


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
                Panel pnl_all_Birth_Date = new Panel();
                pnl_all_Birth_Date.CssClass = "pnl_all_content";

                pnl_all_Birth_Date.Controls.Add(lblsubBirth_Date);
                pnl_all_Birth_Date.Controls.Add(pnlcontentBirth_Date);

                content_block1.Controls.Add(pnl_all_Birth_Date);

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

                    ddlSchool.Items.Add(new ListItem("-בחר בית ספר-", "-1"));

                    ddlSchool.SelectedValue = ch_schoolsSvc.GetId(sc_name, ch_citiesSvc.GetIdByCtyName(sc_cty_name)).ToString();
                    ddlSchool.Visible = false;
                }
                

                pnlcontentSc_Name.Controls.Add(ddlSchool);

                //add
                Panel pnl_all_Sc_Name = new Panel();
                pnl_all_Sc_Name.CssClass = "pnl_all_content";

                pnl_all_Sc_Name.Controls.Add(lblsubSc_Name);
                pnl_all_Sc_Name.Controls.Add(pnlcontentSc_Name);

                content_block1.Controls.Add(pnl_all_Sc_Name);
                pnlcontentSc_Name.Controls.Add(lblcontentSc_Name);


                //---------------------------------

                //lvl_name
                Panel pnl_all_Lvl_Name = new Panel();
                pnl_all_Lvl_Name.CssClass = "pnl_all_content";

                Label lblsubLvl_Name = new Label();
                lblsubLvl_Name.Text = "רמת משתמש: ";
                lblsubLvl_Name.CssClass = "lbl_subject";

                Panel pnlcontentLvl_Name = new Panel();
                pnlcontentLvl_Name.CssClass = "pnl_content";

                Label lblcontentLvl_Name = new Label();
                lblcontentLvl_Name.Text = lvl_name;
                lblcontentLvl_Name.ID = "lblcontentLvl_Name";
                lblcontentLvl_Name.CssClass = "lbl_content";

                //add
                pnl_all_Lvl_Name.Controls.Add(lblsubLvl_Name);
                pnl_all_Lvl_Name.Controls.Add(pnlcontentLvl_Name);

                content_block1.Controls.Add(pnl_all_Lvl_Name);

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

                ddlCities.Items.Add(new ListItem("-בחר עיר-", "-1"));
                ddlCities.SelectedItem.Text = cty_name;
                ddlCities.Visible = false;

                pnlcontentCty_Name.Controls.Add(ddlCities);

                //add
                Panel pnl_all_Cty_Name = new Panel();
                pnl_all_Cty_Name.CssClass = "pnl_all_content";

                pnl_all_Cty_Name.Controls.Add(lblsubCty_Name);
                pnl_all_Cty_Name.Controls.Add(pnlcontentCty_Name);

                content_block2.Controls.Add(pnl_all_Cty_Name);

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
                Panel pnl_all_Usr_Address = new Panel();
                pnl_all_Usr_Address.CssClass = "pnl_all_content";

                pnl_all_Usr_Address.Controls.Add(lblsubUsr_Address);
                pnl_all_Usr_Address.Controls.Add(pnlcontentUsr_Address);

                content_block2.Controls.Add(pnl_all_Usr_Address);

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
                Panel pnl_all_Home_Phone = new Panel();
                pnl_all_Home_Phone.CssClass = "pnl_all_content";

                pnl_all_Home_Phone.Controls.Add(lblsubHome_Phone);
                pnl_all_Home_Phone.Controls.Add(pnlcontentHome_Phone);

                content_block2.Controls.Add(pnl_all_Home_Phone);

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
                Panel pnl_all_Cellphone = new Panel();
                pnl_all_Cellphone.CssClass = "pnl_all_content";

                pnl_all_Cellphone.Controls.Add(lblsubCellphone);
                pnl_all_Cellphone.Controls.Add(pnlcontentCellphone);

                content_block2.Controls.Add(pnl_all_Cellphone);

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
                Panel pnl_all_Email = new Panel();
                pnl_all_Email.CssClass = "pnl_all_content";

                pnl_all_Email.Controls.Add(lblsubEmail);
                pnl_all_Email.Controls.Add(pnlcontentEmail);

                content_block2.Controls.Add(pnl_all_Email);

                pnlcontentEmail.Controls.Add(lblcontentEmail);
                


                ///*//*/*//*/*/*/**/*/*/*/*/*/*

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
                header_block3.Controls.Add(new LiteralControl("פרטים נוספים"));

                //---------------------------------

                //Mom_Identity
                Label lblsubMom_Identity = new Label();
                lblsubMom_Identity.Text = "מספר זהות - אמא: ";
                lblsubMom_Identity.CssClass = "lbl_subject";

                Panel pnlcontentMom_Identity = new Panel();
                pnlcontentMom_Identity.CssClass = "pnl_content";

                Label lblcontentMom_Identity = new Label();
                lblcontentMom_Identity.Text = mom_identity;
                lblcontentMom_Identity.ID = "lblcontentMom_Identity";
                lblcontentMom_Identity.CssClass = "lbl_content";

                TextBox txt_Mom_Identity = new TextBox();
                txt_Mom_Identity.Text = mom_identity;
                txt_Mom_Identity.ID = "txt_Mom_Identity";
                txt_Mom_Identity.Visible = false;
                pnlcontentMom_Identity.Controls.Add(txt_Mom_Identity);

                //add
                Panel pnl_all_Mom_Identity = new Panel();
                pnl_all_Mom_Identity.CssClass = "pnl_all_content";

                pnl_all_Mom_Identity.Controls.Add(lblsubMom_Identity);
                pnl_all_Mom_Identity.Controls.Add(pnlcontentMom_Identity);

                content_block3.Controls.Add(pnl_all_Mom_Identity);

                pnlcontentMom_Identity.Controls.Add(lblcontentMom_Identity);
                


                //---------------------------------

                //Mom_First_Name
                Label lblsubMom_First_Name = new Label();
                lblsubMom_First_Name.Text = "שם האמא: ";
                lblsubMom_First_Name.CssClass = "lbl_subject";

                Panel pnlcontentMom_First_Name = new Panel();
                pnlcontentMom_First_Name.CssClass = "pnl_content";

                Label lblcontentMom_First_Name = new Label();
                lblcontentMom_First_Name.Text = mom_first_name;
                lblcontentMom_First_Name.ID = "lblcontentMom_First_Name";
                lblcontentMom_First_Name.CssClass = "lbl_content";

                TextBox txt_Mom_First_Name = new TextBox();
                txt_Mom_First_Name.Text = mom_first_name;
                txt_Mom_First_Name.ID = "txt_Mom_First_Name";
                txt_Mom_First_Name.Visible = false;
                pnlcontentMom_First_Name.Controls.Add(txt_Mom_First_Name);

                //add
                Panel pnl_all_Mom_First_Name = new Panel();
                pnl_all_Mom_First_Name.CssClass = "pnl_all_content";

                pnl_all_Mom_First_Name.Controls.Add(lblsubMom_First_Name);
                pnl_all_Mom_First_Name.Controls.Add(pnlcontentMom_First_Name);

                content_block3.Controls.Add(pnl_all_Mom_First_Name);

                pnlcontentMom_First_Name.Controls.Add(lblcontentMom_First_Name);
                


                //---------------------------------

                //Mom_Cellphone
                Label lblsubMom_Cellphone = new Label();
                lblsubMom_Cellphone.Text = "פלאפון - אמא: ";
                lblsubMom_Cellphone.CssClass = "lbl_subject";

                Panel pnlcontentMom_Cellphone = new Panel();
                pnlcontentMom_Cellphone.CssClass = "pnl_content";

                Label lblcontentMom_Cellphone = new Label();
                lblcontentMom_Cellphone.Text = mom_cellphone;
                lblcontentMom_Cellphone.ID = "lblcontentMom_Cellphone";
                lblcontentMom_Cellphone.CssClass = "lbl_content";

                TextBox txt_Mom_Cellphone = new TextBox();
                txt_Mom_Cellphone.Text = mom_cellphone;
                txt_Mom_Cellphone.ID = "txt_Mom_Cellphone";
                txt_Mom_Cellphone.Visible = false;
                pnlcontentMom_Cellphone.Controls.Add(txt_Mom_Cellphone);

                //add
                Panel pnl_all_Mom_Cellphone = new Panel();
                pnl_all_Mom_Cellphone.CssClass = "pnl_all_content";

                pnl_all_Mom_Cellphone.Controls.Add(lblsubMom_Cellphone);
                pnl_all_Mom_Cellphone.Controls.Add(pnlcontentMom_Cellphone);

                content_block3.Controls.Add(pnl_all_Mom_Cellphone);

                pnlcontentMom_Cellphone.Controls.Add(lblcontentMom_Cellphone);
                

                //---------------------------------

                //Dad_Identity
                Label lblsubDad_Identity = new Label();
                lblsubDad_Identity.Text = "מספר זהות - אבא: ";
                lblsubDad_Identity.CssClass = "lbl_subject";

                Panel pnlcontentDad_Identity = new Panel();
                pnlcontentDad_Identity.CssClass = "pnl_content";

                Label lblcontentDad_Identity = new Label();
                lblcontentDad_Identity.Text = dad_identity;
                lblcontentDad_Identity.ID = "lblcontentDad_Identity";
                lblcontentDad_Identity.CssClass = "lbl_content";

                TextBox txt_Dad_Identity = new TextBox();
                txt_Dad_Identity.Text = dad_identity;
                txt_Dad_Identity.ID = "txt_Dad_Identity";
                txt_Dad_Identity.Visible = false;
                pnlcontentDad_Identity.Controls.Add(txt_Dad_Identity);

                //add
                Panel pnl_all_Dad_Identity = new Panel();
                pnl_all_Dad_Identity.CssClass = "pnl_all_content";

                pnl_all_Dad_Identity.Controls.Add(lblsubDad_Identity);
                pnl_all_Dad_Identity.Controls.Add(pnlcontentDad_Identity);

                content_block3.Controls.Add(pnl_all_Dad_Identity);

                pnlcontentDad_Identity.Controls.Add(lblcontentDad_Identity);
                


                //---------------------------------

                //Dad_First_Name
                Label lblsubDad_First_Name = new Label();
                lblsubDad_First_Name.Text = "שם האבא: ";
                lblsubDad_First_Name.CssClass = "lbl_subject";

                Panel pnlcontentDad_First_Name = new Panel();
                pnlcontentDad_First_Name.CssClass = "pnl_content";

                Label lblcontentDad_First_Name = new Label();
                lblcontentDad_First_Name.Text = dad_first_name;
                lblcontentDad_First_Name.ID = "lblcontentDad_First_Name";
                lblcontentDad_First_Name.CssClass = "lbl_content";

                TextBox txt_Dad_First_Name = new TextBox();
                txt_Dad_First_Name.Text = dad_first_name;
                txt_Dad_First_Name.ID = "txt_Dad_First_Name";
                txt_Dad_First_Name.Visible = false;
                pnlcontentDad_First_Name.Controls.Add(txt_Dad_First_Name);

                //add
                Panel pnl_all_Dad_First_Name = new Panel();
                pnl_all_Dad_First_Name.CssClass = "pnl_all_content";

                pnl_all_Dad_First_Name.Controls.Add(lblsubDad_First_Name);
                pnl_all_Dad_First_Name.Controls.Add(pnlcontentDad_First_Name);

                content_block3.Controls.Add(pnl_all_Dad_First_Name);

                pnlcontentDad_First_Name.Controls.Add(lblcontentDad_First_Name);
                


                //---------------------------------

                //Dad_Cellphone
                Label lblsubDad_Cellphone = new Label();
                lblsubDad_Cellphone.Text = "פלאפון - אבא: ";
                lblsubDad_Cellphone.CssClass = "lbl_subject";

                Panel pnlcontentDad_Cellphone = new Panel();
                pnlcontentDad_Cellphone.CssClass = "pnl_content";

                Label lblcontentDad_Cellphone = new Label();
                lblcontentDad_Cellphone.Text = dad_cellphone;
                lblcontentDad_Cellphone.ID = "lblcontentDad_Cellphone";
                lblcontentDad_Cellphone.CssClass = "lbl_content";

                TextBox txt_Dad_Cellphone = new TextBox();
                txt_Dad_Cellphone.Text = dad_cellphone;
                txt_Dad_Cellphone.ID = "txt_Dad_Cellphone";
                txt_Dad_Cellphone.Visible = false;
                pnlcontentDad_Cellphone.Controls.Add(txt_Dad_Cellphone);

                //add
                Panel pnl_all_Dad_Cellphone = new Panel();
                pnl_all_Dad_Cellphone.CssClass = "pnl_all_content";

                pnl_all_Dad_Cellphone.Controls.Add(lblsubDad_Cellphone);
                pnl_all_Dad_Cellphone.Controls.Add(pnlcontentDad_Cellphone);

                content_block3.Controls.Add(pnl_all_Dad_Cellphone);

                pnlcontentDad_Cellphone.Controls.Add(lblcontentDad_Cellphone);



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
                btnDelete.Click += new ImageClickEventHandler(btnDelete_Click);
                btnDelete.OnClientClick += "return ConfirmDelete()";
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

        DropDownList ddlRoom = (DropDownList)gvr.FindControl("ddlRoom");
        ddlRoom.Visible = false;
        Label lblcontentRm_Name = (Label)gvr.FindControl("lblcontentRm_Name");
        lblcontentRm_Name.Visible = true;

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

        TextBox txt_Mom_Identity = (TextBox)gvr.FindControl("txt_Mom_Identity");
        txt_Mom_Identity.Visible = false;
        Label lblcontentMom_Identity = (Label)gvr.FindControl("lblcontentMom_Identity");
        lblcontentMom_Identity.Visible = true;

        TextBox txt_Mom_First_Name = (TextBox)gvr.FindControl("txt_Mom_First_Name");
        txt_Mom_First_Name.Visible = false;
        Label lblcontentMom_First_Name = (Label)gvr.FindControl("lblcontentMom_First_Name");
        lblcontentMom_First_Name.Visible = true;

        TextBox txt_Mom_Cellphone = (TextBox)gvr.FindControl("txt_Mom_Cellphone");
        txt_Mom_Cellphone.Visible = false;
        Label lblcontentMom_Cellphone = (Label)gvr.FindControl("lblcontentMom_Cellphone");
        lblcontentMom_Cellphone.Visible = true;

        TextBox txt_Dad_Identity = (TextBox)gvr.FindControl("txt_Dad_Identity");
        txt_Dad_Identity.Visible = false;
        Label lblcontentDad_Identity = (Label)gvr.FindControl("lblcontentDad_Identity");
        lblcontentDad_Identity.Visible = true;

        TextBox txt_Dad_First_Name = (TextBox)gvr.FindControl("txt_Dad_First_Name");
        txt_Dad_First_Name.Visible = false;
        Label lblcontentDad_First_Name = (Label)gvr.FindControl("lblcontentDad_First_Name");
        lblcontentDad_First_Name.Visible = true;

        TextBox txt_Dad_Cellphone = (TextBox)gvr.FindControl("txt_Dad_Cellphone");
        txt_Dad_Cellphone.Visible = false;
        Label lblcontentDad_Cellphone = (Label)gvr.FindControl("lblcontentDad_Cellphone");
        lblcontentDad_Cellphone.Visible = true;


        ImageButton btnEdit = (ImageButton)gvr.FindControl("btnEdit");
        btnEdit.Visible = true;

        ImageButton btnDelete = (ImageButton)gvr.FindControl("btnDelete");
        btnDelete.Visible = true;

        ImageButton btnCancel = (ImageButton)gvr.FindControl("btnCancel");
        btnCancel.Visible = false;

        ImageButton btnUpdate = (ImageButton)gvr.FindControl("btnUpdate");
        btnUpdate.Visible = false;
    }
    protected void GVStudents_PreRender(object sender, EventArgs e)
    {
    }
    protected void GVStudents_RowCreated(object sender, GridViewRowEventArgs e)
    {
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

        //if (e.Row.Cells.Count > 14)
        //    e.Row.Cells[14].Visible = false;

        if (e.Row.Cells.Count > 15)
            e.Row.Cells[15].Visible = false;

        if (e.Row.Cells.Count > 16)
            e.Row.Cells[16].Visible = false;

        if (e.Row.Cells.Count > 17)
            e.Row.Cells[17].Visible = false;

        if (e.Row.Cells.Count > 18)
            e.Row.Cells[18].Visible = false;

        if (e.Row.Cells.Count > 19)
            e.Row.Cells[19].Visible = false;

        if (e.Row.Cells.Count > 20)
            e.Row.Cells[20].Visible = false;
    }
    protected void btnInsert_Click(object sender, EventArgs e) {
        Response.Redirect("RegisterStu.aspx");
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
            //Bind data to GridView
            gvBind();
        }
        else {
            txtSearch.Text = "";
            //Bind data to GridView
            int sc_id = Convert.ToInt32(ddlSchools.SelectedValue);
            DataSet dsStudents = ch_studentsSvc.GetStudentsGV(sc_id);
            for (int i = 0; i < dsStudents.Tables[0].Rows.Count; i += 2) {
                DataRow details = dsStudents.Tables[0].NewRow();
                dsStudents.Tables[0].Rows.InsertAt(details, i + 1);
            }

            GridViewSvc.GVBind(dsStudents, GVStudents);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e) {
        Button btn = (Button)sender;
        string currentUrl = Request.Url.AbsolutePath; // get current url eg.  /TESTERS/Default6.aspx
        Response.Redirect(currentUrl + "?search=" + txtSearch.Text);
    }
}