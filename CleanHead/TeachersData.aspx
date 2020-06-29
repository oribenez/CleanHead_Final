<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="TeachersData.aspx.cs" Inherits="TeachersData" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <style>
                        /*------------Side-Bar------------*/
        .control_panel{
            color:#fff !important;
            background: #64B5F6 !important;
        }



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
            padding: 35px; 
        }
        
        
        .rblGender td{
            background: none !important;
            cursor: auto !important;
            padding: 0 !important;
        }
        .rblGender tbody tr:not(:first-child){
            background: none !important;
            cursor: auto;
            padding: 0;
        }
        .rblGender tbody tr:not(:first-child):hover{
            background: none !important;
            cursor: auto;
            padding: 0;
        }

        .wrap_block{
            background: #fff;
            width: 220px;
            border: 1px #bbbbbb solid;
            color: #383838;
            float: right;
            margin: 0 0 0 30px;
        }
        .header_block{
            background: #ef5350;
            height: 37px;
            color: #fff;
            font-size: 20px;
            padding: 3px 11px 0 0;
            border-bottom: 3px solid #EAA997;
        }
        span.lbl_subject {
            font-weight: bold;
            cursor: auto;
            float: right;
        }
        .pnl_content{
            text-indent: 5px;
        }
        .content_block{
            padding:10px;
        }
        .wrap_block input[type="text"]{
            width: 180px;
        }

        .lblError{
            color: #ef5350;
            clear: both;
            display: block;
            font-weight: bold;
            text-align: left;
        }
    </style>
    <script>
        function ConfirmDelete() {
            if ("<%=Session["gender"].ToString() %>" == "זכר") {
                return confirm("האם אתה בטוח שאתה רוצה למחוק את התלמיד?");
            }
            else {
                return confirm("האם את בטוחה שאת רוצה למחוק את התלמיד?");
            }
        }

        $(function () {
            var research = $('.gvTeachers'),
                accordions = research.find('tbody tr:nth-child(2n+3)'),
                hiddens = research.find("tbody tr:nth-child(2n+2)"),
                selected = $('#selectedRowIndex');

            accordions.hide();

            if (selected.val() != '') {
                accordions.eq(selected.val()).show();
                selected.val('');
            }


            hiddens.click(function () {

                if ($(this).next().css('display') !== 'none') {
                    $(this).next().hide(150);
                } else {
                    accordions.hide();
                    $(this).next().fadeToggle(150);
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">פרטי מורים</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server"><a href="Default.aspx">ראשי</a> » <a href="ControlPanel.aspx">פאנל ניהול</a> » מורים
    <%if (Convert.ToInt32(Session["lvl_id"]) >= 3) {
        %><br /><br />
        <asp:Button ID="btnInsert" runat="server" Text="+" CssClass="btn-plus-header" CausesValidation="False" OnClick="btnInsert_Click" />
    <%} %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card-panel" style="margin: 40px 0 0 0;">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <asp:Panel ID="PnlShowTeachers" runat="server">
            <asp:HiddenField ID="selectedRowIndex" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="selectedRowName" ClientIDMode="Static" runat="server" />

            <%-- SearchBox --%>
            <div class="float-left">
                <div class="form-group search-box float-right">
                    <asp:TextBox CssClass="material" ID="txtSearch" runat="server"></asp:TextBox>
                    <span class="form-highlight"></span>
                    <span class="form-bar"></span>
                    <asp:Label ID="Label1" AssociatedControlID="txtSearch" CssClass="form-label" runat="server" Text="חיפוש"></asp:Label>
                </div>
                <asp:Button ID="btnSearch" CssClass="float-right" runat="server" Text="חפש" CausesValidation="false" style="margin: 45px 10px 0 0;" OnClick="btnSearch_Click" />
            </div>
            <div class="float-right">
                <%
                if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
                 %>
                    <h4>סינון</h4>
                    <asp:DropDownList ID="ddlSchools" runat="server" OnLoad="ddlSchools_Load" OnSelectedIndexChanged="ddlSchools_SelectedIndexChanged" AutoPostBack="True" style="width: initial;"></asp:DropDownList>
                <%} %>
            </div>
            <asp:GridView ID="GVTeachers" runat="server"  CellPadding="4" CssClass="gvTeachers table-striped-accordion" GridLines="None" BorderStyle="None" OnLoad="GVTeachers_Load" OnRowDeleting="GVTeachers_RowDeleting" AllowPaging="True" OnPageIndexChanging="GVTeachers_PageIndexChanging" BackColor="White" OnPreRender="GVTeachers_PreRender" OnRowCreated="GVTeachers_RowCreated" OnRowDataBound="GVTeachers_RowDataBound" PageSize="16">
            </asp:GridView>
            <br />
            <asp:Label ID="lblErrGV" runat="server" ForeColor="Red" Text=""></asp:Label>
        </asp:Panel>
    </div>
</asp:Content>

