namespace cv_database.DTOs.EducationsDTOs
{
    public class EducationsUpdateDTOs
    {

        public int information_id { get; set; }
        public string name { get; set; }
        public string college_name { get; set; }


        public DateTime start_date { get; set; }
        public DateTime? end_date { get; set; }
        public string board_name { get; set; }
        public string degree_name { get; set; }
        
    }
}
