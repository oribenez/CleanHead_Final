using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

public partial class RegisterStu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lvl_id"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            if (Convert.ToInt32(Session["lvl_id"]) < 3)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    if (Convert.ToInt32(Session["lvl_id"]) == 3) {// if editor
                        //DDL Schools
                        int sc_id = Convert.ToInt32(Session["sc_id"]);
                        DataSet dsSchools = ch_schoolsSvc.GetSchool(sc_id);

                        DDLSchools.DataSource = dsSchools;
                        DDLSchools.DataValueField = "sc_id";
                        dsSchools.Tables[0].Columns.Add("school", typeof(string), "sc_name + ' - ' + cty_name");
                        DDLSchools.DataTextField = "school";
                        DDLSchools.DataBind();

                        //DDL Rooms
                        DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(Session["sc_id"]));

                        ddlRooms.DataSource = dsRooms;
                        ddlRooms.DataValueField = "rm_id";
                        ddlRooms.DataTextField = "rm_name";
                        ddlRooms.DataBind();

                        ddlRooms.Items.Add("-בחר כיתה-");
                        ddlRooms.SelectedIndex = ddlRooms.Items.Count - 1;
                    }
                    else if (Convert.ToInt32(Session["lvl_id"]) >= 4) { // if vice_manager and up
                        //DDL Schools
                        DataSet dsSchools = ch_schoolsSvc.GetSchools();

                        DDLSchools.DataSource = dsSchools;
                        DDLSchools.DataValueField = "sc_id";
                        dsSchools.Tables[0].Columns.Add("school", typeof(string), "sc_name + ' - ' + cty_name");
                        DDLSchools.DataTextField = "school";
                        DDLSchools.DataBind();

                        DDLSchools.Items.Add("-בחר בית ספר-");
                        DDLSchools.SelectedIndex = DDLSchools.Items.Count - 1;

                        
                    }

                    //DDL cities
                    Cities.Cities cty = new Cities.Cities();

                    DDLCity.DataSource = cty.GetCities();
                    DDLCity.DataTextField = "שם_ישוב";
                    DDLCity.DataBind();

                    DDLCity.Items.Add("-בחר עיר-");
                    DDLCity.SelectedIndex = DDLCity.Items.Count - 1;
                }
            }
        }
    }
    protected bool ValidatePage(GridViewRow gvr) {
        TextBox txtUsr_identity = (TextBox)gvr.FindControl("txtStuIdentity");
        TextBox txtUsr_first_name = (TextBox)gvr.FindControl("txtFirstName");
        TextBox txtUsr_last_name = (TextBox)gvr.FindControl("txtLastName");
        RadioButtonList rblGender = (RadioButtonList)gvr.FindControl("rbtGender");
        DropDownList ddlRoom = (DropDownList)gvr.FindControl("ddlRooms");
        TextBox editableBirthdate = (TextBox)gvr.FindControl("DateTextBox");
        DropDownList ddlSchool = (DropDownList)gvr.FindControl("DDLSchools");
        DropDownList ddlCities = (DropDownList)gvr.FindControl("DDLCity");
        TextBox txt_Usr_Address = (TextBox)gvr.FindControl("txtAddress");
        TextBox txt_Home_Phone = (TextBox)gvr.FindControl("txtHomePhone");
        TextBox txt_Cellphone = (TextBox)gvr.FindControl("txtCellphone");
        TextBox txt_Email = (TextBox)gvr.FindControl("txtEmail");
        TextBox txt_Mom_Identity = (TextBox)gvr.FindControl("txtMomIdentity");
        TextBox txt_Mom_First_Name = (TextBox)gvr.FindControl("txtMomFirstName");
        TextBox txt_Mom_Cellphone = (TextBox)gvr.FindControl("txtMomCellphone");
        TextBox txt_Dad_Identity = (TextBox)gvr.FindControl("txtDadIdentity");
        TextBox txt_Dad_First_Name = (TextBox)gvr.FindControl("txtDadFirstName");
        TextBox txt_Dad_Cellphone = (TextBox)gvr.FindControl("txtDadCellphone");

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
        if (!DateTime.TryParse(editableBirthdate.Text, out dt)) {
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

    protected void Send_Click(object sender, EventArgs e)
    {
        //בדיקה אם קיים בWS
        Cities.Cities ctyWs = new Cities.Cities();
        if (!ctyWs.IsExist(DDLCity.SelectedItem.Text)) {
            lblErr.Text = "העיר כבר לא קיימת במאגר הנתונים הארצי";
            return;
        }
        //add cty to db
        ch_cities cty = new ch_cities();
        cty.cty_Name = DDLCity.SelectedItem.Text;

        ch_citiesSvc.AddCity(cty);

        ch_users usr1 = new ch_users();
        usr1.usr_Identity = txtStuIdentity.Text.Trim();
        usr1.usr_First_Name = txtFirstName.Text.Trim();
        usr1.usr_Last_Name = txtLastName.Text.Trim();
        DateTime dt = Convert.ToDateTime(DateTextBox.Text);
        usr1.usr_Birth_Date = dt.ToString("yyyy/MM/dd");
        usr1.usr_Gender = rbtGender.SelectedValue;
        usr1.cty_Id = ch_citiesSvc.GetIdByCtyName(cty.cty_Name);
        usr1.usr_Address = txtAddress.Text.Trim();
        usr1.usr_Home_Phone = txtHomePhone.Text.Trim();
        usr1.usr_Cellphone = txtCellphone.Text.Trim();
        usr1.sc_Id = Convert.ToInt32(DDLSchools.SelectedValue);
        usr1.usr_Email = txtEmail.Text.Trim();
        usr1.usr_Password = txtStuIdentity.Text.Trim() + "s";
        usr1.lvl_Id = 0;

        ch_students stu1 = new ch_students();
        stu1.usr_Id = ch_usersSvc.GetMaxId();
        stu1.rm_Id = Convert.ToInt32(ddlRooms.SelectedValue);
        stu1.stu_Mom_Identity = txtMomIdentity.Text.Trim();
        stu1.stu_Mom_First_Name = txtMomFirstName.Text.Trim();
        stu1.stu_Mom_Cellphone = txtMomCellphone.Text;
        stu1.stu_Dad_Identity = txtDadIdentity.Text;
        stu1.stu_Dad_First_Name = txtDadFirstName.Text;
        stu1.stu_Dad_Cellphone = txtDadCellphone.Text;


        // ביצוע הרשמה וכתיבת השגיאות אם יש!
        lblErr.Text = ch_usersSvc.AddUser(usr1);

        stu1.usr_Id = ch_usersSvc.GetMaxId();

        lblErr.Text = ch_studentsSvc.AddStudent(stu1);

        //אם אין שגיאות בהרשמה
        if (lblErr.Text == "") {
            //Response.Write("<script>alert('המשתמש נרשם בהצלחה');</script>");
            Response.Redirect("StudentsData.aspx");
        }
    }
    protected void DDLSchools_SelectedIndexChanged(object sender, EventArgs e) {
        if (DDLSchools.SelectedItem.Text == "-בחר בית ספר-") {
            ddlRooms.Items.Clear();
        }
        else {
            //DDL Rooms
            DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(DDLSchools.SelectedValue));

            ddlRooms.DataSource = dsRooms;
            ddlRooms.DataValueField = "rm_id";
            ddlRooms.DataTextField = "rm_name";
            ddlRooms.DataBind();

            ddlRooms.Items.Add("-בחר כיתה-");
            ddlRooms.SelectedIndex = ddlRooms.Items.Count - 1;
        }
    }
    protected void ValidateDate_ServerValidate(object source, ServerValidateEventArgs args) {
        DateTime dt;
        if (!DateTime.TryParse(DateTextBox.Text, out dt)) {
            lblErr.Text = "תאריך לא תקין";
            args.IsValid = false;
        }
        double minAgeYears = 5;
        if ((DateTime.Now - dt).TotalDays < minAgeYears * 365) { // גיל מינימלי לתלמיד 5 שנים
            lblErr.Text = "גיל מינימלי לתלמיד - 5 שנים";
            args.IsValid = false;
        }
        double maxAgeYears = 120;
        if (DateTime.Now.Year - dt.Year > maxAgeYears) { // גיל מקסימלי לאדם הוא 120
            lblErr.Text = "גיל מקסימלי להרשמה - 120 שנים";
            args.IsValid = false;
        }

        args.IsValid = true;
    }
}