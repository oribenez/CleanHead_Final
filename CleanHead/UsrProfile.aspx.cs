using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UsrProfile : System.Web.UI.Page
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
        if (Request.QueryString["usr_id"] == null)
        {
            DataRow drUser = ch_usersSvc.GetUserById(Convert.ToInt32(Session["usr_id"]));
            lblSchool.Text = drUser["sc_name"].ToString();
            lblAddress.Text = drUser["usr_address"].ToString();
            lblBirthDay.Text = Convert.ToDateTime(drUser["usr_birth_date"].ToString()).ToShortDateString();
            lblCity.Text = drUser["cty_name"].ToString();
            lblEmail.Text = drUser["usr_email"].ToString();
            lblfname.Text = drUser["usr_first_name"].ToString();
            lblGender.Text = drUser["usr_gender"].ToString();
            lblHomePhone.Text = drUser["usr_home_phone"].ToString();
            lblCellphone.Text = drUser["usr_cellphone"].ToString();
            lblIdentity.Text = drUser["usr_identity"].ToString();
            lblLname.Text = drUser["usr_last_name"].ToString();

            if (ch_usersSvc.GetUsrType(Convert.ToInt32(drUser["usr_id"])) == "stu")
            {
                DataRow drStudent = ch_studentsSvc.GetStudentById(Convert.ToInt32(Session["usr_id"]));
                lblRm_name.Text = drStudent["rm_name"].ToString();
                lblMomCellphone.Text = drStudent["stu_mom_cellphone"].ToString();
                lblMomFname.Text = drStudent["stu_mom_first_name"].ToString();
                lblMomIdentity.Text = drStudent["stu_mom_identity"].ToString();
                lblDadCellphone.Text = drStudent["stu_dad_cellphone"].ToString();
                lblDadFname.Text = drStudent["stu_dad_first_name"].ToString();
                lblDadIdentity.Text = drStudent["stu_dad_identity"].ToString();
                pnlStu.Visible = true;
            }
            else
            {
                if (ch_usersSvc.GetUsrType(Convert.ToInt32(drUser["usr_id"])) == "tch")
                {
                    DataSet professions = ch_teachers_professionsSvc.GetProfessionsByTch(Convert.ToInt32(Session["usr_id"]));
                    int i = 0;
                    foreach (DataRow dr in professions.Tables[0].Rows) {
                        if (i == 0) {
                            lblTchProfessions.Text = dr["pro_name"].ToString();
                        }
                        else {
                            lblTchProfessions.Text += ", " + dr["pro_name"].ToString();
                        }

                        i++;
                    }

                    pnlTch.Visible = true;
                }
                if (ch_usersSvc.GetUsrType(Convert.ToInt32(drUser["usr_id"])) == "crw")
                {
                    DataRow drCrew = ch_crewSvc.GetCrw(Convert.ToInt32(Session["usr_id"]));
                    lblJob.Text = drCrew["job_name"].ToString();

                    pnlCrw.Visible = true;
                }
            }
            
        }
        else
        {
            if (Request.QueryString["usr_id"].ToString() == "")
                Response.Redirect("UsrProfile.aspx");
            else if (!ch_usersSvc.IsExist(Convert.ToInt32(Request.QueryString["usr_id"].ToString())))
                Response.Redirect("Default.aspx");
            else
            {
                btnEdit.Visible = false;
                btnChangePass.Visible = false;

                DataRow drUser = ch_usersSvc.GetUserById(Convert.ToInt32(Request.QueryString["usr_id"].ToString()));
                lblSchool.Text = drUser["sc_name"].ToString();
                lblAddress.Text = drUser["usr_address"].ToString();
                lblBirthDay.Text = drUser["usr_birth_date"].ToString();
                lblCity.Text = drUser["cty_name"].ToString();
                lblEmail.Text = drUser["usr_email"].ToString();
                lblfname.Text = drUser["usr_first_name"].ToString();
                lblGender.Text = drUser["usr_gender"].ToString();
                lblHomePhone.Text = drUser["usr_home_phone"].ToString();
                lblCellphone.Text = drUser["usr_cellphone"].ToString();
                lblIdentity.Text = drUser["usr_identity"].ToString();
                lblLname.Text = drUser["usr_last_name"].ToString();

                if (ch_usersSvc.GetUsrType(Convert.ToInt32(Request.QueryString["usr_id"].ToString())) == "stu")
                {
                    DataRow drStudent = ch_studentsSvc.GetStudentById(Convert.ToInt32(Request.QueryString["usr_id"].ToString()));
                    lblRm_name.Text = drStudent["rm_name"].ToString();
                    lblMomCellphone.Text = drStudent["stu_mom_cellphone"].ToString();
                    lblMomFname.Text = drStudent["stu_mom_first_name"].ToString();
                    lblMomIdentity.Text = drStudent["stu_mom_identity"].ToString();
                    lblDadCellphone.Text = drStudent["stu_dad_cellphone"].ToString();
                    lblDadFname.Text = drStudent["stu_dad_first_name"].ToString();
                    lblDadIdentity.Text = drStudent["stu_dad_identity"].ToString();
                    pnlStu.Visible = true;
                }
                else
                {
                    if (ch_usersSvc.GetUsrType(Convert.ToInt32(Request.QueryString["usr_id"].ToString())) == "tch")
                    {
                        DataSet professions = ch_teachers_professionsSvc.GetProfessionsByTch(Convert.ToInt32(Session["usr_id"]));
                        int i = 0;
                        foreach (DataRow dr in professions.Tables[0].Rows) {
                            if (i == 0) {
                                lblTchProfessions.Text = dr["pro_name"].ToString();
                            }
                            else {
                                lblTchProfessions.Text += ", " + dr["pro_name"].ToString();
                            }
                            i++;
                        }

                        pnlTch.Visible = true;
                    }
                    if (ch_usersSvc.GetUsrType(Convert.ToInt32(Request.QueryString["usr_id"].ToString())) == "crw")
                    {
                        DataRow drCrew = ch_crewSvc.GetCrw(Convert.ToInt32(Session["usr_id"]));
                        lblJob.Text = drCrew["job_name"].ToString();

                        pnlCrw.Visible = true;
                    }
                }
            }
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["usr_id"] == null)
        {
            Response.Redirect("UsrProfileUpdate.aspx");
        }
        else
        {
            if (Request.QueryString["usr_id"].ToString() == "")
                Response.Redirect("UsrProfile.aspx");
            else if (!ch_lessonsSvc.IsExist(Convert.ToInt32(Request.QueryString["usr_id"].ToString())))
                Response.Redirect("Default.aspx");
            else
                Response.Redirect("UsrProfileUpdate.aspx?usr_id=" + Request.QueryString["usr_id"].ToString());
        }

        
    }
    protected void btnChangePass_Click(object sender, EventArgs e) {
        Response.Redirect("ChangePass.aspx");
    }
}