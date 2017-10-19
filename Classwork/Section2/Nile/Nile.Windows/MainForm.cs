using System;
using System.Windows.Forms;

namespace Nile.Windows 
{
    public partial class MainForm : Form 
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad( EventArgs e )
        {
            // Once the object is created and before the window renders
            base.OnLoad(e);

            UpdateList();                  
        }

        //private int FindAvailableElement()
        //{
        //    for(var i = 0; i < _products.Length; ++i)
        //    {
        //        if(_products[i] == null)               
        //            return i;             
        //    };

        //    return -1;
        //}

        //private int FindFirstProduct()
        //{
        //    for (var i = 0; i < _products.Length; ++i)
        //    {
        //        if (_products[i] != null)
        //            return i;
        //    };

        //    return -1;
        //}

        private Product GetSelectedProduct()
        {
            return _listProducts.SelectedItem as Product;
        }

        private void UpdateList()
        {
            _listProducts.Items.Clear();

            foreach (var product in _database.GetAll())
                _listProducts.Items.Add(product);
        }

        private void OnFileExit( object sender, EventArgs e )
        {
            Close();
        }

        private void OnProductAdd( object sender, EventArgs e )
        {
            //var index = FindAvailableElement();

            //// Make sure there is room left in array
            //if (index < 0)
            //{
            //    MessageBox.Show("No more products available.");
            //};

            var child = new ProductDetailForm("Product Details");
            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            // Save product           
            _database.Add(child.Product);
            UpdateList();
        }

        private void OnProductEdit( object sender, EventArgs e )
        {
            //var index = FindFirstProduct();

            //// Are there any products
            //if(index < 0)
            //{
            //    MessageBox.Show("No products available.");
            //    return;
            //};

            var product = GetSelectedProduct();     

            if(product == null)
            {
                MessageBox.Show("No products available.");
                return;
            }

            var child = new ProductDetailForm("Product Details");
            child.Product = product;

            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            // Edit product
            _database.Update(child.Product);
            UpdateList();
        }

        private void OnProductDelete( object sender, EventArgs e )
        {
            //var index = FindFirstProduct();

            //if (index < 0)
            //    return;

            var product = GetSelectedProduct();

            if(product == null)
            {
                return;
            }

            //Confirm
            if (MessageBox.Show(this, $"Are you sure you want to delete '{product.Name}'?",
                                "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            // Delete product
            _database.Remove(product.Id);
            UpdateList();
        }

        private void OnHelpAbout( object sender, EventArgs e )
        {
            var about = new AboutBox();
            about.ShowDialog(this);

            //CallButton(OnProductAdd);
        }

        public delegate void ButtonClickCall( object sender, EventArgs e );

        private void CallButton( ButtonClickCall functionToCall )
        {
            functionToCall(this, EventArgs.Empty);
        }

        private IProductDatabase _database = new Nile.Stores.MemoryProductDatabase();
        //Function of the product database
        //private Product[] _products = new Product[100];
    }
}
