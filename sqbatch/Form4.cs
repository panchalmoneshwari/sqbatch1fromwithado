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
    public partial class Form4 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder scb;

        public Form4()
        {
            InitializeComponent();
            string strconnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(strconnection);
        }
        private DataSet GetEmpData()
        {
            da = new SqlDataAdapter("select * from courseinfo", con);
            // add PK constraint to the col (id) which is in DataSet
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            // build the command for DataAdpater 
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            // Fill() fire the select qry in DB & load data in DataSet
            da.Fill(ds, "course"); // emp is the name given to the table which get loaded in the DataSet
            return ds;
        }


        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmpData();
                DataRow row = ds.Tables["course"].NewRow();
                row["coursename"] = txtname.Text;
                row["fees"] = txtfees.Text;
                ds.Tables["course"].Rows.Add(row);
                // reflect the changes from DataSet to DB
                int result = da.Update(ds.Tables["course"]);
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
        
       
        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmpData();
                DataRow row = ds.Tables["course"].Rows.Find(txtid.Text);
                if (row != null)
                {
                    row["coursename"] = txtname.Text;
                    row["fees"] = txtfees.Text;
                    int result = da.Update(ds.Tables["course"]);
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

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmpData();
                DataRow row = ds.Tables["course"].Rows.Find(txtid.Text);
                if (row != null)
                {
                    row.Delete();// delete the row from DataSet
                    int result = da.Update(ds.Tables["course"]);
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

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmpData();
                DataRow row = ds.Tables["course"].Rows.Find(txtid.Text);
                if (row != null)
                {
                    txtname.Text = row["coursename"].ToString();
                    txtfees.Text = row["fees"].ToString();
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

        private void btnShowAll_Click_1(object sender, EventArgs e)
        {
            ds = GetEmpData();
            dataGridView1.DataSource = ds.Tables["course"];
        }
    }
}
