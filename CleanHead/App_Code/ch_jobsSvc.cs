using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ch_jobsSvc
/// </summary>
public class ch_jobsSvc
{
    /// <summary>
    /// Adds a new record of job to the database.
    /// </summary>
    /// <param name="job1">The job to add</param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string AddJob(ch_jobs job1)
    {
        string strSql1 = "SELECT COUNT(job_id) FROM ch_jobs WHERE job_name = '" + job1.job_Name + "'";
        int num = Convert.ToInt32(Connect.MathAction(strSql1, "ch_jobs"));

        if (num > 0)
            return "התפקיד כבר קיים";

        string strSql = "INSERT INTO ch_jobs(job_name)  VALUES('" + job1.job_Name + "')";
        Connect.DoAction(strSql, "ch_jobs");
        return "";
    }
    /// <returns>DataSet of all jobs data</returns>
    public static DataSet GetJobs()
    {
        string strSql = "SELECT * FROM ch_jobs";
        DataSet ds = Connect.GetData(strSql, "ch_jobs");
        return ds;
    }

    /// <summary>
    /// Delete job using job id
    /// </summary>
    /// <param name="id">job_id statement</param>
    public static void DeleteJobById(int id)
    {
        string strSql = "DELETE * FROM ch_jobs WHERE job_id=" + id;
        Connect.DoAction(strSql, "ch_jobs");
    }

    /// <summary>
    /// Update job using its id
    /// </summary>
    /// <param name="id">job_id statement</param>
    /// <param name="newJob1">ch_jobs object</param>
    public static string UpdateJobById(int id, ch_jobs newJob1)
    {
        string strSql1 = "SELECT COUNT(job_id) FROM ch_jobs WHERE job_name = '" + newJob1.job_Name + "' AND job_id <>" + id;
        int num = Convert.ToInt32(Connect.MathAction(strSql1, "ch_jobs"));

        if (num > 0)
            return "התפקיד כבר קיים";

        string strSql = "UPDATE ch_jobs SET job_name='" + newJob1.job_Name + "' WHERE job_id=" + id;
        Connect.DoAction(strSql, "ch_jobs");

        return "";
    }

    /// <summary>
    /// Get job id by job name
    /// </summary>
    /// <param name="name">job name</param>
    /// <returns>-1 if not exist or return the id if name exist</returns>
    public static int GetIdByJobName(string name)
    {
        string strSql = "SELECT COUNT(job_id) FROM ch_jobs WHERE job_name = '" + name + "'";
        int num = Convert.ToInt32(Connect.MathAction(strSql, "ch_jobs"));
        if (num > 0)
        {
            string strSql2 = "SELECT job_id FROM ch_jobs WHERE job_name = '" + name + "'";
            DataSet ds = Connect.GetData(strSql2, "ch_jobs");
            return Convert.ToInt32(ds.Tables["ch_jobs"].Rows[0][0].ToString());
        }
        return -1;
    }
}