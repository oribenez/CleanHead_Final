<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="AddHomework.aspx.cs" Inherits="AddHomework" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        #homework{
            background: url(Images/behavior2_selected.png) no-repeat;
        }
        .homework:hover > #homework{
            background: url(Images/behavior2_selected.png) no-repeat;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">הוספת שיעורי בית</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server"><a href="Default.aspx">ראשי</a> » <a href="Homework.aspx">שיעורי בית</a> » הוספת שיעורי בית</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card-panel" style="width:500px;">
        <table>
            <%if (Convert.ToInt32(Session["lvl_id"]) >= 4) { 
              %>
            <tr>
                <td>בית ספר</td>
                <td>
                    <asp:DropDownList ID="ddlSchools" runat="server" OnLoad="ddlSchools_Load" OnSelectedIndexChanged="ddlSchools_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlLessons" InitialValue="-בחר בית ספר-" runat="server" ErrorMessage="בחר בית ספר" required="required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <%} %>
              <% if (Convert.ToInt32(Session["lvl_id"]) >= 3) { 
              %>
            <tr>
                <td>שכבה</td>
                <td>
                    <asp:DropDownList ID="ddlLayers" runat="server" OnLoad="ddlLayers_Load" 
                OnSelectedIndexChanged="ddlLayers_SelectedIndexChanged"
                AutoPostBack="True"></asp:DropDownList> 
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlLayers" InitialValue="-בחר שכבה-" runat="server" ErrorMessage="בחר שכבה" required="required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>מקצוע</td>
                <td>
                    <asp:DropDownList ID="ddlProfessions" runat="server" OnLoad="ddlProfessions_Load"
                OnSelectedIndexChanged="ddlProfessions_SelectedIndexChanged"
                AutoPostBack="True"></asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="ddlProfessions" InitialValue="-בחר מקצוע-" runat="server" ErrorMessage="בחר מקצוע" required="required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>מורה</td>
                <td>
                    <asp:DropDownList ID="ddlTeachers" runat="server" OnLoad="ddlTeachers_Load" OnSelectedIndexChanged="ddlTeachers_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddlLessons" InitialValue="-בחר מורה-" runat="server" ErrorMessage="בחר מורה" required="required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <%} %>
            <tr>
                <td>שיעור</td>
                <td>
                    <asp:DropDownList ID="ddlLessons" runat="server" OnLoad="ddlLessons_Load" OnSelectedIndexChanged="ddlLessons_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlLessons" InitialValue="-בחר שיעור-" runat="server" ErrorMessage="בחר שיעור" required="required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>תיאור העבודה</td>
                <td>
                    <div class="form-group">
                        <asp:TextBox CssClass="material" required="required" ID="txtHw" TextMode="MultiLine" Width="100%" Height="200" runat="server"></asp:TextBox>
                        <span class="form-highlight"></span>
                        <span class="form-bar"></span>
                        <asp:Label ID="Label2" AssociatedControlID="txtHw" CssClass="form-label" runat="server" Text="לדוג': פתח את הספר בעמוד 53 ובצע משימות 1-6, 8"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="err" Display="Dynamic" ID="RequiredFieldValidator1" ControlToValidate="txtHw" runat="server" ErrorMessage="שדה ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
                    </div>
                </td>

            </tr>
            <tr>
                <td>תאריך ההגשה</td>
                <td>
                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    </asp:ToolkitScriptManager>
                    <div class="form-group">
                        <asp:TextBox CssClass="material" required="required" ID="txtDeadlineDate" runat="server"></asp:TextBox>
                        <span class="form-highlight"></span>
                        <span class="form-bar"></span>
                        <asp:Label ID="Label1" AssociatedControlID="txtDeadlineDate" CssClass="form-label" runat="server" Text="תאריך ההגשה"></asp:Label>
                        <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDeadlineDate" Format="dd/MM/yyyy" runat="server" />
                        <asp:RequiredFieldValidator CssClass="err" Display="Dynamic" ID="RequiredFieldValidator2" ControlToValidate="txtDeadlineDate" runat="server" ErrorMessage="שדה ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" required="required"></asp:RequiredFieldValidator>
                        <asp:CustomValidator CssClass="err" Display="Dynamic" ID="ValidateDate" runat="server" ControlToValidate="txtDeadlineDate" OnServerValidate="ValidateDate_ServerValidate" ErrorMessage="תאריך לא תקין<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:CustomValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td>שעת ההגשה</td>
                <td>
                    <asp:DropDownList ID="ddlDeadlineHr" runat="server" OnLoad="ddlDeadlineHr_Load"></asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="ddlDeadlineHr" InitialValue="-בחר שעה-" runat="server" ErrorMessage="שדה ריק"></asp:RequiredFieldValidator>
                </td>
            </tr>
        
        </table>
        <br />
        <asp:Label ID="lblErr" ForeColor="Red" runat="server" Text=""></asp:Label>
        <br />
        <asp:Button ID="btnAddHw" runat="server" OnClick="btnAddHw_Click" Text="הוסף שיעורי בית" />
    </div>
</asp:Content>

