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
    public partial class Form3 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder scb;

        public Form3()
        {
            
            InitializeComponent();
            string strconnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(strconnection);
        }
        private DataSet GetEmpData()
        {
            da = new SqlDataAdapter("select * from Emp", con);
            // add PK constraint to the col (id) which is in DataSet
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            // build the command for DataAdpater 
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            // Fill() fire the select qry in DB & load data in DataSet
            da.Fill(ds, "emp"); // emp is the name given to the table which get loaded in the DataSet
            return ds;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmpData();
                DataRow row = ds.Tables["emp"].NewRow();
                row["Name"] = textempname.Text;
                row["Salary"] = textempsal.Text;
                ds.Tables["emp"].Rows.Add(row);
                // reflect the changes from DataSet to DB
                int result = da.Update(ds.Tables["emp"]);
                if (result == 1)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmpData();
                DataRow row = ds.Tables["emp"].Rows.Find(textempid.Text);
                if (row != null)
                {
                    row["Name"] = textempname.Text;
                    row["Salary"] = textempsal.Text;
                    int result = da.Update(ds.Tables["emp"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record updated");
                    }

                }
                else
                {
                    MessageBox.Show("Id does not exists to update");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmpData();
                DataRow row = ds.Tables["emp"].Rows.Find(textempid.Text);
                if (row != null)
                {
                    row.Delete();// delete the row from DataSet
                    int result = da.Update(ds.Tables["emp"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record deleted");
                    }

                }
                else
                {
                    MessageBox.Show("Id does not exists to delete");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                ds = GetEmpData();
                DataRow row = ds.Tables["emp"].Rows.Find(textempid.Text);
                if (row != null)
                {
                    textempname.Text = row["Name"].ToString();
                    textempsal.Text = row["Salary"].ToString();
                }
                else
                {
                    MessageBox.Show("Record does not exists");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            ds = GetEmpData();
            dataGridView1.DataSource = ds.Tables["emp"];

        }
    }
}
