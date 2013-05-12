using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1
{
    public partial class pass : Form
    {
        MySqlConnection con = new MySqlConnection("Server=localhost;Port=3306;Database=distribuimi;Uid=root;");
        public pass()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Jeni të Sigurt ", "Nëdrro", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                MySqlCommand ndrro = con.CreateCommand();
                MySqlCommand konfirm = con.CreateCommand();
                konfirm.CommandText = "SELECT IF (EXISTS (SELECT * FROM pas WHERE USER ='" + textBox3.Text + "' AND PASSWORD = '" + textBox1.Text + "' ),1,0)";
                ndrro.CommandText = "UPDATE pas SET PASSWORD = '" + textBox2.Text + "' WHERE id = 1001 AND USER ='"+textBox3.Text +"' AND PASSWORD = '"+textBox1.Text +"'";

                try
                {
                    con.Open();
                }
                catch 
                {}
                int k = int.Parse(konfirm.ExecuteScalar().ToString());
                if (k == 1)
                {
                    ndrro.ExecuteNonQuery();
                    MessageBox.Show(" fjalkalimi është ndrrua me sukses ");
                }
                else
                {
                    MessageBox.Show("perdorusi ose fjalkalimi është gabim ");
                }
              
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                Class.Klasa1.MbyllConnection();
 
            }
                
            }
        }
    }

