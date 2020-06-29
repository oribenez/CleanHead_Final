using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Survey
/// </summary>
public class Survey
{
    public string srv_question { get; set; } // שאלת הסקר
    public string srv_ans1 { get; set; } // תשובה 1
    public string srv_ans2 { get; set; } // תשובה 2
    public string srv_ans3 { get; set; } // תשובה 3
    public string srv_ans4 { get; set; } // תשובה 4
    public Survey(string srv_question) {
        this.srv_question = srv_question;
    }
    public Survey(string srv_question, string srv_ans1, string srv_ans2, string srv_ans3, string srv_ans4)
	{
        this.srv_question = srv_question;
        this.srv_ans1 = srv_ans1;
        this.srv_ans2 = srv_ans2;
        this.srv_ans3 = srv_ans3;
        this.srv_ans4 = srv_ans4;
	}
}