<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Messages.aspx.cs" Inherits="Messages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        #arrow{
            display:none;
        }

        h1, h3, h2, h4 {
            margin: 0;
            padding: 0;
        }

        input, select {
            padding: 3px;
            margin: 0 0 8px 0;
        }

        .popupBtnX {
            float: left;
        }

        .popupBack {
            display: block;
            z-index: 998;
            background: rgba(255, 255, 255, 0.21);
            position: fixed;
            width: 100%;
            height: 100%;
        }

        .popupContainer {
            width: 460px;
            height: 380px;
            background: #eeeeee;
            border: #EA6A46 2px dashed;
            z-index: 999;
            position: fixed;
            margin: auto auto;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            padding: 40px;
            overflow: overlay;
        }

        /*tbody tr:nth-child(even):hover {
            background: #b4ddec !important;
            cursor: pointer;
        }

        table {
            border: 2px solid #ffffff;
            border-collapse: collapse;
        }

        table th {
            border: 2px solid #ffffff;
            border-bottom-color: #d4dde4;
            background: rgba(212, 221, 228, .5);
            padding: 2px 8px 4px;
        }

        td {
            padding: 6px 8px 8px;
            border: 2px solid #ffffff;
            font-size: .9em;
            box-shadow: inset 0 -1px 0 0 rgba(212, 221, 228, .5);
        }

        table tr:nth-child(2n+2) {
            border: 2px solid #ffffff;
            font-size: 1em;
            box-shadow: inset 0 -1px 0 0 rgba(212, 221, 228, .5);
            background: rgba(212,221,228,0.15);
            padding: 6px 8px 8px;
            letter-spacing: -0.3px;
        }*/

        td > input[type="button"] {
            cursor: pointer;
        }

        .lbl_title{
            font-weight: bold;
            font-size: 19px;
        }
        .lbl_content{
            font-size: 16px;
        }
        .selectedRowStyle{
            background: #d5db5d;
        }
        .ui-tabs .ui-tabs-nav {
          margin: 0;
          padding: .2em .2em 0;
          height: 45px;
        }
    </style>

    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
    <link href="Styles/jquery-ui.css" rel="stylesheet" />
    <link href="Styles/jquery-ui.structure.css" rel="stylesheet" />
    <link href="Styles/jquery-ui.theme.css" rel="stylesheet" />
    
    <script>
        function ConfirmDelete() {
            if ("<%=Session["gender"].ToString() %>" == "זכר") {
                return confirm("האם אתה בטוח שאתה רוצה למחוק את ההודעה?");
            }
            else {
                return confirm("האם את בטוחה שאת רוצה למחוק את ההודעה?");
            }
        }

        $(function () {
            $("#messagesTabs").tabs({ 
                activate: function() {
                    var selectedTab = $('#messagesTabs').tabs('option', 'active');
                    $("#<%= hfSelectedTab.ClientID %>").val(selectedTab);
                },
                active: <%= hfSelectedTab.Value %>
                });
        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">הודעות</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server"><a href="Default.aspx">ראשי</a> » הודעות
    <br />
    <br />
    <asp:Button ID="btnInsert" runat="server" Text="+" CssClass="btn-plus-header" CausesValidation="False" OnClick="btnInsert_Click" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card-panel" style="margin: 40px 0 0 0;">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <asp:HiddenField ID="hfSelectedTab" runat="server" Value="0" />
    
        <div id="messagesTabs">
            <ul>
                <li>
                    <a href="#messagesRecived">
                        <img src="Images/messagesRecived.png" style="display:block;float:right;margin: 0 0 0 5px;" height="22" />
                        <span style="display:block;float:right;font-size: 17px;">הודעות נכנסות</span>
                    </a>
                </li>
                <li>
                    <a href="#messagesSent">
                        <img src="Images/messagesSent.png" style="display:block;float:right;margin: 0 0 0 5px;" height="22" />
                        <span style="display:block;float:right;font-size: 17px;">הודעות יוצאות</span>
                    </a>
                </li>
            </ul>
            <div id="messagesRecived">
                <asp:HiddenField ID="hfSelectedRowsRecived" Value="-1" runat="server" />
                <asp:ImageButton ID="btnMsgRecivedDelete" runat="server" Visible="false" ImageUrl="~/Images/ic_delete_24px.png" OnClientClick="javascript:return ConfirmDelete()" OnClick="btnMsgRecivedDelete_Click" CausesValidation="False" />
                <asp:GridView ID="gvMessagesRecived" Width="1100" BackColor="White" runat="server" OnLoad="gvMessagesRecived_Load" RowStyle-CssClass="rowHover" CssClass="gvMessagesRecived table-striped" AllowPaging="True" OnPageIndexChanging="gvMessagesRecived_PageIndexChanging" OnRowDataBound="gvMessagesRecived_RowDataBound" AutoGenerateColumns="False" OnSelectedIndexChanged="gvMessagesRecived_SelectedIndexChanged" PageSize="14">
                    <Columns>

                        <asp:TemplateField HeaderText="סמן">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="ckMark_recived" OnCheckedChanged="ckMarkRecived_CheckedChanged" AutoPostBack="True"></asp:CheckBox>
                                <asp:Label ID="lblCkMark" runat="server" Text="" AssociatedControlID="ckMark_recived"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="קוד הודעה">
                            <ItemTemplate>
                                <asp:Label ID="lbl_msg_id_recived" runat="server" Text='<%# Bind("[קוד הודעה]") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="שולח">
                            <ItemTemplate>
                                <asp:Label ID="lbl_msg_sender_id_recived" runat="server" Text='<%# Bind("שולח") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="כותרת">
                            <ItemTemplate>
                                <asp:Label ID="lbl_msg_title_recived" runat="server" Text='<%# Bind("כותרת") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="תאריך">
                            <ItemTemplate>
                                <asp:Label ID="lbl_msg_date_recived" runat="server" Text='<%# Bind("תאריך", "{0:dd/MM/yyyy HH:mm}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="נקראה">
                            <ItemTemplate>
                                <asp:CheckBox ID="ck_msg_checked_recived" Enabled="false" Checked='<%#Eval("נקראה").ToString()=="True"?true:false %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <RowStyle CssClass="rowHover"></RowStyle>
                </asp:GridView>
                <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
            </div>
            <div id="messagesSent">
                <asp:HiddenField ID="hfSelectedRowsSent" Value="-1" runat="server" />
                    <asp:ImageButton ID="btnMsgSentDelete" runat="server" Visible="false" ImageUrl="~/Images/ic_delete_24px.png" OnClientClick="javascript:return ConfirmDelete()" OnClick="btnMsgSentDelete_Click" CausesValidation="False" />

                <asp:GridView ID="gvMessagesSent" Width="1100" BackColor="White" runat="server" OnLoad="gvMessagesSent_Load" RowStyle-CssClass="rowHover" CssClass="gvMessagesSent table-striped-accordion" AllowPaging="True" OnPageIndexChanging="gvMessagesSent_PageIndexChanging" OnRowDataBound="gvMessagesSent_RowDataBound" AutoGenerateColumns="False" OnSelectedIndexChanged="gvMessagesSent_SelectedIndexChanged" PageSize="14">
                    <Columns>
                        <asp:TemplateField HeaderText="סמן">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="ckMark_sent" OnCheckedChanged="ckMarkSent_CheckedChanged" AutoPostBack="True"></asp:CheckBox>
                                <asp:Label ID="lblCkMark" runat="server" Text="" AssociatedControlID="ckMark_sent"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="קוד הודעה">
                            <ItemTemplate>
                                <asp:Label ID="lbl_msg_id_sent" runat="server" Text='<%# Bind("[קוד הודעה]") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="מקבל">
                            <ItemTemplate>
                                <asp:Label ID="lbl_msg_reciver_id_sent" runat="server" Text='<%# Bind("מקבל") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="כותרת">
                            <ItemTemplate>
                                <asp:Label ID="lbl_msg_title_sent" runat="server" Text='<%# Bind("כותרת") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="תאריך">
                            <ItemTemplate>
                                <asp:Label ID="lbl_msg_date_sent" runat="server" Text='<%# Bind("תאריך", "{0:dd/MM/yyyy HH:mm}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="נקראה">
                            <ItemTemplate>
                                <asp:CheckBox ID="ck_msg_checked_sent" Enabled="false" Checked='<%#Eval("נקראה").ToString()=="True"?true:false %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>

