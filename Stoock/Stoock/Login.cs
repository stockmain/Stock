﻿using System;
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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        public object Error { get; private set; }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //to-Do:checklogin UserName & Password
          
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-EHPHUUP\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
           SqlDataAdapter sda = new SqlDataAdapter(@"SELECT *
           FROM[Stock].[dbo].[Login] Where UserName = '"+textBox1.Text+"' and Password = '"+textBox2.Text + "'",con);
            DataTable dt=new DataTable();
            sda.Fill(dt);
            if(dt.Rows.Count==1)
                {
                   this.Hide();
                    StockMain main = new StockMain();
                    main.Show(); 
                }
            else
            {
                MessageBox.Show("Invalid UserName & Password...!", "Error",MessageBoxButtons.OK ,MessageBoxIcon.Error);
               
            }
        }
    }
}
