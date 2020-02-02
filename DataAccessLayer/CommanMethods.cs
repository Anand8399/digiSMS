using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace DataAccessLayer
{
    public static class CommanMethods
    {

        private static OleDbConnection oledbConnection;

        private static OleDbCommand oledbCommand;

        private static OleDbDataAdapter oleDbDataAdapter;

        public static int ExecuteNonQuery(string QueryString, string OledbConnectionString)
        {
            int intResult = 0;
            try
            {
                oledbConnection = new OleDbConnection(OledbConnectionString);
                if (oledbConnection.State != ConnectionState.Open) {
                    oledbConnection.Open();
                }
                oledbCommand = new OleDbCommand(QueryString, oledbConnection);
                intResult = oledbCommand.ExecuteNonQuery();
                return intResult;
            }
            catch (OleDbException ex)
            {
                throw ex;
            }
            finally
            {
                if (oledbConnection.State != ConnectionState.Closed)
                {
                    oledbConnection.Close();
                }
            }

        }

        public static DataSet GetDataSet(string QueryString, string OledbConnectionString)
        {
            try
            {
                DataSet dataSet = new DataSet();
                oledbConnection = new OleDbConnection(OledbConnectionString);
                oleDbDataAdapter = new OleDbDataAdapter(QueryString, oledbConnection);
                oleDbDataAdapter.Fill(dataSet);
                return dataSet;
            }
            catch (OleDbException ex)
            {
                throw ex;
            }
            finally
            {
                if (oledbConnection.State != ConnectionState.Closed)
                {
                    oledbConnection.Close();
                }
            }

        }

        public static DataTable GetDataTable(string QueryString, string OledbConnectionString)
        {
            try
            {
                DataTable dataTable = new DataTable();
                oledbConnection = new OleDbConnection(OledbConnectionString);
                oleDbDataAdapter = new OleDbDataAdapter(QueryString, oledbConnection);
                oleDbDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (OleDbException ex)
            {
                throw ex;
            }
            finally
            {
                if (oledbConnection.State != ConnectionState.Closed)
                {
                    oledbConnection.Close();
                }
            }
        }

    }
}
