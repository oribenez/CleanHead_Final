using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WriteMsg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["usr_id"] == null)
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        ch_messages msg = new ch_messages();
        msg.msg_Date = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
        msg.msg_Sender_Id = Session["usr_id"].ToString();
        msg.msg_Reciver_Id = ddlTo.SelectedValue;
        msg.msg_Title = txtTitle.Text.Trim();
        msg.msg_Content = txtContent.Text.Trim();
        msg.msg_Checked = false;

        ch_messagesSvc.NewUsrMsg(msg);
        
        lblPopupTitle.Text = "ההודעה נשלחה";
        lblPopupContent.Text = "לחץ אוקיי לכל ההודעות";
        pnlPopup.Visible = true;

    }
    protected void popupBtnX_Click(object sender, ImageClickEventArgs e)
    {
        pnlPopup.Visible = false;
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        Response.Redirect("Messages.aspx");
    }
    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUserType.SelectedValue == "choose")
        {
            ddlTo.Enabled = false;
        }
        else if (ddlUserType.SelectedValue == "crw")
        {
            DataSet dsCrw = ch_usersSvc.GetUsersByType("crw");
            ddlTo.DataSource = dsCrw;
            ddlTo.DataValueField = "usr_id";
            dsCrw.Tables[0].Columns.Add("usr_fullname", typeof(string), "usr_first_name + ' ' + usr_last_name");
            ddlTo.DataTextField = "usr_fullname";
            ddlTo.DataBind();

            ddlTo.Items.Add("-בחר איש צוות-");
            ddlTo.SelectedIndex = ddlTo.Items.Count - 1;
        }
        else if (ddlUserType.SelectedValue == "tch")
        {
            DataSet dsTch = ch_usersSvc.GetUsersByType("tch");
            ddlTo.DataSource = dsTch;
            ddlTo.DataValueField = "usr_id";
            dsTch.Tables[0].Columns.Add("usr_fullname", typeof(string), "usr_first_name + ' ' + usr_last_name");
            ddlTo.DataTextField = "usr_fullname";
            ddlTo.DataBind();

            ddlTo.Items.Add("-בחר מורה-");
            ddlTo.SelectedIndex = ddlTo.Items.Count - 1;
        }
        else
        {
            DataSet dsStu = ch_usersSvc.GetUsersByType("stu");
            ddlTo.DataSource = dsStu;
            ddlTo.DataValueField = "usr_id";
            dsStu.Tables[0].Columns.Add("usr_fullname", typeof(string), "usr_first_name + ' ' + usr_last_name");
            ddlTo.DataTextField = "usr_fullname";
            ddlTo.DataBind();

            ddlTo.Items.Add("-בחר תלמיד-");
            ddlTo.SelectedIndex = ddlTo.Items.Count - 1;
        }
        ddlTo.Enabled = true;
    }
}