using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace Plate.Model
{
    class Plate
    {
        /// <summary>
        /// Gets or sets the ID for this plate
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the plate
        /// </summary>
        public string name { get; set; }
    }
}
