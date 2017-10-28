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
 * Project: 3rd Programming Assignment
 * Class Time: 5:00 pm
 * Date: 10/18/2017
 */

namespace MovieLib.Windows 
{
    public partial class MainForm : Form 
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _gridMovies.AutoGenerateColumns = false;

            UpdateList();
        }

        private Movie GetSelectedMovie()
        {
            if (_gridMovies.SelectedRows.Count > 0)
                return _gridMovies.SelectedRows[0].DataBoundItem as Movie;

            return null;
        }

        private void UpdateList()
        {
            _bsMovies.DataSource = _database.GetAll().ToList();
        }

        private void OnFileExit(object sender, EventArgs e)
        {
            Close();
        }

        private void OnMovieAdd(object sender, EventArgs e)
        {
            
            var child = new MovieDetailForm("Movie Details");
            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            // Save Movie
            _database.Add(child.Movie);
            UpdateList();
        }

        private void OnMovieEdit(object sender, EventArgs e)
        {
            var movie = GetSelectedMovie();

            if (movie == null)
            {
                MessageBox.Show(this, "No movies available.");
                return;
            }

            EditMovie(movie);
        }

        private void EditMovie(Movie movie)
        {
            var child = new MovieDetailForm("Movie Details");
            child.Movie = movie;

            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            // Save movie
            _database.Update(child.Movie);
            UpdateList();
        }

        private void OnMovieDelete(object sender, EventArgs e)
        {
            var movie = GetSelectedMovie();

            if (movie == null)            
                return;           

            DeleteMovie(movie);
        }

        private void DeleteMovie(Movie movie)
        {
            // Confirm
            if (MessageBox.Show(this, $"Are you sure you want to delete '{movie.Title}'?",
                                "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            // Delete product
            _database.Remove(movie.Id);
            UpdateList();
        }

        private void OnHelpAbout(object sender, EventArgs e)
        {
            var about = new AboutBox();
            about.ShowDialog(this);
        }

        private void OnEditRow(object sender, DataGridViewCellEventArgs e)
        {
            var grid = sender as DataGridView;

            // Handle column clicks
            if (e.RowIndex < 0)
                return;

            var row = grid.Rows[e.RowIndex];
            var item = row.DataBoundItem as Movie;

            if (item != null)
                EditMovie(item);
        }

        private void OnKeyDownGrid(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode != Keys.Enter) && (e.KeyCode != Keys.Delete))
                return;

            var movie = GetSelectedMovie();           

            if (movie != null)
            {
                e.SuppressKeyPress = true;

                if(e.KeyCode == Keys.Delete)
                {
                    DeleteMovie(movie);
                }
                else if(e.KeyCode == Keys.Enter)
                {
                    EditMovie(movie);
                }
            }         
        }

        private IMovieDatabase _database = new MovieLib.Stores.SeedMemoryMovieDatabase();
    }
}
