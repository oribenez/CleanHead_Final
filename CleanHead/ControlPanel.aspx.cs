using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ControlPanel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int lvl_id = Convert.ToInt32(Session["lvl_id"]);
        DataRow dr = ch_levelsSvc.GetLevel(lvl_id);

        lbl_lvl_name.Text = "(" + lvl_id + ") " + dr["lvl_name"].ToString();
    }
}