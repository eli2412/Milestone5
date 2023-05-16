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
    public partial class btnSignIn : Form
    {
        InventoryManager inventoryManager;
        Form2 form2;
        public btnSignIn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String username = "elias.cruz";
            String password = "dogs are cool";

            if (userBox.Text.Equals(username) && passBox.Text.Equals(password))
            {
                MessageBox.Show("Successful log in! Item added");
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong username or password. Try again");
            }
        }
    }
}
