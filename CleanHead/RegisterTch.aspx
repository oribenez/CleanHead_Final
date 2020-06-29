<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="RegisterTch.aspx.cs" Inherits="RegisterTch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        /*------------Side-Bar------------*/
        .control_panel{
            color:#fff !important;
            background: #64B5F6 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" Runat="Server">הרשמת מורה
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" Runat="Server"><a href="Default.aspx">ראשי</a> » <a href="ControlPanel.aspx">פאנל ניהול</a> » הרשמת מורה
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card-panel" style="width:500px;">
        <div class="form-group"> 
            <asp:TextBox CssClass="material" ID="txtTchIdentity" MaxLength="9" runat="server" required="required"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label2" AssociatedControlID="txtTchIdentity" CssClass="form-label" runat="server" Text="תעודת זהות"></asp:Label>
            <asp:RequiredFieldValidator CssClass="err" Display="Dynamic" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTchIdentity"
            ErrorMessage="תעודת זהות ריקה<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="err" Display="Dynamic" ID="identityValidation" ValidationExpression="^[0-9]{9}$" ControlToValidate="txtTchIdentity" runat="server" ErrorMessage="ת.ז. לא תקינה<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RegularExpressionValidator>
        </div>
        <div class="form-group"> 
            <asp:DropDownList ID="DDLSchools" runat="server" required="required"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="err" Display="Dynamic" ID="RFVDDLSchools" runat="server" ControlToValidate="DDLSchools"
            ErrorMessage="חובה לבחור בית ספר<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" InitialValue="-בחר בית ספר-"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group"> 
            <asp:TextBox CssClass="material" ID="txtFirstName" runat="server" required="required"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label3" AssociatedControlID="txtFirstName" CssClass="form-label" runat="server" Text="שם פרטי"></asp:Label>
            <asp:RequiredFieldValidator CssClass="err" Display="Dynamic" ID="RFVtxtFirstName" runat="server" ControlToValidate="txtFirstName"
            ErrorMessage="שם פרטי ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="err" Display="Dynamic" runat="server" id="RegularExpressionValidator1" controlToValidate="txtFirstName" ValidationExpression="^[a-zA-Zא-ת ]{2,20}$" ErrorMessage="הכנס אותיות בלבד שאורכם בין 2 ל-20 תווים<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" />
        </div>
        <div class="form-group"> 
            <asp:TextBox CssClass="material" ID="txtLastName" runat="server" required="required"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label4" AssociatedControlID="txtLastName" CssClass="form-label" runat="server" Text="שם משפחה"></asp:Label>
            <asp:RequiredFieldValidator CssClass="err" Display="Dynamic" ID="RFVtxtLastName" runat="server" ControlToValidate="txtLastName"
            ErrorMessage="שם משפחה ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="err" Display="Dynamic" runat="server" id="RegularExpressionValidator2" controlToValidate="txtLastName" ValidationExpression="^[a-zA-Zא-ת ]{2,20}$" ErrorMessage="הכנס אותיות בלבד שאורכם בין 2 ל-20 תווים<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" />
        </div>
        <div class="form-group"> 
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            <asp:TextBox CssClass="material" ID="DateTextBox" runat="server" required="required"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label5" AssociatedControlID="DateTextBox" CssClass="form-label" runat="server" Text="תאריך לידה"></asp:Label>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RequiredFieldValidator3" ControlToValidate="DateTextBox" runat="server" ErrorMessage="שדה ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
            <asp:CustomValidator Display="Dynamic" CssClass="err" ID="ValidateDate" runat="server" ControlToValidate="DateTextBox" OnServerValidate="ValidateDate_ServerValidate" ErrorMessage="תאריך לא תקין<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:CustomValidator>
            <asp:CalendarExtender 
                ID="CalendarExtender1" 
                TargetControlID="DateTextBox" 
                Format="dd/MM/yyyy"
                runat="server" />
        </div>
        <div class="form-group"> 
            <asp:RadioButtonList ID="rbtGender" runat="server" required="required">
                <asp:ListItem Value="זכר" Text="זכר"></asp:ListItem>
                <asp:ListItem Value="נקבה" Text="נקבה"></asp:ListItem>
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator CssClass="err" Display="Dynamic" ID="RequiredFieldValidator8" runat="server"  ControlToValidate="rbtGender" 
             ErrorMessage="הכנס מין<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group"> 
            <asp:TextBox CssClass="material" ID="txtEmail" runat="server" required="required"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label1" AssociatedControlID="txtEmail" CssClass="form-label" runat="server" Text="אימייל"></asp:Label>
            <asp:RequiredFieldValidator CssClass="err" Display="Dynamic" ID="RFVtxtEmail" runat="server" ControlToValidate="txtEmail" 
            ErrorMessage="אימייל ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="err" Display="Dynamic" runat="server" id="RegularExpressionValidator3" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="אימייל לא תקין<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" />
        </div>
        <div class="form-group"> 
            <asp:DropDownList ID="DDLCity" runat="server" required="required"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="err" Display="Dynamic" ID="RFVDDLCity" runat="server" ControlToValidate="DDLCity"
            ErrorMessage="חובה לבחור עיר<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" InitialValue="-בחר עיר-"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group"> 
            <asp:TextBox CssClass="material" ID="txtAddress" runat="server" required="required"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label6" AssociatedControlID="txtAddress" CssClass="form-label" runat="server" Text="כתובת"></asp:Label>
            <asp:RequiredFieldValidator CssClass="err" Display="Dynamic" ID="RFVtxtAddress" ControlToValidate="txtAddress" runat="server"
            ErrorMessage="כתובת ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group"> 
            <asp:TextBox CssClass="material" ID="txtHomePhone" runat="server" required="required"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label7" AssociatedControlID="txtHomePhone" CssClass="form-label" runat="server" Text="טלפון בבית"></asp:Label>
            <asp:RequiredFieldValidator CssClass="err" Display="Dynamic" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtHomePhone" 
            ErrorMessage="טלפון ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group"> 
            <asp:TextBox CssClass="material" ID="txtCellphone" runat="server" required="required"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label8" AssociatedControlID="txtCellphone" CssClass="form-label" runat="server" Text="מספר פלאפון נייד"></asp:Label>
        </div>
        <div class="form-group" style="margin-top: 0;">
            הרשאת גישה
            <asp:DropDownList ID="ddlLevels" required="required" runat="server"></asp:DropDownList>
        </div>
        <div class="form-group"> 
            <asp:ListBox ID="lbProfessions" Height="200" SelectionMode="Multiple" runat="server" required="required"></asp:ListBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label9" AssociatedControlID="lbProfessions" CssClass="form-label" runat="server" Text="ידע המורה" style="top:-23px;"></asp:Label>
        </div>
            <br /> 
            <asp:ValidationSummary ID="ValidationSummary1" HeaderText="תקן את השגיאות הבאות בכדי לשלוח טופס זה:" style="color:Red; font-size: 13px;"  runat="server" />
            <asp:Button ID="Send" runat="server" Text="הירשם" OnClick="Send_Click" />
            <asp:Label ID="lblErr" runat="server" Text="" ForeColor="Red"></asp:Label>
    </div>
</asp:Content>

