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
    public partial class Form2 : Form
    {
        MySqlConnection con = new MySqlConnection("Server=localhost;Port=3306;Database=distribuimi;Uid=root;");
        DataSet ds = new DataSet();
        MySqlDataAdapter da = new MySqlDataAdapter(); 
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            da.SelectCommand = new MySqlCommand("select * from tabela_k where id_Klienti=1001 ", con);
            ds.Clear();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            Class.Klasa1.MbyllConnection();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
      
        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Doni te anuloni Porosin", "Fshirja", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string c;
                MySqlCommand delete = con.CreateCommand();
                MySqlCommand konfirmo = con.CreateCommand();
                MySqlCommand sasia = con.CreateCommand();
                MySqlCommand emri = con.CreateCommand();
                MySqlCommand lloji = con.CreateCommand();
                MySqlCommand min = con.CreateCommand();
                MySqlCommand max = con.CreateCommand();
                MySqlCommand up = con.CreateCommand();
                MySqlCommand tru = con.CreateCommand();
                tru.CommandText = "SELECT IF (EXISTS (SELECT * FROM tabela_k WHERE ID = "+textBox2.Text+"),1,0)";
                delete.CommandText = "delete from tabela_k where ID ='"+textBox2.Text+"'";
                konfirmo.CommandText = "SELECT Konfirmo FROM tabela_k WHERE ID= '"+textBox2.Text+"'";
                sasia.CommandText = "SELECT Sasia_e_Porositur FROM tabela_k WHERE ID='" + textBox2.Text + "'";
                emri.CommandText = "SELECT Emri_i_Produkti  FROM tabela_k WHERE ID='" + textBox2.Text + "'";
                lloji.CommandText = "SELECT Lloji_i_Produktit  FROM tabela_k WHERE ID='" + textBox2.Text + "'";
                try
                {
                    con.Open();
                }
                catch  {}
                if (textBox2.Text != "")
                {
                    int tr = int.Parse(tru.ExecuteScalar().ToString());
                    if (tr > 0)
                    {
                        int q = int.Parse(konfirmo.ExecuteScalar().ToString());
                        if (q > 0)
                        {
                            MessageBox.Show("Porosia nuk mund te anulohet është duke ardhur ");
                        }
                        else
                        {
                            int sassia = int.Parse(sasia.ExecuteScalar().ToString());
                            string emmri = emri.ExecuteScalar().ToString();
                            string llooji = lloji.ExecuteScalar().ToString();
                            min.CommandText = "SELECT MIN( Sasia_e_Produkti) FROM depo WHERE Emri_i_Produktit= '" + emmri + "' AND Lloji_i_Produktit='" + llooji + "' ";
                            max.CommandText = "SELECT MAX( Sasia_e_Produkti) FROM depo WHERE Emri_i_Produktit='" + emmri + "' AND Lloji_i_Produktit='" + llooji + "'";
                            Int32 a = Int32.Parse(min.ExecuteScalar().ToString());
                            Int32 b = Int32.Parse(max.ExecuteScalar().ToString());
                            Int32 p;
                            if (a >= sassia)
                            {
                                p = a + sassia;
                                c = p.ToString();
                                up.CommandText = "UPDATE depo SET Sasia_e_Produkti= '" + c + "' WHERE Emri_i_Produktit='" + emmri + "' AND Lloji_i_Produktit='" + llooji + "'  AND Sasia_e_Produkti='" + a + "'";
                                up.ExecuteNonQuery();
                            }
                            else
                            {
                                p = b + sassia;
                                c = p.ToString();
                                up.CommandText = "UPDATE depo SET Sasia_e_Produkti= '" + c + "' WHERE Emri_i_Produktit='" + emmri + "' AND Lloji_i_Produktit='" + llooji + "' AND Sasia_e_Produkti='" + b + "'";
                                up.ExecuteNonQuery();
                            }
                        }
                        delete.ExecuteNonQuery();
                    }
                    else
                    {
                        MessageBox.Show(" nuk gjendet ne tabel ");
                    }
                textBox2.Text = "";
               Class.Klasa1.MbyllConnection();
                    
                    }       
            }
        }
    }
}
