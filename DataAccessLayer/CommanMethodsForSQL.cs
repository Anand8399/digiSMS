using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class CommanMethodsForSQL
    {

        private static SqlConnection sqlConnection;

        private static SqlCommand sqlCommand;

        private static SqlDataAdapter sqlDataAdapter;
        private static SqlConnection GetConnection()
        {
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnection"].ToString());
            conn.Open();

            return conn;
        }

        public static int ExecuteNonQuery(string QueryString)
        {
            int intResult = 0;
            try
            {
                //sqlConnection = new SqlConnection(OledbConnectionString);
                sqlConnection = GetConnection();
                if (sqlConnection.State != ConnectionState.Open)
                {
                    sqlConnection.Open();
                }
                sqlCommand = new SqlCommand(QueryString, sqlConnection);
                intResult = sqlCommand.ExecuteNonQuery();
                return intResult;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConnection.State != ConnectionState.Closed)
                {
                    sqlConnection.Close();
                }
            }

        }

        public static DataSet GetDataSet(string QueryString)
        {
            try
            {
                DataSet dataSet = new DataSet();
                //sqlConnection = new SqlConnection(OledbConnectionString);
                sqlConnection = GetConnection();
                sqlDataAdapter = new SqlDataAdapter(QueryString, sqlConnection);
                sqlDataAdapter.Fill(dataSet);
                return dataSet;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConnection.State != ConnectionState.Closed)
                {
                    sqlConnection.Close();
                }
            }

        }

        public static DataTable GetDataTable(string QueryString)
        {
            try
            {
                DataTable dataTable = new DataTable();
                //sqlConnection = new SqlConnection(OledbConnectionString);
                sqlConnection = GetConnection();
                if (sqlConnection.State != ConnectionState.Open)
                {
                    sqlConnection.Open();
                }
                sqlDataAdapter = new SqlDataAdapter(QueryString, sqlConnection);
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConnection.State != ConnectionState.Closed)
                {
                    sqlConnection.Close();
                }
            }
        }

        ///<summary>  
        /// ExecuteProcedureReader is the method by which a Stored Procedure is called with the list of  
        /// parameters and its values. This method will execute the stored proc and returns the resultset  
        /// on successfull completion, else throws the error.  
        ///</summary>  
        ///<param name="StoredProcName">Name of the Stored Proc that to be executed  
        ///<param name="parameterList">All required parameters in the Hashtable format with the format  
        ///                                 - @OUT_ prefix for output parameters  
        ///                                 - @IN_ prefix for input parameters  
        ///  
        ///<returns>The resultant dataset</returns>  
        public static DataSet ExecuteProcedureReader(string StoredProcName, Hashtable parameterList)
        {
            DataSet objresultSet = new DataSet();
            SqlDataAdapter objSqlDataAdapter = null;
            int counter = 0;

            try
            {
                sqlConnection = GetConnection();
                SqlCommand objSqlCommand = new SqlCommand(StoredProcName, sqlConnection);
                objSqlCommand.CommandType = CommandType.StoredProcedure;

                if (parameterList != null)
                {
                    foreach (string parametername in parameterList.Keys)
                    {
                        objSqlCommand.Parameters.AddWithValue(parametername, parameterList[parametername]);

                        if (parametername.StartsWith("@OUT_"))
                        {
                            objSqlCommand.Parameters[counter].Direction = ParameterDirection.Output;
                        }
                        else
                        {
                            objSqlCommand.Parameters[counter].Direction = ParameterDirection.Input;
                        }

                        counter++;
                    }
                }

                objSqlDataAdapter = new SqlDataAdapter(objSqlCommand);

                objSqlDataAdapter.Fill(objresultSet);

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (sqlConnection.State != ConnectionState.Closed)
                {
                    sqlConnection.Close();
                }
            }


            return objresultSet;
        }

        ///<summary>  
        /// ExecuteNonQueryProcedure is the method by which a Stored Procedure is called with the list of  
        /// parameters and its values. This method will execute the stored proc and returns the status of  
        /// on completion. Any exceptions during the process will throws the error.  
        ///</summary>  
        ///<param name="StoredProcName">Name of the Stored Proc that to be executed  
        ///<param name="parameterList">All required parameters in the Hashtable format with the format  
        ///                                 - @OUT_ prefix for output parameters  
        ///                                 - @IN_ prefix for input parameters  
        ///  
        ///<returns>The result of the Stored Proc execution</returns>  
        public static int ExecuteNonQueryProcedure(string StoredProcName, Hashtable parameterList)
        {
            int returnValue = 0;
            int counter = 0;

            try
            {
                sqlConnection = GetConnection();
                SqlCommand objSqlCommand = new SqlCommand(StoredProcName, sqlConnection);
                objSqlCommand.CommandType = CommandType.StoredProcedure;

                if (parameterList != null)
                {
                    foreach (string parametername in parameterList.Keys)
                    {
                        objSqlCommand.Parameters.AddWithValue(parametername, parameterList[parametername] == null ? DBNull.Value : parameterList[parametername]);

                        if (parametername.StartsWith("@OUT_"))
                        {
                            objSqlCommand.Parameters[counter].Direction = ParameterDirection.Output;
                        }
                        else
                        {
                            objSqlCommand.Parameters[counter].Direction = ParameterDirection.Input;
                        }

                        counter++;
                    }
                }

                returnValue = objSqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (sqlConnection.State != ConnectionState.Closed)
                {
                    sqlConnection.Close();
                }
            }

            return returnValue;
        }

        public static int ExecuteNonQueryProcedure(string StoredProcName, Hashtable parameterList, ref Hashtable outParameterList)
        {
            int returnValue = 0;
            int counter = 0;

            try
            {
                sqlConnection = GetConnection();
                SqlCommand objSqlCommand = new SqlCommand(StoredProcName, sqlConnection);
                objSqlCommand.CommandType = CommandType.StoredProcedure;

                if (parameterList != null)
                {
                    foreach (string parametername in parameterList.Keys)
                    {
                        

                        if (parametername.StartsWith("@OUT_"))
                        {
                            SqlParameter pvNewId = new SqlParameter();
                            pvNewId.ParameterName = parametername;
                            pvNewId.DbType = DbType.Int32;
                            pvNewId.Direction = ParameterDirection.Output;
                            objSqlCommand.Parameters.Add(pvNewId);
                            //objSqlCommand.Parameters[counter].Direction = ParameterDirection.Output;
                        }
                        else
                        {
                            objSqlCommand.Parameters.AddWithValue(parametername, parameterList[parametername] == null ? DBNull.Value : parameterList[parametername]);
                            objSqlCommand.Parameters[counter].Direction = ParameterDirection.Input;
                        }

                        counter++;
                    }
                }

                returnValue = objSqlCommand.ExecuteNonQuery();

                if (parameterList != null)
                {
                    outParameterList = new Hashtable();
                    foreach (string parametername in parameterList.Keys)
                    {
                        if (parametername.StartsWith("@OUT_"))
                        {
                            outParameterList.Add(parametername, objSqlCommand.Parameters[parametername].Value.ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (sqlConnection.State != ConnectionState.Closed)
                {
                    sqlConnection.Close();
                }
            }

            return returnValue;
        }

        public static int StudentChangeClassDivision(string StoredProcName, Hashtable parameterList)
        {
            int returnValue = 0;
            int counter = 0;

            try
            {
                sqlConnection = GetConnection();
                SqlCommand objSqlCommand = new SqlCommand(StoredProcName, sqlConnection);
                objSqlCommand.CommandType = CommandType.StoredProcedure;

                if (parameterList != null)
                {
                    foreach (string parametername in parameterList.Keys)
                    {
                        if (parametername.Contains("DataType"))
                        {
                            SqlParameter parameter = new SqlParameter();
                            //The parameter for the SP must be of SqlDbType.Structured 
                            parameter.ParameterName = "@ClassChangeDataType";
                            parameter.SqlDbType = System.Data.SqlDbType.Structured;
                            parameter.Value = parameterList[parametername];
                            objSqlCommand.Parameters.Add(parameter);
                        }
                        else
                        {
                            objSqlCommand.Parameters.AddWithValue(parametername, parameterList[parametername] == null ? DBNull.Value : parameterList[parametername]);

                            if (parametername.StartsWith("@OUT_"))
                            {
                                objSqlCommand.Parameters[counter].Direction = ParameterDirection.Output;
                            }
                            else
                            {
                                objSqlCommand.Parameters[counter].Direction = ParameterDirection.Input;
                            }
                        }
                        counter++;
                    }
                }

                returnValue = objSqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (sqlConnection.State != ConnectionState.Closed)
                {
                    sqlConnection.Close();
                }
            }

            return returnValue;
        }
    }
}
