using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Super_market_billing_system
{
    public partial class SalesmanForm : Form
    {
        SqlConnection db;
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        SqlDataReader dr;
        public SalesmanForm()
        {
            InitializeComponent();
        }

        private void refresh()
        {
            string query = @"select * from products";
            ds.Clear();
            da.SelectCommand = new SqlCommand(query, db);
            db.Open();
            da.Fill(ds);
            db.Close();
            listBox1.DataSource = ds.Tables[0];
            listBox1.DisplayMember = "productname";
        }

        private void SalesmanForm_Load(object sender, EventArgs e)
        {
            string query = @"select * from products";
            db = new SqlConnection(@"Data Source=DESKTOP-VGSSBN5;Initial Catalog=SuperMarketBilling;Integrated Security=True");
            ds.Clear();
            da.SelectCommand = new SqlCommand(query, db);
            db.Open();
            da.Fill(ds);
            db.Close();
            listBox1.DataSource = ds.Tables[0];
            listBox1.DisplayMember = "productname";
        }

        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddProduct npro = new AddProduct();
            npro.ShowDialog();
            refresh();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About nabt = new About();
            nabt.ShowDialog();
        }

        private void toolTipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolTipToolStripMenuItem.Checked)
                statusStrip1.Visible = true;
            else
                statusStrip1.Visible = false;
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\HP\source\repos\Super market billing system\Super market billing system\bin\Debug\help.chm");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string query = @"select * from products where productname like '" + textBox2.Text + "%'";
            da.SelectCommand = new SqlCommand(query, db);
            ds.Clear();
            db.Open();
            da.Fill(ds);
            db.Close();
            listBox1.DataSource = ds.Tables[0];
            listBox1.DisplayMember = "productname";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] data;
            string query = @"select * from products where productname='"+ listBox1.GetItemText(listBox1.SelectedItem)+"'";
            SqlCommand cmd= new SqlCommand(query, db);
            db.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr["quantity"].ToString() != "0")
            {
                if (textBox1.Text == "")
                {
                    data = new string[5] { dr["productid"].ToString(), listBox1.GetItemText(listBox1.SelectedItem),
                                            "1",dr["price"].ToString(),dr["discount"].ToString() };
                }
                else
                {
                    data = new string[5] { dr["productid"].ToString(), listBox1.GetItemText(listBox1.SelectedItem),
                                            textBox1.Text,dr["price"].ToString(),dr["discount"].ToString() };
                }
                dataGridView1.Rows.Add(data);
            }
            else
            {
                MessageBox.Show("Product not available","notice");
            }
            db.Close();
            textBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[][] cell=new string[dataGridView1.Rows.Count - 1][];
            string[] invoice = new string[dataGridView1.Rows.Count - 1];
            string final = "product id |product name |quantity |price |total price |with discount\n";
            for(int i = dataGridView1.Rows.Count - 2; i >= 0; i--)
            {
                cell[i] = new string[5];
                for(int j= 4; j >= 0; j--)
                {
                    cell[i][j] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
                invoice[i] = cell[i][0] + "\t|" + cell[i][1] +
                            "\t|" + cell[i][2] + "\t |" + cell[i][3] + "\t |" +
                            int.Parse(cell[i][2]) * double.Parse(cell[i][3]) + "\t |" +
                            (float.Parse(cell[i][3])-(float.Parse(cell[i][3]) * int.Parse(cell[i][4])) / 100) * int.Parse(cell[i][2]+"\n");
            }
            foreach(string str in invoice)
            {
                final += str + "\n";
            }
            MessageBox.Show(final, "invoice");
            dataGridView1.Rows.Clear();
            toolStripStatusLabel1.Text = "product purchased";
        }
    }
}
