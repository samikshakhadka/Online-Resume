using System.ComponentModel.DataAnnotations;

namespace cv_database.Models
{
    public class Experience
    {
        [Key]
        public int experience_id { get; set; }
        public string institution_name { get; set; }
        public string position { get; set; }

        public DateTime start_date { get; set; }

        

        public DateTime? end_date { get; set; }

        

        

        public int information_id { get; set; }
    }
}
