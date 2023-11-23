import React from 'react';


export function Experience(props) {
    return (
        <div className="mt-2">
            <section className="mb-1">
                <div className="flex items-center">
                    <h4 className="text-xl font-base">{props.position} → {props.institution_name} ({props.start_date}) TO ({props.start_date})</h4>

                </div>
            </section>
        </div>
    );
}


