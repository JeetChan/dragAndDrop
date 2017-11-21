using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace dragAndDrop
{
    public delegate void MarkerHEventHandler(string bsm);
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("colBestBefore", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("colStatus", typeof(string)));

            dt.Columns["colStatus"].Expression = String.Format("IIF(colBestBefore < #{0}#, 'Ok','Not ok')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            dt.Rows.Add(DateTime.Now.AddDays(-1));
            dt.Rows.Add(DateTime.Now.AddDays(1));
            dt.Rows.Add(DateTime.Now.AddDays(2));
            dt.Rows.Add(DateTime.Now.AddDays(-2));

            dataGridView1.DataSource = dt;

            //设置当前题号选择框 
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (row.Index == 1 && cell.ColumnIndex == 1)
                    {
                        //dataGridView1.Rows[row.Index].Cells[cell.ColumnIndex].Style.ForeColor = Color.SeaGreen;
                        //cell.Style.Font
                        
                        cell.Style.Font = new Font(cell.InheritedStyle.Font, FontStyle.Bold);
                        cell.Style.ForeColor = Color.SlateGray;
                    }
                    if (row.Index == 2 && cell.ColumnIndex == 1)
                    {
                        cell.Style.ForeColor = Color.SlateGray;
                        //cell.Style.Font = new Font(cell.InheritedStyle.Font, FontStyle.Regular);
                        cell.Style.Font = new Font(cell.InheritedStyle.Font, FontStyle.Bold);
                        //cell.Style = row.DefaultCellStyle;


                    }
                }
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the index of the item the mouse is below.
            var hittestInfo = dataGridView1.HitTest(e.X, e.Y);

            if (hittestInfo.RowIndex != -1 && hittestInfo.ColumnIndex != -1)
            {

                object aa = dataGridView1.Rows[hittestInfo.RowIndex].Cells[hittestInfo.ColumnIndex].Value;
                object tag = dataGridView1.Rows[hittestInfo.RowIndex].Cells[hittestInfo.ColumnIndex].Tag;
                if (tag == null)
                {
                    tag = "12345";
                }
                valueFromMouseDown = aa + "," + tag;
                if (valueFromMouseDown != null)
                {
                    // Remember the point where the mouse down occurred. 
                    // The DragSize indicates the size that the mouse can move 
                    // before a drag event should be started.                
                    Size dragSize = SystemInformation.DragSize;

                    // Create a rectangle using the DragSize, with the mouse position being
                    // at the center of the rectangle.
                    dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
                }
            }
            else
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown = Rectangle.Empty;
        }

        private Rectangle dragBoxFromMouseDown;
        private object valueFromMouseDown;
        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty && !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    // Proceed with the drag and drop, passing in the list item.                    
                    DragDropEffects dropEffect = dataGridView1.DoDragDrop(valueFromMouseDown, DragDropEffects.Copy);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows[2].Cells[1].Style = this.dataGridView1.Rows[2].DefaultCellStyle;
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
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

        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void dataGridView1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
    }
}
