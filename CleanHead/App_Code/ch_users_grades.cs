using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ch_users_grades
/// </summary>
public class ch_users_grades
{
    public int usr_Id { get; set; } // מזהה התלמיד
    public int grd_Id { get; set; } // מזהה המבחן
    public int grd_Num { get; set; } // ציון במבחן

    public ch_users_grades() { }
    public ch_users_grades(int usr_id, int grd_id, int grd_num) {
        this.usr_Id = usr_id;
        this.grd_Id = grd_id;
        this.grd_Num = grd_num;
    }
}