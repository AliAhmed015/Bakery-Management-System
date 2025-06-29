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
    abstract class BakerySystem
    {
        public BakerySystem()
        {

        }
        ~BakerySystem()
        {
            GC.Collect();
        }
        protected SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9KTGROV;Initial Catalog=BakeryDB;Integrated Security=True");

        public abstract void searchRecord(ref Bunifu.UI.WinForms.BunifuDataGridView dgv, ref Bunifu.UI.WinForms.BunifuTextBox tb);

        public void displayElements(string TName, Bunifu.UI.WinForms.BunifuDataGridView dgv )
        {
            con.Open();
            string query = "SELECT * FROM " + TName + "";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dgv.DataSource = ds.Tables[0];
            con.Close();
        }
    }
}
