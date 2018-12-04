using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Super_market_billing_system
{
    public partial class AdminWindow : Form
    {
        SqlConnection db;
        SqlDataAdapter da = new SqlDataAdapter();
        SqlDataReader dr;
        DataSet ds = new DataSet();
        public AdminWindow()
        {
            InitializeComponent();
        }
        private void refresh()
        {
            string ldproduct = "select * from products select * from users";
            db = new SqlConnection(@"Data Source=DESKTOP-VGSSBN5;Initial Catalog=SuperMarketBilling;Integrated Security=True");
            da.SelectCommand = new SqlCommand(ldproduct, db);
            ds.Clear();
            db.Open();
            da.Fill(ds);
            db.Close();
            listBox1.DataSource = ds.Tables[0];
            listBox1.DisplayMember = "productname";
            listBox2.DataSource = ds.Tables[1];
            listBox2.DisplayMember = "username";
        }
        private void AdminWindow_Load(object sender, EventArgs e)
        {
            string ldproduct = "select * from products select * from users";
            db = new SqlConnection(@"Data Source=DESKTOP-VGSSBN5;Initial Catalog=SuperMarketBilling;Integrated Security=True");
            da.SelectCommand = new SqlCommand(ldproduct, db);
            ds.Clear();
            db.Open();
            da.Fill(ds);
            db.Close();
            listBox1.DataSource=ds.Tables[0];
            listBox1.DisplayMember = "productname";
            listBox2.DataSource = ds.Tables[1];
            listBox2.DisplayMember = "username";
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

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("C:\\Users\\HP\\source\\repos\\Super market billing system\\Super market billing system\\bin\\Debug\\help.chm");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query;
            ModifyProduct mdf = new ModifyProduct();
            if (mdf.ShowDialog() == DialogResult.OK)
            {
                query = @"update products set productname='"+mdf.pname+"',quantity='"+mdf.qua+
                                             "',price='"+mdf.price+"',discount='"+mdf.dis+
                                             "',expiredate='"+mdf.expdate+"' where productname='"+textBox5.Text+"'";
                da.UpdateCommand = new SqlCommand(query, db);
                db.Open();
                da.UpdateCommand.ExecuteNonQuery();
                db.Close();
                refresh();
                MessageBox.Show("product modified");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = @"delete from products where productname='"+textBox5.Text+"'";
            da.DeleteCommand = new SqlCommand(query, db);
            db.Open();
            da.DeleteCommand.ExecuteNonQuery();
            db.Close();
            refresh();
            MessageBox.Show("product deleted");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = @"update users set _access='" + checkBox1.Checked + "', _modify='"
                                                      + checkBox2.Checked + "', _delete='"
                                                      + checkBox3.Checked + "' where username='" + textBox3.Text + "'";
            da.UpdateCommand = new SqlCommand(query, db);
            db.Open();
            da.UpdateCommand.ExecuteNonQuery();
            db.Close();
            refresh();
            MessageBox.Show("privilage chenged succesfully");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.GetItemText(listBox1.SelectedItem) != "System.Data.DataRowView")
            {
                string query = @"select * from products where productname='" + listBox1.GetItemText(listBox1.SelectedItem) + "'";
                SqlCommand cmd = new SqlCommand(query, db);
                db.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox4.Text = dr["productid"].ToString();
                    textBox5.Text = dr["productname"].ToString();
                    textBox6.Text = dr["quantity"].ToString();
                    textBox7.Text = dr["price"].ToString();
                    textBox8.Text = dr["discount"].ToString();
                    textBox9.Text = dr["expiredate"].ToString();
                }
                db.Close();
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.GetItemText(listBox2.SelectedItem) != "System.Data.DataRowView")
            {
                string query = @"select * from users where username='" + listBox2.GetItemText(listBox2.SelectedItem) + "'";
                SqlCommand cmd = new SqlCommand(query, db);
                db.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox3.Text = dr["username"].ToString();
                    checkBox1.Checked = bool.Parse(dr["_access"].ToString());
                    checkBox2.Checked = bool.Parse(dr["_modify"].ToString());
                    checkBox3.Checked = bool.Parse(dr["_delete"].ToString());
                }
                db.Close();
            }
        }
    }
}
