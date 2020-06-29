using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;

/// <summary>
/// Summary description for ADO
/// </summary>
public class Connect
{
    /// <returns>the connection string to the database</returns>
    static string GetConnection() {
        return @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\clean_head.accdb;Persist Security Info=True";
    }
    /// <param name="strSql">the sql query</param>
    /// <param name="tblName">the table</param>
    /// <returns>DataSet of data from a specific table by query</returns>
    public static DataSet GetData(string strSql, string tblName) {
        OleDbConnection con = new OleDbConnection(GetConnection());
        OleDbCommand cmd = new OleDbCommand(strSql, con);
        DataSet ds = new DataSet();
        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
        adapter.Fill(ds, tblName);
        return ds;

    }
    /// <summary>
    /// Do actions in database such as INSERT, UPDATE, DELETE
    /// </summary>
    /// <param name="strSql">the sql query</param>
    /// <param name="tblName">the table name</param>
    /// <returns>int of ht row that took an effect</returns>
    public static int DoAction(string strSql, string tblName)
    {
        OleDbConnection con = new OleDbConnection(GetConnection());
        OleDbCommand cmd = new OleDbCommand(strSql, con);

        con.Open();
        int row = cmd.ExecuteNonQuery();
        con.Close();

        return row;
    }
    /// <summary>
    /// Do a math action such as (MAX,MIN,COUNT,AVG)
    /// </summary>
    /// <param name="strSql">query of the math action</param>
    /// <param name="tblName">the table name</param>
    /// <returns>an object of the math result </returns>
    public static object MathAction(string strSql, string tblName)
    {
        OleDbConnection con = new OleDbConnection(GetConnection());
        OleDbCommand cmd = new OleDbCommand(strSql, con);

        con.Open();
        object result = cmd.ExecuteScalar();
        con.Close();

        return result;
    }
}