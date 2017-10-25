using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dragAndDrop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //dt.Columns.Add(new DataColumn("colBestBefore", typeof(DateTime)));
            //dt.Columns.Add(new DataColumn("colStatus", typeof(string)));

            //dt.Columns["colStatus"].Expression = String.Format("IIF(colBestBefore < #{0}#, 'Ok','Not ok')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            //dt.Rows.Add(DateTime.Now.AddDays(-1));
            //dt.Rows.Add(DateTime.Now.AddDays(1));
            //dt.Rows.Add(DateTime.Now.AddDays(2));
            //dt.Rows.Add(DateTime.Now.AddDays(-2));

            //dataGridView1.DataSource = dt;

            for (int i = 0; i < 3; i++)
            {
                object[] a = { (3 * i).ToString(), (3 * i + 1).ToString(), (3 * i + 2).ToString() };
                dataGridView1.Rows.Add(a);
            }

            dataGridView1.AutoResizeColumns();

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

        }

        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void dataGridView1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = dataGridView1.PointToClient(new Point(e.X, e.Y));

            // If the drag operation was a copy then add the row to the other control.
            if (e.Effect == DragDropEffects.Copy)
            {
                string cellvalue = e.Data.GetData(typeof(string)) as string;
                var hittest = dataGridView1.HitTest(clientPoint.X, clientPoint.Y);
                var oldValue = dataGridView1[hittest.ColumnIndex, hittest.RowIndex].Value;
                if (hittest.ColumnIndex != -1
                    && hittest.RowIndex != -1)
                {
                    dataGridView1[hittest.ColumnIndex, hittest.RowIndex].Value = oldValue + "\r\n" + cellvalue;
                }


            }
        }



    }
}
