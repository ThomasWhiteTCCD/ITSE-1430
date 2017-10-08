using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Name: Thomas White
 * Class: ITSE 1430
 * Project: 2nd Programming Assignment
 * Class Time: 5:00 pm
 * Date: 10/7/2017
 */

namespace MovieLib 
{
    public class Movie 
    {
        public string Title
        {
            get { return _title ?? ""; }
            set { _title = value?.Trim(); }
        }

        public string Description
        {
            get { return _description ?? ""; }
            set { _description = value?.Trim(); }
        }

        public int Length { get; set; } = 0;

        public bool IsOwned { get; set; }

        public override string ToString()
        {
            return Title;
        }

        public virtual string Validate()
        {
            //Title cannot be empty
            if (String.IsNullOrEmpty(Title))
                return "Name cannot be empty.";

            //Length >= 0
            if (Length < 0)
                return "Length must be >= 0.";

            return null;
        }

        private string _title;
        private string _description;
    }  
}
