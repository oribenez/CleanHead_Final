<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="CitiesData.aspx.cs" Inherits="CitiesData" %>

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
    </style>
    <script>
        function Relationship() {
            alert("לא ניתן ל מחוק שדה זה משום שהוא קשור בקשרי גומלין עם שדות אחרים.\nכדי למחוק שדה זה, יש למחוק את השדות הקשורים לשדה זה");
        }
        function ConfirmDelete() {
            if ("<%=Session["gender"].ToString() %>" == "זכר") {
                return confirm("האם אתה בטוח שאתה רוצה למחוק את העיר?");
            }
            else {
                return confirm("האם את בטוחה שאת רוצה למחוק את העיר?");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">ערים</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server"><a href="Default.aspx">ראשי</a> » <a href="ControlPanel.aspx">פאנל ניהול</a> » ערים
    <br />
    <br />
    <asp:Button ID="btnInsert" runat="server" Text="+" CssClass="btn-plus-header" CausesValidation="False" OnClick="btnInsert_Click" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card-panel" style="margin: 40px 0 0 0;">
        <%-- SearchBox --%>
        <div class="float-right">
            <div class="form-group search-box float-right">
                <asp:TextBox CssClass="material" ID="txtSearch" runat="server"></asp:TextBox>
                <span class="form-highlight"></span>
                <span class="form-bar"></span>
                <asp:Label ID="Label1" AssociatedControlID="txtSearch" CssClass="form-label" runat="server" Text="חיפוש"></asp:Label>
            </div>
            <asp:Button ID="btnSearch" CssClass="float-right" runat="server" Text="חפש" CausesValidation="false" style="margin: 45px 10px 0 0;" OnClick="btnSearch_Click" />
        </div>
        <asp:GridView ID="gvCities" runat="server" CellPadding="4" GridLines="None" CssClass="table-striped" OnLoad="gvCities_Load" AutoGenerateColumns="False" DataKeyNames="cty_id" AllowPaging="True" OnPageIndexChanging="gvCities_PageIndexChanging" PagerSettings-PageButtonCount="5" PageSize="15">   
            <Columns>
                <asp:TemplateField HeaderText="מזהה עיר">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_edit_cty_id" runat="server" Text='<%# Bind("cty_id") %>' Width="35" ReadOnly="true"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_show_cty_id" runat="server" Text='<%# Bind("cty_id") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="שם עיר">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_edit_cty_name" runat="server" Text='<%# Bind("cty_name") %>' MaxLength="35" Width="115"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_show_cty_name" runat="server" Text='<%# Bind("cty_name") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_insert_cty_name" placeholder="שם העיר" runat="server" MaxLength="35" Width="115"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="עריכה" ShowHeader="False">
                    <EditItemTemplate>
                        <asp:ImageButton ID="btn_cancel_update_cty" runat="server" CausesValidation="False" ImageUrl="~/Images/ic_cancel_24px.png" Text="בטל" OnClick="btn_cancel_update_cty_Click" />&nbsp;
                        <asp:ImageButton ID="btn_update_cty" runat="server" CausesValidation="True" ImageUrl="~/Images/ic_done_24px.png" Text="עדכן" OnClick="btn_update_cty_Click" />
                        
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="btn_edit_cty" runat="server" CausesValidation="False" ImageUrl="~/Images/ic_edit_24px.png" Text="ערוך" OnClick="btn_edit_cty_Click" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ID="btn_insert_cty" runat="server" CausesValidation="False" ImageUrl="~/Images/ic_done_24px.png" Text="הוסף" OnClick="btn_insert_cty_Click" />
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="מחיקה" ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="btn_delete_cty" runat="server" CausesValidation="False" ImageUrl="~/Images/ic_delete_24px.png" Text="מחק" OnClientClick="return ConfirmDelete();" OnClick="btn_delete_cty_Click" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ID="btn_cancel_insert_cty" runat="server" CausesValidation="False" ImageUrl="~/Images/ic_cancel_24px.png" Text="בטל" OnClick="btn_cancel_insert_cty_Click" />
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblErrGV" runat="server" ForeColor="Red" Text=""></asp:Label>
    </div>
</asp:Content>

