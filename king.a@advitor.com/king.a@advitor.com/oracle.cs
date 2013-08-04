//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data;
//using System.Data.OracleClient;

//using kpa.developers.helper.Properties;
//using kpa.Developer.Controller.Collection.Data;

//namespace kpa.Developer.Controller.OracleClient
//{
//    public class OracleClient
//    {
//        static OracleConnection sqlOra;
//        public static void credetials(string dataSource)
//        {
//            sqlOra = new OracleConnection(dataSource);
//            try
//            {
//                sqlOra.Open();
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//        }

//        public static bool IsOpen
//        {
//            get{
//                if (sqlOra.State == ConnectionState.Open){
//                    return true;
//                }
//                else{
//                    return false;
//                }
//            }
//        }

//        public columnCollections Select(string query, int[] column)
//        {
//            columnCollections columnCollection = new columnCollections();
//            OracleCommand sqlCom = new OracleCommand(query, sqlOra);
//            OracleDataReader sqlDr;
//            try
//            {
//                if (!IsOpen)
//                {
//                    sqlOra.Open();
//                }
//                sqlDr = sqlCom.ExecuteReader();
//                while (sqlDr.Read())
//                {
//                    columns field = new columns();
//                    if (column.Length == 1)
//                    {
//                        field.Column1 = sqlDr[column[0]].ToString();
//                    }
//                    else if (column.Length == 2)
//                    {
//                        field.Column1 = sqlDr[column[0]].ToString();
//                        field.Column2 = sqlDr[column[1]].ToString();
//                    }
//                    else if (column.Length == 3)
//                    {
//                        field.Column1 = sqlDr[column[0]].ToString();
//                        field.Column2 = sqlDr[column[1]].ToString();
//                        field.Column3 = sqlDr[column[2]].ToString();
//                    }
//                    else if (column.Length == 4)
//                    {
//                        field.Column1 = sqlDr[column[0]].ToString();
//                        field.Column2 = sqlDr[column[1]].ToString();
//                        field.Column3 = sqlDr[column[2]].ToString();
//                        field.Column4 = sqlDr[column[3]].ToString();
//                    }
//                    else if (column.Length == 5)
//                    {
//                        field.Column1 = sqlDr[column[0]].ToString();
//                        field.Column2 = sqlDr[column[1]].ToString();
//                        field.Column3 = sqlDr[column[2]].ToString();
//                        field.Column4 = sqlDr[column[3]].ToString();
//                        field.Column5 = sqlDr[column[4]].ToString();
//                    }
//                    else if (column.Length == 6)
//                    {
//                        field.Column1 = sqlDr[column[0]].ToString();
//                        field.Column2 = sqlDr[column[1]].ToString();
//                        field.Column3 = sqlDr[column[2]].ToString();
//                        field.Column4 = sqlDr[column[3]].ToString();
//                        field.Column5 = sqlDr[column[4]].ToString();
//                        field.Column6 = sqlDr[column[5]].ToString();
//                    }
//                    else if (column.Length == 7)
//                    {
//                        field.Column1 = sqlDr[column[0]].ToString();
//                        field.Column2 = sqlDr[column[1]].ToString();
//                        field.Column3 = sqlDr[column[2]].ToString();
//                        field.Column4 = sqlDr[column[3]].ToString();
//                        field.Column5 = sqlDr[column[4]].ToString();
//                        field.Column6 = sqlDr[column[5]].ToString();
//                        field.Column7 = sqlDr[column[6]].ToString();
//                    }
//                    else if (column.Length == 8)
//                    {
//                        field.Column1 = sqlDr[column[0]].ToString();
//                        field.Column2 = sqlDr[column[1]].ToString();
//                        field.Column3 = sqlDr[column[2]].ToString();
//                        field.Column4 = sqlDr[column[3]].ToString();
//                        field.Column5 = sqlDr[column[4]].ToString();
//                        field.Column6 = sqlDr[column[5]].ToString();
//                        field.Column7 = sqlDr[column[6]].ToString();
//                        field.Column8 = sqlDr[column[7]].ToString();
//                    }
//                    else if (column.Length == 9)
//                    {
//                        field.Column1 = sqlDr[column[0]].ToString();
//                        field.Column2 = sqlDr[column[1]].ToString();
//                        field.Column3 = sqlDr[column[2]].ToString();
//                        field.Column4 = sqlDr[column[3]].ToString();
//                        field.Column5 = sqlDr[column[4]].ToString();
//                        field.Column6 = sqlDr[column[5]].ToString();
//                        field.Column7 = sqlDr[column[6]].ToString();
//                        field.Column8 = sqlDr[column[7]].ToString();
//                        field.Column9 = sqlDr[column[8]].ToString();
//                    }
//                    else if (column.Length == 10)
//                    {
//                        field.Column1 = sqlDr[column[0]].ToString();
//                        field.Column2 = sqlDr[column[1]].ToString();
//                        field.Column3 = sqlDr[column[2]].ToString();
//                        field.Column4 = sqlDr[column[3]].ToString();
//                        field.Column5 = sqlDr[column[4]].ToString();
//                        field.Column6 = sqlDr[column[5]].ToString();
//                        field.Column7 = sqlDr[column[6]].ToString();
//                        field.Column8 = sqlDr[column[7]].ToString();
//                        field.Column9 = sqlDr[column[8]].ToString();
//                        field.Column10 = sqlDr[column[9]].ToString();
//                    }
//                    columnCollection.Add(field);
//                }
//                sqlDr.Dispose();
//                sqlDr.Close();
//                return columnCollection;
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//                return columnCollection = null;
//            }
//        }
//    }
//}