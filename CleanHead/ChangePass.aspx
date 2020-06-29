<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="ChangePass.aspx.cs" Inherits="ChangePass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
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

        /*input[type="submit"] {
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
        table td:nth-child(1){
            font-weight: bold;
            font-size: 17px;
            border: 1px #dddddd solid;
            background: #eeeeee;
            padding: 1px 3px;
        }
        table td:nth-child(2){
            border-bottom: 1px #dddddd solid;
        }*/
        .lblErr{
            color: #EF5350;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">שינוי סיסמה</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server"><a href="Default.aspx">ראשי</a> » <a href="UsrProfile.aspx">פרופיל</a> » שינוי סיסמה</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card-panel" style="width:500px;">
        <div class="form-group">
            <asp:TextBox ID="txtOldPass" CssClass="material" required="required" TextMode="Password" runat="server"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label1" AssociatedControlID="txtOldPass" CssClass="form-label" runat="server" Text="סיסמה ישנה"></asp:Label>
            <asp:RequiredFieldValidator CssClass="err" Display="Dynamic" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOldPass" ErrorMessage="הכנס סיסמה ישנה<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
        </div>
    
        <div class="form-group">
            <asp:TextBox ID="txtNewPass" CssClass="material" required="required" TextMode="Password" runat="server"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label2" AssociatedControlID="txtNewPass" CssClass="form-label" runat="server" Text="סיסמה חדשה"></asp:Label>
            <asp:RequiredFieldValidator CssClass="err" Display="Dynamic" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewPass" ErrorMessage="הכנס סיסמה חדשה<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
        </div>
    
        <div class="form-group">
            <asp:TextBox CssClass="material" required="required" ID="txtConfirmNewPass" TextMode="Password" runat="server"></asp:TextBox>
            <span class="form-highlight"></span>
            <span class="form-bar"></span>
            <asp:Label ID="Label3" AssociatedControlID="txtConfirmNewPass" CssClass="form-label" runat="server" Text="אמת סיסמה חדשה"></asp:Label>
            <asp:RequiredFieldValidator CssClass="err" Display="Dynamic" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConfirmNewPass" ErrorMessage="אמת סיסמה חדשה<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
        </div>
        <asp:Label ID="lblErr" runat="server" Text=""></asp:Label>
        <br />
        <asp:Button ID="btnCancel" runat="server" Text="בטל" OnClientClick="location.href='UsrProfile.aspx'" />&nbsp;
        <asp:Button ID="btnChangePass" runat="server" Text="עדכן סיסמה" OnClick="btnChangePass_Click" />
    </div>
</asp:Content>

