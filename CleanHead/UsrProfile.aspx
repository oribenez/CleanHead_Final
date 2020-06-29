<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="UsrProfile.aspx.cs" Inherits="UsrProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>

        table td:nth-child(1){
            font-weight: bold;
            font-size: 17px;
            border: 1px #dddddd solid;
            background: #eeeeee;
            padding: 1px 3px;
        }
        table td:nth-child(2){
            border-bottom: 1px #dddddd solid;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" Runat="Server">פרופיל</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" Runat="Server"><a href="Default.aspx">ראשי</a> » <a href="ControlPanel.aspx">פאנל ניהול</a> » הרשמת תלמיד</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card-panel" style="width: 500px;">
         <table border="0" cellpadding="0" cellspacing="7" dir="rtl">

            <tr>
                <td>
                    תעודת זהות
                </td>
                <td>
                    <asp:Label ID="lblIdentity" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    בית ספר
                </td>
                <td>
                    <asp:Label ID="lblSchool" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    שם פרטי
                </td>
                <td>
                    <asp:Label ID="lblfname" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    שם משפחה
                </td>
                <td>
                    <asp:Label ID="lblLname" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    תאריך לידה
                </td>
                <td>
                    <asp:Label ID="lblBirthDay" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    מין
                </td>
                <td>
                    <asp:Label ID="lblGender" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    אימייל
                </td>
                <td>
                    <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                </td>
            </tr>        
            <tr>
                <td>
                    מקום מגורים
                </td>
                <td>
                    <asp:Label ID="lblCity" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    כתובת
                </td>
                <td>
                    <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    טלפון בבית
                </td>
                <td>
                    <asp:Label ID="lblHomePhone" runat="server" Text=""></asp:Label>
                </td>
            </tr>
             <tr>
                <td>
                    פלאפון נייד
                </td>
                <td>
                    <asp:Label ID="lblCellphone" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlStu" runat="server" Visible="false">
            <table border="0" cellpadding="0" cellspacing="7" dir="rtl">
                <tr>
                    <td>
                        כיתה
                    </td>
                    <td>
                        <asp:Label ID="lblRm_name" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        מספר זהות של האמא
                    </td>
                    <td>
                        <asp:Label ID="lblMomIdentity" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        שם האמא
                    </td>
                    <td>
                        <asp:Label ID="lblMomFname" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        מספר פלאפון של האמא
                    </td>
                    <td>
                        <asp:Label ID="lblMomCellphone" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        מספר זהות של האבא
                    </td>
                    <td>
                        <asp:Label ID="lblDadIdentity" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        שם האבא
                    </td>
                    <td>
                        <asp:Label ID="lblDadFname" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        מספר פלאפון של האבא
                    </td>
                    <td>
                        <asp:Label ID="lblDadCellphone" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlTch" runat="server" Visible="false">
        
            <table border="0" cellpadding="0" cellspacing="7" dir="rtl">
                <tr>
                    <td>
                        ידע המורה
                    </td>
                    <td>
                        <asp:Label ID="lblTchProfessions" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <asp:Panel ID="pnlCrw" runat="server" Visible="false">
        
            <table border="0" cellpadding="0" cellspacing="7" dir="rtl">
                <tr>
                    <td>
                        תפקיד
                    </td>
                    <td>
                        <asp:Label ID="lblJob" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <asp:Button ID="btnEdit" runat="server" Text="עריכת פרטים" OnClick="btnEdit_Click" />
        <asp:Button ID="btnChangePass" runat="server" Text="שנה סיסמה" OnClick="btnChangePass_Click" />
    </div>
</asp:Content>

