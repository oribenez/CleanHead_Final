<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="LessonsData.aspx.cs" Inherits="LessonsData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        /*------------Side-Bar------------*/
        .control_panel{
            color:#fff !important;
            background: #64B5F6 !important;
        }

        .popupBtnX {
            float: left;
        }

        .popupBack {
            display: block;
            z-index: 998;
            background: rgba(51, 51, 51, 0.6);
            position: fixed;
            width: 100%;
            height: 100%;
        }

        .popupContainer {
            background: #8FC4D7;
            border: solid #808080 4px;
            z-index: 999;
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            padding: 35px;
        }

        /*table*/

        /*tbody tr:not(:first-child):hover{
            background: #b4ddec !important;
            cursor: pointer;
        }
        tbody tr:nth-child(2n+3) td{
            background: #eee;
            cursor: auto;
            padding: 20px;
        }
        table{
            border: 2px solid #ffffff;
            border-collapse:collapse
        }

        table th{
            border: 2px solid #ffffff;
            border-bottom-color: #d4dde4;
            background: rgba(212, 221, 228, .5);
            padding:2px 8px 4px;
        }
        td{
            padding: 5px 10px;
        }

        table tr:nth-child(2n+2){
            border: 2px solid #ffffff;
            font-size: 1em;
            box-shadow: inset 0 -1px 0 0 rgba(212, 221, 228, .5);
            background: rgba(212,221,228,0.15);
            padding: 6px 8px 8px;
            letter-spacing: -0.3px;
            transition: all 0.1s;
        }*/

        .wrap_block{
            background: #fff;
            width: 200px;
            border: 1px #bbbbbb solid;
            color: #383838;
            float: right;
            margin: 0 0 0 30px;
        }
        .wrap_block2{
            background: #fff;
            width: 400px;
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
            color: #EF5350;
            clear: both;
            display: block;
            font-weight: bold;
            text-align: left;
        }

        .pnl_all_content {
            clear:both;
        }

        .pnl_name{
            background: #646361;
            color: #ffffff;
            padding: 2px 7px;
            margin: 5px;
            display: inline-block;
            cursor: pointer;
        }
    </style>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script>
        function ConfirmDelete() {
            if ("<%=Session["gender"].ToString() %>" == "זכר") {
                return confirm("האם אתה בטוח שאתה רוצה למחוק את השיעור?");
            }
            else {
                return confirm("האם את בטוחה שאת רוצה למחוק את השיעור?");
            }
        }

        $(function () {
            var research = $('.gvLessons'),
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
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">שיעורים</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server"><a href="Default.aspx">ראשי</a> » <a href="ControlPanel.aspx">פאנל ניהול</a> » שיעורים</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card-panel">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:HiddenField ID="selectedRowIndex" ClientIDMode="Static" runat="server" />
        <div class="filtersWrap">
            <h4>סינון</h4>
            <%if (Convert.ToInt32(Session["lvl_id"]) >= 4) { %>
                <asp:DropDownList ID="ddlSchools" runat="server" OnLoad="ddlSchools_Load" 
                    OnSelectedIndexChanged="ddlSchools_SelectedIndexChanged"
                    AutoPostBack="True" style="width: initial;"></asp:DropDownList>
                &nbsp;←&nbsp;
            <%} %>

            <%if (Convert.ToInt32(Session["lvl_id"]) >= 3) { %> 
                <asp:DropDownList ID="ddlLayers" runat="server" OnLoad="ddlLayers_Load" 
                    OnSelectedIndexChanged="ddlLayers_SelectedIndexChanged" 
                    AutoPostBack="True" style="width: initial;"></asp:DropDownList>
                &nbsp;<b>←</b>&nbsp;
                <asp:DropDownList ID="ddlProfessions" runat="server" OnLoad="ddlProfessions_Load" 
                    OnSelectedIndexChanged="ddlProfessions_SelectedIndexChanged"
                    AutoPostBack="True" style="width: initial;"></asp:DropDownList>
                &nbsp;<b>←</b>&nbsp;
                <asp:DropDownList ID="ddlTeachers" AppendDataBoundItems="false" runat="server" OnLoad="ddlTeachers_Load" 
                    OnSelectedIndexChanged="ddlTeachers_SelectedIndexChanged"
                    AutoPostBack="True" style="width: initial;"></asp:DropDownList>
            <%} %>
        </div>

        <asp:GridView ID="gvLessons" CssClass="gvLessons table-striped-accordion" runat="server" OnLoad="gvLessons_Load" OnRowDataBound="gvLessons_RowDataBound" OnRowCreated="gvLessons_RowCreated" AllowPaging="True" PageSize="16" OnPageIndexChanging="gvLessons_PageIndexChanging">
        </asp:GridView>    
        <br />
        <%
        if (Convert.ToInt32(Session["lvl_id"]) == 3) {
             %>
        <asp:Button ID="btnInsert" runat="server" Text="+" CssClass="btn-plus" CausesValidation="False" OnClick="btnInsert_Click" />
        <%} %>
    </div>
</asp:Content>