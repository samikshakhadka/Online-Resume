import React from 'react';


export function Education(props) {
  return (
    <div>
        
            <p className='font-bold'>{props.name}</p>
            <p>{props.college_name} <br />
              {props.degree_name} <br />
              Start Date: {props.start_date} <br />
              End Date: {props.end_date}
            </p>
    </div>
  );
}


