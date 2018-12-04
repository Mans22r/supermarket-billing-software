using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Super_market_billing_system
{
    public partial class LoginWindow : Form
    {
        SqlConnection db;
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginWindow_Load(object sender, EventArgs e)
        {
            db = new SqlConnection(@"Data Source=DESKTOP-VGSSBN5;Initial Catalog=SuperMarketBilling;Integrated Security=True");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool work = false;
            string comm="select * from users where username='"+userNameTb.Text+"' and password='"+passWordTb.Text+"' and usertype='a'";
            noticeLabel.Text = "";
            ds.Clear();
            da.SelectCommand = new SqlCommand(comm, db);
            db.Open();
            da.Fill(ds);
            db.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                userNameTb.Text = "";
                passWordTb.Text = "";
                AdminWindow nad = new AdminWindow();
                this.Hide();
                nad.ShowDialog();
                this.Show();
                work = true;
            }
            else if (work == false)
            {
                comm = "select * from users where username='" + userNameTb.Text + "' and password='" + passWordTb.Text + "' and usertype='s'";
                da.SelectCommand = new SqlCommand(comm, db);
                db.Open();
                da.Fill(ds);
                db.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    userNameTb.Text = "";
                    passWordTb.Text = "";
                    SalesmanForm nsf = new SalesmanForm();
                    this.Hide();
                    nsf.ShowDialog();
                    this.Show();
                }
                else
                {
                    userNameTb.Text = "";
                    passWordTb.Text = "";
                    noticeLabel.Text = " invalid username or password";
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
