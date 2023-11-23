import React from 'react';
import './App.css';
import axios from 'axios';
import { useState } from "react";
import { useNavigate } from 'react-router-dom';




function Login() {

    const navigate = useNavigate();

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const emailChangeHandler = (e) => {
        setEmail(e.target.value);
    };
    const passwordChangeHandler = (e) => {
        setPassword(e.target.value);
    };
    const formSubmitHandler = async (e) => {
        e.preventDefault();
        try {
            const response = await axios.get("https://localhost:7222/api/Login", {
                headers: {
                    UserEmail: email,
                    Password: password, // Use "pass" as the header name
                    Authorization: "asdfghj"
                },
            });
            // Handle successful login (e.g., redirect to dashboard)

            if(response)
            {
                localStorage.setItem("UserEmail", JSON.stringify(response.data))
                navigate("/Login");
            }
        } catch (error) {
            // Handle login error (e.g., show error message)
            console.error(error);
        }
    };








    return (
        <div className="bg-gray-100 min-h-screen flex items-center justify-center">
            <div className="bg-white p-8 rounded shadow-md w-80">
                <h2 className="text-2xl font-semibold text-center mb-4">Login</h2>
                <form onSubmit={formSubmitHandler}>
                    <div className="mb-4">
                        <label htmlFor="UserName" className="block text-gray-600">User Name</label>
                        <input
                            type="UserName"
                            id="UserName"
                            name="UserName"
                            placeholder="text"

                            value={email}
                            onChange={emailChangeHandler}
                            className="border rounded w-full py-2 px-3 text-gray-700 focus:outline-none focus:ring focus:border-blue-300"
                        />
                    </div>

                    <div className="mb-6">
                        <label htmlFor="password" className="block text-gray-600">Password</label>
                        <input
                            type="password"
                            id="password"
                            name="password"
                            placeholder="Password"
                            value={password}

                            onChange={passwordChangeHandler}
                            className="border rounded w-full py-2 px-3 text-gray-700 focus:outline-none focus:ring focus:border-blue-300"
                        />
                    </div>

                    <div className="flex items-center justify-between">
                        <button
                            type="submit"
                            className="bg-blue-500 hover:bg-blue-600 text-white py-2 px-4 rounded focus:outline-none focus:ring focus:border-blue-300"
                        >
                            Login
                        </button>
                        
                    </div>
                </form>
            </div>
        </div>
    );
}

export default Login;
