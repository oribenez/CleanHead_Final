using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Dashboard : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["fullname"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            if (Session["fullname"].ToString() != "")
            {
                DataRow dr_lvl = ch_levelsSvc.GetLevel(Convert.ToInt32(Session["lvl_id"]));
                user_text.Text = Session["fullname"].ToString() + " (" + dr_lvl["lvl_name"].ToString() + ")";
            }
        }
    }
}
