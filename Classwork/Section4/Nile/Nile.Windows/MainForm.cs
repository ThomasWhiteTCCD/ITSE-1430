﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using Nile.Stores;

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

            //_miFileExit.Click += OnFileExit;

            //Lambda
            _miFileExit.Click += (s, ea) => Close();

           var connString = ConfigurationManager.ConnectionStrings["ProductDatabase"].ConnectionString;           
            _database = new Nile.Stores.Sql.SqlProductDatabase(connString);
            //ProductDatabaseExtensions.WithSeedData(_database);
            //_database.WithSeedData();

            _gridProducts.AutoGenerateColumns = false;

            UpdateList();                  
        }        

        private Product GetSelectedProduct()
        {
            //return _listProducts.SelectedItem as Product;
            if (_gridProducts.SelectedRows.Count > 0)
                return _gridProducts.SelectedRows[0].DataBoundItem as Product;
         
            return null;
        }

        //private List<Product> _products = new List<Product>();

        private void UpdateList()
        {
            //_listProducts.Items.Clear();
            //foreach (var product in _database.GetAll())
            // _listProducts.Items.Add(product);

            //new BindingList<Product>();
            
            _bsProducts.DataSource = _database.GetAll().ToList();
            //_products.Clear();
            //_products.AddRange(_database.GetAll());

            ////var data = _database.GetAll().ToList();

            //_gridProducts.DataSource = null;
            //_gridProducts.DataSource = _products;            
        }

        //private void OnFileExit( object sender, EventArgs e )
        //{
        //    Close();
        //}

        private void OnProductAdd( object sender, EventArgs e )
        {            
            var child = new ProductDetailForm("Product Details");
            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            // Save product 
            try
            {
                _database.Add(child.Product);
            } catch (ValidationException ex)
            {
                MessageBox.Show(this, "Validation Failed.", "Error");
            } catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error");              
            };
            
            UpdateList();
        }

        private void OnProductEdit( object sender, EventArgs e )
        {
            var product = GetSelectedProduct();

            if (product == null)
            {
                MessageBox.Show("No products available.");
                return;
            }

            EditProduct(product);
        }

        private void EditProduct( Product product )
        {
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
            var product = GetSelectedProduct();

            if (product == null)
            {
                return;
            }

            DeleteProduct(product);
        }

        private void DeleteProduct( Product product )
        {
            //Confirm
            if (MessageBox.Show(this, $"Are you sure you want to delete '{product.Name}'?",
                                "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            // Delete product
            try
            {
                _database.Remove(product.Id);
            } catch (Exception e)
            {
                DisplayError(e, "Delete Failed");
            };
            
            UpdateList();
        }

        private void DisplayError ( Exception error, string title = "Error" )
        {
            DisplayError(error.Message, title);
        }

        private void DisplayError (string message, string title = "Error")
        {
            MessageBox.Show(this, message, title ?? "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void OnHelpAbout( object sender, EventArgs e )
        {
            var about = new AboutBox();
            about.ShowDialog(this);
        }

        public delegate void ButtonClickCall( object sender, EventArgs e );

        private IProductDatabase _database;

        private void OnEditRow( object sender, DataGridViewCellEventArgs e )
        {
            var grid = sender as DataGridView;

            if(e.RowIndex < 0)            
                return;
            
            var row = grid.Rows[e.RowIndex];
            var item = row.DataBoundItem as Product;

            if(item != null)
               EditProduct(item);
        }

        private void OnKeyDownGrid( object sender, KeyEventArgs e )
        {
            if(e.KeyCode != Keys.Delete)            
                return;

            var product = GetSelectedProduct();
            if (product != null)
                DeleteProduct(product);
            
        }
        //Function of the product database
        //private Product[] _products = new Product[100];
    }
}
