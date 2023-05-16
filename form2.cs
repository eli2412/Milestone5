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
    public partial class Form2 : Form
    {
        public InventoryManager inventoryManager { get; set; }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // loads data grid
            ItemsDataGridView.ReadOnly = true;
            ItemsDataGridView.AutoGenerateColumns = true;

            RefreshDataGrid(inventoryManager);
        }
        
        public void RefreshDataGrid(InventoryManager inventoryManager) {
            // clears grid
            ItemsDataGridView.DataSource = null;
            ItemsDataGridView.Rows.Clear();

            var items = inventoryManager.GetItems();
            if (items == null)
            {
                return;
            }
            
            ItemsDataGridView.DataSource = items;

            ItemsDataGridView.Columns["Name"].HeaderText = "Item Name";
            ItemsDataGridView.Columns["Price"].HeaderText = "Item Price";
            ItemsDataGridView.Columns["Quantity"].HeaderText = "Item Quantity";

            ItemsDataGridView.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ItemsDataGridView.Columns["Price"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ItemsDataGridView.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            ItemsDataGridView.Columns["Name"].DisplayIndex = 0;
            ItemsDataGridView.Columns["Price"].DisplayIndex = 1;
            ItemsDataGridView.Columns["Quantity"].DisplayIndex = 2;

            // format price column
            ItemsDataGridView.Columns["Price"].DefaultCellStyle.Format = "c";

            // refresh
            ItemsDataGridView.Refresh();
        }

        private void RemoveSelectedButton_Click(object sender, EventArgs e)
        {
            // removes
            if (ItemsDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to remove");
                return;
            }

            var selectedItemName = ItemsDataGridView.SelectedRows[0].Cells["Name"].Value.ToString();

            var result = inventoryManager.RemoveItem(selectedItemName);
            if (result == -1)
            {
                MessageBox.Show("Item does not exist");
            }
            else
            {
                RefreshDataGrid(inventoryManager);
            }
        }

        private void RestockSelectedButton_Click(object sender, EventArgs e)
        {
            // edits items
            if (ItemsDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to restock");
                return;
            }

            var selectedItemName = ItemsDataGridView.SelectedRows[0].Cells["Name"].Value.ToString();

            InventoryItem selectedItem = inventoryManager.SearchItemByName(selectedItemName);
            if (selectedItem == null)
            {
                MessageBox.Show("Item does not exist");
                return;
            }
            // opens new form
            var form3 = new Form3();
            form3.inventoryItem = selectedItem;
            form3.form2 = this;
            form3.Show();

        }

        private void SearchByNameButton_Click(object sender, EventArgs e)
        {
            // search by name
            List<InventoryItem> items = null;
            InventoryItem item = inventoryManager.SearchItemByName(SearchTextBox.Text);
            if (item != null)
            {
                items = new List<InventoryItem>();
                items.Add(item);
            }

            ItemsDataGridView.DataSource = null;
            ItemsDataGridView.Rows.Clear();

            if (items == null)
            {
                return;
            }

            ItemsDataGridView.DataSource = items;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // clears search filter
            RefreshDataGrid(inventoryManager);
            SearchTextBox.Text = "";
        }

        private void SearchByPriceButton_Click(object sender, EventArgs e)
        {
            // search price
            List<InventoryItem> items = null;
            try
            {
                items = inventoryManager.SearchItemByPrice(Convert.ToDouble(SearchTextBox.Text));
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid price");
                return;
            }

            ItemsDataGridView.DataSource = null;
            ItemsDataGridView.Rows.Clear();

            if (items == null)
            {
                return;
            }

            ItemsDataGridView.DataSource = items;
        }
    }
}
