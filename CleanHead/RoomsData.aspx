<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="RoomsData.aspx.cs" Inherits="RoomsData" %>

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
        .popupContainer{
            background: #8FC4D7;
            border: solid #808080 4px;
            z-index: 999;
            position: fixed;
            top: 50%;
            left: 50%;
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
        }*/

        .filtersWrap{
            padding: 10px;
        }
    </style>
    <script>
        function ConfirmDelete() {
            if ("<%=Session["gender"].ToString() %>" == "זכר") {
                return confirm("חדר זה קשור בקשרי גומלין לנתונים אחרים.\n אם תמחק חדר זה, נתונים אחרים התלויים בחדר זה עלולים גם להמחק!\nהאם אתה בטוח שאתה רוצה למחוק את החדר?");
            }
            else {
                return confirm("חדר זה קשור בקשרי גומלין לנתונים אחרים.\n אם תמחקי חדר זה, נתונים אחרים התלויים בחדר זה עלולים גם להמחק!\nהאם את בטוחה שאת רוצה למחוק את החדר?");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">חדרים פיזיים</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server"><a href="Default.aspx">ראשי</a> » <a href="ControlPanel.aspx">פאנל ניהול</a> » חדרים פיזיים
    <br /><br />
    <asp:Button ID="btnInsert" runat="server" Text="+" CssClass="btn-plus-header" CausesValidation="False" OnClick="btnInsert_Click" />
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
        <%} %>
        <asp:GridView ID="gvRooms" runat="server" CellPadding="4" CssClass="table-striped" GridLines="None" OnLoad="gvRooms_Load" AutoGenerateColumns="False" DataKeyNames="rm_id" AllowPaging="True" OnPageIndexChanging="gvRooms_PageIndexChanging" PageSize="15">          
            <Columns>
                <asp:TemplateField HeaderText="מזהה חדר">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_edit_rm_id" runat="server" Text='<%# Bind("rm_id") %>' Width="35" ReadOnly="true"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_show_rm_id" runat="server" Text='<%# Bind("rm_id") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="שם חדר">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_edit_rm_name" runat="server" Text='<%# Bind("rm_name") %>' Width="115"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_show_rm_name" runat="server" Text='<%# Bind("rm_name") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_insert_rm_name" placeholder="שם החדר" runat="server" Width="115"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="עריכה" ShowHeader="False">
                    <EditItemTemplate>
                        <asp:ImageButton ID="btn_cancel_update_rm" runat="server" CausesValidation="False" CommandName="CancelUpdate" ImageUrl="~/Images/ic_cancel_24px.png" Text="בטל" OnClick="btn_cancel_update_rm_Click" />&nbsp;
                        <asp:ImageButton ID="btn_update_rm" runat="server" CausesValidation="True" CommandName="Update" ImageUrl="~/Images/ic_done_24px.png" Text="עדכן" OnClick="btn_update_rm_Click" />
                        
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="btn_edit_rm" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Images/ic_edit_24px.png" Text="ערוך" OnClick="btn_edit_rm_Click" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ID="btn_insert_rm" runat="server" CausesValidation="False" CommandName="Insert" ImageUrl="~/Images/ic_done_24px.png" Text="הוסף" OnClick="btn_insert_rm_Click" />
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="מחיקה" ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="btn_delete_rm" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Images/ic_delete_24px.png" Text="מחק" OnClientClick="return ConfirmDelete();" OnClick="btn_delete_rm_Click" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ID="btn_cancel_insert_rm" runat="server" CausesValidation="False" CommandName="CancelInsert" ImageUrl="~/Images/ic_cancel_24px.png" Text="בטל" OnClick="btn_cancel_insert_rm_Click" />
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblErrGV" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>

