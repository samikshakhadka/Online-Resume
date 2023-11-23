using AutoMapper;
using cv_database.Models;
using cv_database.DTOs.InformationsDTOs;
using cv_database.DTOs.SkillsDTOs;
using cv_database.DTOs.ExperiencesDTOs;
using cv_database.DTOs.EducationsDTOs;
using cv_database.DTOs.ContactsDTOs;

namespace cv_database.Helpers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles() {

            CreateMap<Information, InformationsReadDTOs>();
            CreateMap<InformationsCreateDTOs, Information>();
            CreateMap<InformationsUpdateDTOs, Information>();
            CreateMap<Information, InformationsUpdateDTOs>();


            //skilll
            CreateMap<Skills, SkillsReadDTOs>();
            CreateMap<SkillsCreateDTOs, Skills>();
            CreateMap<SkillsUpdateDTOs, Skills>();
            CreateMap<Skills, SkillsUpdateDTOs>();

            //experiences
            CreateMap<Experience, ExperiencesReadDTOs>();
            CreateMap<ExperiencesCreateDTOs, Experience>();
            CreateMap<ExperiencesUpdateDTOs, Experience>();
            CreateMap<Experience, ExperiencesUpdateDTOs>();

            //education
            CreateMap<Education, EducationsReadDTOs>();
            CreateMap<EducationsCreateDTOs, Education>();
            CreateMap<EducationsUpdateDTOs, Education>();
            CreateMap<Education, EducationsUpdateDTOs>();


            //contacts

            CreateMap< contact, ContactsReadDTOs>();
            CreateMap<ContactsCreateDTOs, contact>();
            CreateMap<ContactsUpdateDTOs, contact>();
            CreateMap<contact, ContactsUpdateDTOs>();
        }
    }
}
