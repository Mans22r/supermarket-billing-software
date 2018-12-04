using System;
using System.Windows.Forms;

namespace Super_market_billing_system
{
    public partial class ModifyProduct : Form
    {
        public ModifyProduct()
        {
            InitializeComponent();
        }
        public string pname,qua,price,dis,expdate;

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                label8.Text = "warning fill required data";
            }
            else
            {
                pname = textBox2.Text;
                qua = textBox4.Text;
                price = textBox3.Text;
                dis = numericUpDown1.Value.ToString();
                expdate = dateTimePicker1.Value.ToString();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
