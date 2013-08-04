using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Configuration;

using System.Data.SqlClient;
using kpa.developers.helper.Properties;
using kpa.Developer.Controller.Collection;
using kpa.Developer.Controller.Collection.Data;

namespace kpa.Developer.Controller.SqlClient
{
    public class SqlClient
    {
        SqlConnection sqlCon;
        DataTable dt;

        public bool Open()
        {
            sqlCon = new SqlConnection(Connection.inputString);
            try
            {
                if (IsOpen)
                {
                    Close();
                }
                sqlCon.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }

        public bool IsOpen
        {
            get
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Close()
        {
           if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Dispose();
                sqlCon.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        public columnCollections Select(string query, int[] index)
        {
            columnCollections columnCollection = new columnCollections();
            SqlCommand sqlCom = new SqlCommand(query, sqlCon);
            SqlDataReader sqlDr;
            try
            {
                if (!IsOpen)
                {
                    sqlCon.Open();
                }
                sqlDr = sqlCom.ExecuteReader();
                while (sqlDr.Read())
                {
                    columns l = new columns();
                    for (int i = 0; i < index.Length; i++)
                    {
                        l.sqlDr[i] = sqlDr[i].ToString();
                    }
                    columnCollection.Add(l);
                }
                if (IsOpen)
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return columnCollection;
        }

        public Int64 Count(string query)
        {
            SqlCommand sqlCom = new SqlCommand(query, sqlCon);
            SqlDataReader sqlDr;
            Int64 count = 0;
            try
            {
                if (!IsOpen)
                {
                    sqlCon.Open();
                }
                sqlDr = sqlCom.ExecuteReader();
                if (sqlDr.Read())
                {
                    count =  Convert.ToInt64(sqlDr[0]);
                }
                if (IsOpen)
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return count;
        }

        public DataTable DataTable(string query)
        {
            SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
            try
            {
                if (!IsOpen)
                {
                    sqlCon.Open();
                }
                dt = new DataTable("table");
                sqlDa.Fill(dt);
                dt = (dt.Rows.Count > 0) ? dt : null;
                if (IsOpen)
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

        public bool Execute(string query)
        {
            SqlCommand sqlCom = new SqlCommand(query, sqlCon);
            try
            {
                if (!IsOpen)
                {
                    sqlCon.Open();
                }
                sqlCom.ExecuteNonQuery();
                if (IsOpen)
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }
    }
}
