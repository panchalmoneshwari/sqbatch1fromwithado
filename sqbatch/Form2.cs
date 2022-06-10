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
    public partial class Form2 : Form
    {
        SqlConnection conn;
        SqlCommand cmdd;
        SqlDataReader drr;
        public Form2()
        {
            InitializeComponent();
            string strcon = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            conn = new SqlConnection(strcon);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "select * from courseinfo where courseid=@id";
                cmdd = new SqlCommand(str, conn);
                cmdd.Parameters.AddWithValue("@id", Convert.ToInt32(textid.Text));
                // open DB connection
                conn.Open();
                // fire the query select
                drr = cmdd.ExecuteReader();
                if (drr.HasRows)// check that record is present in dr object or not
                {
                    while (drr.Read()) // read data from dr object
                    {
                        textname.Text = drr["courcename"].ToString();
                        textfees.Text = drr["fees"].ToString();
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
                conn.Close();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // @id, @name,@salary are the variable names
                string str = "insert into courseinfo values(@name,@fees)";
                cmdd = new SqlCommand(str, conn);
               
                cmdd.Parameters.AddWithValue("@name", textname.Text);
                cmdd.Parameters.AddWithValue("@fees", Convert.ToDouble(textfees.Text));
                // open DB connection
                conn.Open();
                // fire the query insert / update / delete
                int result = cmdd.ExecuteNonQuery();
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
                conn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // @id, @name,@salary are the variable names
                string str = "update courseinfo set courcename=@name,fees=@fees where courseid=@id";
                cmdd = new SqlCommand(str, conn);
                cmdd.Parameters.AddWithValue("@id", Convert.ToInt32(textid.Text));
                cmdd.Parameters.AddWithValue("@name", textname.Text);
                cmdd.Parameters.AddWithValue("@fees", Convert.ToDouble(textfees.Text));
                // open DB connection
                conn.Open();
                // fire the query insert / update / delete
                int result = cmdd.ExecuteNonQuery();
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
                conn.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // @id, @name,@salary are the variable names
                string str = "Delete from courseinfo where courseid=@id";
                cmdd = new SqlCommand(str, conn);
                cmdd.Parameters.AddWithValue("@id", Convert.ToInt32(textid.Text));

                // open DB connection
                conn.Open();
                // fire the query insert / update / delete
                int result = cmdd.ExecuteNonQuery();
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
                conn.Close();
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "select * from courseinfo";
                cmdd = new SqlCommand(str, conn);
               
                // open DB connection
                conn.Open();
                // fire the query select
                drr = cmdd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(drr);
                dataGridView1.DataSource = table;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
