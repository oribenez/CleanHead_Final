<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <style type="text/css">
        #default{
            fill: #fff !important;
        }
        .default:hover > #default{
            fill: #fff !important;
        }
        .default{
            color:#fff !important;
            background: #64B5F6 !important;
        }



        /*Survey*/
        .pnlSurveyFill {
            font-size: 1.1em;
        }
        .rblAnswers label{
            margin: 0 5px 0 0;
        }
          
        /*survey[END]*/
        .imgbtnNewSurvey{
            position: absolute;
            left: 20px;
            bottom: 20px;
        }
        .imgbtnEditSurvey{
            position: absolute;
            left: 50px;
            bottom: 20px;
        }
        .imgbtnDeleteSurvey{
            position: absolute;
            left: 80px;
            bottom: 20px;
        }

        /*תקציר לדף הבית*/
        a.a-card-panel{
            display:inline-block;
            width: 32%;
        }
        a.bhv{
            margin: 0 1.3rem;
        }
        .bhvBlockAbstract{
            background:#7986cb;
            color: #e8eaf6;
            position:relative;
            overflow: hidden;
            display:block;
            margin: 0;
        }
        .hwBlockAbstract{
            background:#66BB6A;
            color: #E8F5E9;
            position:relative;
            overflow: hidden;
            display:block;
            margin: 0;
        }
        .lblValueAbstract{
            font-weight:bold;
            font-size: 32px;
            margin: 10px;
            line-height: 36px;
        }
        .lblNameAbstract{

        }
        .icnAbstract{
            left: 0;
            bottom: -40px;
            position: absolute;
        }
        
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">ראשי</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server">ראשי</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="card-panel float-right" style=" width: 32%; position:relative;">
        <h4>סקר</h4>
        <asp:Panel ID="pnlSurvey" runat="server">
            <asp:Panel ID="pnlSurveyFill" CssClass="pnlSurveyFill" runat="server" Visible="true">
                <%if (Convert.ToInt32(Session["lvl_id"]) >= 4) { %>
                    <asp:ImageButton ID="imgbtnEditSurvey" CssClass="imgbtnEditSurvey" runat="server" ImageUrl="~/Images/ic_edit_24px.png" ToolTip="עדכן סקר" OnClick="imgbtnEditSurvey_Click" />
                    <asp:ImageButton ID="imgbtnNewSurvey" CssClass="imgbtnNewSurvey" runat="server" ImageUrl="~/Images/ic_add_box_24px.png" ToolTip="סקר חדש" OnClick="imgbtnNewSurvey_Click" />
                <%} %>
                <div class="form-group">
                    <h5 style="font-weight:bold;"> שאלה: <asp:Label ID="lblQuestion" runat="server"></asp:Label></h5>
                    <asp:RadioButtonList ID="rblAnswers" CssClass="rblAnswers" style="margin: 0 30px 0 0;" runat="server"></asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="surveyFill" CssClass="err" Display="Dynamic" runat="server" ControlToValidate="rblAnswers" ErrorMessage="לא נבחרה תשובה<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
                </div>
                <asp:Button ID="btnSendAns" runat="server" Text="הצבע" ValidationGroup="surveyFill" OnClick="btnSendAns_Click" />
            </asp:Panel>

            <asp:Panel ID="pnlUpdateSurvey" runat="server" Visible="false">
                <div class="form-group">
                    <asp:TextBox CssClass="material" ID="txtQuestion" runat="server" required="required"></asp:TextBox>
                    <span class="form-highlight"></span>
                    <span class="form-bar"></span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="err" Display="Dynamic" ValidationGroup="surveyUpdate" ControlToValidate="txtQuestion" runat="server" ErrorMessage="הכנס את שאלת הסקר<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
                    <br />
                    <asp:Label ID="Label1" AssociatedControlID="txtQuestion" CssClass="form-label" runat="server" Text="שאלת הסקר"></asp:Label>
                </div>
                <asp:Panel ID="pnlTxtAnswers" runat="server" style="margin: 0 30px 0 0;"></asp:Panel>
                <asp:Label ID="lblErr" CssClass="" runat="server" Text=""></asp:Label>
                <asp:Button ID="btnCancel" runat="server" Text="בטל" OnClick="btnCancel_Click" />
                <asp:Button ID="btnUpdateSrv" runat="server" Text="עדכן" ValidationGroup="surveyUpdate" OnClick="btnUpdateSrv_Click" />
            </asp:Panel>

            <asp:Panel ID="pnlNewSurvey" runat="server" Visible="false">
                <h5>סקר חדש</h5>
                
                <div class="form-group">
                    <asp:TextBox CssClass="material" ID="txtNewQuestion" runat="server" required="required"></asp:TextBox>
                    <span class="form-highlight"></span>
                    <span class="form-bar"></span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="err" Display="Dynamic" ValidationGroup="surveyNew" ControlToValidate="txtQuestion" runat="server" ErrorMessage="הכנס את שאלת הסקר<i class='err'><svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M0 0h24v24h-24z' fill='none'/><path d='M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z'/></svg></i>"></asp:RequiredFieldValidator>
                    <br />
                    <asp:Label ID="Label2" AssociatedControlID="txtNewQuestion" CssClass="form-label" runat="server" Text="שאלת הסקר"></asp:Label>
                </div>
                <asp:Panel ID="pnlTxtNewAnswers" runat="server" style="margin: 0 30px 0 0;"></asp:Panel>
                <h6><b>חובה לקרוא</b><br /> יצירת סקר חדש תביא למחיקה לצמיתות של הסקר הקודם</h6>
                <asp:Label ID="lblErr_new" CssClass="" runat="server" Text=""></asp:Label>
                <asp:Button ID="btnCancelNewSurvey" runat="server" Text="בטל" OnClick="btnCancelNewSurvey_Click" />
                <asp:Button ID="btnMakeSrv" runat="server" Text="צור סקר" ValidationGroup="surveyNew" OnClick="btnMakeSrv_Click" />
            </asp:Panel>

            <asp:Panel ID="pnlSurveyAfterFill" runat="server" Visible="false">
                <%if (Convert.ToInt32(Session["lvl_id"]) >= 4) { %>
                    <asp:ImageButton ID="ImageButton1" CssClass="imgbtnEditSurvey" runat="server" ImageUrl="~/Images/ic_edit_24px.png" ToolTip="עדכן סקר" OnClick="imgbtnEditSurvey_Click" />
                    <asp:ImageButton ID="ImageButton2" CssClass="imgbtnNewSurvey" runat="server" ImageUrl="~/Images/ic_add_box_24px.png" ToolTip="סקר חדש" OnClick="imgbtnNewSurvey_Click" />
                <%} %>
                <div class="form-group">
                    <h5 style="font-weight:bold;"> שאלה: <asp:Label ID="lblQuestionAfter" runat="server"></asp:Label></h5>
                    <asp:Panel ID="pnlStatsAnswers" style="width: 300px;border-right: 5px solid #DDD;margin: 0 30px 0 0;" runat="server"></asp:Panel>
                </div>
            </asp:Panel>
        </asp:Panel>
    </div>
    <%if (Convert.ToInt32(Session["lvl_id"]) == 0) { %>
        <a href="Behaviors.aspx" class="a-card-panel bhv">
            <div class="card-panel bhvBlockAbstract">    
                <asp:Label ID="lblBehavior" CssClass="lblValueAbstract" runat="server" Text="95"></asp:Label>
                <div class="lblNameAbstract">ציון בהתנהגות (החודש)</div>
                <i class="icnAbstract">
                    <svg xmlns="http://www.w3.org/2000/svg" width="120" height="120" viewBox="0 0 24 24">
                        <path d="M0 0h24v24h-24z" fill="none"/>
                        <path fill="#e8eaf6" d="M11.99 2c-5.52 0-9.99 4.48-9.99 10s4.47 10 9.99 10c5.53 0 10.01-4.48 10.01-10s-4.48-10-10.01-10zm.01 18c-4.42 0-8-3.58-8-8s3.58-8 8-8 8 3.58 8 8-3.58 8-8 8zm3.5-9c.83 0 1.5-.67 1.5-1.5s-.67-1.5-1.5-1.5-1.5.67-1.5 1.5.67 1.5 1.5 1.5zm-7 0c.83 0 1.5-.67 1.5-1.5s-.67-1.5-1.5-1.5-1.5.67-1.5 1.5.67 1.5 1.5 1.5zm3.5 6.5c2.33 0 4.31-1.46 5.11-3.5h-10.22c.8 2.04 2.78 3.5 5.11 3.5z"/>
                    </svg>
                </i>
            </div>
        </a>
        <a href="Homework.aspx" class="a-card-panel">
            <div class="card-panel hwBlockAbstract">    
                <asp:Label ID="lblHomeworks" CssClass="lblValueAbstract" runat="server" Text="6"></asp:Label>
                <div class="lblNameAbstract">שיעורים להגשה</div>
                <i class="icnAbstract">
                    <svg xmlns="http://www.w3.org/2000/svg" width="120" height="120" viewBox="0 0 24 24">
                        <path d="M0 0h24v24h-24z" fill="none"/>
                        <path fill="#E8F5E9" d="M18 2h-12c-1.1 0-2 .9-2 2v16c0 1.1.9 2 2 2h12c1.1 0 2-.9 2-2v-16c0-1.1-.9-2-2-2zm-12 2h5v8l-2.5-1.5-2.5 1.5v-8z"/>
                    </svg>
                </i>
            </div>
        </a>
    <%} %>
    <div class="clear"></div>
</asp:Content>
