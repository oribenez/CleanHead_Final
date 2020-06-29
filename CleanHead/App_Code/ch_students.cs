using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ch_students
/// </summary>
public class ch_students
{
    public int usr_Id { get; set; } // מזהה משתמש(תלמיד)
    public int rm_Id { get; set; } // כיתת אם של התלמיד
    public string stu_Mom_Identity { get; set; } // ת.ז. של אם התלמיד
    public string stu_Mom_First_Name { get; set; } // שם פרטי של אם התלמיד
    public string stu_Mom_Cellphone { get; set; } // מספר טלפון נייד של אם התלמיד
    public string stu_Dad_Identity { get; set; } // ת.ז. של אב התלמיד
    public string stu_Dad_First_Name { get; set; } // שם פרטי של אב התלמיד
    public string stu_Dad_Cellphone { get; set; } // מספר טלפון נייד של אב התלמיד
}