using System.ComponentModel.DataAnnotations;

namespace cv_database.Models
{
    public class Information
    {
        [Key]
        public int information_id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
       
        public string Summary { get; set; }
        
        
    }
}
