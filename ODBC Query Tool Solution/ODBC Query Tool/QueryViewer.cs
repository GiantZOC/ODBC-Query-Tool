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
				string query = "";
				if (richTextBox1.SelectedText != "") {
					query = richTextBox1.SelectedText;
				}
				else {
					query = richTextBox1.Text;
				}
				this.UseWaitCursor = true;
				DataTable data = _connector.GetDataTableFromReader(query);
				if (data != null && data.Rows != null) {
					dataGridView1.DataSource = data;
					dataGridView1.Refresh();
				}
				this.UseWaitCursor = false;
			}
			catch (Exception ex) {
				this.UseWaitCursor = false;
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
