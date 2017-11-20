

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            get => _title ?? ""; 
            set => _title = value?.Trim();
        }

        /// <summary>Gets or sets the description.</summary>
        public string Description
        {
            get => _description ?? ""; 
            set => _description = value?.Trim(); 
        }

        /// <summary>Gets or sets the length.</summary>
        public int Length { get; set; } = 0;

        /// <summary>Determines if owned.</summary>
        public bool IsOwned { get; set; }

        public override string ToString() => Title;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Title cannot be empty
            if (String.IsNullOrEmpty(Title))
                yield return new ValidationResult("Title cannot be empty.", new[] { nameof(Title) });

            //Length >= 0
            if (Length < 0)
                yield return new ValidationResult("Length must be >= 0.", new[] { nameof(Length) });
        }

        #region Private Members

        private string _title;
        private string _description;
        #endregion
    }  
}
