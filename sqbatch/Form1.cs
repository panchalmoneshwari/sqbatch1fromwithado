using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace sqbatch
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form1()
        {
            
            InitializeComponent();
            
            string strconnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(strconnection);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // @id, @name,@salary are the variable names
                string str = "insert into Emp values(@id,@name,@salary)";
                cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textempid.Text));
                cmd.Parameters.AddWithValue("@name", textempname.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToDouble(textempsal.Text));
                // open DB connection
                con.Open();
                // fire the query insert / update / delete
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record inserted");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // @id, @name,@salary are the variable names
                string str = "update Emp set Name=@name,Salary=@salary where Id=@id";
                cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textempid.Text));
                cmd.Parameters.AddWithValue("@name", textempname.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToDouble(textempsal.Text));
                // open DB connection
                con.Open();
                // fire the query insert / update / delete
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record updated");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // @id, @name,@salary are the variable names
                string str = "Delete from Emp where Id=@id";
                cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textempid.Text));
               
                // open DB connection
                con.Open();
                // fire the query insert / update / delete
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record Deleted");
                }
                else
                {
                    MessageBox.Show("Record Not Found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "select * from Emp where Id=@id";
                cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textempid.Text));
                // open DB connection
                con.Open();
                // fire the query select
                dr = cmd.ExecuteReader();
                if (dr.HasRows)// check that record is present in dr object or not
                {
                    while (dr.Read()) // read data from dr object
                    {
                        textempname.Text = dr["Name"].ToString();
                        textempsal.Text = dr["Salary"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "select * from Emp";
                cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textempid.Text));
                cmd.Parameters.AddWithValue("@name", textempname.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToDouble(textempsal.Text));
                // open DB connection
                con.Open();
                // fire the query select
                dr = cmd.ExecuteReader();
               DataTable table=new DataTable();
                table.Load(dr);
                dataGridView1.DataSource = table;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
