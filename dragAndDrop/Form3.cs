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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private ComboBox cmb_Temp = new ComboBox();

        private void Form3_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();


            dataGridView1.Columns.Add("0", "字段名");
            dataGridView1.Columns[0].Width = 90;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns.Add("0", "属性项");
            dataGridView1.Columns[1].Width = 90;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns.Add("0", "属性值");
            dataGridView1.Columns[2].Width = 200;
            this.dataGridView1.Controls.Add(this.cmb_Temp);
        }

        private void cmb_Temp_SelectedIndexChanged(object sender, EventArgs e)
        {

            dataGridView1.CurrentCell.Value = ((ComboBox)sender).Text;

        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                this.cmb_Temp.Visible = false;
                this.cmb_Temp.Width = 0;
                if (this.dataGridView1.CurrentCell == null)
                {
                    //this.cobgv.Visible = false;
                    //this.cobgv.Width = 0;
                }
                else
                {

                    if (this.dataGridView1.CurrentCell.ColumnIndex == 2)//下拉框所放置的列
                    {
                        int rowIndex = dataGridView1.CurrentCell.RowIndex;
                        string rowName = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
                        if (GetEnum(cmb_Temp, rowName) == 0)
                        {

                            this.cmb_Temp.Visible = false;
                            this.cmb_Temp.Width = 0;
                            this.cmb_Temp.Left = this.dataGridView1.GetCellDisplayRectangle(this.dataGridView1.CurrentCell.ColumnIndex, this.dataGridView1.CurrentCell.RowIndex, true).Left;
                            this.cmb_Temp.Top = this.dataGridView1.GetCellDisplayRectangle(this.dataGridView1.CurrentCell.ColumnIndex, this.dataGridView1.CurrentCell.RowIndex, true).Top;
                            this.cmb_Temp.Width = this.dataGridView1.GetCellDisplayRectangle(this.dataGridView1.CurrentCell.ColumnIndex, this.dataGridView1.CurrentCell.RowIndex, true).Width;
                            string ffff = Convert.ToString(this.dataGridView1.CurrentCell.Value);
                            this.cmb_Temp.Text = ffff;
                            this.cmb_Temp.Visible = true;
                            //  添加下拉列表框事件 
                            cmb_Temp.SelectedIndexChanged += new EventHandler(cmb_Temp_SelectedIndexChanged);
                        }

                    }
                    else
                    {
                        this.cmb_Temp.Visible = false;
                        this.cmb_Temp.Width = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
