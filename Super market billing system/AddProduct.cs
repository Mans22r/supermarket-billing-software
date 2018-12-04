using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Super_market_billing_system
{
    public partial class AddProduct : Form
    {
        SqlConnection db;
        SqlDataAdapter da = new SqlDataAdapter();
        public AddProduct()
        {
            InitializeComponent();
        }

        private void AddProduct_Load(object sender, EventArgs e)
        {
            db = new SqlConnection(@"Data Source=DESKTOP-VGSSBN5;Initial Catalog=SuperMarketBilling;Integrated Security=True");
            Random rnd = new Random();
            textBox1.Text = rnd.Next(1000, 9999).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" )
            {
                string query = @"insert into products values('" + textBox1.Text + "','" + textBox2.Text + "',"
                                                               + textBox4.Text + ","+ textBox3.Text + ","+ numericUpDown1.Value.ToString() + ",'"
                                                               + dateTimePicker1.Value.ToString() + "')";
                da.InsertCommand = new SqlCommand(query, db);
                db.Open();
                da.InsertCommand.ExecuteNonQuery();
                db.Close();
                this.Close();
            }
            else
            {
                label8.Text = "warning fill all required data";
            }
        }
    }
}
