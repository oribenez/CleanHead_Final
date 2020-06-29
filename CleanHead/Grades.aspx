<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="Grades.aspx.cs" Inherits="Grades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #grades{
            fill:#fff !important;
        }
        .grades:hover > #grades{
            fill:#fff !important;
        }
        .grades{
            color:#fff !important;
            background: #64B5F6 !important;
        }


        /*table*/

        
        
        /*table{
            border: 2px solid #ffffff;
            border-collapse:collapse;
            background: #fcfcfc;
        }
        tbody tr:nth-child(2n+3){
            background: #dedede;
        }
        table th{
            border: 2px solid #ffffff;
            border-bottom-color: #d4dde4;
            background: rgba(212, 221, 228, .5);
            padding:2px 8px 4px;
        }
        td{
            padding: 1px 10px;
        }

        table tr:nth-child(2n+2){
            border: 2px solid #ffffff;
            font-size: 1em;
            box-shadow: inset 0 -1px 0 0 rgba(212, 221, 228, .5);
            background: rgba(212,221,228,0.15);
            padding: 6px 8px 8px;
            letter-spacing: -0.3px;
        }
        
        .gvGrades tr:not(:first-child):not(:last-child):hover{
            background:#a1a1a1;
            color: white;
        }
        .gvGrades td:first-child:not(:last-child){
            padding: 3px 8px;
            background:#e87c5e;
            color: white;
            font-weight: bold;
        }
        .gvGrades td:first-child:not(:last-child):hover{
            background:#e87c5e;
            color: white;
            font-weight: bold;
        }*/
        /*table td:nth-child(1){
            background: #8FC4D7;
            font-weight: bold;
            text-align: center;
        }*/


        .lblHeadPercents
        {
            font-size: 22px;
            font-weight: bold;
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
        }*/
        

        /*percents*/
        .form-group{
            margin: 12px 0 0 0;
        }
        .pnltype1{
            margin:50px 0 0 0;
            padding: 20px;
            background: #b4ddec;
            border: 1px solid #8FC4D7;
            width: 330px;
        }
        
        .lblSumPercents{
            font-size: 20px;
        }
        .txtSumPercents{
            text-align: center;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">דוח ציונים</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server"><a href="Default.aspx">ראשי</a> » דוח ציונים
    <%if (Convert.ToInt32(Session["lvl_id"]) == 1 || Convert.ToInt32(Session["lvl_id"]) >= 3) {  
        %><br /><br />
        <asp:Button ID="btnInsert" runat="server" Text="+" CssClass="btn-plus-header" CausesValidation="False" OnClick="btnInsert_Click" />
    <%} %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="card-panel" style="margin: 40px 0 0 0;">
        <div class="filtersWrap">
            <h4>סינון</h4>
            <%if (Convert.ToInt32(Session["lvl_id"]) >= 4) { 
	            %>
            <asp:DropDownList ID="ddlSchools" runat="server" OnLoad="ddlSchools_Load" 
                OnSelectedIndexChanged="ddlSchools_SelectedIndexChanged"
                AutoPostBack="True"></asp:DropDownList>
            &nbsp;←&nbsp;
            <%} %>

            <%if (Convert.ToInt32(Session["lvl_id"]) >= 3) { 
	            %> 
            <asp:DropDownList ID="ddlLayers" runat="server" OnLoad="ddlLayers_Load" 
                OnSelectedIndexChanged="ddlLayers_SelectedIndexChanged" 
                AutoPostBack="True"></asp:DropDownList>
            &nbsp;<b>←</b>&nbsp;
            <asp:DropDownList ID="ddlProfessions" runat="server" OnLoad="ddlProfessions_Load" 
                OnSelectedIndexChanged="ddlProfessions_SelectedIndexChanged"
                AutoPostBack="True"></asp:DropDownList>
            &nbsp;<b>←</b>&nbsp;
            <asp:DropDownList ID="ddlTeachers" AppendDataBoundItems="false" runat="server" OnLoad="ddlTeachers_Load" 
                OnSelectedIndexChanged="ddlTeachers_SelectedIndexChanged"
                AutoPostBack="True"></asp:DropDownList>
            &nbsp;<b>←</b>&nbsp;
            <%} %>
            <asp:DropDownList ID="ddlLessons" runat="server" AppendDataBoundItems="false" onload="ddlLessons_Load" 
                onselectedindexchanged="ddlLessons_SelectedIndexChanged" 
                AutoPostBack="True" ClientIDMode="AutoID"></asp:DropDownList>
            <div class="clear"></div>
        </div>
        <br />
        <asp:Panel ID="pnlReport" Visible="false" runat="server">
            <asp:GridView ID="gvGrades" CssClass="gvGrades table-striped" AutoGenerateColumns="true" runat="server" OnLoad="gvGrades_Load" OnRowCreated="gvGrades_RowCreated" PageSize="20" AllowPaging="True" OnPageIndexChanging="gvGrades_PageIndexChanging"></asp:GridView>
            <br />
            הערה: עליך להגדיר אחוזי מבחנים לפני שתוכל/י לראות ציונים סופיים
            <br />
        
        </asp:Panel>
        <div class="clear"></div>
    </div>
    <%if (Convert.ToInt32(Session["lvl_id"]) == 1 || Convert.ToInt32(Session["lvl_id"]) >= 3) { 
	     %>
    <asp:Panel ID="pnlPercents" Visible="false" runat="server">
        <div class="card-panel">
            <h4>אחוזי מבחנים</h4>
            <br />
            <asp:GridView ID="gvPercents" CssClass="table-striped" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="שמות המבחנים">
                        <ItemTemplate>
                            <asp:Label ID="lbl_grd_name" runat="server" Text='<%# Bind("grd_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="שווי המבחן(ב-%)">
                        <ItemTemplate>
                            <div class="form-group">
                                <asp:TextBox CssClass="material" ID="txt_grd_percents" runat="server" Text='<%# Bind("grd_percents") %>' required="required" ></asp:TextBox>
                                <span class="form-highlight"></span>
                                <span class="form-bar"></span>
                                <asp:Label ID="Label1" AssociatedControlID="txt_grd_percents" CssClass="form-label" runat="server" Text="אחוזים"></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblComment" CssClass="lblComment" runat="server" Text="הערה: סך האחוזים צריכים להיות שווים ל 100 ומטה."></asp:Label> 
    <%--        <br />
            <asp:Label ID="lblSumPercents" CssClass="lblSumPercents" runat="server" Text="סכום:"></asp:Label>
            <asp:TextBox ID="txtSumPercents" CssClass="txtSumPercents" runat="server" Text="0" ReadOnly="true" Width="40" ></asp:TextBox>--%>
            <br />
            <asp:Label ID="lblErr" ForeColor="Red" runat="server"></asp:Label> 
            <br />
            <asp:Button ID="btnChangePercents" runat="server" 
                Text="עדכן אחוזים" CausesValidation="False" OnClick="btnChangePercents_Click" />
    
            <div class="clear"></div>
        </div>
    </asp:Panel>
    <%} %>
</asp:Content>

