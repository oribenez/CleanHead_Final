using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ch_students_behavior
/// </summary>
public class ch_students_behaviors : ch_behaviors
{
    public int les_id { get; set; } // מזהה השיעור בו בוצעה התנהגות התלמיד
    public DateTime stu_bhv_date { get; set; } // תאריך הדוח
    public int stu_id { get; set; } // מזהה תלמיד שביצע התנהגות
    public int hr_id { get; set; } // מזהה השעה

	public ch_students_behaviors() : base() { }
    public ch_students_behaviors(int bhv_id, int stu_id) : base(bhv_id) {
        this.stu_id = stu_id;
    }
    public ch_students_behaviors(int bhv_id, int les_id, DateTime stu_bhv_date, int stu_id, int hr_id) : base(bhv_id) {
        this.les_id = les_id;
        this.stu_bhv_date = stu_bhv_date;
        this.stu_id = stu_id;
        this.hr_id = hr_id;
    }
    public bool IsEqual(ch_students_behaviors stu_bhv) {
        if (this.stu_id != stu_bhv.stu_id) { return false; }
        if (this.bhv_id != stu_bhv.bhv_id) { return false; }
        return true;
    }
}