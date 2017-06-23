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
	public partial class Main : Form
	{
		public Main() {
			InitializeComponent();
		}

		private void btnConnect_Click(object sender, EventArgs e) {
			try {
				string connectionString = txtConnectionString.Text;
				DatabaseConnector connector = new DatabaseConnector(connectionString);
				TableViewer viewer = new TableViewer(connector);
				viewer.ShowDialog();
			}
			catch (Exception ex) {

				MessageBox.Show(ex.ToString());
			}
		}

		private void button2_Click(object sender, EventArgs e) {
			string connectionString = txtConnectionString.Text;
			DatabaseConnector connector = new DatabaseConnector(connectionString);
			QueryViewer viewer = new QueryViewer(connector);
			viewer.ShowDialog();
		}

		private void button1_Click(object sender, EventArgs e) {
			string connectionString = txtConnectionString.Text;
			DatabaseConnector conn = new DatabaseConnector(connectionString);
			conn.Test();

		}
	}
}
