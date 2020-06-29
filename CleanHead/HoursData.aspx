<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="HoursData.aspx.cs" Inherits="HoursData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <style>
                /*------------Side-Bar------------*/
        #control_panel {
            background: url(Images/control_panel_selected.png) no-repeat;
        }
        .control_panel:hover > #control_panel {
            background: url(Images/control_panel_selected.png) no-repeat;
        }
        .control_panel{
            color:#fff !important;
            background: #64B5F6 !important;
        }



        h3{
            margin:0;
            padding:0;
        }
        input,select{
            padding:3px;
        }
        .popupBtnX{
            float:left;
        }
        .popupBack{
            display: block;
            z-index: 998;
            background: rgba(51, 51, 51, 0.6);
            position: fixed;
            width: 100%;
            height: 100%;
        }
            .popupContainer {
                background: #8FC4D7;
                border: solid #808080 4px;
                z-index: 999;
                position: fixed;
                top: 50%;
                left: 50%;
                -moz-transform: translate(-50%, -50%);
                -ms-transform: translate(-50%, -50%);
                -o-transform: translate(-50%, -50%);
                -webkit-transform: translate(-50%, -50%);
                transform: translate(-50%, -50%);
                padding: 35px;
            }

        /*table*/

        
        /*tbody tr:nth-child(2n+3){
            background: #eee;
        }
        table{
            border: 2px solid #ffffff;
            border-collapse:collapse
        }

        table th{
            border: 2px solid #ffffff;
            border-bottom-color: #d4dde4;
            background: rgba(212, 221, 228, .5);
            padding:2px 8px 4px;
        }
        td{
            padding: 1px 10px;
        }

        table tr:nth-child(2n+2){
            border: 2px solid #ffffff;
            font-size: 1em;
            box-shadow: inset 0 -1px 0 0 rgba(212, 221, 228, .5);
            background: rgba(212,221,228,0.15);
            padding: 6px 8px 8px;
            letter-spacing: -0.3px;
        }
        tbody tr:not(:first-child):hover{
            background: #b4ddec !important;
        }
        table td:nth-child(2){
            background: #8FC4D7;
            font-weight: bold;
            text-align: center;
        }
        .filtersWrap{
            padding: 10px;
            background: #b4ddec;
            border: 1px solid #8FC4D7;
        }*/
        .lblErrGV{
            color: #EF5350;
        }
    </style>
    <script>
        function ConfirmDelete() {
            if ("<%=Session["gender"].ToString() %>" == "זכר") {
                return confirm("שעה זו קשורה בקשרי גומלין לנתונים אחרים.\n אם תמחק את השעה, נתונים אחרים התלויים בשעה זה עלולים גם להמחק!\nהאם אתה בטוח שאתה רוצה למחוק שעה זו?");
            }
            else {
                return confirm("שעה זו קשורה בקשרי גומלין לנתונים אחרים.\n אם תמחקי  את השעה, נתונים אחרים התלויים בשעה זה עלולים גם להמחק!\nהאם את בטוחה שאת רוצה למחוק שעה זו?");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">שעות בית ספריות</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server"><a href="Default.aspx">ראשי</a> » <a href="ControlPanel.aspx">פאנל ניהול</a> » שעות בית ספריות
        <%
    if (Convert.ToInt32(Session["lvl_id"]) >= 3) {
            %><br /><br />
        <asp:Button ID="btnInsert" runat="server" Text="+" CssClass="btn-plus-header" CausesValidation="False" OnClick="btnInsert_Click" />
    <%} %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card-panel" style="margin: 40px 0 0 0;">
        <%if (Convert.ToInt32(Session["lvl_id"]) >= 4) { 
	    %>
        <div class="filtersWrap">
            <h4>סינון</h4>
            <asp:DropDownList ID="ddlSchools" runat="server" OnLoad="ddlSchools_Load" 
                OnSelectedIndexChanged="ddlSchools_SelectedIndexChanged"
                AutoPostBack="True" style="width: initial;"></asp:DropDownList>
        </div>
        <br />
        <%} %>
        <asp:GridView ID="gvHours" runat="server" CellPadding="4" CssClass="table-striped" GridLines="None" OnLoad="gvHours_Load" AutoGenerateColumns="False" DataKeyNames="hr_id" AllowPaging="True" OnPageIndexChanging="gvHours_PageIndexChanging" PageSize="20">           
            <Columns>
                <asp:TemplateField HeaderText="מזהה שעה">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_edit_hr_id" runat="server" Text='<%# Bind("hr_id") %>' Width="35" ReadOnly="true"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_show_hr_id" runat="server" Text='<%# Bind("hr_id") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="שם שעה">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_edit_hr_name" runat="server" Text='<%# Bind("hr_name") %>' Width="115"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_show_hr_name" runat="server" Text='<%# Bind("hr_name") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_insert_hr_name" placeholder="שם השעה" runat="server" Width="115"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="זמן התחלה">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_edit_hr_start_time" runat="server" Text='<%# string.Format("{0:hh:mm}", Eval("hr_start_time")) %>' Width="115"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_show_hr_start_time" runat="server" Text='<%# string.Format("{0:HH:mm}", Eval("hr_start_time")) %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_insert_hr_start_time" placeholder="זמן התחלה" runat="server" Width="115"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="זמן סיום">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_edit_hr_end_time" runat="server" Text='<%# string.Format("{0:HH:mm}", Eval("hr_end_time")) %>' Width="115"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_show_hr_end_time" runat="server" Text='<%# string.Format("{0:HH:mm}", Eval("hr_end_time")) %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_insert_hr_end_time" placeholder="זמן סיום" runat="server" Width="115"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="עריכה" ShowHeader="False">
                    <EditItemTemplate>
                        <asp:ImageButton ID="btn_cancel_update_hr" runat="server" CausesValidation="False" ImageUrl="~/Images/ic_cancel_24px.png" Text="בטל" OnClick="btn_cancel_update_hr_Click" />&nbsp;
                        <asp:ImageButton ID="btn_update_hr" runat="server" CausesValidation="True" ImageUrl="~/Images/ic_done_24px.png" Text="עדכן" OnClick="btn_update_hr_Click" />
                        
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="btn_edit_hr" runat="server" CausesValidation="False" ImageUrl="~/Images/ic_edit_24px.png" Text="ערוך" OnClick="btn_edit_hr_Click" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ID="btn_insert_hr" runat="server" CausesValidation="False" ImageUrl="~/Images/ic_done_24px.png" Text="הוסף" OnClick="btn_insert_hr_Click" />
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="מחיקה" ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="btn_delete_hr" runat="server" CausesValidation="False" ImageUrl="~/Images/ic_delete_24px.png" Text="מחק" OnClientClick="return ConfirmDelete();" OnClick="btn_delete_hr_Click" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ID="btn_cancel_insert_hr" runat="server" CausesValidation="False"  ImageUrl="~/Images/ic_cancel_24px.png" Text="בטל" OnClick="btn_cancel_insert_hr_Click" />
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblErrGV" CssClass="lblErrGV" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>

