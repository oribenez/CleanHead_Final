<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="WriteMsg.aspx.cs" Inherits="WriteMsg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .popupBtnX{
            float:left;
        }
        .popupBack{
            display: block;
            z-index: 998;
            background: rgba(51, 51, 51, 0.6);
            position: fixed;
            width: 100%;
            height: 100%;
        }
        .popupContainer{
            background: #8FC4D7;
            border: solid #808080 4px;
            z-index: 999;
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            width: 460px;
            height: 380px;         
            padding: 40px; 
        }
        /*input[type="submit"]{
            background: #d96557;
            padding: 4px 32px;
            font-family: "Alef Hebrew",
                       “Helvetica Neue”,
                       Helvetica,Tahoma,Arial;
            border: none;
            font-size: 24px;
            border-radius: 3px;
            color: #fff;
            cursor: pointer;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PopUps" Runat="Server">
    <asp:Panel ID="pnlPopup" runat="server" Visible="false">
        <asp:Panel ID="pnlPopupBack" CssClass="popupBack" runat="server">

        </asp:Panel>
        <asp:Panel ID="pnlPopupContainer" CssClass="popupContainer" runat="server">
            <asp:ImageButton ID="popupBtnX" ImageUrl="~/Images/x.png" Width="22" Height="22" CssClass="popupBtnX" runat="server" OnClick="popupBtnX_Click" />
            <h3>
            <asp:Label ID="lblPopupTitle" runat="server" Text=""></asp:Label></h3><br />
            <p>
            <asp:Label ID="lblPopupContent" runat="server" Text=""></asp:Label></p><br />
            <asp:Button ID="btnOk" runat="server" Text="אוקיי" OnClick="btnOk_Click" />
        </asp:Panel>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">הודעה חדשה</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server"><a href="Default.aspx">ראשי</a> » <a href="Messages.aspx">הודעות</a> » הודעה חדשה</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card-panel" style="width:500px;">
        <div id="msgContent">
            הודעה ל: 
            <asp:DropDownList ID="ddlUserType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged">
                <asp:ListItem Value="choose" Selected="True">-בחר תפקיד-</asp:ListItem>
                <asp:ListItem Value="crw">איש צוות</asp:ListItem>
                <asp:ListItem Value="tch">מורה</asp:ListItem>
                <asp:ListItem Value="stu">תלמיד</asp:ListItem>
            </asp:DropDownList><br />
            <asp:DropDownList ID="ddlTo" runat="server" Enabled="false"></asp:DropDownList>
            <div class="form-group">
                <asp:TextBox CssClass="material" required="required" ID="txtTitle" runat="server"></asp:TextBox>
                <span class="form-highlight"></span>
                <span class="form-bar"></span>
                <asp:Label ID="Label1" AssociatedControlID="txtTitle" CssClass="form-label" runat="server" Text="כותרת ההודעה"></asp:Label>
            </div>

            <div class="form-group">
                <asp:TextBox CssClass="material" required="required" ID="txtContent" TextMode="MultiLine" runat="server"></asp:TextBox>
                <span class="form-highlight"></span>
                <span class="form-bar"></span>
                <asp:Label ID="Label2" AssociatedControlID="txtContent" CssClass="form-label" runat="server" Text="תוכן ההודעה"></asp:Label>
            </div>
            <asp:Button ID="btnSend" runat="server" Text="שלח הודעה" OnClick="btnSend_Click" />
        </div>
    </div>
</asp:Content>

