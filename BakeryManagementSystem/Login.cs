using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BakeryManagementSystem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbLoginUsername.Text == "" && tbLoginPassword.Text == "")
            {
                MessageBox.Show("Missing Information!!!",MessageBoxIcon.Warning.ToString(),MessageBoxButtons.OK);
            }
            else
            {
                if (tbLoginUsername.Text == "username" && tbLoginPassword.Text == "password")
                {
                    formMain main = new formMain();
                    this.Hide();
                    main.Show();
                }
                else
                {
                    MessageBox.Show("Wrong username or password!!!", MessageBoxIcon.Warning.ToString(), MessageBoxButtons.OK);
                    tbLoginUsername.Text = "";
                    tbLoginPassword.Text = "";
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
