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
    class Product : BakerySystem
    {
        public Product()
        {

        }

        public Product(string name, int quantity, int price, int category)
        {
            // Constructor with parameters for setting the properties of the object
            this.Productname = name;
            this.Productprice = price;
            this.Productcategory = category;
            this.Productquantity = quantity;
        }
        ~Product()
        {
            GC.Collect();
        }
        string productname;
        int productprice;
        int productquantity;
        int productcategory;
        

        public string Productname { get => productname; set => productname = value; }
        public int Productprice { get => productprice; set => productprice = value; }
        public int Productquantity { get => productquantity; set => productquantity = value; }
        public int Productcategory { get => productcategory; set => productcategory = value; }

        public void addProduct(ref BunifuDataGridView dgv)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO ProductTbl(ProdName,ProdQty,ProdPrice,ProdCat) VALUES(@ProdName, @ProdQty, @ProdPrice, @ProdCat)", con);
                cmd.Parameters.AddWithValue("@ProdName", Productname);
                cmd.Parameters.AddWithValue("@ProdQty", Productquantity);
                cmd.Parameters.AddWithValue("@ProdPrice", Productprice);
                cmd.Parameters.AddWithValue("@ProdCat", Productcategory.ToString());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Added!!!");
                con.Close();
                displayElements("ProductTbl", dgv);
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

        public void addProduct(Product product, ref BunifuDataGridView dgv)
        {
            addProduct(ref dgv);
        }

        public void editProduct(int pkey, ref BunifuDataGridView dgv)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE ProductTbl SET ProdName=@PN,ProdCat=@PC,ProdPrice=@PP,ProdQty=@PQ WHERE ProdID=@PKey", con);
                cmd.Parameters.AddWithValue("@PN", Productname);
                cmd.Parameters.AddWithValue("@PC", Productcategory);
                cmd.Parameters.AddWithValue("@PP", Productprice);
                cmd.Parameters.AddWithValue("@PQ", Productquantity);
                cmd.Parameters.AddWithValue("@PKey", pkey);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Updated!!!");
                con.Close();

                displayElements("ProductTbl", dgv);
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
        public void deleteProduct(int pkey, ref BunifuDataGridView dgv)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@"DELETE FROM ProductTbl WHERE ProdID=@PKey", con);
                cmd.Parameters.AddWithValue("@PKey", pkey);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Deleted!!!");
                con.Close();
                displayElements("ProductTbl", dgv);

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
                    SqlCommand command = new SqlCommand("SELECT * FROM ProductTbl WHERE ProdName LIKE '%"+ tb.Text +"%'", connection);
                    //command.Parameters.AddWithValue("@name", tb.Text);

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

        public void getProducts(System.Windows.Forms.DataVisualization.Charting.Chart chart )
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter adapt = new SqlDataAdapter("Select ProdName,ProdQty from ProductTbl", con);
                adapt.Fill(ds);
                chart.DataSource = ds;
                chart.Series["Products"].XValueMember = "ProdName";
                chart.Series["Products"].YValueMembers = "ProdQty";
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
        
        public void countProducts(ref Label lbl)
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM ProductTbl", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                lbl.Text = dt.Rows[0][0].ToString() + " Items";
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
        public void countSales(ref Label lbl)
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT SUM(SAmount) FROM SalesTbl", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                lbl.Text = "Rs " + dt.Rows[0][0].ToString();
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
