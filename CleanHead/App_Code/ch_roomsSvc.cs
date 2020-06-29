using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;

/// <summary>
/// Summary description for ch_roomsSvc
/// </summary>
public class ch_roomsSvc
{
    /// <summary>
    /// Add a new ch_rooms record to the database
    /// </summary>
    /// <param name="rm1">the new ch_room record you want to add to the database</param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string AddRoom(ch_rooms rm1)
    {
        string strSql1 = "SELECT COUNT(rm_id) FROM ch_rooms WHERE rm_name = '" + rm1.rm_Name + "' AND sc_id = " + rm1.sc_Id;
        int num = Convert.ToInt32(Connect.MathAction(strSql1, "ch_rooms"));

        if (num > 0)
            return "החדר כבר קיים";

        string strSql = "INSERT INTO ch_rooms(rm_name,sc_id)  VALUES('" + rm1.rm_Name + "', " + rm1.sc_Id + ")";
        Connect.DoAction(strSql, "ch_rooms");
        return "";
    }

    /// <returns>The number of ch_rooms instances in database</returns>
    public static int IsRoomExist(ch_rooms rm1)
    {
        string strSql = "SELECT COUNT(rm_id) FROM ch_rooms WHERE rm_name = '" + rm1.rm_Name + "' AND sc_id = " + rm1.sc_Id;
        return Convert.ToInt32(Connect.MathAction(strSql, "ch_rooms"));
    }
    /// <param name="old_id">the old_id</param>
    /// <param name="rm1"></param>
    /// <returns>The number of ch_rooms instances in database except of old_id</returns>
    public static int IsRoomExistUpdate(int old_id, ch_rooms rm1) {
        string strSql = "SELECT COUNT(rm_id) FROM ch_rooms WHERE rm_name = '" + rm1.rm_Name + "' AND sc_id = " + rm1.sc_Id + " AND rm_id <> " + old_id;
        return Convert.ToInt32(Connect.MathAction(strSql, "ch_rooms"));
    }

    /// <returns>DataSet of all rooms data</returns>
    public static DataSet GetRooms()
    {
        string strSql = "SELECT * FROM ch_rooms";
        DataSet ds = Connect.GetData(strSql, "ch_rooms");
        return ds;
    }
    public static DataSet GetRooms(int sc_id) {
        string strSql = "SELECT * FROM ch_rooms WHERE sc_id = " + sc_id;
        DataSet ds = Connect.GetData(strSql, "ch_rooms");
        return ds;
    }

    /// <summary>
    /// Delete room using room id
    /// </summary>
    /// <param name="id">rm_id statement</param>
    public static void DeleteRoomById(int id)
    {
        string strSql = "DELETE * FROM ch_rooms WHERE rm_id=" + id;
        Connect.DoAction(strSql, "ch_rooms");
    }

    /// <summary>
    /// Update room using its id
    /// </summary>
    /// <param name="id">rm_id statement</param>
    /// <param name="newRoom1">ch_rooms object</param>
    public static string UpdateRoomById(int id, ch_rooms newRoom1)
    {
        string strSql1 = "SELECT COUNT(rm_id) FROM ch_rooms WHERE rm_name = '" + newRoom1.rm_Name + "' AND sc_id = " + newRoom1.sc_Id + " AND rm_id <>" + id;
        int num = Convert.ToInt32(Connect.MathAction(strSql1, "ch_rooms"));

        if (num > 0)
            return "החדר כבר קיים";

        string strSql = "UPDATE ch_rooms SET rm_name='" + newRoom1.rm_Name + "' WHERE rm_id=" + id;
        Connect.DoAction(strSql, "ch_rooms");

        return "";
    }

    /// <summary>
    /// Get room id by room name
    /// </summary>
    /// <param name="name">room name</param>
    /// <returns>-1 if not exist or return the id if name exist</returns>
    public static int GetId(ch_rooms rm1)
    {
        string strSql = "SELECT COUNT(rm_id) FROM ch_rooms WHERE rm_name = '" + rm1.rm_Name + "' AND sc_id = " + rm1.sc_Id;
        int num = Convert.ToInt32(Connect.MathAction(strSql, "ch_rooms"));
        if (num > 0)
        {
            string strSql2 = "SELECT rm_id FROM ch_rooms WHERE rm_name = '" + rm1.rm_Name + "' AND sc_id = " + rm1.sc_Id;
            DataSet ds = Connect.GetData(strSql2, "ch_rooms");
            return Convert.ToInt32(ds.Tables["ch_rooms"].Rows[0][0].ToString());
        }
        return -1;
    }
    /// <returns>DataSet of classes that students are studying there, in other words active classes</returns>
    public static DataSet GetStuPrimaryClasses() {
        string strSql = "SELECT * FROM ch_rooms WHERE rm_id IN (SELECT rm_id FROM ch_students);";
        return Connect.GetData(strSql, "ch_rooms");
    }
    /// <returns>DataSet of classes that students are studying there, in other words active classes,
    /// filtered by a school
    /// </returns>
    public static DataSet GetStuPrimaryClasses(int sc_id)
    {
        string strSql = "SELECT * FROM ch_rooms WHERE rm_id IN (SELECT rm_id FROM ch_students) AND sc_id = " + sc_id + ";";
        return Connect.GetData(strSql, "ch_rooms");
    }
    /// <returns>DataSet of classes that students are studying there, in other words active classes,
    /// filtered by a school and layer
    /// </returns>
    public static DataSet GetStuPrimaryClasses(int sc_id, string layer) {
        string strSql = "SELECT * FROM ch_rooms WHERE rm_id IN (SELECT rm_id FROM ch_students) AND sc_id = " + sc_id + " AND MID(rm_name,1,INSTR(1,rm_name,' ')) = '" + layer + "';";
        return Connect.GetData(strSql, "ch_rooms");
    }
    /// <returns>get the layers of the classes
    /// </returns>
    public static string[] GetClassesLayers(int sc_id)
    {

        DataSet ds = GetStuPrimaryClasses(sc_id);
        string[] arrayLayers = new string[ds.Tables[0].Rows.Count];

        int k = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            arrayLayers[k] = dr["rm_name"].ToString().Substring(0, dr["rm_name"].ToString().LastIndexOf(' '));

            k++;
        }
        arrayLayers = arrayLayers.Distinct<string>().ToArray();
        return arrayLayers;
    }

    public static string GetLayer(int les_id) {
        string querySchool = "SELECT rm.rm_name ";
        querySchool += "FROM ((ch_lessons AS `les` ";
        querySchool += "INNER JOIN ch_students_lessons AS `stu_les` ON stu_les.les_id = les.les_id) ";
        querySchool += "INNER JOIN ch_students AS `stu` ON stu.usr_id = stu_les.usr_id) ";
        querySchool += "INNER JOIN ch_rooms AS `rm` ON rm.rm_id = stu.rm_id ";
        querySchool += "WHERE les.les_id = " + les_id;

        string layer = Connect.GetData(querySchool, "ch_lessons").Tables[0].Rows[0][0].ToString();
        return layer.Substring(0, layer.LastIndexOf(' '));
    }
}