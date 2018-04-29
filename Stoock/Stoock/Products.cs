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

namespace Stock
{
    public partial class Products : Form
    {
    

        public Products()
        {
            InitializeComponent();
        }

        private void Products_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-EHPHUUP\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            con.Open();
            bool Status = false;
            if (comboBox1.SelectedIndex == 0)
            {
                Status = true;
            }
            else
            {
                Status = false;
            }
            var sqlQuery = "";
            if (IfProductExists(con, textBox1.Text))
            {
                sqlQuery = @"UPDATE [Products] SET [ProductName] = '" + textBox2.Text + "',[ProductStatus] = <,'" + Status + "' WHERE [PrductCode] ='" + textBox1.Text + "'"; 
            }
        else
            {
              sqlQuery = @"INSERT INTO[Stock].[dbo].[Products] ([PrductCode] ,[ProductName] ,[ProductStatus]) VALUES
                      ('" + textBox1.Text + "','" + textBox2.Text + "','" + Status + "')";
            }
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            cmd.ExecuteNonQuery();
            con.Close();

            //Reading data 
            LoadData();
        }
        private bool IfProductExists(SqlConnection con, String ProductCode)  
            {
            SqlDataAdapter sda = new SqlDataAdapter(@"Select 1 from [Products] Where [PrductCode]='" + ProductCode+"'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
            }
        public void LoadData()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-EHPHUUP\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
         SqlDataAdapter sda = new SqlDataAdapter(@"Select * from [Stock].[dbo].[Products]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["PrductCode"].ToString();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductName"].ToString();

                if ((bool)item["ProductStatus"])
                {
                    dataGridView1.Rows[n].Cells[0].Value = "Active";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[0].Value = "diactive";
                }
            }
            }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "Active")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-EHPHUUP\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            var sqlQuery = "";
            if (IfProductExists(con, textBox1.Text))
            {
                con.Open();
                sqlQuery = @"DELETE FROM [Products] WHERE [PrductCode] ='" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("Reacord Not Exists.....!");
            }
           //Reading data 
            LoadData();
        }
    }
  }

        
 