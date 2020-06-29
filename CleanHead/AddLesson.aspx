<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="AddLesson.aspx.cs" Inherits="AddLesson" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        /*------------Side-Bar------------*/
        .control_panel{
            color:#fff !important;
            background: #64B5F6 !important;
        }
        

        .wrapper{
            border: 1px #dddddd solid;
            background: #eeeeee;
            padding: 15px;
            /*width: 600px;*/
        }
        .pnlName{
            background: #646361;
            color: #ffffff;
            padding: 2px 7px;
            margin: 5px;
            display: inline-block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">הוספת שיעור</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server"><a href="Default.aspx">ראשי</a> » <a href="ControlPanel.aspx">פאנל ניהול</a> » <a href="LessonsData.aspx">שיעורים</a> » הוספת שיעור</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card-panel" style="width: 500px;">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="form-group">
            <asp:DropDownList ID="ddlProf" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProf_SelectedIndexChanged" required="required" />
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlProf" CssClass="err" Display="Dynamic" InitialValue="-בחר מקצוע-" runat="server" ErrorMessage="לא בחרת מקצוע<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
        </div>
        <br />
        <asp:UpdatePanel UpdateMode="Conditional" ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="form-group">
                    <asp:DropDownList ID="ddlLayers" runat="server" OnLoad="ddlLayers_Load" AutoPostBack="True" required="required" />
                    <span class="form-highlight"></span>
                    <span class="form-bar"></span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlLayers" CssClass="err" Display="Dynamic" InitialValue="-בחר שכבה-" runat="server" ErrorMessage="לא בחרת שכבה<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlLayers" EventName="Load" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="form-group"> 
            <asp:TextBox CssClass="material" ID="txtLes_name" runat="server" required="required"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label1" AssociatedControlID="txtLes_name" CssClass="form-label" runat="server" Text="שם השיעור"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtLes_name" CssClass="err" Display="Dynamic" runat="server" ErrorMessage="לא כתבת שם שיעור<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
        </div>
        <br />
        <asp:UpdatePanel UpdateMode="Conditional" ID="update_pnlTeachers" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlTeachers" runat="server" CssClass="wrapper">
                    <b> המורים המלמדים: </b><br />
                    <asp:Label ID="lblEmpty_tch" runat="server" Text="לא נבחרו מורים..."></asp:Label>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ckliTeachers" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <br />

        <asp:UpdatePanel UpdateMode="Conditional" ID="update_ckliTeachers" runat="server">
            <ContentTemplate>
                <asp:CheckBoxList ID="ckliTeachers" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ckliTeachers_SelectedIndexChanged"></asp:CheckBoxList>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlProf" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>

        <br />
        <hr />
        <br />
        <asp:UpdatePanel UpdateMode="Conditional" ID="update_pnlStudents" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlStudents" runat="server" CssClass="wrapper">
                    <b> התלמידים הלומדים: </b><br />
                    <%--<asp:Label ID="lblEmpty_stu" runat="server" Text="לא נבחרו תלמידים..."></asp:Label>--%>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ckliStudentsByClass" />
            </Triggers>
        </asp:UpdatePanel><br />
        הוספה מכיתה
        <asp:UpdatePanel ID="update_ddlClasses" runat="server">
            <ContentTemplate>
                <asp:DropDownList ID="ddlClasses" runat="server" OnSelectedIndexChanged="ddlClasses_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><br /><%--רק כיתות--%>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel UpdateMode="Conditional" ID="update_ckliStudentsByClass" runat="server">
            <ContentTemplate>
                <asp:CheckBoxList ID="ckliStudentsByClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ckliStudentsByClass_SelectedIndexChanged"></asp:CheckBoxList>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlClasses" />
            </Triggers>
        </asp:UpdatePanel>
        <br />
        <asp:Button ID="btnAddLesson" runat="server" Text="הוסף שיעור" OnClick="btnAddLesson_Click" />
        <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
    </div>
</asp:Content>

