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
    public partial class Form1 : Form
    {
        InventoryManager inventoryManager;
        Form2 form2;
        public Form1()
        {
            InitializeComponent();
            
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            // load forms
            
            inventoryManager = new InventoryManager();
            form2 = new Form2();
            form2.inventoryManager = inventoryManager;
            form2.Show();
            
            

        }

        private void AddItemButton_Click(object sender, EventArgs e)
        {
            // enter proporties
            if(ItemNameTextBox.Text.Equals("") || ItemPriceTextBox.Text.Equals("") || ItemQuantityTextBox.Text.Equals(""))
            {
                MessageBox.Show("Please enter all fields");
                return;
            }

            try {
                var result = inventoryManager.AddItem(ItemNameTextBox.Text, Convert.ToDouble(ItemPriceTextBox.Text), Convert.ToInt32(ItemQuantityTextBox.Text));
                if (result == -1)
                {
                    MessageBox.Show("Item already exists");
                }
                else
                {
                    MessageBox.Show("Please log in to add item");
                    btnSignIn signIn = new btnSignIn();
                     signIn.Show();
                    
                    // item added
                    ItemNameTextBox.Text = "";
                    ItemPriceTextBox.Text = "";
                    ItemQuantityTextBox.Text = "";

                    form2.RefreshDataGrid(inventoryManager);
                    // MessageBox.Show("Item added successfully");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid values");
            }
        }

        private void ShowInventoryButton_Click(object sender, EventArgs e)
        {
            form2.inventoryManager = inventoryManager;
            form2.Show();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
