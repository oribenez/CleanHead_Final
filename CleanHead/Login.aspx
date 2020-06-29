<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" dir="rtl">
<head runat="server">
    <link href="Styles/GeneralStl.css" rel="stylesheet" />
    <link href="Styles/LoginStl.css" rel="stylesheet" />
    
    <title>ראש נקי - התחברות</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="wrapper">
		    <div id="clouds"></div>
		    <div id="mountains"></div>
	    </div>
                
        <div id="block_wrapper">
            <div id="wrap_statements">
                <h3 class="title">מערכת מידע אישי לתלמידים</h3>
                <div class="usr-photo"></div>
                <h5  class="title">התחבר למשתמש שלך</h5>
                <div class="form-group">
                    <asp:TextBox ID="txtIdentity" CssClass="material" runat="server" MaxLength="9" required="required"></asp:TextBox>
                    <span class="form-highlight"></span>
                    <span class="form-bar"></span>
                    <asp:Label ID="Label1" AssociatedControlID="txtIdentity" CssClass="form-label" runat="server" Text="ת.ז."></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="err" Display="Dynamic" ControlToValidate="txtIdentity"
                    ErrorMessage="הכנס תעודת זהות<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" />
                    <asp:RegularExpressionValidator ID="identityValidation" CssClass="err" ValidationExpression="^[0-9]{9}$" Display="Dynamic" ControlToValidate="txtIdentity" runat="server" ErrorMessage="ת.ז. לא תקינה<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RegularExpressionValidator>
                </div>

                <div class="form-group">
                    <asp:TextBox ID="txtPassword" CssClass="material" runat="server" TextMode="Password" required="required"></asp:TextBox>
                    <span class="form-highlight"></span>
                    <span class="form-bar"></span>
                    <asp:Label ID="Label2" AssociatedControlID="txtPassword" CssClass="form-label" runat="server" Text="סיסמה"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="err" Display="Dynamic" ControlToValidate="txtPassword"
                    ErrorMessage="הכנס סיסמה<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>" />
                </div>

                <asp:Button ID="btnSend" CssClass="btnSend" runat="server" Text="התחבר" OnClick="btnSend_Click" />&nbsp;&nbsp;
                <asp:Label ID="lblErr" CssClass="lblErr" runat="server" Text=""></asp:Label>
                
            </div>
        </div>
    </form>
</body>
</html>
