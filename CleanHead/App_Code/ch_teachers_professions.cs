using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ch_teachers_professions
/// </summary>
public class ch_teachers_professions
{
    public int pro_Id { get; set; } // מזהה המקצוע שעליו המורה הוכשר
    public int usr_Id { get; set; } // מזהה המשתמש - מורה

    public ch_teachers_professions() { }
    public ch_teachers_professions(int pro_id, int usr_id) {
        this.pro_Id = pro_id;
        this.usr_Id = usr_id;
    }
}