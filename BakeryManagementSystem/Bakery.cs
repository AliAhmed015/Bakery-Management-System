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
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
            Category category = new Category();
            category.getCategorie(ref cmbProductCategory, ref dgvCategory);
            category.displayElements("ProductTbl", dgvProducts);
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void lblProducts_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(0);
            Category category = new Category();
            category.getCategorie(ref cmbProductCategory, ref dgvCategory);
            category.displayElements("ProductTbl", dgvProducts);
        }

        private void lblCustomers_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(1);
            Customer customer = new Customer();
            customer.displayElements("CustomerTbl", dgvCustomers);
        }

        private void lblCategories_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(2);
            Category category = new Category();
            category.displayElements("CategoryTbl", dgvCategory);
        }

        private void lblBilling_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(3);
            Product product = new Product();
            product.displayElements("ProductTbl", dgvBillingProducts);
            product.displayElements("SalesTbl", dgvBillingList);
            Customer cust = new Customer();
            cust.getCustomer(cmbBillingCustomer);
        }

        private void lblDashboard_Click(object sender, EventArgs e)
        {

            Product product = new Product();
            bunifuPages1.SetPage(4);
            product.getProducts(chart1);
            chart1.Invalidate();
            Customer cust = new Customer();
            cust.countCustomers(lblDashboardCust);
            product.countProducts(ref lblDashboardProducts);
            product.countSales(ref lblDashboardSales);

        }
        
        private void btnAddProduct_Click(object sender, EventArgs e)
        {

            if (tbProductName.Text == "" || tbProductQuantity.Text == "" || tbProductPrice.Text == "" || cmbProductCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Enter Complete Information!!!");
            }
            else
            {
                Product product = new Product(tbProductName.Text.Trim(), int.Parse(tbProductQuantity.Text.Trim()), int.Parse(tbProductPrice.Text.Trim()), cmbProductCategory.SelectedIndex+1);
                product.addProduct(ref dgvProducts);
                product.getProducts(chart1);
            }
        }

        public int PKey = 0;

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbProductName.Text = dgvProducts.SelectedRows[0].Cells[1].Value.ToString();
            tbProductQuantity.Text = dgvProducts.SelectedRows[0].Cells[4].Value.ToString();
            tbProductPrice.Text = dgvProducts.SelectedRows[0].Cells[3].Value.ToString();
            cmbProductCategory.Text = dgvProducts.SelectedRows[0].Cells[2].Value.ToString();

            if (tbProductName.Text == "")
            {
                PKey = 0;
            }
            else
            {
                PKey = int.Parse(dgvProducts.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (tbProductName.Text == "" || tbProductQuantity.Text == "" || tbProductPrice.Text == "" || cmbProductCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Enter Complete Information!!!");
            }
            else
            {
                Product product = new Product(tbProductName.Text.Trim(), int.Parse(tbProductQuantity.Text.Trim()), int.Parse(tbProductPrice.Text.Trim()), cmbProductCategory.SelectedIndex+1);
                product.editProduct(PKey, ref dgvProducts);
                product.getProducts(chart1);
            }
        }



        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            if (tbCustName.Text == "" || tbCustPhone.Text == "" || tbCustAddress.Text == "")
            {
                MessageBox.Show("Enter Complete Information!!!");
            }
            else
            {
                Customer customer = new Customer();
                customer.addCustomer(tbCustName.Text.Trim(), tbCustPhone.Text.Trim(), tbCustAddress.Text.Trim(), ref dgvCustomers);
            }
        }

        public int CKey = 0;

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbCustName.Text = dgvCustomers.SelectedRows[0].Cells[1].Value.ToString();
            tbCustPhone.Text = dgvCustomers.SelectedRows[0].Cells[2].Value.ToString();
            tbCustAddress.Text = dgvCustomers.SelectedRows[0].Cells[3].Value.ToString();

            if (tbCustName.Text == "")
            {
                CKey = 0;
            }
            else
            {
                CKey = int.Parse(dgvCustomers.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (CKey == 0)
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                Customer customer = new Customer();
                customer.deleteCustomer(CKey,dgvCustomers);
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            
            if (PKey == 0)
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                Product product = new Product();
                product.deleteProduct(PKey, ref dgvProducts);
                product.getProducts(chart1);
                chart1.Invalidate();
            }
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            if (tbCustName.Text == "" || tbCustPhone.Text == "" || tbCustAddress.Text == "")
            {
                MessageBox.Show("Enter Complete Information!!!");
            }
            else
            {
                Customer customer = new Customer(tbCustName.Text,tbCustPhone.Text,tbCustAddress.Text);
                customer.editCustomer(ref CKey, ref dgvCustomers);
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (tbCategoryName.Text == "")
            {
                MessageBox.Show("Enter Complete Information!!!");
            }
            else
            {
                Category category = new Category();
                category.addCategory(tbCategoryName.Text, ref dgvCategory);
            }
        }

        public int CatKey = 0;

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbCategoryName.Text = dgvCategory.SelectedRows[0].Cells[1].Value.ToString();

            if (tbCategoryName.Text == "")
            {
                CatKey = 0;
            }
            else
            {
                CatKey = int.Parse(dgvCategory.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (CatKey == 0)
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                Category category = new Category();
                category.deleteCategory(CatKey, ref dgvCategory);
            }
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            if (tbCategoryName.Text == "")
            {
                MessageBox.Show("Enter Complete Information!!!");
            }
            else
            {
                Category category = new Category();
                category.editCategory(tbCategoryName.Text,CatKey, ref dgvCategory);
            }
        }
        int BPKey = 0;
        public int stock = 0;
        private void dgvInvoice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbBillingProductName.Text = dgvBillingProducts.SelectedRows[0].Cells[1].Value.ToString();
            tbBillingPrice.Text = dgvBillingProducts.SelectedRows[0].Cells[3].Value.ToString();

            if (tbBillingProductName.Text == "")
            {
                PKey = 0;
                stock = 0;
            }
            else
            {
                PKey = int.Parse(dgvBillingProducts.SelectedRows[0].Cells[0].Value.ToString());
                stock = int.Parse(dgvBillingProducts.SelectedRows[0].Cells[2].Value.ToString());
            }
        }

        int n = 0;
        int GrdTotal = 0;
        private void btnAddToBill_Click(object sender, EventArgs e)
        {
            if (tbBillingQuantity.Text == "")
            {
                MessageBox.Show("Enter Quantity!!!");
            }
            else if (int.Parse(tbBillingQuantity.Text) > stock)
            {
                MessageBox.Show("Not Enough Stock!!!");
            }
            else
            {
                string product = tbBillingProductName.Text;
                Billing billing = new Billing();
                billing.addToBill(ref product, ref n, ref GrdTotal, ref lblGrdTotal, ref tbBillingQuantity, ref tbBillingPrice, ref tbProductName, ref dgvBillingInvoice);

            }
        }
        
        private void btnRefreshBill_Click(object sender, EventArgs e)
        {
            tbBillingPrice.Text = "";
            tbBillingProductName.Text = "";
            tbBillingQuantity.Text = "";
        }

        private void btnBillSave_Click(object sender, EventArgs e)
        {

            if (cmbBillingCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("Enter Complete Information!!!");
            }
            else
            {
                Billing billing = new Billing();
                billing.saveBill(ref cmbBillingCustomer, ref GrdTotal, ref dgvBillingList, ref tbBillingPrice, ref tbBillingProductName, ref tbBillingQuantity);
            }
        }
       
        private void lblLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void productsTextBoxTextChange(object sender, EventArgs e)
        {
            BakerySystem bs = new Product();
            bs.searchRecord(ref dgvProducts, ref tbSearchProducts);
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {
            BakerySystem bs = new Customer();
            bs.searchRecord(ref dgvCustomers, ref tbSearchCustomer);
        }

        private void bunifuTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            BakerySystem bs = new Category();
            bs.searchRecord(ref dgvCategory, ref tbSearchCategory);
        }
    }
}