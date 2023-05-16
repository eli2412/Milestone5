using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagement
{
    public partial class Form3 : Form
    {
        public InventoryItem inventoryItem { get; set; }
        public Form2 form2 { get; set; }
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            PriceTextBox.Text = inventoryItem.Price.ToString();
            QuantityTextBox.Text = inventoryItem.Quantity.ToString();
            ItemNameLabel.Text = inventoryItem.Name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Cancel button
            this.Close();
        }

        private void UpdateItemButton_Click(object sender, EventArgs e)
        {
            // Update button
            if (PriceTextBox.Text.Equals("") || QuantityTextBox.Text.Equals(""))
            {
                MessageBox.Show("Please enter all fields");
                return;
            }

            try
            {
                var result = form2.inventoryManager.EditItem(inventoryItem.Name, Convert.ToDouble(PriceTextBox.Text), Convert.ToInt32(QuantityTextBox.Text));
                if (result == -1)
                {
                    MessageBox.Show("Item does not exist");
                }
                else
                {
                    this.Close();
                    form2.RefreshDataGrid(form2.inventoryManager);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid values");
            }
        }
    }
}
