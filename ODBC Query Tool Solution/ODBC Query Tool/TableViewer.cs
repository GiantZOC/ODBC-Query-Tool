using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Data.SqlClient;

namespace ODBC_Query_Tool
{
	public partial class TableViewer : Form, IDisposable
	{
		private DatabaseConnector _connectory;
		public TableViewer(DatabaseConnector connectory) {
			this._connectory = connectory;
			InitializeComponent();
		}

		private void btnRun_Click(object sender, EventArgs e) {
			try {
				listBox1.Items.Clear();
				//DatabaseConnector dbConn = new DatabaseConnector(_connectionString);
				DataTable data = _connectory.ExecuteTable(textBox1.Text);
				if (data != null && data.Rows != null && data.Rows.Count > 0) {
					foreach (DataRow row in data.Rows) {
						listBox1.Items.Add(row["Name"].ToString());
					}
				}

			}
			catch (Exception ex) {
				MessageBox.Show(ex.ToString());
			}
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
			try {
				//DatabaseConnector dbConn = new DatabaseConnector(_connectionString);
				string query = textBox2.Text.Replace("{TABLE}", listBox1.SelectedItem.ToString());
				
				DataTable data = _connectory.GetDataTableFromReader(query);
				if (data != null && data.Rows != null && data.Rows.Count > 0) {
					dataGridView1.DataSource = data;
					dataGridView1.Refresh();
				}
				
			}
			catch (Exception ex) {
				MessageBox.Show(ex.ToString());
			}
		}
	}
}
