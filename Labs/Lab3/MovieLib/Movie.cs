using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Name: Thomas White
 * Class: ITSE 1430
 * Project: 3rd Programming Assignment
 * Class Time: 5:00 pm
 * Date: 10/18/2017
 */

namespace MovieLib 
{
    /// <summary>Represents a movie.</summary>
    /// <remarks>This will represent a movie with other stuff.</remarks>
    public class Movie : IValidatableObject
    {
        ///<summary>Gets or sets the unique identifier.</summary>
        public int Id { get; set; }

        /// <summary>Gets or sets the title.</summary>
        /// <value>Never returns null.</value>
        public string Title
        {
            get { return _title ?? ""; }
            set { _title = value?.Trim(); }
        }

        /// <summary>Gets or sets the description.</summary>
        public string Description
        {
            get { return _description ?? ""; }
            set { _description = value?.Trim(); }
        }

        /// <summary>Gets or sets the length.</summary>
        public int Length { get; set; } = 0;

        /// <summary>Determines if owned.</summary>
        public bool IsOwned { get; set; }

        public override string ToString()
        {
            return Title;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Title cannot be empty
            if (String.IsNullOrEmpty(Title))
                yield return new ValidationResult("Title cannot be empty.", new[] { nameof(Title) });

            //Length >= 0
            if (Length < 0)
                yield return new ValidationResult("Length must be >= 0.", new[] { nameof(Length) });
        }

        private string _title;
        private string _description;
    }  
}
