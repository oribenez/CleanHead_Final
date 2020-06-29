<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="UsrProfileUpdate.aspx.cs" Inherits="UsrProfileUpdate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">עריכת פרופיל</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server"><a href="Default.aspx">ראשי</a> » <a href="UsrProfile.aspx">פרופיל</a> » עריכת פרופיל</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card-panel" style="width:500px;">
        <div class="form-group">
            תעודת זהות
            <asp:TextBox CssClass="material" required="required" ID="txtIdentity" MaxLength="9" runat="server" ReadOnly="true"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label1" AssociatedControlID="txtIdentity" CssClass="form-label" runat="server" Text=""></asp:Label>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIdentity"
                ErrorMessage="תעודת זהות ריקה<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator Display="Dynamic" CssClass="err" ID="identityValidation" ValidationExpression="^[0-9]{9}$" ControlToValidate="txtIdentity" runat="server" ErrorMessage="ת.ז. לא תקינה<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RegularExpressionValidator>
        </div>    
        <div class="form-group">
            <asp:DropDownList ID="DDLSchools" runat="server"></asp:DropDownList>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RFVDDLSchools" runat="server" ControlToValidate="DDLSchools"
            ErrorMessage="חובה לבחור בית ספר<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" InitialValue="-בחר בית ספר-"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:TextBox CssClass="material" required="required" ID="txtFirstName" runat="server"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label3" AssociatedControlID="txtFirstName" CssClass="form-label" runat="server" Text="שם פרטי"></asp:Label>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RFVtxtFirstName" runat="server" ControlToValidate="txtFirstName"
                ErrorMessage="שם פרטי ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator Display="Dynamic" CssClass="err" runat="server" id="RegularExpressionValidator1" controlToValidate="txtFirstName" ValidationExpression="^[a-zA-Zא-ת ]{2,20}$" ErrorMessage="הכנס אותיות בלבד שאורכם בין 2 ל-20 תווים<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" />
        </div>
        <div class="form-group">
            <asp:TextBox CssClass="material" required="required" ID="txtLastName" runat="server"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label4" AssociatedControlID="txtLastName" CssClass="form-label" runat="server" Text="שם משפחה"></asp:Label>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RFVtxtLastName" runat="server" ControlToValidate="txtLastName"
                ErrorMessage="שם משפחה ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator Display="Dynamic" CssClass="err" runat="server" id="RegularExpressionValidator2" controlToValidate="txtLastName" ValidationExpression="^[a-zA-Zא-ת ]{2,20}$" ErrorMessage="הכנס אותיות בלבד שאורכם בין 2 ל-20 תווים<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" />
        </div>
    
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <div class="form-group">
            <asp:TextBox CssClass="material" required="required" ID="DateTextBox" runat="server"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label5" AssociatedControlID="DateTextBox" CssClass="form-label" runat="server" Text="תאריך לידה"></asp:Label>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RequiredFieldValidator3" ControlToValidate="DateTextBox" runat="server" ErrorMessage="שדה ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" required="required"></asp:RequiredFieldValidator>
            <asp:CustomValidator Display="Dynamic" CssClass="err" ID="ValidateDate" runat="server" ControlToValidate="DateTextBox" OnServerValidate="ValidateDate_ServerValidate" ErrorMessage="תאריך לא תקין<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:CustomValidator>

        </div>
        <asp:CalendarExtender 
            ID="CalendarExtender1" 
            TargetControlID="DateTextBox"
                Format="dd/MM/yyyy"
            runat="server" />

        <div class="form-group">
            <asp:RadioButtonList ID="rbtGender" runat="server">
                <asp:ListItem Value="זכר" Text="זכר"></asp:ListItem>
                <asp:ListItem Value="נקבה" Text="נקבה"></asp:ListItem>
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RequiredFieldValidator8" runat="server"  ControlToValidate="rbtGender" 
                ErrorMessage="הכנס מין<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:TextBox CssClass="material" required="required" ID="txtEmail" runat="server"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label7" AssociatedControlID="txtEmail" CssClass="form-label" runat="server" Text="אימייל"></asp:Label>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RFVtxtEmail" runat="server" ControlToValidate="txtEmail" 
                ErrorMessage="אימייל ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server" id="RegularExpressionValidator3" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="אימייל לא תקין<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" />
        </div>
        <div class="form-group">
            עיר
            <asp:DropDownList ID="DDLCity" runat="server"></asp:DropDownList>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RFVDDLCity" runat="server" ControlToValidate="DDLCity"
            ErrorMessage="חובה לבחור עיר<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" InitialValue="-בחר עיר-"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:TextBox CssClass="material" required="required" ID="txtAddress" runat="server"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label9" AssociatedControlID="txtAddress" CssClass="form-label" runat="server" Text="כתובת"></asp:Label>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RFVtxtAddress" ControlToValidate="txtAddress" runat="server"
                ErrorMessage="כתובת ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:TextBox CssClass="material" required="required" ID="txtHomePhone" runat="server"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label10" AssociatedControlID="txtHomePhone" CssClass="form-label" runat="server" Text="טלפון בבית"></asp:Label>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtHomePhone" 
                ErrorMessage="טלפון ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:TextBox CssClass="material" required="required" ID="txtCellphone" runat="server"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label11" AssociatedControlID="txtCellphone" CssClass="form-label" runat="server" Text="מספר פלאפון נייד"></asp:Label>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtCellphone" 
                ErrorMessage="פלאפון ריק<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
        </div>
        <asp:Panel ID="pnlTch" runat="server" Visible="false">
            ידע המורה
            <asp:ListBox ID="lbProfessions" Height="200" SelectionMode="Multiple" runat="server"></asp:ListBox>
        </asp:Panel>
        <asp:Panel ID="pnlCrw" runat="server" Visible="false">
            <div class="form-group">
                <asp:DropDownList ID="ddlJobs" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator Display="Dynamic" CssClass="err" ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlJobs"
                ErrorMessage="חובה לבחור תפקיד<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" InitialValue="-בחר תפקיד-"></asp:RequiredFieldValidator>
            </div>
        </asp:Panel>
        
        <asp:Label ID="lblErr" runat="server" Text=""></asp:Label>
        <br /> 
        <asp:Button ID="btnCancel" runat="server" Text="בטל" OnClientClick="location.href='UsrProfile.aspx'" />&nbsp;
        <asp:Button ID="Send" runat="server" Text="עדכן" OnClick="Send_Click" />
    </div>
</asp:Content>

