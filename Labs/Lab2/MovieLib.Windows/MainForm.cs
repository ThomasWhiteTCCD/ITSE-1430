using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/* Name: Thomas White
 * Class: ITSE 1430
 * Project: 2nd Programming Assignment
 * Class Time: 5:00 pm
 * Date: 10/7/2017
 */

namespace MovieLib.Windows 
{
    public partial class MainForm : Form 
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void OnFileExit(object sender, EventArgs e)
        {
            Close();
        }

        private void OnMovieAdd(object sender, EventArgs e)
        {
            
            var child = new MovieDetailForm("Product Details");
            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            //TODO: Save product
            _movie = child.Movie;
        }

        private void OnMovieEdit(object sender, EventArgs e)
        {
            if (_movie == null)
            {
                MessageBox.Show(this, "No movie exists.", "Alert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var child = new MovieDetailForm("Product Details");
            child.Movie = _movie;
            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            //TODO: Save product
            _movie = child.Movie;
        }

        private void OnMovieDelete(object sender, EventArgs e)
        {
            if (_movie == null)
            {
                MessageBox.Show(this, "No movie exists.", "Alert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }              

            //Confirm
            if (MessageBox.Show(this, $"Are you sure you want to delete '{_movie.Title}'?",
                                "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //TODO: Delete product
            _movie = null;
        }

        private void OnHelpAbout(object sender, EventArgs e)
        {
            var about = new AboutBox();
            about.ShowDialog(this);

            //CallButton(OnProductAdd);
        }

        private Movie _movie;
    }
}
