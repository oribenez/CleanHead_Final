<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="AddBehaviors.aspx.cs" Inherits="AddBehaviors" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .behavior{
            color:#fff !important;
            background: #64B5F6 !important;
        }

        .lblErrGV{
            color: #EF5350;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">הוספת דוח התנהגות בשיעור</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server"><a href="Default.aspx">ראשי</a> » <a href="Behaviors.aspx">דוח התנהגות</a> » הוספת דוח התנהגות בשיעור</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card-panel">
        <%if (Convert.ToInt32(Session["lvl_id"]) >= 4) { %>
            <asp:DropDownList CssClass="material" ID="ddlSchools" OnLoad="ddlSchools_Load" OnSelectedIndexChanged="ddlSchools_SelectedIndexChanged" runat="server" AutoPostBack="True" style="width:initial;"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlLessons" InitialValue="-בחר בית ספר-" runat="server" ErrorMessage="בחר בית ספר"></asp:RequiredFieldValidator>
        <%} %>
        <% if (Convert.ToInt32(Session["lvl_id"]) >= 3) { %>
            <asp:DropDownList ID="ddlLayers" runat="server" OnLoad="ddlLayers_Load" OnSelectedIndexChanged="ddlLayers_SelectedIndexChanged" AutoPostBack="True" style="width:initial;"></asp:DropDownList> 
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlLayers" InitialValue="-בחר שכבה-" runat="server" ErrorMessage="בחר שכבה"></asp:RequiredFieldValidator>
                
            <asp:DropDownList ID="ddlProfessions" runat="server" OnLoad="ddlProfessions_Load" OnSelectedIndexChanged="ddlProfessions_SelectedIndexChanged" AutoPostBack="True" style="width:initial;"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="ddlProfessions" InitialValue="-בחר מקצוע-" runat="server" ErrorMessage="בחר מקצוע"></asp:RequiredFieldValidator>
                
            <asp:DropDownList ID="ddlTeachers" runat="server" OnLoad="ddlTeachers_Load" OnSelectedIndexChanged="ddlTeachers_SelectedIndexChanged" AutoPostBack="True" style="width:initial;"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddlLessons" InitialValue="-בחר מורה-" runat="server" ErrorMessage="בחר מורה" required="required"></asp:RequiredFieldValidator>
        <%} %>
        <asp:DropDownList ID="ddlLessons" runat="server" OnLoad="ddlLessons_Load" OnSelectedIndexChanged="ddlLessons_SelectedIndexChanged" AutoPostBack="True" style="width:initial;"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="ddlLessons" InitialValue="-בחר שיעור-" runat="server" ErrorMessage="בחר שיעור" required="required"></asp:RequiredFieldValidator>
        <asp:Panel ID="pnlBhv" runat="server">
            <table>
                <tr>
                    <td>תאריך השיעור</td>
                    <td>
                        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                        </asp:ToolkitScriptManager>

                        <div class="form-group">
                            <asp:TextBox CssClass="material" ID="txtLesDate" runat="server" required="required"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtLesDate" Format="dd/MM/yyyy" runat="server" />
                            <span class="form-highlight"></span>
                            <span class="form-bar"></span>
                            <asp:Label ID="Label1" AssociatedControlID="txtLesDate" CssClass="form-label" runat="server" Text="dd/MM/yyyy"></asp:Label>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RequiredFieldValidator3" ControlToValidate="txtLesDate" runat="server" ErrorMessage="שדה ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
                            <asp:CustomValidator Display="Dynamic" CssClass="err" ID="CustomValidator1" runat="server" ControlToValidate="txtLesDate"  OnServerValidate="ValidateDate_ServerValidate" ErrorMessage="תאריך לא תקין<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:CustomValidator>
                        </div>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtLesDate" runat="server" ErrorMessage="שדה ריק"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="ValidateDate" runat="server" ControlToValidate="txtLesDate" OnServerValidate="ValidateDate_ServerValidate" ErrorMessage="תאריך לא תקין"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td>שעת לימוד</td>
                    <td><asp:DropDownList ID="ddlHours" runat="server" OnLoad="ddlHours_Load"></asp:DropDownList></td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlHours" InitialValue="-בחר שעה-" ErrorMessage="בחר שעה"></asp:RequiredFieldValidator></td>
                </tr>
            </table>
            <br />
            
            
                    <asp:GridView ID="gvBhv" runat="server" OnRowCreated="gvBhv_RowCreated" OnRowDataBound="gvBhv_RowDataBound" CssClass="table-striped cancel-user-select"></asp:GridView>

            <br />
        </asp:Panel>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnAddBhv" runat="server" Text="הוסף דוח" OnClick="btnAddBhv_Click" />
                <asp:Label ID="lblErrGV" CssClass="lblErrGV" runat="server" Text=""></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

