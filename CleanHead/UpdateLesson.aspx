<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="UpdateLesson.aspx.cs" Inherits="AddLesson" %>

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
        

        .wrapper{
            border: 1px #dddddd solid;
            background: #eeeeee;
            padding: 15px;
            width: 600px;
        }
        .pnlName{
            background: #646361;
            color: #ffffff;
            padding: 2px 7px;
            margin: 5px;
            display: inline-block;
        }

        input[type="text"], input[type="password"]{
            border: 0;
            border-radius: 5px;
            padding: 5px;
            border: 1px #dddddd solid;
        }
        input[type="submit"]{
            background: #d96557;
            padding: 3px 10px;
            font-family: "Alef Hebrew", “Helvetica Neue”, Helvetica,Tahoma,Arial;
            border: none;
            font-size: 17px;
            border-radius: 3px;
            color: #fff;
            cursor: pointer;
            font-weight: bold;
            transition: all 0.3s;
        }
        input[type="submit"]:hover{
            background: #EA8376;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <a href="ControlPanel.aspx">פאנל ניהול</a> » <a href="LessonsData.aspx">שיעורים</a> » עדכון שיעור
    <br />
    <h1>עדכון שיעור</h1>
    <asp:DropDownList ID="ddlProf" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProf_SelectedIndexChanged" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlProf" InitialValue="-בחר מקצוע-" runat="server" ErrorMessage="לא בחרת מקצוע"></asp:RequiredFieldValidator>
    <br />
    <asp:UpdatePanel UpdateMode="Conditional" ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:DropDownList ID="ddlLayers" runat="server" OnLoad="ddlLayers_Load" AutoPostBack="True" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlLayers" InitialValue="-בחר שכבה-" runat="server" ErrorMessage="לא בחרת שכבה"></asp:RequiredFieldValidator>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlLayers" EventName="Load" />
        </Triggers>
    </asp:UpdatePanel>
    

    <asp:TextBox ID="txtLes_name" placeholder="שם השיעור" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtLes_name" runat="server" ErrorMessage="לא כתבת שם שיעור"></asp:RequiredFieldValidator>
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
    <asp:Button ID="btnAddLesson" runat="server" Text="עדכן שיעור" OnClick="btnUpdateLesson_Click" />
</asp:Content>

