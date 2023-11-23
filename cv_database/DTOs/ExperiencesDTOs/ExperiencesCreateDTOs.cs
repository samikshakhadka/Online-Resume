namespace cv_database.DTOs.ExperiencesDTOs
{
    public class ExperiencesCreateDTOs
    {
        
        public string institution_name { get; set; }
        public string position { get; set; }

        public DateTime start_date { get; set; }



        public DateTime? end_date { get; set; }
    }
}
