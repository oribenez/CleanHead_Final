<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="UpdateHomework.aspx.cs" Inherits="UpdateHomework" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        #homework {
            background: url(Images/control_panel_selected.png) no-repeat;
        }
        .homework:hover > #homework {
            background: url(Images/control_panel_selected.png) no-repeat;
        }
        .homework{
            color:#fff !important;
            background: #64B5F6 !important;
        }


        /*input[type="text"], input[type="password"]{
            border-radius: 5px;
            padding: 5px;
            border: 1px #dddddd solid;
        }
        input[type="submit"] {
            background: #d96557;
            padding: 3px 10px;
            font-family: "Alef Hebrew", “Helvetica Neue”, Helvetica,Tahoma,Arial;
            border: none;
            font-size: 17px;
            border-radius: 3px;
            color: #fff;
            cursor: pointer;
            font-weight: bold;
            -moz-transition: all 0.3s;
            -o-transition: all 0.3s;
            -webkit-transition: all 0.3s;
            transition: all 0.3s;
        }
        input[type="submit"]:hover{
            background: #EA8376;
        }*/
    </style>
    <script>
        function Relationship() {
            alert("לא ניתן ל מחוק שדה זה משום שהוא קשור בקשרי גומלין עם שדות אחרים.\nכדי למחוק שדה זה, יש למחוק את השדות הקשורים לשדה זה");
        }
        function ConfirmDelete() {
            if ("<%=Session["gender"].ToString() %>" == "זכר") {
                return confirm("האם אתה בטוח שאתה רוצה למחוק את השיעורי בית?");
            }
            else {
                return confirm("האם את בטוחה שאת רוצה למחוק את השיעורי בית?");
            }
        }
        function ConfirmUpdate() {
            if ("<%=Session["gender"].ToString() %>" == "זכר") {
                return confirm("האם אתה בטוח שאתה רוצה לעדכן את השיעורי בית?");
            }
            else {
                return confirm("האם את בטוחה שאת רוצה לעדכן את השיעורי בית?");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">עדכון שיעורי בית</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server"><a href="Default.aspx">ראשי</a> » <a href="Homework.aspx">שיעורי בית</a> » עדכון שיעורי בית</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card-panel" style="width: 500px;">
        <asp:ImageButton ID="imgbtnDelete" ImageUrl="~/Images/ic_delete_24px.png" ToolTip="מחיקת המבחן" runat="server" OnClientClick="return ConfirmDelete()" OnClick="imgbtnDelete_Click" />
        <br />
        <br />
        <%if (Convert.ToInt32(Session["lvl_id"]) >= 4) { 
          %>
            בית ספר
            <asp:DropDownList ID="ddlSchools" runat="server" OnLoad="ddlSchools_Load" OnSelectedIndexChanged="ddlSchools_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RequiredFieldValidator4" ControlToValidate="ddlLessons" InitialValue="-בחר בית ספר-" runat="server" ErrorMessage="בחר בית ספר<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" required="required"></asp:RequiredFieldValidator>
        <%} %>
          <% if (Convert.ToInt32(Session["lvl_id"]) >= 3) { 
          %>
            שכבה
            <asp:DropDownList ID="ddlLayers" runat="server" OnLoad="ddlLayers_Load" 
            OnSelectedIndexChanged="ddlLayers_SelectedIndexChanged"
            AutoPostBack="True"></asp:DropDownList> 
                <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RequiredFieldValidator6" ControlToValidate="ddlLayers" InitialValue="-בחר שכבה-" runat="server" ErrorMessage="בחר שכבה<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" required="required"></asp:RequiredFieldValidator>
                
            מקצוע
                <asp:DropDownList ID="ddlProfessions" runat="server" OnLoad="ddlProfessions_Load"
            OnSelectedIndexChanged="ddlProfessions_SelectedIndexChanged"
            AutoPostBack="True"></asp:DropDownList>

            <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RequiredFieldValidator7" ControlToValidate="ddlProfessions" InitialValue="-בחר מקצוע-" runat="server" ErrorMessage="בחר מקצוע<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" required="required"></asp:RequiredFieldValidator>
            
            מורה
            <asp:DropDownList ID="ddlTeachers" runat="server" OnLoad="ddlTeachers_Load" OnSelectedIndexChanged="ddlTeachers_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RequiredFieldValidator5" ControlToValidate="ddlLessons" InitialValue="-בחר מורה-" runat="server" ErrorMessage="בחר מורה<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" required="required"></asp:RequiredFieldValidator>
        <%} %>
        שיעור
        <asp:DropDownList ID="ddlLessons" runat="server" OnLoad="ddlLessons_Load" OnSelectedIndexChanged="ddlLessons_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
        <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RequiredFieldValidator3" ControlToValidate="ddlLessons" InitialValue="-בחר שיעור-" runat="server" ErrorMessage="בחר שיעור<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" required="required"></asp:RequiredFieldValidator>

        <div class="form-group">
            <asp:TextBox required="required" CssClass="material" ID="txtHw" TextMode="MultiLine" Height="200" runat="server"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label1" AssociatedControlID="txtHw" CssClass="form-label" runat="server" Text="תיאור העבודה"></asp:Label>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RequiredFieldValidator1" ControlToValidate="txtHw" runat="server" ErrorMessage="שדה ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
        </div>

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>

        <div class="form-group">
            <asp:TextBox required="required" CssClass="material" ID="txtDeadlineDate" runat="server"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label2" AssociatedControlID="txtDeadlineDate" CssClass="form-label" runat="server" Text="תאריך ההגשה"></asp:Label>
            <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDeadlineDate" Format="dd/MM/yyyy" runat="server" />
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RequiredFieldValidator2" ControlToValidate="txtDeadlineDate" runat="server" ErrorMessage="שדה ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" required="required"></asp:RequiredFieldValidator>
            <asp:CustomValidator Display="Dynamic" CssClass="err" ID="ValidateDate" runat="server" ControlToValidate="txtDeadlineDate" OnServerValidate="ValidateDate_ServerValidate" ErrorMessage="תאריך לא תקין<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:CustomValidator>
        </div>

        שעת ההגשה
        <asp:DropDownList ID="ddlDeadlineHr" runat="server" OnLoad="ddlDeadlineHr_Load"></asp:DropDownList>
        <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RequiredFieldValidator8" ControlToValidate="ddlDeadlineHr" InitialValue="-בחר שעה-" runat="server" ErrorMessage="שדה ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="lblErr" ForeColor="Red" runat="server" Text=""></asp:Label>
        <br />
        <asp:Button ID="btnCancel" runat="server" Text="בטל" OnClientClick="location.href='Homework.aspx'" />&nbsp;
        <asp:Button ID="btnUpdateHw" runat="server" OnClick="btnUpdateHw_Click" OnClientClick="return ConfirmUpdate()" Text="עדכן שיעורי בית" />
    </div>
</asp:Content>

