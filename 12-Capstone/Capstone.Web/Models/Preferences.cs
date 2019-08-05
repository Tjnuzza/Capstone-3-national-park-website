using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Preferences
    {
        // We aren't sure how JSON would interact with a primitive data type (i.e. a boolean)
        // It would probably be fine, but we decided to do it the way we know would work

        // Default to Fahrenheit because it's an American site
        public string PreferredTemperature { get; set; } = "fahrenheit";
    }
}