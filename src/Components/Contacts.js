import React from 'react';


export function Contact(props) {
    console.log(props)
    return (
        <div>
            <p className='font-bold'> {props.contact_name}</p>
               <p> {props.contact_link} <br />
                {props.contact_href}<br />
                </p>

            
        </div>

    );
}


