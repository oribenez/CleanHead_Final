<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="BehaviorsTypes.aspx.cs" Inherits="BehaviorsTypes" %>

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

        .lblErr {
            color: #EF5350;
        }
    </style>
    <script>
        function Relationship() {
            alert("לא ניתן ל מחוק שדה זה משום שהוא קשור בקשרי גומלין עם שדות אחרים.\nכדי למחוק שדה זה, יש למחוק את השדות הקשורים לשדה זה");
        }
        function ConfirmDelete() {
            if ("<%=Session["gender"].ToString() %>" == "זכר") {
                return confirm("האם אתה בטוח שאתה רוצה למחוק את ההתנהגות?");
            }
            else {
                return confirm("האם את בטוחה שאת רוצה למחוק את ההתנהגות?");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitle" Runat="Server">
    סוגי ההתנהגויות
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageLinks" Runat="Server">
    <a href="Default.aspx">ראשי</a> » <a href="ControlPanel.aspx">פאנל ניהול</a> » סוגי ההתנהגויות
    <br />
    <br />
    <asp:Button ID="btnInsert" runat="server" Text="+" CssClass="btn-plus-header" CausesValidation="False" OnClick="btnInsert_Click" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card-panel" style="margin: 40px 0 0 0;">
        <asp:GridView ID="gvBehaviorsTypes" runat="server" CellPadding="4" GridLines="None" CssClass="table-striped" OnLoad="gvBehaviorsTypes_Load" AutoGenerateColumns="False" DataKeyNames="bhv_id" AllowPaging="True" OnPageIndexChanging="gvBehaviorsTypes_PageIndexChanging" PagerSettings-PageButtonCount="5" PageSize="15">   
            <Columns>
                <asp:TemplateField HeaderText="מזהה">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_edit_bhv_id" runat="server" Text='<%# Bind("bhv_id") %>' Width="35" ReadOnly="true"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_show_bhv_id" runat="server" Text='<%# Bind("bhv_id") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="סוג ההתנהגות">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_edit_bhv_name" runat="server" Text='<%# Bind("bhv_name") %>' MaxLength="35" Width="115"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_show_bhv_name" runat="server" Text='<%# Bind("bhv_name") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_insert_bhv_name" placeholder="סוג ההתנהגות" runat="server" MaxLength="35" Width="115"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="שווי ערך ההתנהגות">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_edit_bhv_value" runat="server" Text='<%# Bind("bhv_value") %>' MaxLength="35" Width="115"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_show_bhv_value" runat="server" Text='<%# Bind("bhv_value") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_insert_bhv_value" placeholder="שווי ערך ההתנהגות" runat="server" MaxLength="35" Width="115"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="עריכה" ShowHeader="False">
                    <EditItemTemplate>
                        <asp:ImageButton ID="btn_cancel_update_bhv" runat="server" CausesValidation="False" ImageUrl="~/Images/ic_cancel_24px.png" Text="בטל" OnClick="btn_cancel_update_bhv_Click" />&nbsp;
                        <asp:ImageButton ID="btn_update_bhv" runat="server" CausesValidation="True" ImageUrl="~/Images/ic_done_24px.png" Text="עדכן" OnClick="btn_update_bhv_Click" />
                        
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="btn_edit_bhv" runat="server" CausesValidation="False" ImageUrl="~/Images/ic_edit_24px.png" Text="ערוך" OnClick="btn_edit_bhv_Click" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ID="btn_insert_bhv" runat="server" CausesValidation="False" ImageUrl="~/Images/ic_done_24px.png" Text="הוסף" OnClick="btn_insert_bhv_Click" />
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="מחיקה" ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="btn_delete_bhv" runat="server" CausesValidation="False" ImageUrl="~/Images/ic_delete_24px.png" Text="מחק" OnClientClick="return ConfirmDelete();" OnClick="btn_delete_bhv_Click" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ID="btn_cancel_insert_bhv" runat="server" CausesValidation="False" ImageUrl="~/Images/ic_cancel_24px.png" Text="בטל" OnClick="btn_cancel_insert_bhv_Click" />
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblErr" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>

