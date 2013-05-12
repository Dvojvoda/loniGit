using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Web ;
namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {
        MySqlConnection con = new MySqlConnection("Server=localhost;Port=3306;Database=distribuimi;Uid=root;");
        DataSet ds = new DataSet();
        DataSet dc = new DataSet();
        DataSet db = new DataSet();
        MySqlDataAdapter da = new MySqlDataAdapter();        
        public Form1()
        { 
            InitializeComponent();
 
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "") || (textBox2.Text == "") || (textBox3.Text ==""))
            {
                MessageBox.Show("Emri Loji dhe Sasia  jan te domosdoshme");
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Jeni Sigurt Per Porosin", "Porosia", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    MySqlCommand siguri = con.CreateCommand();
                    siguri.CommandText = "SELECT IF (EXISTS (SELECT * FROM depo WHERE Emri_i_Produktit='" + textBox1.Text + "' AND Lloji_i_Produktit='" + textBox2.Text + "'),1,0)";  
                    try
                    {
                        con.Open();
                    }
                    catch  { }
                    int w =int.Parse ((siguri.ExecuteScalar().ToString()));
                    if (w != 1 )
                    {
                        MessageBox.Show("Produkti Nuk Ndodhet ne Depo");
                    }
                    else
                    {
                        string c;
                        Int32 i = Math.Abs(Int32.Parse(textBox3.Text));
                        MySqlCommand command = con.CreateCommand();
                        MySqlCommand command1 = con.CreateCommand();
                        MySqlCommand min = con.CreateCommand();
                        MySqlCommand max = con.CreateCommand();
                        MySqlCommand up = con.CreateCommand();
                        MySqlCommand tru = con.CreateCommand();
                        tru.CommandText = "SELECT IF (EXISTS (SELECT * FROM tabela_k WHERE ID = " + textBox2.Text + " ),1,0)";
                        command.CommandText = "insert into tabela_arkiv values ('',1001,'Skendearj 1','" + textBox1.Text + "','" + textBox2.Text + "','" + i + "','" + textBox4.Text + "','" + textBox5.Text + "','rr.28 Nentori Skenderaj','" + textBox6.Text + "',now(),0)";
                        command1.CommandText = "insert into tabela_k values ('',1001,'Skenderaj 1','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','rr.28 Nentori Skenderaj ','" + textBox6.Text + "',now(),0)";
                        min.CommandText = "SELECT MIN( Sasia_e_Produkti) FROM depo WHERE Emri_i_Produktit= '" + textBox1.Text + "' AND Lloji_i_Produktit='" + textBox2.Text + "' ";
                        max.CommandText = "SELECT MAX( Sasia_e_Produkti) FROM depo WHERE Emri_i_Produktit='" + textBox1.Text + "' AND Lloji_i_Produktit='" + textBox2.Text + "'";
                        label1.Text = "'" + textBox1.Text + "'";
                        label9.Text = "'" + textBox2.Text + "'";  
                        Int32 a = Int32.Parse(min.ExecuteScalar().ToString());
                        Int32 b = Int32.Parse(max.ExecuteScalar().ToString());
                        Int32 p;
                        if ((i > a) & (i > b))
                        {
                            MessageBox.Show("sasia e porositur është më madhe se ajo në Depo");
                        }
                        else
                        {
                            command.ExecuteNonQuery();
                            command1.ExecuteNonQuery();
                            if (a >= i)
                            {
                                p = a - i;
                                c = p.ToString();
                                up.CommandText = "UPDATE depo SET Sasia_e_Produkti= '" + c + "' WHERE Emri_i_Produktit=" + label1.Text + " AND Lloji_i_Produktit=" + label9.Text + "  AND Sasia_e_Produkti='" + a + "'";
                                up.ExecuteNonQuery();
                            }
                            else
                            {
                                p = b - i;
                                c = p.ToString();
                                up.CommandText = "UPDATE depo SET Sasia_e_Produkti= '" + c + "' WHERE Emri_i_Produktit=" + label1.Text + " AND Lloji_i_Produktit=" + label9.Text + " AND Sasia_e_Produkti='" + b + "'";
                                up.ExecuteNonQuery();
                            }
                        }
                        Class.Klasa1.MbyllConnection();  
                    }
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox6.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                }
            }
        }
         private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true ;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;
            da.SelectCommand = new MySqlCommand("select * from depo",con);
            dc.Clear();
            da.Fill(dc);
            dataGridView1.DataSource = dc.Tables[0];
            Class.Klasa1.MbyllConnection();         
        }
        private void Form1_Load(object sender, EventArgs e)
        {
          
           
            dataGridView1.Visible = false;
            dataGridView2.Visible = false ;
            dataGridView3.Visible = false;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView2.Visible = true;
            dataGridView1.Visible = false;
            dataGridView3.Visible = false;
            da.SelectCommand = new MySqlCommand("select * from tabela_k where id_Klienti=1001 ",con);
            ds.Clear();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            Class.Klasa1.MbyllConnection();
        }
        private void button5_Click(object sender, EventArgs e)
        {
           
            MySqlCommand  a= con.CreateCommand();
            a.CommandText = "delete from tabela_k where Konfirmo = 1";          
            try
            {
                con.Open();
            }
            catch
            {}
            a.ExecuteNonQuery();
            Class.Klasa1.MbyllConnection();     
        }
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            label11.Text ="'"+ textBox7.Text + "%'";
            dataGridView3.Visible = true;
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            da.SelectCommand = new MySqlCommand("SELECT * FROM depo WHERE Emri_i_Produktit LIKE "+label11.Text +"", con);
            db.Clear();
            da.Fill(db);
            dataGridView3.DataSource = db.Tables[0];
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            MySqlCommand loginn = con.CreateCommand();
            loginn.CommandText = "SELECT IF ( EXISTS (SELECT * FROM pas WHERE USER= '" + textBox9.Text + "' AND  PASSWORD='" + textBox8.Text + "' AND id = 1001),1,0) ";
            try
            {
                con.Open();
            }
            catch
            { }
            int a = int.Parse(loginn.ExecuteScalar().ToString());
            if (a == 1)
            {
                pictureBox3.Visible = false;
                label13.Visible = false;
                label15.Visible = false;
                label14.Visible = false;
                textBox8.Visible = false;
                textBox9.Visible = false;
                button7.Visible = false;
                button8.Visible = false;
            }
            else
            {
                MessageBox.Show("emri perdorusit ose fjalkalimi është gabim ");

            }
            textBox8.Text = "";
            textBox9.Text = "";
            
        }


        private void button8_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            pass ndrro = new pass();
            ndrro.Show();
        }



       

      
       
    }
}
