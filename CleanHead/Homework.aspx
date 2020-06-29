<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="Homework.aspx.cs" Inherits="Homework" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #homework{
            background: url(Images/homework_selected.png) no-repeat;
        }
        .homework:hover > #homework{
            background: url(Images/homework_selected.png) no-repeat;
        }
        .homework{
            color:#fff !important;
            background: #64B5F6 !important;
        }
        
        .pnlReport button, .pnlReport input[type="submit"] {
            background: none;
            position: absolute;
            width: inherit;
            outline: none;
        }

        .pnlReport button:hover, .pnlReport input[type="submit"]:hover {
            background: none;
        }

        /*-------------*/

         .hwWrap{
            display: inline-block;
            background: #66BB6A;
            box-shadow: 4px 2px #EEEEEE;
            overflow: hidden;
            margin: 40px 0 0 20px;
            width: 280px;
            height: 350px;
            cursor: pointer;
            -moz-transition: all 0.3s;
            -o-transition: all 0.3s;
            -webkit-transition: all 0.3s;
            transition: all 0.3s;
            color: #fff;
        }
            .hwWrap:hover {
                box-shadow: 5px 3px #B3B3B3;
                -moz-transition: all 0.3s;
                -o-transition: all 0.3s;
                -webkit-transition: all 0.3s;
                transition: all 0.3s;
            }
        .hwHeader {
          text-align: center;
          padding: 10px;
          background: #81C784;
          /*border-bottom: 6px solid #8FC4D7;*/
        }
        .hwContentWrap{
            padding: 16px
        }
        .hwContent {
            height: 180px;
            display: block;
            overflow: hidden;
            position:relative;
        }
        .lbl_pro_name{
            font-size: 30px;
            font-weight:bold;
        }
        .divide{
            width: 200px;
            height:2px;
            border-bottom:dotted 2px #fff;
            margin: 0 auto;
        }
        .divide2{
            width: 150px;
            height:1px;
            border-bottom:dotted 1px #fff;
            margin: 2px auto;
        }
        /*.hwpubDate{
            text-align:left;
            font-size: 13px;
            margin: 0 0 0 7px;
            position:absolute;
        }*/

        .hwDeadlineDate {
            height: 30px;
            background: #81C784;
            text-align: center;
            margin: 0 auto;
            line-height: 16px;
            padding: 6px;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
        }
        .lbl_timeleft{
            font-size: 20px;
        }
        .hwFooter{
            font-size: 13px;
            color: #C8E6C9;
            text-align: left;
            padding: 0 8px;
        }
        .hwEditDate{
            float:right;
        }




        /*------------*/
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
        .popupContainer {
            background: #66BB6A;
            color: #fff;
            overflow:auto;
            z-index: 999;
            position: fixed;
            top: 50%;
            left: 50%;
            -moz-transform: translate(-50%, -50%);
            -ms-transform: translate(-50%, -50%);
            -o-transform: translate(-50%, -50%);
            -webkit-transform: translate(-50%, -50%);
            transform: translate(-50%, -50%);
            min-width: 30vw;
            max-width: 45vw;
        }
        .popupContainer .hwContent {
            overflow: auto;
            max-height: 50vh;
            min-height: 30vh;
        }

        .btnBiggerHw {
            position:absolute;
            width: inherit;
            height:inherit;
            background: none;
            border: none;
            z-index:100;
            cursor:pointer;
        }

        .btn_edit_hw{
            float:right;
        }
         
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" Runat="Server">
    <asp:Panel ID="popup" runat="server">
        <asp:Panel ID="popupBack" CssClass="popupBack" runat="server">
            <asp:LinkButton ID="btnClosePopup" OnClick="btnClosePopup_Click" Height="100%" Width="100%" runat="server"></asp:LinkButton>
        </asp:Panel>
        <asp:Panel ID="popupContainer" CssClass="popupContainer" runat="server">
            <div class="hwHeader">
                <%if (Convert.ToInt32(Session["lvl_id"]) > 0) { 
	         %>
                <asp:ImageButton ID="btn_edit_hw" CssClass="btn_edit_hw" runat="server" CausesValidation="False" ImageUrl="~/Images/ic_edit_24px.png" Width="25" Text="ערוך" OnClick="btn_edit_hw_Click" />
                <%} %>
                <asp:Label ID="lbl_pro_name" CssClass="lbl_pro_name" runat="server" Text=''></asp:Label>
                <br />
                <asp:Label ID="lbl_les_name" CssClass="lbl_les_name" runat="server" Text=''></asp:Label>
            </div>

            <div class="hwContentWrap">
                <div class="hwContent">
                    <asp:Label ID="lbl_txt" runat="server" CssClass="lbl_txt" Text=''></asp:Label>
                </div>             
            </div>              
            <div class="hwDeadlineDate">
                    <asp:Label ID="lbl_timeleft" CssClass="lbl_timeleft" runat="server" Text=''></asp:Label>
                </div>
            <div class="hwFooter">
                <b>להגשה ב: </b>
                    <asp:Label ID="lbl_deadlinedate" runat="server" Text=''></asp:Label>
                    ב<asp:Label ID="lbl_hr_name" runat="server" Text=''></asp:Label>
                <div class="hwEditDate">
                    <asp:Label ID="lbl_editdate" runat="server" Text=''></asp:Label>
                </div>
            </div>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">שיעורי בית</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server"><a href="Default.aspx">ראשי</a> » שיעורי בית
    <%if (Convert.ToInt32(Session["lvl_id"]) == 1 || Convert.ToInt32(Session["lvl_id"]) >= 3) {  %>
        <br /><br />
        <asp:Button ID="btnInsert" runat="server" Text="+" CssClass="btn-plus-header" CausesValidation="False" OnClick="btnInsert_Click" />
      <%} %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card-panel" style="margin:40px 0 0 0;">      
        <h4>סינון</h4>
        <div class="filtersWrap">
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
            <asp:DropDownList ID="ddlLessons" runat="server" AppendDataBoundItems="false" OnLoad="ddlLessons_Load" 
                onselectedindexchanged="ddlLessons_SelectedIndexChanged" 
                AutoPostBack="True" ClientIDMode="AutoID"></asp:DropDownList>
        </div>
        <br />  

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        
    </div>
    
        <asp:Panel ID="pnlReport" CssClass="pnlReport" runat="server">
            <asp:DataList ID="dlHw" runat="server" OnLoad="dlHw_Load" RepeatDirection="Horizontal" RepeatLayout="Flow" OnItemDataBound="dlHw_ItemDataBound">
                <ItemTemplate>
                    <asp:Panel CssClass="hwWrap" ID="hwWrap" ToolTip='<%# Bind("hw_id") %>' runat="server">
                        <asp:Button ID="btnBiggerHw" CssClass="btnBiggerHw" OnClick="btnBiggerHw_Click" runat="server" />
                        <div class="hwHeader">
                            <asp:Label ID="lbl_pro_name" CssClass="lbl_pro_name" runat="server" Text='<%# Bind("pro_name") %>'></asp:Label>
                            <br />
                            <asp:Label ID="lbl_les_name" CssClass="lbl_les_name" runat="server" Text='<%# Bind("les_name") %>'></asp:Label>
                        </div>
                        <div class="hwContentWrap">
                            <div class="hwContent">
                                <asp:Label ID="lbl_txt" runat="server" CssClass="lbl_txt" Text='<%# Bind("hw_txt") %>'></asp:Label>
                            </div>
                        </div>
                        <div class="hwDeadlineDate">
                            <asp:Label ID="lbl_timeleft" CssClass="lbl_timeleft" runat="server" Text='<%# Bind("hw_timeleft") %>'></asp:Label>
                        </div>
                        <div class="hwFooter">
                            <b>להגשה ב: </b>
                                <asp:Label ID="lbl_deadlinedate" runat="server" Text='<%# Bind("hw_deadlinedate") %>'></asp:Label>
                                ב<asp:Label ID="lbl_hr_name" runat="server" Text='<%# Bind("hr_name") %>'></asp:Label>
                            <div class="hwEditDate">
                                <b>נערך ב: </b>
                                <asp:Label ID="lbl_editdate" runat="server" Text='<%# Bind("hw_editdate") %>'></asp:Label>
                            </div>
                        </div>
                    </asp:Panel>
                </ItemTemplate>
            </asp:DataList>
            <br />
            <br />
            <asp:LinkButton ID="lnkbtnPrevPage" CssClass="pager lnkbtnPrevPage" runat="server" OnClick="lnkbtnPrevPage_Click">הקודם</asp:LinkButton>
            <asp:DataList ID="dlPaging" runat="server" OnItemCommand="dlPaging_ItemCommand" OnItemDataBound="dlPaging_ItemDataBound" RepeatDirection="Horizontal" RepeatLayout="Flow"> 
                <ItemTemplate> 
                    <asp:LinkButton ID="lnkbtnPaging" CssClass="pager" runat="server" CommandArgument='<%# Eval("PageIndex") %>' CommandName="lnkbtnPaging" Text='<%# Eval("PageText") %>'></asp:LinkButton> 
                </ItemTemplate> 
            </asp:DataList>
            <asp:LinkButton ID="lnkbtnNextPage" CssClass="pager lnkbtnNextPage" runat="server" OnClick="lnkbtnNextPage_Click">הבא</asp:LinkButton>
        </asp:Panel>
    
</asp:Content>

