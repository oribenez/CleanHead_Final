<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="RegisterStu.aspx.cs" Inherits="RegisterStu" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        /*------------Side-Bar------------*/
        .control_panel{
            color:#fff !important;
            background: #64B5F6 !important;
        }
        .btnSend{
            width: initial !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">הרשמת תלמיד</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server"><a href="Default.aspx">ראשי</a> » <a href="ControlPanel.aspx">פאנל ניהול</a> » הרשמת תלמיד</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card-panel" style="width:500px;">
        <div class="form-group"> 
            <asp:TextBox CssClass="material" ID="txtStuIdentity" MaxLength="9" runat="server" required="required"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="lblStuIdentity" AssociatedControlID="txtStuIdentity" CssClass="form-label" runat="server" Text="תעודת זהות"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="err" Display="Dynamic" ControlToValidate="txtStuIdentity"
            ErrorMessage="תעודת זהות ריקה<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" />
            <asp:RegularExpressionValidator ID="identityValidation" CssClass="err" ValidationExpression="^[0-9]{9}$" Display="Dynamic" ControlToValidate="txtStuIdentity" runat="server" ErrorMessage="ת.ז. לא תקינה<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RegularExpressionValidator>
        </div>
 
        <div class="form-group"> 
            <asp:DropDownList ID="DDLSchools" runat="server" OnSelectedIndexChanged="DDLSchools_SelectedIndexChanged" AutoPostBack="True" required="required"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RFVDDLSchools" CssClass="err" Display="Dynamic" runat="server" ControlToValidate="DDLSchools"
            ErrorMessage="חובה לבחור בית ספר<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"
                    InitialValue="-בחר בית ספר-"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group"> 
            <asp:TextBox CssClass="material" ID="txtFirstName" MaxLength="20" runat="server" required="required"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="lblFirstName" AssociatedControlID="txtFirstName" CssClass="form-label" runat="server" Text="שם פרטי"></asp:Label>
            <asp:RequiredFieldValidator ID="RFVtxtFirstName" CssClass="err" Display="Dynamic" runat="server" ControlToValidate="txtFirstName"
                ErrorMessage="שם פרטי ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server" CssClass="err" Display="Dynamic" id="RegularExpressionValidator1" controlToValidate="txtFirstName" ValidationExpression="^[a-zA-Zא-ת ]{2,20}$" ErrorMessage="הכנס אותיות בלבד שאורכם בין 2 ל-20 תווים<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" />
        </div>

        <div class="form-group"> 
            <asp:TextBox CssClass="material" ID="txtLastName" MaxLength="20" runat="server" required="required"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="lblLastName" AssociatedControlID="txtLastName" CssClass="form-label" runat="server" Text="שם משפחה"></asp:Label>
            <asp:RequiredFieldValidator ID="RFVtxtLastName" CssClass="err" Display="Dynamic" runat="server" ControlToValidate="txtLastName"
                ErrorMessage="שם משפחה ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server" CssClass="err" Display="Dynamic" id="RegularExpressionValidator2" controlToValidate="txtLastName" ValidationExpression="^[a-zA-Zא-ת ]{2,20}$" ErrorMessage="הכנס אותיות בלבד שאורכם בין 2 ל-20 תווים<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" />
        </div>
  
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>

        <div class="form-group"> 
            <asp:TextBox CssClass="material" ID="DateTextBox" required="required" runat="server"></asp:TextBox>
            <asp:CalendarExtender 
                ID="CalendarExtender1" 
                TargetControlID="DateTextBox"
                    Format="dd/MM/yyyy"
                runat="server" />
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="DateLabel" AssociatedControlID="DateTextBox" CssClass="form-label" runat="server" Text="תאריך לידה"></asp:Label>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RequiredFieldValidator10" ControlToValidate="DateTextBox" runat="server" ErrorMessage="שדה ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
            <asp:CustomValidator Display="Dynamic" CssClass="err" ID="ValidateDate" runat="server" ControlToValidate="DateTextBox" OnServerValidate="ValidateDate_ServerValidate" ErrorMessage="תאריך לא תקין<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:CustomValidator>

        </div>

        <div class="form-group"> 
            <asp:RadioButtonList ID="rbtGender" RepeatLayout="Table" RepeatDirection="Horizontal" runat="server">
                <asp:ListItem Value="זכר" Text="זכר"></asp:ListItem>
                <asp:ListItem Value="נקבה" Text="נקבה"></asp:ListItem>
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="err" Display="Dynamic" runat="server"  ControlToValidate="rbtGender" 
            ErrorMessage="הכנס מין<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
        </div>

        <div class="form-group"> 
            <asp:TextBox CssClass="material" ID="txtEmail" required="required" runat="server"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="lblEmail" AssociatedControlID="txtEmail" CssClass="form-label" runat="server" Text="אימייל"></asp:Label>
            <asp:RequiredFieldValidator ID="RFVtxtEmail" CssClass="err" Display="Dynamic" runat="server" ControlToValidate="txtEmail" 
                ErrorMessage="אימייל ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server" CssClass="err" Display="Dynamic" id="RegularExpressionValidator3" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="אימייל לא תקין<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" />
        </div>

        <div class="form-group">
            <asp:DropDownList ID="DDLCity" runat="server" required="required"></asp:DropDownList>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:RequiredFieldValidator ID="RFVDDLCity" CssClass="err" Display="Dynamic" runat="server" ControlToValidate="DDLCity"
            ErrorMessage="חובה לבחור עיר<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" InitialValue="-בחר עיר-"></asp:RequiredFieldValidator>
        </div>

        <div class="form-group">
            <asp:TextBox CssClass="material" ID="txtAddress" required="required" runat="server"></asp:TextBox>                    
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label1" AssociatedControlID="txtAddress" CssClass="form-label" runat="server" Text="כתובת"></asp:Label>
            <asp:RequiredFieldValidator ID="RFVtxtAddress" CssClass="err" Display="Dynamic" ControlToValidate="txtAddress" runat="server"
            ErrorMessage="כתובת ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
        </div>

        <div class="form-group">
            <asp:TextBox CssClass="material" ID="txtHomePhone" required="required" runat="server"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label2" AssociatedControlID="txtHomePhone" CssClass="form-label" runat="server" Text="טלפון"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="err" Display="Dynamic" runat="server" ControlToValidate="txtHomePhone" 
                ErrorMessage="טלפון ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
        </div>

        <div class="form-group">
            <asp:TextBox CssClass="material" ID="txtCellphone" required="required" runat="server"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label3" AssociatedControlID="txtCellphone" CssClass="form-label" runat="server" Text="פלאפון"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" CssClass="err" Display="Dynamic" runat="server" ControlToValidate="txtCellphone" 
                ErrorMessage="פלאפון ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
        </div>

        <div class="form-group">
            <asp:DropDownList ID="ddlRooms" required="required" runat="server"></asp:DropDownList>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="err" Display="Dynamic" runat="server" ControlToValidate="ddlRooms"
            ErrorMessage="חובה לבחור כיתה<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" InitialValue="-בחר כיתה-"></asp:RequiredFieldValidator>
        </div>

        <div class="form-group">
            <asp:TextBox CssClass="material" ID="txtMomIdentity" required="required" MaxLength="9" runat="server"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label5" AssociatedControlID="txtMomIdentity" CssClass="form-label" runat="server" Text="מספר זהות של האמא"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="err" Display="Dynamic" runat="server" ControlToValidate="txtMomIdentity" 
                ErrorMessage="מספר זהות של האמא ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" CssClass="err" Display="Dynamic" ValidationExpression="^[0-9]{9}$" ControlToValidate="txtMomIdentity" runat="server" ErrorMessage="ת.ז. לא תקינה<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RegularExpressionValidator>
        </div>

        <div class="form-group">
            <asp:TextBox CssClass="material" ID="txtMomFirstName" required="required" runat="server"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label6" AssociatedControlID="txtMomFirstName" CssClass="form-label" runat="server" Text="שם האמא"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="err" Display="Dynamic" runat="server" ControlToValidate="txtMomFirstName" 
                ErrorMessage="שם האמא ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
        </div>

        <div class="form-group">
            <asp:TextBox CssClass="material" ID="txtMomCellphone" required="required" runat="server"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label7" AssociatedControlID="txtMomCellphone" CssClass="form-label" runat="server" Text="מספר פלאפון של האמא"></asp:Label>
        </div>

        <div class="form-group">
            <asp:TextBox CssClass="material" ID="txtDadIdentity" required="required" MaxLength="9" runat="server"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label8" AssociatedControlID="txtDadIdentity" CssClass="form-label" runat="server" Text="מספר זהות של האבא"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="err" Display="Dynamic" runat="server" ControlToValidate="txtDadIdentity" 
                ErrorMessage="מספר זהות של האבא ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" CssClass="err" Display="Dynamic" ValidationExpression="^[0-9]{9}$" ControlToValidate="txtDadIdentity" runat="server" ErrorMessage="ת.ז. לא תקינה<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RegularExpressionValidator>
        </div>

        <div class="form-group">
            <asp:TextBox CssClass="material" ID="txtDadFirstName" required="required" runat="server"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label9" AssociatedControlID="txtDadFirstName" CssClass="form-label" runat="server" Text="שם האבא"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="err" Display="Dynamic" runat="server" ControlToValidate="txtDadFirstName" 
                ErrorMessage="שם האבא ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
        </div>

        <div class="form-group">
            <asp:TextBox CssClass="material" ID="txtDadCellphone" required="required" runat="server"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label10" AssociatedControlID="txtDadCellphone" CssClass="form-label" runat="server" Text="מספר פלאפון של האבא"></asp:Label>
        </div>

        <br /> 
        <asp:Button ID="Send" runat="server" Text="הירשם" OnClick="Send_Click" CssClass="btnSend" />
        <asp:Label ID="lblErr" runat="server" Text="" ForeColor="LightBlue"></asp:Label>
    </div>
</asp:Content>
