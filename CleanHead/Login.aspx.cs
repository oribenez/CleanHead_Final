using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usr_id"] != null)
        {
            Response.Redirect("Default.aspx");
        }
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        ch_users usr1 = new ch_users();
        usr1.usr_Identity = txtIdentity.Text.Trim();
        usr1.usr_Password = txtPassword.Text.Trim();

        lblErr.Text = "";

        if (ch_usersSvc.Login(usr1))
        {
            DataSet ds = ch_usersSvc.GetUserByIdentity(usr1.usr_Identity);
            int id = Convert.ToInt32(ds.Tables["ch_users"].Rows[0][0].ToString());

            Session["sc_id"] = ds.Tables["ch_users"].Rows[0]["sc_id"].ToString();
            Session["usr_id"] = id;
            Session["usr_type"] = ch_usersSvc.GetUsrType(id);
            Session["lvl_id"] = Convert.ToInt32(ds.Tables["ch_users"].Rows[0][13].ToString());
            Session["gender"] = ds.Tables["ch_users"].Rows[0]["usr_gender"].ToString();
            Session["fullName"] = ds.Tables["ch_users"].Rows[0]["usr_first_name"].ToString() + " " + ds.Tables["ch_users"].Rows[0]["usr_last_name"].ToString();
            
            Response.Redirect("Default.aspx");
        }
        else
        {
            lblErr.Text = "אימייל או סיסמא לא נכונים :(";
        }
    }
}