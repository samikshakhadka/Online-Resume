using System.ComponentModel.DataAnnotations;

namespace cv_database.Models
{
    public class contact
    {
        [Key]
        public int contact_id { get; set; }

        public string contact_name { get; set; }

        public string contact_link { get; set; }

        public string contact_href { get; set; }

        public int information_id { get; set; }


    }
}
