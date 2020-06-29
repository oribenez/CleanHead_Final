using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChangePass : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnChangePass_Click(object sender, EventArgs e) {
        if (ValidatePage()) {
            int usr_id = Convert.ToInt32(Session["usr_id"]);
            lblErr.Text = ch_usersSvc.ChangePass(usr_id, txtOldPass.Text.Trim(), txtNewPass.Text.Trim());

            //update complete
            if (lblErr.Text == "") {
                Response.Redirect("UsrProfile.aspx");
            }
        }
    }
    protected bool ValidatePage() {
        if (txtNewPass.Text != txtConfirmNewPass.Text) {
            lblErr.Text = "הסיסמאות לא תואמות";
            return false;
        }

        return true;
    }
}