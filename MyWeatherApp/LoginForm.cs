using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyWeatherApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, System.EventArgs e)
        {
           
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            int rowCount;

            DataTable dt = new DataTable();
            DataBase db = new DataBase();

            db.OpenDB();
            dt = db.Data(txtUserName.Text,txtPassword.Text);

            rowCount = Convert.ToInt32(dt.Rows.Count.ToString());

            if (rowCount == 0)
            {
                lblLogin.Text = "You have entered invalid username or password";
            } else
            {
                //dataGridView1.DataSource = dt;
                lblLogin.Text = " ";

                MainForm main = new MainForm(txtUserName.Text);
                main.Show();
            }
            
            db.CloseDB();            
        }
    }
}
