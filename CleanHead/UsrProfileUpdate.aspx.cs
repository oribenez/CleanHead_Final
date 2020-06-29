using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class UsrProfileUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usr_id"] == null) {
            Response.Redirect("Login.aspx");
        }
        else {
            if (Session["usr_id"].ToString() == "") {
                Response.Redirect("Login.aspx");
            }
        }

        int usr_id = Convert.ToInt32(Session["usr_id"]);

        if (!IsPostBack) {

            //DDL Schools
            DataSet dsSchools = ch_schoolsSvc.GetSchools();

            DDLSchools.DataSource = dsSchools;
            DDLSchools.DataValueField = "sc_id";
            dsSchools.Tables[0].Columns.Add("school", typeof(string), "sc_name + ' - ' + cty_name");
            DDLSchools.DataTextField = "school";
            DDLSchools.DataBind();

            DDLSchools.Items.Add("-בחר בית ספר-");
            DDLSchools.SelectedIndex = DDLSchools.Items.Count - 1;

            //DDL cities
            Cities.Cities cty = new Cities.Cities();

            DDLCity.DataSource = cty.GetCities();
            DDLCity.DataTextField = "שם_ישוב";
            DDLCity.DataBind();

            DDLCity.Items.Add("-בחר עיר-");
            DDLCity.SelectedIndex = DDLCity.Items.Count - 1;

            //DDL Jobs
            DataSet dsJobs = ch_jobsSvc.GetJobs();

            ddlJobs.DataSource = dsJobs;
            ddlJobs.DataValueField = "job_id";
            ddlJobs.DataTextField = "job_name";
            ddlJobs.DataBind();

            ddlJobs.Items.Add("-בחר תפקיד-");
            ddlJobs.SelectedIndex = ddlJobs.Items.Count - 1;


            DataRow drUser = ch_usersSvc.GetUserById(usr_id);
            DDLSchools.SelectedValue = drUser["usr.sc_id"].ToString();
            txtAddress.Text = drUser["usr_address"].ToString();
            DateTextBox.Text = Convert.ToDateTime(drUser["usr_birth_date"].ToString()).ToShortDateString();
            DDLCity.SelectedItem.Text = drUser["cty_name"].ToString();
            txtEmail.Text = drUser["usr_email"].ToString();
            txtFirstName.Text = drUser["usr_first_name"].ToString();
            rbtGender.SelectedValue = drUser["usr_gender"].ToString();
            txtHomePhone.Text = drUser["usr_home_phone"].ToString();
            txtCellphone.Text = drUser["usr_cellphone"].ToString();
            txtIdentity.Text = drUser["usr_identity"].ToString();
            txtLastName.Text = drUser["usr_last_name"].ToString();

            if (ch_usersSvc.GetUsrType(usr_id) == "tch") {
                DataSet pro = ch_professionsSvc.GetProfessions();
                DataSet tch_pro = ch_teachers_professionsSvc.GetProfessionsByTch(usr_id);

                int i = 0;
                foreach (DataRow dr_pro in pro.Tables[0].Rows) {
                    ListItem li = new ListItem(dr_pro["pro_name"].ToString(), dr_pro["pro_id"].ToString());
                    lbProfessions.Items.Add(li);

                    foreach (DataRow dr_tch_pro in tch_pro.Tables[0].Rows) {
                        if (dr_tch_pro["pro_id"].ToString() == dr_pro["pro_id"].ToString()) {
                            li.Selected = true;
                        }
                    }
                    i++;
                }
                pnlTch.Visible = true;
            }
            if (ch_usersSvc.GetUsrType(usr_id) == "crw") {
                DataRow drCrew = ch_crewSvc.GetCrw(Convert.ToInt32(Session["usr_id"]));
                ddlJobs.SelectedValue = drCrew["job_id"].ToString();

                pnlCrw.Visible = true;
            }
        }  
    }
    protected void Send_Click(object sender, EventArgs e) {
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



        int usr_id = Convert.ToInt32(Session["usr_id"]);
        ch_users newUsr1 = new ch_users();
        newUsr1.sc_Id = Convert.ToInt32(DDLSchools.SelectedValue);
        newUsr1.usr_Address = txtAddress.Text;
        newUsr1.usr_Birth_Date = DateTextBox.Text;
        newUsr1.cty_Id = ch_citiesSvc.GetIdByCtyName(cty.cty_Name);
        newUsr1.usr_Email = txtEmail.Text;
        newUsr1.usr_First_Name = txtFirstName.Text;
        newUsr1.usr_Gender = rbtGender.SelectedValue;
        newUsr1.usr_Home_Phone = txtHomePhone.Text;
        newUsr1.usr_Cellphone = txtCellphone.Text;
        newUsr1.usr_Identity = txtIdentity.Text;
        newUsr1.usr_Last_Name = txtLastName.Text;

        if (ch_usersSvc.GetUsrType(usr_id) == "tch") {
            if (ValidateTch()) {
                foreach (ListItem li in lbProfessions.Items) {
                    ch_teachers_professions tch_pro = new ch_teachers_professions(Convert.ToInt32(li.Value), usr_id);

                    if (!ch_teachers_professionsSvc.IsExist(tch_pro) && li.Selected) {
                            ch_teachers_professionsSvc.AddTeacherProfessions(tch_pro);
                    }
                    else if (ch_teachers_professionsSvc.IsExist(tch_pro) && !li.Selected) {
                        ch_teachers_professionsSvc.DeleteTeacherProfessions(tch_pro);
                    }
                }
            }
            else {
                lblErr.Text = "הכנס מקצועות עליהם עברת הכשרה";
            }
        }
        if (ch_usersSvc.GetUsrType(usr_id) == "crw") {
            ch_crew newCrw1 = new ch_crew();
            newCrw1.job_Id = Convert.ToInt32(ddlJobs.SelectedValue);
            newCrw1.usr_Id = usr_id;
            ch_crewSvc.UpdateCrwById(newCrw1);
        }

        ch_usersSvc.UpdateUserById(usr_id, newUsr1);

        //update sessions
        Session["sc_id"] = newUsr1.sc_Id;
        Session["gender"] = newUsr1.usr_Gender;
        Session["fullName"] = newUsr1.usr_First_Name + " " + newUsr1.usr_Last_Name;

        Response.Redirect("UsrProfile.aspx");
    }
    protected bool ValidateTch() {
        int i = 0;
        foreach (ListItem li in lbProfessions.Items) {
            if (li.Selected) {
                i++;
            }
        }
        return (i == 0) ? false : true;
    }
    protected void ValidateDate_ServerValidate(object source, ServerValidateEventArgs args) {
        int usr_id = Convert.ToInt32(Session["usr_id"]);

        if (ch_usersSvc.GetUsrType(usr_id) == "stu") {
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
        else if (ch_usersSvc.GetUsrType(usr_id) == "tch") {
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
        else if (ch_usersSvc.GetUsrType(usr_id) == "crw") {
            DateTime dt;
            if (!DateTime.TryParse(DateTextBox.Text, out dt)) {
                lblErr.Text = "תאריך לא תקין";
                args.IsValid = false;
            }
            double minAgeYears = 18;
            if ((DateTime.Now - dt).TotalDays < minAgeYears * 365) { // גיל מינימלי לאיש צוות 18 שנים
                lblErr.Text = "גיל מינימלי לאיש צוות - 18 שנים";
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
}