using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
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
                    dataGridView1[hittest.ColumnIndex, hittest.RowIndex].Tag = oldValue + "\r\n" + cellvalue;
                }


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {


            List<string> list = new List<string>(_max);
            list.Add("one");
            list.Add("two");
            list.Add("three");
            list.Add("four");
            list.Add("five");


            var s1 = Stopwatch.StartNew();
            for (int i = 0; i < _max; i++)
            {
                bool f = ContainsLoop(list, "four");
            }
            s1.Stop();
            var s2 = Stopwatch.StartNew();
            for (int i = 0; i < _max; i++)
            {
                bool f = list.Contains("four");
            }
            s2.Stop();
            Console.WriteLine(((double)(s1.Elapsed.TotalMilliseconds * 1000000) /
                _max).ToString("0.00 ns"));
            Console.WriteLine(((double)(s2.Elapsed.TotalMilliseconds * 1000000) /
                _max).ToString("0.00 ns"));
            Console.Read();
        }

        private const int _max = 100000000;

        private bool ContainsLoop(List<string> list, string value)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (value.Equals(list[i]))
                {
                    return true;
                }
            }
            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show(this);
        }
    }
}
