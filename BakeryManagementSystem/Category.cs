using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using Bunifu.UI.WinForms;

namespace BakeryManagementSystem
{
    class Category : BakerySystem
    {
        public Category()
        {

        }
        ~Category()
        {
            GC.Collect();
        }

        string categoryName;

        public string CategoryName { get => categoryName; set => categoryName = value; }

        

        public void addCategory(string name)
        {
            CategoryName = name;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO CategoryTbl VALUES('" + CategoryName + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Added!!!");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void addCategory(string name, ref BunifuDataGridView dgv)
        {
            addCategory(name);
            displayElements("CategoryTbl", dgv);
        }

        public void editCategory(string name, int catkey, ref BunifuDataGridView dgv)
        {
            CategoryName = name;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE CategoryTbl SET CatName=@CatN WHERE CatID=@CKey", con);
                cmd.Parameters.AddWithValue("@CatN", CategoryName);
                cmd.Parameters.AddWithValue("@CKey", catkey);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Updated!!!");
                displayElements("CategoryTbl", dgv);
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void deleteCategory(int catkey, ref BunifuDataGridView dgv)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@"DELETE FROM CategoryTbl WHERE CatID=@CKey", con);
                cmd.Parameters.AddWithValue("@CKey", catkey);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Deleted!!!");
                displayElements("CategoryTbl", dgv);

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public override void searchRecord(ref BunifuDataGridView dgv, ref Bunifu.UI.WinForms.BunifuTextBox tb)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-TE9DHC5\SQLEXPRESS;Initial Catalog=BakeryDB;Integrated Security=True"))
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM CategoryTbl WHERE CatName LIKE '%"+ tb.Text +"%''", connection);
                    command.Parameters.AddWithValue("@name", tb.Text);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();

                    adapter.Fill(table);

                    dgv.DataSource = table;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void getCategorie(ref ComboBox cmb, ref BunifuDataGridView dgv)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT CatID FROM CategoryTbl", con);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatID", typeof(int));
                dt.Load(rdr);
                cmb.ValueMember = "CatID";
                cmb.DataSource = dt;
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        
    }
}

