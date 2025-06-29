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
    //Inheritance
    class Customer : BakerySystem
    {
        //Encapsulation
        string customerName;
        string customerPhone;
        string customerAddress;
        public string CustomerName { get => customerName; set => customerName = value; }
        public string CustomerPhone { get => customerPhone; set => customerPhone = value; }
        public string CustomerAddress { get => customerAddress; set => customerAddress = value; }
        //Constructor Overloading
        public Customer()
        {
            customerName = "";
            customerPhone = "";
            customerAddress = "";
        }

        public Customer(string name, string phone, string address)
        {
            customerName = name;
            customerPhone = phone;
            customerAddress = address;
        }
        ~Customer()
        {
            GC.Collect();
        }
        
        
        int CKey = 0;

        //Method overloading or Static Polymorphism
        public void addCustomer(ref BunifuDataGridView dgv)
        {
            //Exception Handling
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO CustomerTbl(CustName,CustPhone,CustAddress) VALUES(@CN,@CP,@CA)", con);
                cmd.Parameters.AddWithValue("@CN", CustomerName);
                cmd.Parameters.AddWithValue("@CP", CustomerPhone);
                cmd.Parameters.AddWithValue("@CA", CustomerAddress);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Added!!!");
                con.Close();

                displayElements("CustomerTbl", dgv);
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

        public void addCustomer(string name, string phone, string address, ref BunifuDataGridView dgv)
        {
            CustomerName = name;
            CustomerPhone = phone;
            CustomerAddress = address;
            addCustomer(ref dgv);
        }

        public void editCustomer(ref int ckey, ref BunifuDataGridView dgv)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE CustomerTbl SET CustName=@CN,CustPhone=@CP,CustAddress=@CA WHERE CustID=@CKey", con);
                cmd.Parameters.AddWithValue("@CN", CustomerName);
                cmd.Parameters.AddWithValue("@CP", CustomerPhone);
                cmd.Parameters.AddWithValue("@CA", CustomerAddress);
                cmd.Parameters.AddWithValue("@CKey", ckey);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Updated!!!");
                con.Close();

                displayElements("CustomerTbl", dgv);
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
        public void deleteCustomer(int ckey, BunifuDataGridView dgv)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@"DELETE FROM CustomerTbl WHERE CustID=@CKey", con);
                cmd.Parameters.AddWithValue("@CKey", ckey);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Deleted!!!");
                displayElements("CustomerTbl", dgv);
                con.Close();

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
                    SqlCommand command = new SqlCommand("SELECT * FROM CustomerTbl WHERE CustName LIKE '%"+ tb.Text +"%'", connection);
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

        public void getCustomer(ComboBox cmb)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT CustID FROM CustomerTbl", con);
                SqlDataReader Rdr;
                Rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("CustID", typeof(int));
                dt.Load(Rdr);
                cmb.ValueMember = "CustID";
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
        public void countCustomers(Label lbl)
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM CustomerTbl", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                lbl.Text = dt.Rows[0][0].ToString() + " Customers";
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
