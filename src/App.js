import React, { useEffect, useState } from 'react';
import './App.css';
//import axios from 'axios'
import { useQuery } from 'react-query';
import { Education } from './Components/Educations';
import { Experience } from './Components/Experiences';
import { Skill } from './Components/Skills';
import { Contact } from './Components/Contacts';
import moment from "moment"
import { readRequest } from "./Components/apiHandler"

function App() {
  const [data, setData] = useState(null);
  const [educationData, setEducationData] = useState(null);
  const [experienceData, setExperienceData] = useState(null);
  const [skillData, setSkillData] = useState(null);
  const [contactData, setContactData] = useState(null);
  const user = JSON.parse(localStorage.getItem("UserEmail"));



  useEffect(() => {
    if (user) {
      setData(user);
      responseEducations();
      responseExperiences();
      responseSkills();
      responseContacts();
    }
  }, []);



  // const responseEducations = async () => {
  //   //For Educations
  //   await axios.get(`https://localhost:7222/api/Educations/${user.information_id}`,
  //     {
  //       headers: {
  //         Authorization: "sdhfslkdj"
  //       }
  //     })
  //     .then(res => {
  //       setEducationData(res.data);
  //     })
  //     .catch(err => {
  //       console.log(err);
  //     })
  // }


  // const responseExperiences = async () => {
  //   //For Experiences
  //   await axios.get(`https://localhost:7222/api/Experiences/${user.information_id}`,
  //     {
  //       headers: {
  //         Authorization: "sdhfslkdj"
  //       }
  //     })
  //     .then(res => {
  //       setExperienceData(res.data);
  //     })
  //     .catch(err => {
  //       console.log(err);
  //     })
  // }


  //const [skills, setSkills] = useState(null);
  const responseSkills = async () => {
    await readRequest("Skills/" + user.information_id)
      .then((res) => {
        setSkillData(res.data);
      })
      .catch((err) => {
        console.log(err);
      });
  };

  const responseEducations = async () => {
    await readRequest("Educations/" + user.information_id)
      .then((res) => {
        setEducationData(res.data);
      })
      .catch((err) => {
        console.log(err);
      });
  };

  const responseExperiences = async () => {
    await readRequest("Experiences/" + user.information_id)
      .then((res) => {
        setExperienceData(res.data);
      })
      .catch((err) => {
        console.log(err);
      });
  };


  const responseContacts = async () => {
    await readRequest("contacts/" + user.information_id)
      .then((res) => {
        setContactData(res.data);
      })
      .catch((err) => {
        console.log(err);
      });
  };


  // const responseSkills = async () => {
  //   //For Experiences
  //   await axios.get(`https://localhost:7222/api/Skills/${user.information_id}`,
  //     {
  //       headers: {
  //         Authorization: "sdhfslkdj"
  //       }
  //     })
  //     .then(res => {
  //       setSkillData(res.data);
  //     })
  //     .catch(err => {
  //       console.log(err);
  //     })
  // }


  // const responseContacts = async () => {
  //   //For Experiences
  //   await axios.get(`https://localhost:7222/api/Contacts/${user.information_id}`,
  //     {
  //       headers: {
  //         Authorization: "sdhfslkdj"
  //       }
  //     })
  //     .then(res => {
  //       setContactData(res.data);
  //     })
  //     .catch(err => {
  //       console.log(err);
  //     })



  // }


  return (

    <div className="h-screen cursor-not-allowed">
      <div className="bg-white mx-auto max-w-4xl p-10 border border-gray-300">
        <p className="text-center text-lg font-bold">{data?.name}</p>
        <div className="text-center text-lg font-medium break-all">
          Email: {data?.email}
        </div>

        <div className="mt-2">
          <section className="">
            <h2 className="text-2xl font-semibold ">ABOUT ME</h2>
            <hr className=" border-black mb-0 mt-0 my-5" />
            <p font-medium mt-0> {data?.summary}</p>
          </section>
        </div>

        {/* <div className="App">
          <div className='mt-2 text-left'>
            <section className="mb-2 ">
              <h2 className='text-2xl font-semibold'>EDUCATION</h2>
              <hr className=" border-black mt-0 my-5 mb-0" />
              {(educationData == null) ? <p>Loading..</p> :
                educationData.map((item, index) => (
                  <div key={index}>
                    <Education
                      name={item.name}
                      college_name={item.college_name}
                      degree_name={item.degree_name}
                      start_date={moment(item.start_date).format("MMMM YYYY")}
                      end_date={null?"Present": moment(item.end_date).format("MMMM YYYY")}
                    />
                  </div>
                ))}
            </section>
          </div>
        </div> */}
        <div>
          <section className='mb-2'>
            <h2 className='text-2xl font-semibold'> Education </h2>
            <hr className=" border-black mt-0 my-5 mb-0" />
            {educationData == null ? (
              <p>Wait.. Data is Loading..</p>
            ) : (
              educationData.map((education, index) => <Education education_id={education.education_id}

                name={education.name}
                college_name={education.college_name}
                degree_name={education.degree_name}
                start_date={moment(education.start_date).format("MMMM YYYY")}
                end_date={null ? "Present" : moment(education.end_date).format("MMMM YYYY")}
              />)
            )}
          </section>
        </div>

        {/* <div>
          <section className='mb-2'>
            <h2 className='text-2xl font-semibold'> EXPERIENCE </h2>
            <hr className=" border-black mt-0 my-5 mb-0" />
            {(experienceData == null) ? <p>Loading..</p> :
              experienceData?.map((item, index) => (
                <div key={index}>
                  <Experience
                    experience_id={item.experience_id}
                    institution_name={item.institution_name}
                    position={item.position}
                    start_date={moment(item.start_date).format("MMMM YYYY")}
                    end_date= {null?"Present": moment(item.end_date).format("MMMM YYYY")}
                    information_id={item.information_id} />
                </div>
              ))}
          </section>
        </div> */}

        <div>
          <section className='mb-2'>
            <h2 className='text-2xl font-semibold'> Experience </h2>
            <hr className=" border-black mt-0 my-5 mb-0" />
            {experienceData == null ? (
              <p>Wait.. Data is Loading..</p>
            ) : (
              experienceData.map((experience, index) => <Experience experience_id={experience.experience_id}


                institution_name={experience.institution_name}
                position={experience.position}
                start_date={moment(experience.start_date).format("MMMM YYYY")}
                end_date={null ? "Present" : moment(experience.end_date).format("MMMM YYYY")}
                information_id={experience.information_id}
              />)
            )}
          </section>
        </div>

        {/* <div>
          <section className='mb-2'>
            <h2 className='text-2xl font-semibold'> Skills </h2>
            <hr className=" border-black mt-0 my-5 mb-0" />
            {(skillData == null) ? <p>Loading..</p> :
              skillData?.map((item, index) => (
                <div key={index}>
                  <Skill
                    skill_id={item.skil_id}
                    skill_name = {item.skill_name}
                    information_id = {item.information_id} />
                </div>
              ))}
          </section>
        </div> */}



        <div>
          <section className='mb-2'>
            <h2 className='text-2xl font-semibold'> Skills </h2>
            <hr className=" border-black mt-0 my-5 mb-0" />
            {skillData == null ? (
              <p>Wait.. Data is Loading..</p>
            ) : (
              skillData.map((skill, index) => <Skill skill_id={skill.skil_id}
                skill_name={skill.skill_name}
                information_id={skill.information_id} />)
            )}
          </section>
        </div>

        {/* <div>
          <section className='mb-2'>
            <h2 className='text-2xl font-semibold'> References </h2>
            <hr className=" border-black mt-0 my-5 mb-0" />
            {(contactData == null) ? <p>Loading..</p> :
              contactData?.map((item, index) => (
                <div key={index}>
                  <Contact
                    contact_id= {item.contact_id}
                    contact_name= {item.contact_name}
                    contact_link = {item.contact_link}
                    contact_href= {item.contact_href}
                    information_id ={item.information_id}
                    />
                </div>
              ))} 
          </section>
        </div> */}

        <div>
          <section className='mb-2'>
            <h2 className='text-2xl font-semibold'> Contact </h2>
            <hr className=" border-black mt-0 my-5 mb-0" />
            {contactData == null ? (
              <p>Wait.. Data is Loading..</p>
            ) : (
              contactData.map((contact, index) => <Contact contact_id={contact.contact_id}
              contact_name= {contact.contact_name}
              contact_link = {contact.contact_link}
              contact_href= {contact.contact_href}
              information_id ={contact.information_id} />)
            )}
          </section>
        </div>
      </div>
    </div>
  );
}

export default App;
