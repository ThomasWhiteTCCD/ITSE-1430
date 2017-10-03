using System;
using System.Windows.Forms;

namespace Nile.Windows {
    public partial class MainForm : Form {

        public MainForm()
        {
            InitializeComponent();
            
        }

        private void OnFileExit( object sender, EventArgs e )
        {
            Close();
        }

        private void OnProductAdd( object sender, EventArgs e )
        {
            var child = new ProductDetailForm("Product Details");

            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            //TODO: Save product
            _product = child.Product;
        }

        private void OnProductEdit( object sender, EventArgs e )
        {
            // Create new ProductDetailForm
            var child = new ProductDetailForm("Product Details");

            // To populate the Edit Form
            child.Product = _product;

            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            //TODO: Save product
            _product = child.Product;

        }

        private void OnProductDelete( object sender, EventArgs e )
        {
            if(_product == null)
                return;

            // Confirm
            if (MessageBox.Show(this, $"Are you sure you want to delete '{_product.Name}'?",
                                 "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)            
                return;

            _product = null;                     
        }       

        private void OnHelpAbout( object sender, EventArgs e )
        {
            var about = new AboutBox();
            about.ShowDialog(this);

            //CallButton(OnProductAdd);
        }

        public delegate void ButtonClickCall( object sender, EventArgs eventArgs );

        private void CallButton ( ButtonClickCall functionToCall)
        {
            functionToCall(this, EventArgs.Empty);
        }

        private Product _product;

        private void MainForm_FormClosed( object sender, FormClosedEventArgs e )
        {
            // Please no
            // var form = (Form)sender;

            // Please yes
            var form = sender as Form;

            // Variation
            // casting for value types
            if (sender is int)
            {
                //var intValue = (int)sender;
            };

            // Pattern matching
            if (sender is int intValue)
            {

            };

            if (MessageBox.Show(this, "Are you sure?", "Closing", MessageBoxButtons.YesNo) == DialogResult.No)
                e.Cancel = true;
        }

        private void MainForm_FormClosing( object sender, FormClosingEventArgs e )
        {



        }
    }
}
