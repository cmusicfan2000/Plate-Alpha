using SQLite.Net.Attributes;

namespace Model
{
    class PlateModel
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

        /// <summary>
        /// Gets or sets the "a" value of the plate color
        /// </summary>
        public byte a { get; set; }

        /// <summary>
        /// Gets or sets the "r" value of the plate color
        /// </summary>
        public byte r { get; set; }

        /// <summary>
        /// Gets or sets the "g" value of the plate color
        /// </summary>
        public byte g { get; set; }

        /// <summary>
        /// Gets or sets the "b" value of the plate color
        /// </summary>
        public byte b { get; set; }
    }
}
