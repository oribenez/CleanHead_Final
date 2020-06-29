<%@ Application Language="C#" %>
<%@ Import Namespace="System.Data" %>
<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        DataRow dr = SurveySvc.GetSurvey(1);
        Application["srv_ans1_score"] = dr["srv_ans1_score"].ToString();
        Application["srv_ans2_score"] = dr["srv_ans2_score"].ToString();
        Application["srv_ans3_score"] = dr["srv_ans3_score"].ToString();
        Application["srv_ans4_score"] = dr["srv_ans4_score"].ToString();
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        // Save scores before delete Application
        SurveySvc.SurveySaveScore(Application["srv_ans1_score"].ToString(), Application["srv_ans2_score"].ToString(), Application["srv_ans3_score"].ToString(), Application["srv_ans4_score"].ToString());
    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs
        //var serverError = Server.GetLastError() as HttpException;

        //if (null != serverError)
        //{
        //    int errorCode = serverError.GetHttpCode();

        //    if (404 == errorCode)
        //    {
        //        Server.ClearError();
        //        Server.Transfer("/Errors/404Error.aspx");
        //    }
        //}

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
