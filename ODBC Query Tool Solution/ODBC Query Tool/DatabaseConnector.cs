using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ODBC_Query_Tool
{
	public class DatabaseConnector
	{
		private string _connectionString;
		public DatabaseConnector(string connectionString) {
			this._connectionString = connectionString;
			
		}

		public void Test() {
			try {


				OdbcConnection dbConnection = new OdbcConnection(_connectionString);
				dbConnection.Open();
				dbConnection.Close();
				MessageBox.Show("Success");
			}
			catch (Exception ex) {

				MessageBox.Show(ex.ToString());
			}
			
		}

		public DataTable ExecuteTable(string queryString) {
			DataTable dt = new DataTable();
			try {

			
			OdbcConnection dbConnection = new OdbcConnection(_connectionString);
			dbConnection.Open();
			OdbcCommand dbCommand = dbConnection.CreateCommand();
			dbCommand.CommandText = queryString;
			dbCommand.CommandTimeout = 30; //seconds
			OdbcDataReader dbReader = dbCommand.ExecuteReader();
			
			dt.Load(dbReader);

			//dbReader.Close();
			dbCommand.Dispose();
			dbConnection.Close();
			}
			catch (Exception) {

				throw;
			}
			return dt;
		}

		public DataTable ExecuteReader(string queryString) {
			DataTable dt = new DataTable();
			try {


				OdbcConnection dbConnection = new OdbcConnection(_connectionString);
				dbConnection.Open();
				OdbcCommand dbCommand = dbConnection.CreateCommand();
				dbCommand.CommandText = queryString;
				OdbcDataReader dbReader = dbCommand.ExecuteReader();

				int fCount = dbReader.FieldCount;
				Console.Write(":");
				for (int i = 0; i < fCount; i++) {
					String fName = dbReader.GetName(i);
					Console.Write(fName + ":");
				}
				Console.WriteLine();

				while (dbReader.Read()) {
					Console.Write(":");
					for (int i = 0; i < fCount; i++) {
						String col = dbReader.GetString(i);

						Console.Write(col + ":");
					}
					Console.WriteLine();
				}

				dbReader.Close();
				dbCommand.Dispose();
				dbConnection.Close();
			}
			catch (Exception) {

				throw;
			}
			return dt;
		}

		public DataTable GetDataTableFromReader(string queryString) {
			OdbcConnection dbConnection = new OdbcConnection(_connectionString);
			DataTable dt = new DataTable();
			try {
				dbConnection.Open();
				OdbcCommand dbCommand = dbConnection.CreateCommand();
				dbCommand.CommandText = queryString;
				OdbcDataReader dbReader = dbCommand.ExecuteReader();

				DataTable dtSchema = dbReader.GetSchemaTable();
				
				// You can also use an ArrayList instead of List<>
				List<DataColumn> listCols = new List<DataColumn>();

				if (dtSchema != null) {
					foreach (DataRow drow in dtSchema.Rows) {
						string columnName = System.Convert.ToString(drow["ColumnName"]);
						DataColumn column = new DataColumn(columnName, (Type)(drow["DataType"]));
						if (column.DataType == System.Type.GetType("System.Byte[]")) {
							continue;
						}
						column.Unique = (bool)drow["IsUnique"];
						column.AllowDBNull = (bool)drow["AllowDBNull"];
						column.AutoIncrement = (bool)drow["IsAutoIncrement"];
						listCols.Add(column);
						dt.Columns.Add(column);
					}
				}

				// Read rows from DataReader and populate the DataTable
				while (dbReader.Read()) {
					DataRow dataRow = dt.NewRow();
					for (int i = 0; i < listCols.Count; i++) {
						dataRow[((DataColumn)listCols[i])] = dbReader[i];
					}
					dt.Rows.Add(dataRow);
				}
				dbReader.Close();
				dbCommand.Dispose();
				dbConnection.Close();
				return dt;
			}
			finally {
				dbConnection.Close();
			}

		}
	}
}
