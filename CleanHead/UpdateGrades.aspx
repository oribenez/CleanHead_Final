<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="UpdateGrades.aspx.cs" Inherits="UpdateGrades" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        #grades {
            background: url(Images/control_panel_selected.png) no-repeat;
        }
        .grades:hover > #grades {
            background: url(Images/control_panel_selected.png) no-repeat;
        }
        .grades{
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
                return confirm("האם אתה בטוח שאתה רוצה למחוק מבחן זה?");
            }
            else {
                return confirm("האם את בטוחה שאת רוצה למחוק מבחן זה?");
            }
        }
        function ConfirmUpdate() {
            if ("<%=Session["gender"].ToString() %>" == "זכר") {
                return confirm("האם אתה בטוח שאתה רוצה לעדכן מבחן זה?");
            }
            else {
                return confirm("האם את בטוחה שאת רוצה לעדכן מבחן זה?");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">עריכת ציונים</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server"><a href="Default.aspx">ראשי</a> » <a href="Grades.aspx">דוח ציונים</a> » עריכת ציונים</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card-panel" style="width: 500px;">
        <asp:Panel ID="pnlPermission" runat="server">
        <asp:ImageButton ID="imgbtnDelete" ImageUrl="~/Images/ic_delete_24px.png" ToolTip="מחיקת המבחן" runat="server" OnClientClick="return ConfirmDelete()" OnClick="imgbtnDelete_Click" />

        <div class="form-group">
            <asp:TextBox CssClass="material" required="required" ID="txtGradeName" runat="server"></asp:TextBox>           
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label2" AssociatedControlID="txtGradeName" CssClass="form-label" runat="server" Text="ציון ב"></asp:Label>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RequiredFieldValidator1" ControlToValidate="txtGradeName" runat="server" ErrorMessage="שדה ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator Display="Dynamic" CssClass="err" ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtGradeName" ValidationExpression="[א-תA-Za-z -_]{2,35}" ErrorMessage="אותיות ומספרים בלבד בין 2 ל-35 תווים<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RegularExpressionValidator>
        </div>

        <div class="form-group">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            <asp:TextBox CssClass="material" required="required" ID="txtGradeDate" runat="server"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label1" AssociatedControlID="txtGradeDate" CssClass="form-label" runat="server" Text="תאריך המבחן"></asp:Label>
            <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtGradeDate" Format="dd/MM/yyyy" runat="server" />
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RequiredFieldValidator2" ControlToValidate="txtGradeDate" runat="server" ErrorMessage="שדה ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" required="required"></asp:RequiredFieldValidator>
            <asp:CustomValidator Display="Dynamic" CssClass="err" ID="ValidateDate" runat="server" ControlToValidate="txtGradeDate" OnServerValidate="ValidateDate_ServerValidate" ErrorMessage="תאריך לא תקין<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:CustomValidator>
        </div>

        <h5>
            שיעור:
            <asp:Label ID="lblLesson" runat="server" OnLoad="lblLesson_Load"></asp:Label>
        </h5>

            <b>הכנס ציונים:</b>
        <asp:GridView ID="gvStudents" CssClass="gvStudents table-striped" runat="server" BorderWidth="0" AutoGenerateColumns="False" OnLoad="gvStudents_Load">
            <Columns>
                <asp:TemplateField HeaderText="מזהה תלמיד">
                    <ItemTemplate>
                        <asp:Label ID="lbl_usr_id" runat="server" Text='<%# Bind("usr_id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="שם תלמיד">
                    <ItemTemplate>
                        <asp:Label ID="lbl_usr_fullname" runat="server" Text='<%# Bind("usr_fullname") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="ציון">
                    <ItemTemplate>
                        <div class="form-group">
                            <asp:TextBox CssClass="material" required="required" ID="txtGrade" MaxLength="3" runat="server"></asp:TextBox>
                            <span class="form-highlight"></span>
                            <span class="form-bar"></span>
                            <asp:Label ID="Label1" AssociatedControlID="txtGrade" CssClass="form-label" runat="server" Text="ציון"></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:Label ID="lblErr" ForeColor="Red" runat="server" Text=""></asp:Label>
        <br />
        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="בטל" />
        <asp:Button ID="btnAddGrades" runat="server" OnClientClick="return ConfirmUpdate()" OnClick="btnUpdateGrades_Click" Text="עדכן ציונים" />
    
        </asp:Panel>
    </div>
</asp:Content>
