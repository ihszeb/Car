using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication14
{
    public partial class CarList : Form
    {
        public CarList()
        {
            InitializeComponent();
        }

        private void CarList_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {

             SqlConnection con = new SqlConnection("Data Source=SEMMIR-PC\\SQLEXPRESS;Initial Catalog=car;Integrated Security=True");
            //Insert Logic
            con.Open();
            bool status = false;
            if (comboBox1.SelectedIndex == 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }

            var sqlQuery = "";

            if (IfCarListExist(con, textBox1.Text))
            {

                sqlQuery = @"UPDATE[Stock] SET[CarName] ='" + textBox2.Text + "',[CarStatus] ='" + status + "'WHERE[CarID] = '" + textBox1.Text + "'";
            }
            else
            {
                sqlQuery = @"INSERT INTO [dbo].[Stock] ([CarID],[CarName],[CarStatus])
     VALUES
                            ('" + textBox1.Text + "','" + textBox2.Text + "','" + status + "')";
            }

            SqlCommand cmd = new SqlCommand(sqlQuery,con);
            cmd.ExecuteNonQuery();
            con.Close();

            //Reading Data
            LoadData();



        }
        private bool IfCarListExist(SqlConnection con,string carid)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select 1 From [car].[dbo].[Stock] WHERE [CarID]='" + carid+"'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public void LoadData()
        {
            SqlConnection con = new SqlConnection("Data Source=SEMMIR-PC\\SQLEXPRESS;Initial Catalog=car;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter("Select * From [car].[dbo].[Stock]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["CarID"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["CarName"].ToString();
                if ((bool)item["CarStatus"])

                {
                    dataGridView1.Rows[n].Cells[2].Value = "New";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Used";
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection("Data Source=SEMMIR-PC\\SQLEXPRESS;Initial Catalog=car;Integrated Security=True");
            var sqlQuery = "";

            if (IfCarListExist(con, textBox1.Text))
            {
                con.Open();
                sqlQuery = @"DELETE FROM [Stock] WHERE[CarID] = '" + textBox1.Text + "'";

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("Record Not Exists!");
            }


            //Reading Data
            LoadData();
        }
        
        
      
        

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString()== "New")

            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
        }
    }
    

}

