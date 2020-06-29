using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
public partial class RegisterTch : System.Web.UI.Page
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
                    }
                    else if (Convert.ToInt32(Session["lvl_id"]) == 4) { // if vice_manager
                        //DDL Schools
                        DataSet dsSchools = ch_schoolsSvc.GetSchools();

                        DDLSchools.DataSource = dsSchools;
                        DDLSchools.DataValueField = "sc_id";
                        DDLSchools.DataTextField = "sc_name";
                        DDLSchools.DataBind();

                        DDLSchools.Items.Add("-בחר בית ספר-");
                        DDLSchools.SelectedIndex = DDLSchools.Items.Count - 1;
                    }
                    else if (Convert.ToInt32(Session["lvl_id"]) >= 5) { // if manager and up
                        //DDL Levels
                        DataSet dsLevels = ch_levelsSvc.GetLevels();

                        ddlLevels.DataSource = dsLevels;
                        ddlLevels.DataValueField = "lvl_id";
                        ddlLevels.DataTextField = "lvl_name";
                        ddlLevels.DataBind();

                        ddlLevels.Items.Add(new ListItem("-בחר רמה-", "-1"));
                        ddlLevels.SelectedValue = "2";
                    }

                    //DDL cities
                    Cities.Cities cty = new Cities.Cities();
                    DDLCity.DataSource = cty.GetCities();
                    DDLCity.DataTextField = "שם_ישוב";
                    DDLCity.DataBind();

                    DDLCity.Items.Add("-בחר עיר-");
                    DDLCity.SelectedIndex = DDLCity.Items.Count - 1;

                    //listbox professions
                    DataSet dsProfessions = ch_professionsSvc.GetProfessions();

                    lbProfessions.DataSource = dsProfessions;
                    lbProfessions.DataValueField = "pro_id";
                    lbProfessions.DataTextField = "pro_name";
                    lbProfessions.DataBind();
                }
            }
        }
    }
    protected bool ValidatePage(GridViewRow gvr) {
        TextBox txtUsr_identity = (TextBox)gvr.FindControl("txtTchIdentity");
        TextBox txtUsr_first_name = (TextBox)gvr.FindControl("txtFirstName");
        TextBox txtUsr_last_name = (TextBox)gvr.FindControl("txtLastName");
        RadioButtonList rblGender = (RadioButtonList)gvr.FindControl("rbtGender");
        TextBox editableBirthdate = (TextBox)gvr.FindControl("DateTextBox");
        DropDownList ddlSchool = (DropDownList)gvr.FindControl("DDLSchools");
        DropDownList ddlCities = (DropDownList)gvr.FindControl("DDLCity");
        TextBox txt_Usr_Address = (TextBox)gvr.FindControl("txtAddress");
        TextBox txt_Home_Phone = (TextBox)gvr.FindControl("txtHomePhone");
        TextBox txt_Cellphone = (TextBox)gvr.FindControl("txtCellphone");
        TextBox txt_Email = (TextBox)gvr.FindControl("txtEmail");
        ListBox lbProfessions = (ListBox)gvr.FindControl("lbProfessions");

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
        double minAgeYears = 18;
        if ((DateTime.Now - dt).TotalDays < minAgeYears * 365) { // גיל מינימלי למורה 18 שנים
            lblErr.Text = "גיל מינימלי להוראה - 18 שנים";
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

        //tch_professions
        bool flag = false;
        foreach (ListItem li in lbProfessions.Items) {
            if (li.Selected) {
                flag = true;
                break;
            }
        }

        if (!flag) {
            lblErr.Text = "בחר לפחות הכשרה אחת";
            return false;
        }

        lblErr.Text = "";
        return true;
    }

    protected void Send_Click(object sender, EventArgs e)
    {
        //add cty to db
        ch_cities cty = new ch_cities();
        cty.cty_Name = DDLCity.SelectedItem.Text;

        //בדיקה אם קיים בWS
        Cities.Cities ctyWs = new Cities.Cities();
        if (!ctyWs.IsExist(cty.cty_Name)) {
            lblErr.Text = "העיר כבר לא קיימת במאגר הנתונים הארצי";
            return;
        }
        ch_citiesSvc.AddCity(cty);


        ch_users usr1 = new ch_users();
        usr1.usr_Identity = txtTchIdentity.Text.Trim();
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
        usr1.usr_Password = txtTchIdentity.Text.Trim() + "t";
        usr1.lvl_Id = Convert.ToInt32(ddlLevels.SelectedValue);

        // ביצוע הרשמה וכתיבת השגיאות אם יש!
        lblErr.Text = ch_usersSvc.AddUser(usr1);

        ch_teachers tch1 = new ch_teachers();
        tch1.usr_Id = ch_usersSvc.GetMaxId();

        lblErr.Text = ch_teachersSvc.AddTeacher(tch1);

        foreach (ListItem li in lbProfessions.Items) {
            if (li.Selected) {
                ch_teachers_professions tch_pro = new ch_teachers_professions(Convert.ToInt32(li.Value), tch1.usr_Id);
                ch_teachers_professionsSvc.AddTeacherProfessions(tch_pro);
            }
        }
        //אם אין שגיאות בהרשמה
        if (lblErr.Text == "")
        {
            //Response.Write("<script>alert('המשתמש נרשם בהצלחה');</script>");
            Response.Redirect("TeachersData.aspx");
        }
    }
    protected void ValidateDate_ServerValidate(object source, ServerValidateEventArgs args) {
        DateTime dt;
        if (!DateTime.TryParse(DateTextBox.Text, out dt)) {
            lblErr.Text = "תאריך לא תקין";
            args.IsValid = false;
        }
        double minAgeYears = 18;
        if ((DateTime.Now - dt).TotalDays < minAgeYears * 365) { // גיל מינימלי למורה 18 שנים
            lblErr.Text = "גיל מינימלי להוראה - 18 שנים";
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