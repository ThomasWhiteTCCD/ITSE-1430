﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nile.Windows {
    public partial class ProductDetailForm : Form 
    {
        #region Construction
        public ProductDetailForm () //: base()
        {
            InitializeComponent();
        }
        public ProductDetailForm( string title ) : this()
        {
            //InitializeComponent();

            Text = title;
        }

        public ProductDetailForm( string title, Product product )
        {
            //InitializeComponent();

            Text = title;
            Product = product;
        }
        #endregion

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);
            
            if (Product != null)
            {
                _txtName.Text = Product.Name;
                _txtDescription.Text = Product.Description;
                _txtPrice.Text = Product.Price.ToString();
                _chkDiscontinued.Checked = Product.IsDiscontinued;
            };

            ValidateChildren();
        }

        /// <summary>Gets or sets the product being shown.</summary>
        public Product Product { get; set; }

        private void OnCancel ( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }


        private void ShowError(string message, string title)
        {
            MessageBox.Show(this, message, title,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void OnSave( object sender, EventArgs e )
        {
            if (!ValidateChildren())
            {
                return;
            };

            //var product = new Product();
            //product.Id = Product?.Id ?? 0;
            //product.Name = _txtName.Text;
            //product.Description = _txtDescription.Text;
            //product.Price = GetPrice(_txtPrice);
            //product.IsDiscontinued = _chkDiscontinued.Checked;

            // Object initializer syntax
            var product = new Product() {
                Id = Product?.Id ?? 0,
                Name = _txtName.Text,
                Description = _txtDescription.Text,
                Price = GetPrice(_txtPrice),
                IsDiscontinued = _chkDiscontinued.Checked,
            };

            // Add validation

            // Using IValidateableObject

            // Using IValidateableObject
            if (!ObjectValidator.TryValidate(product, out var errors))               
            {
                //Show the error
               ShowError("Not valid", "Validation Error");
               return;
            };

            Product = product;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private decimal GetPrice ( TextBox control )
        {
            if (Decimal.TryParse(control.Text, out decimal price))          
                return price;         

            // Validate Price
            return -1;
        }

        private void ProductDetailForm_FormClosing( object sender, FormClosingEventArgs e )
        {
            // Please no
            // var form = (Form)sender;

            // Please yes
            var form = sender as Form;

            // casting for value types
            if(sender is int)
            {
                var intValue2 = (int)sender;
            };

            // Pattern matching
            if(sender is int intValue)
            {

            };

            if (MessageBox.Show(this, "Are you sure?", "Closing", MessageBoxButtons.YesNo) == DialogResult.No)
                e.Cancel = true;
        }

        private void OnValidatingPrice( object sender, CancelEventArgs e )
        {
            var tb = sender as TextBox;

            if(GetPrice(tb) < 0 )
            {
                _errors.SetError(_txtPrice, "Price must be >= 0.");
                e.Cancel = true;
            }

            else           
                _errors.SetError(_txtPrice, "");
            
        }

        private void OnValidatingName( object sender, CancelEventArgs e )
        {
            var tb = sender as TextBox;

            if (String.IsNullOrEmpty(tb.Text))
            {
                _errors.SetError(tb, "Name is required.");
            }
            else
                _errors.SetError(tb, "");
        }
    }
}
