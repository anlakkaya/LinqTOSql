using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqToSQLGiris
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            NorthWindDataContext ctx = new NorthWindDataContext();
            dataGridView1.DataSource = ctx.Categories;
            comboBox1.DisplayMember = "CategoryName";
            comboBox1.ValueMember = "CategoryID";
            comboBox1.DataSource = ctx.Categories;

            comboBox2.DisplayMember = "CompanyName";
            comboBox2.ValueMember = "SupplierID";
            comboBox2.DataSource = ctx.Suppliers;


            var sonuc = from urun in ctx.Categories;
                     

                dataGridView1.DataSource = sonuc;
            
                        
                      
                     
         

        }

        private void button1_Click(object sender, EventArgs e)
        {
            NorthWindDataContext ctx = new NorthWindDataContext();
            Product p = new Product();
            p.ProductName = textBox1.Text;
            p.UnitPrice = numericUpDown1.Value;
            p.UnitsInStock = Convert.ToInt16(numericUpDown2.Value);
            p.CategoryID = (int)comboBox1.SelectedValue;
            p.SupplierID = (int)comboBox2.SelectedValue;

            ctx.Products.InsertOnSubmit(p);
            MessageBox.Show($"SubmitChanges öncesi ProductID={p.ProductID}");
            ctx.SubmitChanges();
            MessageBox.Show($"SubmitChanges sonrası ProductID={p.ProductID}");

            dataGridView1.DataSource = ctx.Products.GetNewBindingList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NorthWindDataContext ctx = new NorthWindDataContext();
            if (dataGridView1.CurrentRow == null)
                return;
            int urunID = (int)dataGridView1.CurrentRow.Cells["ProductId"].Value;
            Product p = ctx.Products.SingleOrDefault(urun => urun.ProductID == urunID);
            ctx.Products.DeleteOnSubmit(p);
            ctx.SubmitChanges();

            dataGridView1.DataSource = ctx.Products;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow r = dataGridView1.CurrentRow;
            textBox1.Text = r.Cells["ProductName"].Value.ToString();
            numericUpDown1.Value = Convert.ToDecimal(r.Cells["UnitPrice"].Value);
            numericUpDown2.Value = Convert.ToDecimal(r.Cells["UnitsInStock"].Value);
            comboBox1.SelectedValue = (int)r.Cells["CategoryID"].Value;
            comboBox2.SelectedValue = (int)r.Cells["SupplierID"].Value;
            textBox1.Tag = r.Cells["ProductID"].Value;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            NorthWindDataContext ctx = new NorthWindDataContext();
            Product p = ctx.Products.SingleOrDefault(urun => urun.ProductID == (int)textBox1.Tag);
            p.ProductName = textBox1.Text;
            p.UnitPrice = numericUpDown1.Value;
            p.UnitsInStock = Convert.ToInt16(numericUpDown2.Value);
            p.CategoryID = (int)comboBox1.SelectedValue;
            p.SupplierID = (int)comboBox2.SelectedValue;
            ctx.SubmitChanges();

            dataGridView1.DataSource = ctx.Products;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            NorthWindDataContext ctx = new NorthWindDataContext();
            dataGridView1.DataSource = ctx.Products.Where(x => x.ProductName.Contains(textBox2.Text));
        }
    }
}
