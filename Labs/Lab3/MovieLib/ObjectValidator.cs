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
    /// <summary>Validates objects.</summary>
    public class ObjectValidator 
    {
        public static bool TryValidate(IValidatableObject value, out IEnumerable<ValidationResult> errors)
        {
            var context = new ValidationContext(value);
            var results = new List<ValidationResult>();

            errors = results;
            return Validator.TryValidateObject(value, context, results);
        }
    }
}
