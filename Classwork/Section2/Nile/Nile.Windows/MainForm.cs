using System;
using System.Windows.Forms;

namespace Nile.Windows {
    public partial class MainForm : Form {
        public MainForm()
        {
            InitializeComponent();
            
        }

        private void MainForm_Load( object sender, EventArgs e )
        {

        }

        private void button1_Click( object sender, EventArgs e )
        {
            var product2 = new Product();
            product2.Name = "Roger";

            var child = new ProductDetailForm("Product Details");

            if (child.ShowDialog(this) != DialogResult.OK)            
                return;          

            //TODO: Save product
            var product = child.Product;
        }
    }
}
