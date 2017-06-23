using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ODBC_Query_Tool
{
	public partial class QueryViewer : Form
	{
		private DatabaseConnector _connector;
		public QueryViewer(DatabaseConnector connector) {
			this._connector = connector;
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			try {
				string query = richTextBox1.Text;

				DataTable data = _connector.GetDataTableFromReader(query);
				if (data != null && data.Rows != null && data.Rows.Count > 0) {
					dataGridView1.DataSource = data;
					dataGridView1.Refresh();
				}

			}
			catch (Exception ex) {

				MessageBox.Show(ex.ToString());
			}
		}

		private void QueryViewer_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.F5) {
				button1_Click(null, null);
			}
		}
	}
}
