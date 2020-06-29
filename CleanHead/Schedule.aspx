<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="Schedule.aspx.cs" Inherits="Schedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #schedule{
            background: url(Images/schedule_selected.png) no-repeat;
        }
        .schedule:hover > #schedule{
            background: url(Images/schedule_selected.png) no-repeat;
        }
        .schedule{
            color:#fff !important;
            background: #64B5F6 !important;
        }


                .calendarWrapper
        {
            /*background-color: #EA6A46;
            padding: 10px 0 0 0;*/
            margin: 25px 0 0 0;
            display: inline-block;
            float:right;

        }

        .myCalendar
        {
            background-color: #f2f2f2;
            width: 660px;
            border: none !important;
              font-size: 18px;
        }

        .myCalendar a
        {
            text-decoration: none;
        }

        .myCalendar .myCalendarTitle
        {
            font-weight: bold;
            height: 10px;
            line-height: 10px;
            background-color: #383838;
            color: #ffffff;
            border: none !important;
            font-size: 30px !important;
        }

        .myCalendar th.myCalendarDayHeader
        {
            height: 25px;
            background: #ddd;
        }

        .myCalendar tr
        {
            border-bottom: solid 1px #ddd;
        }

        .myCalendar table tr
        {
            border-bottom: none !important;
        }

        .myCalendar tr:last-child td
        {
            border-bottom: none;
        }
                .myCalendar tr td.myCalendarDay {
                    text-align: right;
                    background: #fff;
                }

        .myCalendar tr td.myCalendarDay, .myCalendar tr th.myCalendarDayHeader
        {
            border-right: solid 1px #ddd;
        }

        /*.myCalendar tr td:last-child.myCalendarDay, .myCalendar tr th:last-child.myCalendarDayHeader
        {
            border-right: none;
        }*/

        .myCalendar td.myCalendarDay:nth-child(7) a
        {
            color: #c52e2e !important;
            font-weight: bold;
        }

        .myCalendar .myCalendarNextPrev
        {
            text-align: center;
        }

        .myCalendar .myCalendarNextPrev a
        {
            font-size: 1px;
        }

        .myCalendar .myCalendarNextPrev:nth-child(1) a
        {
            color: #ef5350!important;
            font-size: 43px;
            padding: 25px;
        }

            .myCalendar .myCalendarNextPrev:nth-child(1) a:hover, .myCalendar .myCalendarNextPrev:nth-child(3) a:hover
            {
                background-color: transparent;
            }

        .myCalendar .myCalendarNextPrev:nth-child(3) a
        {
            color: #ef5350 !important;
            font-size: 43px;
            padding: 25px;
        }

        .myCalendar td.myCalendarSelector
        {
            font-size: 18px !important;
            text-align:right;
        }
        .myCalendar td.myCalendarSelector a
        {
            background-color: #2196F3;
            
        }

        .myCalendar .myCalendarDayHeader a,
        .myCalendar .myCalendarDay a,
        .myCalendar .myCalendarSelector a,
        .myCalendar .myCalendarNextPrev a
        {
            display: block;
            padding: 0 10px 36px 0;
        }

        .myCalendar .myCalendarToday
        {
            background-color: #f2f2f2;
            -webkit-box-shadow: 0 0 9px 0 #8f8f8f inset;
            box-shadow: 0 0 9px 0 #8f8f8f inset;
            display: inline-block;
            width: 100% !important;
            height: 100% !important;
            position: relative;
        }

        .myCalendar .myCalendarToday a
        {
            color: #ef5350 !important;
            font-weight: bold;
        }

            .myCalendar .myCalendarToday a:after
            {
                content: "היום";
                color: #383838;
                font-size: 15px;
                display: inline-block;
                pointer-events: none;
                width: 100%;
                float: left;
            }

            .myCalendar .myCalendarDay a:hover,
            .myCalendar .myCalendarSelector a:hover {
                background-color: #eee;
                -moz-transition: 0.1s all;
                -o-transition: 0.1s all;
                -webkit-transition: 0.1s all;
                transition: 0.1s all;
            }

        /*---pnlclndrInfo---*/
        .pnlclndrInfo {
            display:inline-block;
            float:right;
            margin: 0 30px 0 0;
            width: 600px;
        }

        .lblName{
            background: #4CAF50;
            text-align: center;
            display: block;
            color: #fff;
            font-size: 14px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">מערכת שעות</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server"><a href="Default.aspx">ראשי</a> » מערכת שעות</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="calendarWrapper">
        <asp:Calendar ID="cldr1" runat="server" DayNameFormat="Full" NextMonthText="›" PrevMonthText="‹" SelectMonthText="»" SelectWeekText="›" CssClass="myCalendar" CellPadding="0" OnDayRender="cldr1_DayRender" OnSelectionChanged="cldr1_SelectionChanged">
            <OtherMonthDayStyle ForeColor="#b0b0b0" />
            <DayStyle CssClass="myCalendarDay" ForeColor="#2d3338" />
            <DayHeaderStyle CssClass="myCalendarDayHeader" ForeColor="#383838" />
            <SelectedDayStyle Font-Bold="True" Font-Size="12px" CssClass="myCalendarSelector" />
            <TodayDayStyle CssClass="myCalendarToday" />
            <SelectorStyle CssClass="myCalendarSelector" />
            <NextPrevStyle CssClass="myCalendarNextPrev" />
            <TitleStyle CssClass="myCalendarTitle" />
        </asp:Calendar>
    </div>
    <div class="card-panel float-right" style="margin: 25px 1.8rem 0 0;">    
        <h4>סינון לוח שנה</h4> 
            מגזר: <asp:DropDownList ID="ddlReligious" runat="server" OnLoad="ddlReligious_Load" AutoPostBack="True"></asp:DropDownList>
        <asp:Panel ID="pnlclndrInfo" CssClass="pnlclndrInfo flow-text" runat="server"></asp:Panel>
        <div class="clear"></div>
    </div>
    <div class="clear"></div>
</asp:Content>

