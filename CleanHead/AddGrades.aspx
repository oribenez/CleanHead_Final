<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="AddGrades.aspx.cs" Inherits="AddGrades" %>
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
        }

         .gvStudents{
            border: 2px solid #ffffff;
            border-collapse: collapse;
        }

        .gvStudents th {
            border: 2px solid #ffffff;
            border-bottom-color: #d4dde4;
            background: rgba(212, 221, 228, .5);
            padding: 2px 8px 4px;
        }

        .gvStudents td {
            padding: 6px 8px 8px;
            border: 2px solid #ffffff;
            font-size: .9em;
            box-shadow: inset 0 -1px 0 0 rgba(212, 221, 228, .5);
        }

        .gvStudents tr:nth-child(2n+2) {
            border: 2px solid #ffffff;
            font-size: 1em;
            box-shadow: inset 0 -1px 0 0 rgba(212, 221, 228, .5);
            background: rgba(212,221,228,0.15);
            padding: 6px 8px 8px;
            letter-spacing: -0.3px;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">הוספת ציונים</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server"><a href="Default.aspx">ראשי</a> » <a href="Grades.aspx">דוח ציונים</a> » הוספת ציונים</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card-panel" style="width:500px;">
        <asp:Panel ID="pnlPermission" runat="server">
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
                <td>ציון ב -</td>
                <td>
                    <div class="form-group">
                        <asp:TextBox CssClass="material" ID="txtGradeName" runat="server" required="required"></asp:TextBox>
                        <span class="form-highlight"></span>
                        <span class="form-bar"></span>
                        <asp:Label ID="Label2" AssociatedControlID="txtGradeName" CssClass="form-label" runat="server" Text="שם המבחן"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="err" Display="Dynamic" ID="RequiredFieldValidator1" ControlToValidate="txtGradeName" runat="server" ErrorMessage="שדה ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="err" Display="Dynamic" ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtGradeName" ValidationExpression="[א-תA-Za-z -_]{2,35}" ErrorMessage="אותיות ומספרים בלבד בין 2 ל-35 תווים<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RegularExpressionValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td>תאריך המבחן</td>
                <td>
                
                    <div class="form-group">
                        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                        </asp:ToolkitScriptManager>

                        <asp:TextBox ID="txtGradeDate" CssClass="material" runat="server" required="required"></asp:TextBox>
                        <span class="form-highlight"></span>
                        <span class="form-bar"></span>
                        <asp:Label ID="Label1" AssociatedControlID="txtGradeDate" CssClass="form-label" runat="server" Text="תאריך המבחן"></asp:Label>
                        <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtGradeDate" Format="dd/MM/yyyy" runat="server" />
                        <asp:RequiredFieldValidator CssClass="err" Display="Dynamic" ID="RequiredFieldValidator2" ControlToValidate="txtGradeDate" runat="server" ErrorMessage="שדה ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" required="required"></asp:RequiredFieldValidator>
                        <asp:CustomValidator CssClass="err" Display="Dynamic" ID="ValidateDate" runat="server" ControlToValidate="txtGradeDate" OnServerValidate="ValidateDate_ServerValidate" ErrorMessage="תאריך לא תקין<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:CustomValidator>
                    </div>
                </td>
            </tr>
        
        </table>
        <asp:Label ID="lblStudentsGrades" Visible="false" runat="server" Font-Bold="true" Text="הכנס ציונים:"></asp:Label>
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
        <asp:Button ID="btnAddGrades" runat="server" OnClick="btnAddGrades_Click" Text="הוסף ציונים" />
        </asp:Panel>
    </div>
</asp:Content>

