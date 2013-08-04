using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Configuration;


using MySql.Data.MySqlClient;
using kpa.developers.helper.Properties;
using kpa.Developer.Controller.Collection;
using kpa.Developer.Controller.Collection.Data;

namespace kpa.Developer.Controller.MySqlClient
{
    public class MySqlClient
    {
        MySqlConnection mysqlCon;
        DataTable dt;

        public bool Open()
        {
            mysqlCon = new MySqlConnection(Connection.inputString);
            try
            {
                if (IsOpen)
                {
                    Close();
                }
                mysqlCon.Open();
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
                if (mysqlCon.State == ConnectionState.Open)
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
            if (mysqlCon.State == ConnectionState.Open)
            {
                mysqlCon.Dispose();
                mysqlCon.Close();
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
            MySqlCommand mysqlCom = new MySqlCommand(query, mysqlCon);
            MySqlDataReader mysqlDr;
            try
            {
                if (!IsOpen)
                {
                    Open();
                }
                mysqlDr = mysqlCom.ExecuteReader();
                while (mysqlDr.Read())
                {
                    columns l = new columns();
                    for (int i = 0; i < index.Length; i++)
                    {
                        l.sqlDr[i] = mysqlDr[i].ToString();
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
            MySqlCommand mysqlCom = new MySqlCommand(query, mysqlCon);
            MySqlDataReader mysqlDr;
            Int64 count = 0;
            try
            {
                if (!IsOpen)
                {
                    Open();
                }
                mysqlDr = mysqlCom.ExecuteReader();
                if (mysqlDr.Read())
                {
                    count = Convert.ToInt64(mysqlDr[0]);
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
            MySqlDataAdapter mysqlDa = new MySqlDataAdapter(query, mysqlCon);
            try
            {
                if (!IsOpen)
                {
                    Open();
                }
                dt = new DataTable("table");
                mysqlDa.Fill(dt);
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
            MySqlCommand mysqlCom = new MySqlCommand(query, mysqlCon);
            try
            {
                if (!IsOpen)
                {
                    Open();
                }
                mysqlCom.ExecuteNonQuery();
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
