using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ch_lessonsSvc
/// </summary>
public class ch_lessonsSvc
{
    public static int GetIdByLesName(string les_name) {
        string strSql = "SELECT COUNT(les_id) FROM ch_lessons WHERE les_name = '" + les_name + "'";
        int num = Convert.ToInt32(Connect.MathAction(strSql, "ch_lessons"));
        if (num > 0) {
            string strSql2 = "SELECT les_id FROM ch_lessons WHERE les_name = '" + les_name + "'";
            DataSet ds = Connect.GetData(strSql2, "ch_lessons");
            return Convert.ToInt32(ds.Tables["ch_lessons"].Rows[0][0].ToString());
        }
        return -1;
    }
    public static int GetLessonSchoolId(int les_id) {
        string querySchool = "SELECT usr.sc_id ";
        querySchool += "FROM ((ch_lessons AS `les` ";
        querySchool += "INNER JOIN ch_students_lessons AS `stu_les` ON stu_les.les_id = les.les_id) ";
        querySchool += "INNER JOIN ch_students AS `stu` ON stu.usr_id = stu_les.usr_id) ";
        querySchool += "INNER JOIN ch_users AS `usr` ON usr.usr_id = stu.usr_id ";
        querySchool += "WHERE les.les_id = " + les_id;

        return Convert.ToInt32(Connect.GetData(querySchool, "ch_lessons").Tables[0].Rows[0][0]);
    }
    /// <returns>The last inserted lesson id</returns>
    public static int GetLastInsertId()
    {
        string strSql2 = "SELECT LAST(les_id) FROM ch_lessons";
        return Convert.ToInt32(Connect.MathAction(strSql2, "ch_lessons"));
    }
    /// <summary>
    /// Get DataSet of lessons and add 2 columns of students and teachers as:
    ///   teachers | students
    /// 2          | 5,6,1,3,8,7
    /// the lessons are filtered by school.
    /// </summary>
    /// <param name="sc_id">lessons in the specified school</param>
    /// <returns>DataSet of lessons</returns>
	public static DataSet GetLessons(int sc_id)
    {
        string lessonsQuery = "SELECT ";
        lessonsQuery += "les.les_id AS `מזהה שיעור`, ";
        lessonsQuery += "pro.pro_name AS `המקצוע הנלמד`, ";
        lessonsQuery += "les.les_name AS `שם השיעור` ";
        lessonsQuery += "FROM ((ch_lessons AS `les` INNER JOIN ch_professions AS `pro` ON pro.pro_id = les.pro_id) ";
        lessonsQuery += "INNER JOIN ch_teachers_lessons AS `tch_les` ON tch_les.les_id = les.les_id) ";
        lessonsQuery += "INNER JOIN ch_users AS `usr` ON usr.usr_id = tch_les.usr_id ";
        lessonsQuery += "WHERE sc_id = " + sc_id;
        DataSet ds_lessons = Connect.GetData(lessonsQuery, "ch_lessons");

        string teacherslessonsQuery = "SELECT ";
        teacherslessonsQuery += "les.les_id, ";
        teacherslessonsQuery += "usr.usr_id ";
        teacherslessonsQuery += "FROM ((ch_lessons AS `les` ";
        teacherslessonsQuery += "INNER JOIN ch_teachers_lessons AS `tch_les` ON tch_les.les_id = les.les_id) ";
        teacherslessonsQuery += "INNER JOIN ch_teachers AS `tch` ON tch_les.usr_id = tch.usr_id) ";
        teacherslessonsQuery += "INNER JOIN ch_users AS `usr` ON tch.usr_id = usr.usr_id";
        DataSet ds_teacherslessons = Connect.GetData(teacherslessonsQuery, "ch_lessons");

        string studentslessonsQuery = "SELECT ";
        studentslessonsQuery += "les.les_id, ";
        studentslessonsQuery += "usr.usr_id ";
        studentslessonsQuery += "FROM ((ch_lessons AS `les` ";
        studentslessonsQuery += "INNER JOIN ch_students_lessons AS `stu_les` ON stu_les.les_id = les.les_id) ";
        studentslessonsQuery += "INNER JOIN ch_students AS `stu` ON stu_les.usr_id = stu.usr_id) ";
        studentslessonsQuery += "INNER JOIN ch_users AS `usr` ON stu.usr_id = usr.usr_id";
        DataSet ds_studentslessons = Connect.GetData(studentslessonsQuery, "ch_lessons");

        ds_lessons.Tables[0].Columns.Add("מורים");
        ds_lessons.Tables[0].Columns.Add("תלמידים");
        for (int i = 0; i < ds_lessons.Tables[0].Rows.Count; i++)
        {
            for (int j = 0; j < ds_teacherslessons.Tables[0].Rows.Count; j++)
            {
                //if lessons ids are compared
                if (ds_lessons.Tables[0].Rows[i][0].ToString() == ds_teacherslessons.Tables[0].Rows[j][0].ToString())
                {
                    if (ds_lessons.Tables[0].Rows[i]["מורים"].ToString() == "")
                    {
                        ds_lessons.Tables[0].Rows[i]["מורים"] = ds_teacherslessons.Tables[0].Rows[j][1].ToString();
                    }
                    else
                    {
                        ds_lessons.Tables[0].Rows[i]["מורים"] += "," + ds_teacherslessons.Tables[0].Rows[j][1].ToString();
                    }
                }
            }

            for (int j = 0; j < ds_studentslessons.Tables[0].Rows.Count; j++)
            {
                //if lessons ids are compared
                if (ds_lessons.Tables[0].Rows[i][0].ToString() == ds_studentslessons.Tables[0].Rows[j][0].ToString())
                {
                    if (ds_lessons.Tables[0].Rows[i]["תלמידים"].ToString() == "")
                    {
                        ds_lessons.Tables[0].Rows[i]["תלמידים"] = ds_studentslessons.Tables[0].Rows[j][1].ToString();
                    }
                    else
                    {
                        ds_lessons.Tables[0].Rows[i]["תלמידים"] += "," + ds_studentslessons.Tables[0].Rows[j][1].ToString();
                    }
                }
             }
        }

        return ds_lessons;
    }

    /// <summary>
    /// Get DataSet of lessons and add 2 columns of students and teachers as:
    ///   teachers | students
    /// 2          | 5,6,1,3,8,7
    /// the lessons are filtered by school, layer, profession.
    /// </summary>
    /// <param name="sc_id">lessons in the specified school</param>
    /// <param name="layer">lessons in the specified layer</param>
    /// <param name="pro_id">lessons in the specified profession</param>
    /// <returns>DataSet of lessons</returns>
    public static DataSet GetLessonsGV(int sc_id, string layer, int pro_id) {
        string lessonsQuery = "SELECT ";
        lessonsQuery += "les.les_id AS `מזהה שיעור`, ";
        lessonsQuery += "MIN(pro.pro_name) AS `המקצוע הנלמד`, ";
        lessonsQuery += "MIN(les.les_name) AS `שם השיעור` ";
        lessonsQuery += "FROM (((((ch_lessons AS `les` INNER JOIN ch_professions AS `pro` ON pro.pro_id = les.pro_id) ";
        lessonsQuery += "INNER JOIN ch_teachers_lessons AS `tch_les` ON tch_les.les_id = les.les_id) ";
        lessonsQuery += "INNER JOIN ch_users AS `usr_tch` ON tch_les.usr_id = usr_tch.usr_id) ";
        lessonsQuery += "INNER JOIN ch_students_lessons AS `stu_les` ON les.les_id = stu_les.les_id) ";
        lessonsQuery += "INNER JOIN ch_students AS `stu` ON stu_les.usr_id = stu.usr_id) ";
        lessonsQuery += "INNER JOIN ch_rooms AS `rm` ON stu.rm_id = rm.rm_id ";
        lessonsQuery += "WHERE usr_tch.sc_id = " + sc_id + " ";
        lessonsQuery += "AND ";
        lessonsQuery += "MID(rm.rm_name,1,INSTR(1,rm.rm_name,' ')) = '" + layer + "' ";
        lessonsQuery += "AND ";
        lessonsQuery += "les.pro_id = " + pro_id + " ";
        lessonsQuery += "GROUP BY les.les_id";
        DataSet ds_lessons = Connect.GetData(lessonsQuery, "ch_lessons");

        string teacherslessonsQuery = "SELECT ";
        teacherslessonsQuery += "les.les_id, ";
        teacherslessonsQuery += "usr.usr_id ";
        teacherslessonsQuery += "FROM ((ch_lessons AS `les` ";
        teacherslessonsQuery += "INNER JOIN ch_teachers_lessons AS `tch_les` ON tch_les.les_id = les.les_id) ";
        teacherslessonsQuery += "INNER JOIN ch_teachers AS `tch` ON tch_les.usr_id = tch.usr_id) ";
        teacherslessonsQuery += "INNER JOIN ch_users AS `usr` ON tch.usr_id = usr.usr_id";
        DataSet ds_teacherslessons = Connect.GetData(teacherslessonsQuery, "ch_lessons");

        string studentslessonsQuery = "SELECT ";
        studentslessonsQuery += "les.les_id, ";
        studentslessonsQuery += "usr.usr_id ";
        studentslessonsQuery += "FROM ((ch_lessons AS `les` ";
        studentslessonsQuery += "INNER JOIN ch_students_lessons AS `stu_les` ON stu_les.les_id = les.les_id) ";
        studentslessonsQuery += "INNER JOIN ch_students AS `stu` ON stu_les.usr_id = stu.usr_id) ";
        studentslessonsQuery += "INNER JOIN ch_users AS `usr` ON stu.usr_id = usr.usr_id";
        DataSet ds_studentslessons = Connect.GetData(studentslessonsQuery, "ch_lessons");

        ds_lessons.Tables[0].Columns.Add("מורים");
        ds_lessons.Tables[0].Columns.Add("תלמידים");
        for (int i = 0; i < ds_lessons.Tables[0].Rows.Count; i++) {
            for (int j = 0; j < ds_teacherslessons.Tables[0].Rows.Count; j++) {
                //if lessons ids are compared
                if (ds_lessons.Tables[0].Rows[i][0].ToString() == ds_teacherslessons.Tables[0].Rows[j][0].ToString()) {
                    if (ds_lessons.Tables[0].Rows[i]["מורים"].ToString() == "") {
                        ds_lessons.Tables[0].Rows[i]["מורים"] = ds_teacherslessons.Tables[0].Rows[j][1].ToString();
                    }
                    else {
                        ds_lessons.Tables[0].Rows[i]["מורים"] += "," + ds_teacherslessons.Tables[0].Rows[j][1].ToString();
                    }
                }
            }

            for (int j = 0; j < ds_studentslessons.Tables[0].Rows.Count; j++) {
                //if lessons ids are compared
                if (ds_lessons.Tables[0].Rows[i][0].ToString() == ds_studentslessons.Tables[0].Rows[j][0].ToString()) {
                    if (ds_lessons.Tables[0].Rows[i]["תלמידים"].ToString() == "") {
                        ds_lessons.Tables[0].Rows[i]["תלמידים"] = ds_studentslessons.Tables[0].Rows[j][1].ToString();
                    }
                    else {
                        ds_lessons.Tables[0].Rows[i]["תלמידים"] += "," + ds_studentslessons.Tables[0].Rows[j][1].ToString();
                    }
                }
            }
        }

        return ds_lessons;
    }
    /// <summary>
    /// Get DataSet of lessons and add 2 columns of students and teachers as:
    ///   teachers | students
    /// 2          | 5,6,1,3,8,7
    /// the lessons are filtered by school, layer, profession, teacher.
    /// </summary>
    /// <param name="sc_id">lessons in the specified school</param>
    /// <param name="layer">lessons in the specified layer</param>
    /// <param name="pro_id">lessons in the specified profession</param>
    /// <param name="tch_id">lessons in the specified teacher</param>
    /// <returns>DataSet of lessons</returns>
    public static DataSet GetLessonsGV(int sc_id, string layer, int pro_id, int tch_id) {
        string lessonsQuery = "SELECT ";
        lessonsQuery += "les.les_id AS `מזהה שיעור`, ";
        lessonsQuery += "MIN(pro.pro_name) AS `המקצוע הנלמד`, ";
        lessonsQuery += "MIN(les.les_name) AS `שם השיעור` ";
        lessonsQuery += "FROM (((((ch_lessons AS `les` INNER JOIN ch_professions AS `pro` ON pro.pro_id = les.pro_id) ";
        lessonsQuery += "INNER JOIN ch_teachers_lessons AS `tch_les` ON tch_les.les_id = les.les_id) ";
        lessonsQuery += "INNER JOIN ch_users AS `usr_tch` ON tch_les.usr_id = usr_tch.usr_id) ";
        lessonsQuery += "INNER JOIN ch_students_lessons AS `stu_les` ON les.les_id = stu_les.les_id) ";
        lessonsQuery += "INNER JOIN ch_students AS `stu` ON stu_les.usr_id = stu.usr_id) ";
        lessonsQuery += "INNER JOIN ch_rooms AS `rm` ON stu.rm_id = rm.rm_id ";
        lessonsQuery += "WHERE usr_tch.sc_id = " + sc_id + " ";
        lessonsQuery += "AND ";
        lessonsQuery += "MID(rm.rm_name,1,INSTR(1,rm.rm_name,' ')) = '" + layer + "' ";
        lessonsQuery += "AND ";
        lessonsQuery += "les.pro_id = " + pro_id + " ";
        lessonsQuery += "AND ";
        lessonsQuery += "usr_tch.usr_id = " + tch_id + " ";
        lessonsQuery += "GROUP BY les.les_id";
        DataSet ds_lessons = Connect.GetData(lessonsQuery, "ch_lessons");

        string teacherslessonsQuery = "SELECT ";
        teacherslessonsQuery += "les.les_id, ";
        teacherslessonsQuery += "usr.usr_id ";
        teacherslessonsQuery += "FROM ((ch_lessons AS `les` ";
        teacherslessonsQuery += "INNER JOIN ch_teachers_lessons AS `tch_les` ON tch_les.les_id = les.les_id) ";
        teacherslessonsQuery += "INNER JOIN ch_teachers AS `tch` ON tch_les.usr_id = tch.usr_id) ";
        teacherslessonsQuery += "INNER JOIN ch_users AS `usr` ON tch.usr_id = usr.usr_id";
        DataSet ds_teacherslessons = Connect.GetData(teacherslessonsQuery, "ch_lessons");

        string studentslessonsQuery = "SELECT ";
        studentslessonsQuery += "les.les_id, ";
        studentslessonsQuery += "usr.usr_id ";
        studentslessonsQuery += "FROM ((ch_lessons AS `les` ";
        studentslessonsQuery += "INNER JOIN ch_students_lessons AS `stu_les` ON stu_les.les_id = les.les_id) ";
        studentslessonsQuery += "INNER JOIN ch_students AS `stu` ON stu_les.usr_id = stu.usr_id) ";
        studentslessonsQuery += "INNER JOIN ch_users AS `usr` ON stu.usr_id = usr.usr_id";
        DataSet ds_studentslessons = Connect.GetData(studentslessonsQuery, "ch_lessons");

        ds_lessons.Tables[0].Columns.Add("מורים");
        ds_lessons.Tables[0].Columns.Add("תלמידים");
        for (int i = 0; i < ds_lessons.Tables[0].Rows.Count; i++) {
            for (int j = 0; j < ds_teacherslessons.Tables[0].Rows.Count; j++) {
                //if lessons ids are compared
                if (ds_lessons.Tables[0].Rows[i][0].ToString() == ds_teacherslessons.Tables[0].Rows[j][0].ToString()) {
                    if (ds_lessons.Tables[0].Rows[i]["מורים"].ToString() == "") {
                        ds_lessons.Tables[0].Rows[i]["מורים"] = ds_teacherslessons.Tables[0].Rows[j][1].ToString();
                    }
                    else {
                        ds_lessons.Tables[0].Rows[i]["מורים"] += "," + ds_teacherslessons.Tables[0].Rows[j][1].ToString();
                    }
                }
            }

            for (int j = 0; j < ds_studentslessons.Tables[0].Rows.Count; j++) {
                //if lessons ids are compared
                if (ds_lessons.Tables[0].Rows[i][0].ToString() == ds_studentslessons.Tables[0].Rows[j][0].ToString()) {
                    if (ds_lessons.Tables[0].Rows[i]["תלמידים"].ToString() == "") {
                        ds_lessons.Tables[0].Rows[i]["תלמידים"] = ds_studentslessons.Tables[0].Rows[j][1].ToString();
                    }
                    else {
                        ds_lessons.Tables[0].Rows[i]["תלמידים"] += "," + ds_studentslessons.Tables[0].Rows[j][1].ToString();
                    }
                }
            }
        }

        return ds_lessons;
    }
    /// <summary>
    /// Get lessons by teacher in a specified school
    /// </summary>
    /// <param name="usr_id">user id of teacher you want to filter</param>
    /// <param name="sc_id">school id of school you want to filter</param>
    /// <returns>DataSet of lessons filtered by teacher and school</returns>
    public static DataSet GetLessonsByTeacher(int usr_id, int sc_id) {
        string lessonsQuery = "SELECT ";
        lessonsQuery += "les.les_id AS `מזהה שיעור`, ";
        lessonsQuery += "pro.pro_name AS `המקצוע הנלמד`, ";
        lessonsQuery += "les.les_name AS `שם השיעור` ";
        lessonsQuery += "FROM ((ch_lessons AS `les` INNER JOIN ch_professions AS `pro` ON pro.pro_id = les.pro_id) ";
        lessonsQuery += "INNER JOIN ch_teachers_lessons AS `tch_les` ON tch_les.les_id = les.les_id) ";
        lessonsQuery += "INNER JOIN ch_users AS `usr` ON tch_les.usr_id = usr.usr_id ";
        lessonsQuery += "WHERE tch_les.usr_id = " + usr_id + " AND usr.sc_id = " + sc_id;
        DataSet ds_lessons = Connect.GetData(lessonsQuery, "ch_lessons");

        string teacherslessonsQuery = "SELECT ";
        teacherslessonsQuery += "les.les_id, ";
        teacherslessonsQuery += "usr.usr_id ";
        teacherslessonsQuery += "FROM ((ch_lessons AS `les` ";
        teacherslessonsQuery += "INNER JOIN ch_teachers_lessons AS `tch_les` ON tch_les.les_id = les.les_id) ";
        teacherslessonsQuery += "INNER JOIN ch_teachers AS `tch` ON tch_les.usr_id = tch.usr_id) ";
        teacherslessonsQuery += "INNER JOIN ch_users AS `usr` ON tch.usr_id = usr.usr_id";
        DataSet ds_teacherslessons = Connect.GetData(teacherslessonsQuery, "ch_lessons");

        string studentslessonsQuery = "SELECT ";
        studentslessonsQuery += "les.les_id, ";
        studentslessonsQuery += "usr.usr_id ";
        studentslessonsQuery += "FROM ((ch_lessons AS `les` ";
        studentslessonsQuery += "INNER JOIN ch_students_lessons AS `stu_les` ON stu_les.les_id = les.les_id) ";
        studentslessonsQuery += "INNER JOIN ch_students AS `stu` ON stu_les.usr_id = stu.usr_id) ";
        studentslessonsQuery += "INNER JOIN ch_users AS `usr` ON stu.usr_id = usr.usr_id";
        DataSet ds_studentslessons = Connect.GetData(studentslessonsQuery, "ch_lessons");

        ds_lessons.Tables[0].Columns.Add("מורים");
        ds_lessons.Tables[0].Columns.Add("תלמידים");
        for (int i = 0; i < ds_lessons.Tables[0].Rows.Count; i++) {
            for (int j = 0; j < ds_teacherslessons.Tables[0].Rows.Count; j++) {
                //if lessons ids are compared
                if (ds_lessons.Tables[0].Rows[i][0].ToString() == ds_teacherslessons.Tables[0].Rows[j][0].ToString()) {
                    if (ds_lessons.Tables[0].Rows[i]["מורים"].ToString() == "") {
                        ds_lessons.Tables[0].Rows[i]["מורים"] = ds_teacherslessons.Tables[0].Rows[j][1].ToString();
                    }
                    else {
                        ds_lessons.Tables[0].Rows[i]["מורים"] += "," + ds_teacherslessons.Tables[0].Rows[j][1].ToString();
                    }
                }
            }

            for (int j = 0; j < ds_studentslessons.Tables[0].Rows.Count; j++) {
                //if lessons ids are compared
                if (ds_lessons.Tables[0].Rows[i][0].ToString() == ds_studentslessons.Tables[0].Rows[j][0].ToString()) {
                    if (ds_lessons.Tables[0].Rows[i]["תלמידים"].ToString() == "") {
                        ds_lessons.Tables[0].Rows[i]["תלמידים"] = ds_studentslessons.Tables[0].Rows[j][1].ToString();
                    }
                    else {
                        ds_lessons.Tables[0].Rows[i]["תלמידים"] += "," + ds_studentslessons.Tables[0].Rows[j][1].ToString();
                    }
                }
            }
        }

        return ds_lessons;
    }
    /// <summary>
    /// Get lessons of a specified teacher
    /// </summary>
    /// <param name="tch_id">the teacher you want to specify</param>
    /// <returns>DataSet of lessons of a specified teacher</returns>
    public static DataSet GetTeacherLessons(int tch_id) {
        string lessosnQuery = "SELECT ";
        lessosnQuery += "les.les_id AS `מזהה השיעור`, ";
        lessosnQuery += "les.les_name AS `שם השיעור` ";
        lessosnQuery += "FROM ch_teachers_lessons AS `tch_les` ";
        lessosnQuery += "INNER JOIN ch_lessons AS `les` ON les.les_id = tch_les.les_id ";
        lessosnQuery += "WHERE tch_les.usr_id = " + tch_id + " ";
        lessosnQuery += "ORDER BY les.les_name;";

        return Connect.GetData(lessosnQuery, "ch_teachers_lessons");
    }
    /// <summary>
    /// Delete lesson by his id
    /// </summary>
    /// <param name="les_id"></param>
    public static void DeleteLesson(int les_id)
    {
        string strSql1 = "DELETE * FROM ch_lessons WHERE les_id=" + les_id;
        Connect.DoAction(strSql1, "ch_lessons");
    }
    /// <summary>
    /// Add a new lesson
    /// </summary>
    /// <param name="newLes">The lesson you want to add</param>
    public static void AddLesson(ch_lessons newLes)
    {
        string strSql1 = "INSERT INTO ch_lessons(pro_id, les_name) VALUES(" + newLes.pro_Id + ",'" + newLes.les_Name + "')";
        Connect.DoAction(strSql1, "ch_lessons");
    }
    /// <summary>
    /// Get lesson by his id
    /// </summary>
    /// <param name="les_id">les_id of the lesson you want to get</param>
    /// <returns>DataRow of the specific lesson</returns>
    public static DataRow GetLesson(int les_id)
    {
        string selectQuery = "SELECT pro_id, les_name FROM ch_lessons WHERE les_id=" + les_id;
        return Connect.GetData(selectQuery, "ch_lessons").Tables[0].Rows[0];
    }
    /// <summary>
    /// Check if lesson exist by his id
    /// </summary>
    /// <param name="les_id">les_id of the lesson you want to get</param>
    /// <returns>true if lesson is found, false if not found</returns>
    public static bool IsExist(int les_id)
    {
        //check in students
        string strSql = "SELECT COUNT(les_id) FROM ch_lessons WHERE les_id=" + les_id;
        int num = Convert.ToInt32(Connect.MathAction(strSql, "ch_lessons"));
        if (num > 0)
            return true;
        return false;
    }
    /// <summary>
    /// Update a specific lesson by his id
    /// </summary>
    /// <param name="les_id">les_id of the lesson you want to update</param>
    /// <param name="newLes">the new lesson to update</param>
    public static void UpdateLesson(int les_id ,ch_lessons newLes)
    {
        string strSql1 = "UPDATE ch_lessons SET pro_id=" + newLes.pro_Id + ", les_name='" + newLes.les_Name + "' WHERE les_id=" + les_id;
        Connect.DoAction(strSql1, "ch_lessons");
    }

    /// <summary>
    /// Get lessons filtered by a specific student
    /// </summary>
    /// <param name="stu_id">stu_id of the student lessons</param>
    /// <returns>DataSet of lessons filtered by a specific student</returns>
    public static DataSet GetStudentLessons(int stu_id) {
        string lessosnQuery = "SELECT ";
        lessosnQuery += "les.les_id AS `מזהה השיעור`, ";
        lessosnQuery += "les.les_name AS `שם השיעור` ";
        lessosnQuery += "FROM ch_students_lessons AS `stu_les` ";
        lessosnQuery += "INNER JOIN ch_lessons AS `les` ON les.les_id = stu_les.les_id ";
        lessosnQuery += "WHERE stu_les.usr_id = " + stu_id + " ";
        lessosnQuery += "ORDER BY les.les_name;";

        return Connect.GetData(lessosnQuery, "ch_students_lessons");
    }
    /// <summary>
    /// Get lessons filtered by school, layer, profession
    /// </summary>
    /// <param name="sc_id">school id of school you want to specify</param>
    /// <param name="layer">layer id of layer you want to specify</param>
    /// <param name="pro_id">profession id of profession you want to specify</param>
    /// <returns>DataSet lessons filtered by school, layer, profession</returns>
    public static DataSet GetLessons(int sc_id, string layer, int pro_id) {
        string lessosnQuery = "SELECT ";
        lessosnQuery += "MIN(les.les_id) AS `מזהה השיעור`, ";
        lessosnQuery += "les.les_name ";
        lessosnQuery += "FROM ((((ch_lessons AS `les` ";
        lessosnQuery += "INNER JOIN ch_teachers_lessons AS `tch_les` ON les.les_id = tch_les.les_id) ";
        lessosnQuery += "INNER JOIN ch_students_lessons AS `stu_les` ON les.les_id = stu_les.les_id) ";
        lessosnQuery += "INNER JOIN ch_students AS `stu` ON stu_les.usr_id = stu.usr_id) ";
        lessosnQuery += "INNER JOIN ch_users AS `usr` ON stu.usr_id = usr.usr_id) ";
        lessosnQuery += "INNER JOIN ch_rooms AS `rm` ON stu.rm_id = rm.rm_id ";
        lessosnQuery += "WHERE usr.sc_id = " + sc_id + " ";
        lessosnQuery += "AND ";
        lessosnQuery += "MID(rm.rm_name,1,INSTR(1,rm.rm_name,' ')) = '" + layer + "' ";
        lessosnQuery += "AND ";
        lessosnQuery += "les.pro_id = " + pro_id + " ";
        lessosnQuery += "GROUP BY les.les_name ";
        lessosnQuery += "ORDER BY les.les_name;";

        return Connect.GetData(lessosnQuery, "ch_lessons");
    }
    /// <summary>
    /// Get lessons filtered by school, layer, profession, teacher
    /// </summary>
    /// <param name="sc_id">school id of school you want to specify</param>
    /// <param name="layer">layer id of layer you want to specify</param>
    /// <param name="pro_id">profession id of profession you want to specify</param>
    /// <param name="tch_id">teacher id of teacher you want to specify</param>
    /// <returns>DataSet lessons filtered by school, layer, profession,teacher</returns>
    public static DataSet GetLessons(int sc_id, string layer, int pro_id, int tch_id) {
        string lessosnQuery = "SELECT ";
        lessosnQuery += "MIN(les.les_id) AS `מזהה השיעור`, ";
        lessosnQuery += "les.les_name ";
        lessosnQuery += "FROM ((((ch_lessons AS `les` ";
        lessosnQuery += "INNER JOIN ch_teachers_lessons AS `tch_les` ON les.les_id = tch_les.les_id) ";
        lessosnQuery += "INNER JOIN ch_students_lessons AS `stu_les` ON les.les_id = stu_les.les_id) ";
        lessosnQuery += "INNER JOIN ch_students AS `stu` ON stu_les.usr_id = stu.usr_id) ";
        lessosnQuery += "INNER JOIN ch_users AS `usr` ON stu.usr_id = usr.usr_id) ";
        lessosnQuery += "INNER JOIN ch_rooms AS `rm` ON stu.rm_id = rm.rm_id ";
        lessosnQuery += "WHERE usr.sc_id = " + sc_id + " ";
        lessosnQuery += "AND ";
        lessosnQuery += "MID(rm.rm_name,1,INSTR(1,rm.rm_name,' ')) = '" + layer + "' ";
        lessosnQuery += "AND ";
        lessosnQuery += "les.pro_id = " + pro_id + " ";
        lessosnQuery += "AND ";
        lessosnQuery += "tch_les.usr_id = " + tch_id + " ";
        lessosnQuery += "GROUP BY les.les_name ";
        lessosnQuery += "ORDER BY les.les_name;";

        return Connect.GetData(lessosnQuery, "ch_lessons");
    }
}