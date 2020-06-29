<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="ControlPanel.aspx.cs" Inherits="ControlPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        /*------------Side-Bar------------*/
        .control_panel{
            color:#fff !important;
            background: #64B5F6 !important;
        }

        /*------------Content------------*/
        .wrap_sec{
            float:right;
            margin: 0 0 0 50px;
        }

        .header{
            border-bottom: 2px #d7d7d7 solid;
            height: 46px;
            padding: 5px 10px;
        }

        #register_img{
            background: url(Images/register.png) no-repeat;
            width: 44px;
            height: 32px;
            float: right;

        }

        
        #data_img{
            background: url(Images/data.png) no-repeat;
            width: 44px;
            height: 32px;
            float:right;
        }
        #lesson_img{
            background: url(Images/lessons.png) no-repeat;
            width: 44px;
            height: 32px;
            float:right;
        }

        .header_txt h1{
            margin: 0;
            font-size: 36px;
        }
        .header_txt{
            float: right;
            display: block;
            margin: -6px 12px 0 0;
        }
        h3{
            margin: 0;
            font-size: 20px;
            color: #383838;
        }

        .links_txt{
            display: block;
            margin: 0 20px 0 0;
            font-size: 18px;
            cursor: default;
            font-weight: bold;
        }
        .links_txt a {
            color: #646361;
            text-decoration: none;
            font-size: 18px;
            font-weight: normal;
        }
        .links_txt a:hover {
            color: #ea6a46;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">פאנל ניהול</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
    <div class="card-panel">
        <b>רמת משתמש: </b><asp:Label ID="lbl_lvl_name" runat="server"></asp:Label><br />
        <%if (Convert.ToInt32(Session["lvl_id"]) >= 3) {
              %>
        <div class="wrap_sec">
            <div class="header">
                <div id="register_img"></div>
                <div class="header_txt">
                    <h1>הרשמה</h1>
                </div>
            </div>
            <div class="links_txt">
                •&nbsp;<a href="RegisterCrw.aspx">הרשמת איש צוות</a><br />
                •&nbsp;<a href="RegisterTch.aspx">הרשמת מורה</a><br />
                •&nbsp;<a href="RegisterStu.aspx">הרשמת תלמיד</a><br />
            </div>
        </div>

        <div class="wrap_sec">
            <div class="header">
                <div id="data_img"></div>
                <div class="header_txt">
                    <h1>מידע</h1>
                </div>
            </div>
            <div class="links_txt">
                <h3>מידע משתמשים</h3>
                <div class="links_txt">
                    •&nbsp;<a href="CrewData.aspx">אנשי הצוות</a><br />
                    •&nbsp;<a href="TeachersData.aspx">מורים</a><br />
                    •&nbsp;<a href="StudentsData.aspx">תלמידים</a><br />
                </div>
                <h3>מידע בסיסי</h3>
                <div class="links_txt">
                    <%if (Convert.ToInt32(Session["lvl_id"]) >= 4) { %>
                    •&nbsp;<a href="SchoolsData.aspx">בתי ספר</a><br />
                    <%if (Convert.ToInt32(Session["lvl_id"]) >= 5) { %>
                        •&nbsp;<a href="LevelsData.aspx">רמות משתמשים</a><br />
                    <%} %>
                    •&nbsp;<a href="CitiesData.aspx">ערים</a><br />
                    •&nbsp;<a href="BehaviorsTypes.aspx">סוגי ההתנהגויות</a><br />
                    <%} %>
                    •&nbsp;<a href="RoomsData.aspx">חדרים פיזיים</a><br />
                    •&nbsp;<a href="ProfessionsData.aspx">מקצועות</a><br />
                    •&nbsp;<a href="JobsData.aspx">עבודות אנשי צוות</a><br />
                    •&nbsp;<a href="HoursData.aspx">שעות בית ספריות</a><br />
                </div>
            </div>
        </div>
        <%} %>
        <div class="wrap_sec">
            <div class="header">
                <div id="lesson_img"></div>
                <div class="header_txt">
                    <h1>שיעורים</h1>
                </div>
            </div>
            <div class="links_txt">
                <%if (Convert.ToInt32(Session["lvl_id"]) == 1 || Convert.ToInt32(Session["lvl_id"]) >= 3) {
              %>
                •&nbsp;<a href="LessonsData.aspx">שיעורים</a><br />
                <%} %>

                <%if (Convert.ToInt32(Session["lvl_id"]) == 3) {
              %>
                •&nbsp;<a href="AddLesson.aspx">שיעור חדש</a><br />
                <%} %>
            </div>
        </div>
        <div class="clear"></div>
    </div>
</asp:Content>

