using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
public partial class Messages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lvl_id"] == null)
        {
            Response.Redirect("Login.aspx");
        }
    }
    //-------------------MessagesRecived-------------------
    protected void gvMessagesRecived_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //bind data
            GVBindRecived();

            //dye messages
            DyeUnreadMessages();

            for (int i = 1; i < gvMessagesRecived.Rows.Count; i += 2) {
                gvMessagesRecived.Rows[i].Visible = false;
            }

            gvMessagesRecived.Columns[1].Visible = false; //visible false the = msg_id
            gvMessagesRecived.Columns[5].Visible = false; //visible false the = msg_checked
        }
    }
    protected void gvMessagesRecived_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMessagesRecived.PageIndex = e.NewPageIndex;

        //bind data
        GVBindRecived();

        //dye messages
        DyeUnreadMessages();
        //ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "", true);
        //הסתרת כל שורות המידע
        for (int i = 1; i < gvMessagesRecived.Rows.Count; i += 2) {
            gvMessagesRecived.Rows[i].Visible = false;
        }
    }
    public void GVBindRecived()
    {
        //Bind data to GridView
        DataSet dsMessagesRecived = ch_messagesSvc.GetMessagesRecivedGV(Convert.ToInt32(Session["usr_id"]));
        for (int i = 0; i < dsMessagesRecived.Tables[0].Rows.Count; i += 2) {
            DataRow details = dsMessagesRecived.Tables[0].NewRow();
            dsMessagesRecived.Tables[0].Rows.InsertAt(details, i + 1);
        }
        
        GridViewSvc.GVBind(dsMessagesRecived, gvMessagesRecived);
    }
    public void DyeUnreadMessages() {
        if (gvMessagesRecived.Rows.Count >= 2) {
            for (int i = 0; i < gvMessagesRecived.Rows.Count; i += 2) {
                CheckBox ck_msg_checked_recived = (CheckBox)gvMessagesRecived.Rows[i].FindControl("ck_msg_checked_recived");

                if (!ck_msg_checked_recived.Checked) {
                    gvMessagesRecived.Rows[i].BackColor = ColorTranslator.FromHtml("#ED8977");
                }
            }
        }
    }
    public void SelectCheckboxesRecived() {
        string[] arr = hfSelectedRowsRecived.Value.Split(',');
        bool flag = false;
        if (Convert.ToInt32(arr[0]) >= 0) {
            for (int i = 0; i < arr.Length; i++) {

                flag = true;
                GridViewRow current = gvMessagesRecived.Rows[Convert.ToInt32(arr[i])];

                ((CheckBox)current.FindControl("ckMark_recived")).Checked = true;
                current.BackColor = ColorTranslator.FromHtml("#d5db5d");
            }
        }
        if (flag) {
            btnMsgRecivedDelete.Visible = true;
        }
        else {
            btnMsgRecivedDelete.Visible = false;
        }
    }
    protected void btnMsgRecivedDelete_Click(object sender, EventArgs e) {
        string[] arr = hfSelectedRowsRecived.Value.Split(',');
        for (int i = 0; i < arr.Length; i++) {
            if (Convert.ToInt32(arr[i]) >= 0) {
                GridViewRow current = gvMessagesRecived.Rows[Convert.ToInt32(arr[i])];

                ch_deleted_messages del_msg = new ch_deleted_messages(Convert.ToInt32(((Label)current.FindControl("lbl_msg_id_recived")).Text), Convert.ToInt32(Session["usr_id"]));
                ch_deleted_messagesSvc.Add(del_msg);
            }
        }
        hfSelectedRowsRecived.Value = "-1";

        //bind data
        GVBindRecived();

        //dye messages
        DyeUnreadMessages();

        //select checkboxes
        SelectCheckboxesRecived();
    }

    protected void gvMessagesRecived_RowDataBound(object sender, GridViewRowEventArgs e) {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if (e.Row.RowIndex % 2 == 0) {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvMessagesRecived, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
            }
            else {
                int temp = e.Row.Cells.Count;
                for (int i = 0; i < temp - 1; i++) {
                    e.Row.Cells.RemoveAt(1);
                }
                e.Row.Cells[0].ColumnSpan = temp;
                e.Row.Cells[0].Text = "";

                GridViewRow gvrPrev = gvMessagesRecived.Rows[e.Row.RowIndex - 1];

                int msg_id = Convert.ToInt32(((Label)gvrPrev.FindControl("lbl_msg_id_recived")).Text);
                DataRow dr_msg = ch_messagesSvc.GetMessage(msg_id);

                Label lbl_title = new Label();
                lbl_title.Text = "כותרת: " + dr_msg["כותרת"].ToString();
                lbl_title.CssClass = "lbl_title";

                Label lbl_content = new Label();
                lbl_content.Text = "תוכן: " + dr_msg["הודעה"].ToString();
                lbl_content.CssClass = "lbl_content";

                e.Row.Cells[0].Controls.Add(lbl_title);
                e.Row.Cells[0].Controls.Add(new LiteralControl("<br /><br />"));
                e.Row.Cells[0].Controls.Add(lbl_content);
            }
        }
    }
    protected void ckMarkRecived_CheckedChanged(object sender, EventArgs e) {

        CheckBox ck = (CheckBox)sender;
        GridViewRow current_gvr = (GridViewRow)ck.NamingContainer;
        if (ck.Checked) {//אם זה לא היה מסומן וזה מסומן עכשיו
            if (hfSelectedRowsRecived.Value == "-1") {
                hfSelectedRowsRecived.Value = current_gvr.RowIndex.ToString();
            }
            else {
                hfSelectedRowsRecived.Value += "," + current_gvr.RowIndex.ToString();
            }
        }
        else {
            string[] arr = hfSelectedRowsRecived.Value.Split(',');
            string newStr = "-1";

            if (Convert.ToInt32(arr[0]) >= 0) {
                for (int i = 0; i < arr.Length; i++) {
                    //if checkbox is checked
                    if (current_gvr.RowIndex != Convert.ToInt32(arr[i])) {
                        if (newStr == "-1") {
                            newStr = arr[i];
                        }
                        else {
                            newStr += "," + arr[i];
                        }
                    }
                }
            }
            hfSelectedRowsRecived.Value = newStr;
        }


        //bind data
        GVBindRecived();

        //dye messages
        DyeUnreadMessages();

        //select checkboxes
        SelectCheckboxesRecived();

        //הסתרת כל שורות המידע
        for (int i = 1; i < gvMessagesRecived.Rows.Count; i += 2) {
            gvMessagesRecived.Rows[i].Visible = false;
        }
    }
    protected void gvMessagesRecived_SelectedIndexChanged(object sender, EventArgs e) {
        if (gvMessagesRecived.Rows[gvMessagesRecived.SelectedRow.RowIndex+1].Cells.Count > 1) {//אם לא נלחץ שורת המידע נוסף
            //סמן הודעה כנקראה
            Label lbl_msg_id = (Label)gvMessagesRecived.Rows[gvMessagesRecived.SelectedRow.RowIndex].FindControl("lbl_msg_id_recived");
            ch_messagesSvc.ChangeMsgStatus(int.Parse(lbl_msg_id.Text));
        }
        //bind data
        GVBindRecived();

        //dye messages
        DyeUnreadMessages();

        //הסתרת כל שורות המידע
        for (int i = 1; i < gvMessagesRecived.Rows.Count; i += 2) {
            gvMessagesRecived.Rows[i].Visible = false;
        }


        if (gvMessagesRecived.Rows[gvMessagesRecived.SelectedRow.RowIndex].Cells.Count == 1) {//אם נלחץ שורת המידע נוסף
            if (gvMessagesRecived.Rows[gvMessagesRecived.SelectedRow.RowIndex].Visible == false) {
                gvMessagesRecived.Rows[gvMessagesRecived.SelectedRow.RowIndex].Visible = true;
            }
        }
        else {//אם נלחצה שורת הפרטים
            if (gvMessagesRecived.Rows[gvMessagesRecived.SelectedRow.RowIndex + 1].Visible == true) {
                gvMessagesRecived.Rows[gvMessagesRecived.SelectedRow.RowIndex + 1].Visible = false;
            }
            else {
                gvMessagesRecived.Rows[gvMessagesRecived.SelectedRow.RowIndex + 1].Visible = true;
            }
        }
        //select checkboxes
        SelectCheckboxesRecived();


    }
    
    protected void gvMessagesSent_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            //bind data
            GVBindSent();

            for (int i = 1; i < gvMessagesSent.Rows.Count; i += 2) {
                gvMessagesSent.Rows[i].Visible = false;
            }

            gvMessagesSent.Columns[1].Visible = false; //visible false the = msg_id
            gvMessagesSent.Columns[5].Visible = false; //visible false the = msg_checked
        }
    }
    public void GVBindSent() {
        //Bind data to GridView
        DataSet dsMessagesSent = ch_messagesSvc.GetMessagesSentGV(Convert.ToInt32(Session["usr_id"]));
        for (int i = 0; i < dsMessagesSent.Tables[0].Rows.Count; i += 2) {
            DataRow details = dsMessagesSent.Tables[0].NewRow();
            dsMessagesSent.Tables[0].Rows.InsertAt(details, i + 1);
        }

        GridViewSvc.GVBind(dsMessagesSent, gvMessagesSent);
    }
    public void SelectCheckboxesSent() {
        string[] arr = hfSelectedRowsSent.Value.Split(',');
        bool flag = false;
        if (Convert.ToInt32(arr[0]) >= 0) {
            for (int i = 0; i < arr.Length; i++) {

                flag = true;
                GridViewRow current = gvMessagesSent.Rows[Convert.ToInt32(arr[i])];

                ((CheckBox)current.FindControl("ckMark_sent")).Checked = true;
                current.BackColor = ColorTranslator.FromHtml("#d5db5d");
            }
        }
        if (flag) {
            btnMsgSentDelete.Visible = true;
        }
        else {
            btnMsgSentDelete.Visible = false;
        }
    }
    protected void gvMessagesSent_PageIndexChanging(object sender, GridViewPageEventArgs e) {
        gvMessagesSent.PageIndex = e.NewPageIndex;

        //bind data
        GVBindSent();
    }
    protected void gvMessagesSent_RowDataBound(object sender, GridViewRowEventArgs e) {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if (e.Row.RowIndex % 2 == 0) {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvMessagesSent, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
            }
            else {
                int temp = e.Row.Cells.Count;
                for (int i = 0; i < temp - 1; i++) {
                    e.Row.Cells.RemoveAt(1);
                }
                e.Row.Cells[0].ColumnSpan = temp;
                e.Row.Cells[0].Text = "";

                GridViewRow gvrPrev = gvMessagesSent.Rows[e.Row.RowIndex - 1];

                int msg_id = Convert.ToInt32(((Label)gvrPrev.FindControl("lbl_msg_id_sent")).Text);
                DataRow dr_msg = ch_messagesSvc.GetMessage(msg_id);

                Label lbl_title = new Label();
                lbl_title.Text = "כותרת: " + dr_msg["כותרת"].ToString();
                lbl_title.CssClass = "lbl_title";

                Label lbl_content = new Label();
                lbl_content.Text = "תוכן: " + dr_msg["הודעה"].ToString();
                lbl_content.CssClass = "lbl_content";

                e.Row.Cells[0].Controls.Add(lbl_title);
                e.Row.Cells[0].Controls.Add(new LiteralControl("<br /><br />"));
                e.Row.Cells[0].Controls.Add(lbl_content);
            }
            
        }
    }
    protected void gvMessagesSent_SelectedIndexChanged(object sender, EventArgs e) {
        //bind data
        GVBindSent();

        //הסתרת כל שורות המידע
        for (int i = 1; i < gvMessagesSent.Rows.Count; i += 2) {
            gvMessagesSent.Rows[i].Visible = false;
        }


        if (gvMessagesSent.Rows[gvMessagesSent.SelectedRow.RowIndex].Cells.Count == 1) {//אם נלחץ שורת המידע נוסף
            if (gvMessagesSent.Rows[gvMessagesSent.SelectedRow.RowIndex].Visible == false) {
                gvMessagesSent.Rows[gvMessagesSent.SelectedRow.RowIndex].Visible = true;
            }
        }
        else {//אם נלחצה שורת הפרטים
            if (gvMessagesSent.Rows[gvMessagesSent.SelectedRow.RowIndex + 1].Visible == true) {
                gvMessagesSent.Rows[gvMessagesSent.SelectedRow.RowIndex + 1].Visible = false;
            }
            else {
                gvMessagesSent.Rows[gvMessagesSent.SelectedRow.RowIndex + 1].Visible = true;
            }
        }
        //select checkboxes
        SelectCheckboxesSent();
    }
    protected void ckMarkSent_CheckedChanged(object sender, EventArgs e) {

        CheckBox ck = (CheckBox)sender;
        GridViewRow current_gvr = (GridViewRow)ck.NamingContainer;
        if (ck.Checked) {//אם זה לא היה מסומן וזה מסומן עכשיו
            if (hfSelectedRowsSent.Value == "-1") {
                hfSelectedRowsSent.Value = current_gvr.RowIndex.ToString();
            }
            else {
                hfSelectedRowsSent.Value += "," + current_gvr.RowIndex.ToString();
            }
        }
        else {
            string[] arr = hfSelectedRowsSent.Value.Split(',');
            string newStr = "-1";

            if (Convert.ToInt32(arr[0]) >= 0) {
                for (int i = 0; i < arr.Length; i++) {
                    //if checkbox is checked
                    if (current_gvr.RowIndex != Convert.ToInt32(arr[i])) {
                        if (newStr == "-1") {
                            newStr = arr[i];
                        }
                        else {
                            newStr += "," + arr[i];
                        }
                    }
                }
            }
            hfSelectedRowsSent.Value = newStr;
        }


        //bind data
        GVBindSent();

        //select checkboxes
        SelectCheckboxesSent();

        //הסתרת כל שורות המידע
        for (int i = 1; i < gvMessagesSent.Rows.Count; i += 2) {
            gvMessagesSent.Rows[i].Visible = false;
        }
    }
    protected void btnMsgSentDelete_Click(object sender, ImageClickEventArgs e) {
        string[] arr = hfSelectedRowsSent.Value.Split(',');
        for (int i = 0; i < arr.Length; i++) {
            if (Convert.ToInt32(arr[i]) >= 0) {
                GridViewRow current = gvMessagesSent.Rows[Convert.ToInt32(arr[i])];

                ch_deleted_messages del_msg = new ch_deleted_messages(Convert.ToInt32(((Label)current.FindControl("lbl_msg_id_sent")).Text), Convert.ToInt32(Session["usr_id"]));
                ch_deleted_messagesSvc.Add(del_msg);
            }
        }
        hfSelectedRowsSent.Value = "-1";

        //bind data
        GVBindSent();

        //select checkboxes
        SelectCheckboxesSent();
    }
    protected void btnInsert_Click(object sender, EventArgs e) {
        Response.Redirect("WriteMsg.aspx");
    }
}