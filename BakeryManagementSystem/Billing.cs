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
    class Billing : BakerySystem
    {
        public Billing()
        {

        }
        ~Billing()
        {
            GC.Collect();
        }
        
        public void addToBill(ref string prodname, ref int n, ref int GrdTotal, ref Label lblGrdTotal, ref BunifuTextBox tbBillingQuantity, ref BunifuTextBox tbBillingPrice, ref BunifuTextBox tbBillingProductName, ref BunifuDataGridView dgvBillingInvoice)
        {
            try
            {
                int total = Convert.ToInt32(tbBillingQuantity.Text) * Convert.ToInt32(tbBillingPrice.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                //newRow.CreateCells(dgvBillingInvoice);
                //newRow.Cells[0].Value = n + 1;
                /*newRow.Cells[1].Value = tbBillingProductName.Text;
                newRow.Cells[2].Value = tbBillingQuantity.Text;
                newRow.Cells[3].Value = tbBillingPrice.Text;
                newRow.Cells[4].Value = total;*/
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = n + 1 });
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = prodname });
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = tbBillingQuantity.Text });
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = tbBillingPrice.Text });
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = Convert.ToInt32(tbBillingQuantity.Text) * Convert.ToInt32(tbBillingPrice.Text) });
                dgvBillingInvoice.Rows.Add(newRow);
                n++;
                GrdTotal = GrdTotal + total;
                lblGrdTotal.Text = "Rs: " + GrdTotal;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
            }
        }

        public void saveBill(ref ComboBox cmbBillingCustomer, ref int GrdTotal, ref BunifuDataGridView dgvBillingList, ref BunifuTextBox tbBillingPrice, ref BunifuTextBox tbBillingProductName, ref BunifuTextBox tbBillingQuantity)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO SalesTbl(Customer,SAmount,SDate) VALUES(@CN,@SA,@SD)", con);
                cmd.Parameters.AddWithValue("@CN", cmbBillingCustomer.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@SA", GrdTotal);
                cmd.Parameters.AddWithValue("@SD", DateTime.Today.Date);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Bill Saved!!!");
                con.Close();
                displayElements("SalesTbl", dgvBillingList);
                tbBillingPrice.Text = "";
                tbBillingProductName.Text = "";
                tbBillingQuantity.Text = "";
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
            return;
        }
        
    }
}
