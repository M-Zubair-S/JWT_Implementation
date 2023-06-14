import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import "./components/Login.css";

const Login = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const navigate = useNavigate();

  const handleLogin = async () => {
    if (!username || !password) {
      alert("Please enter both username and password.");
      return;
    }
    const registration = {
      Username: username,
      Password: password,
    };

    const response = await axios.post(
      "https://localhost:7274/api/Registration/signin",
      registration
    );

    if (response.status === 200) {
      // Get the JWT token from the response
      const token = response.data.token;
      console.log(token);
      // Store the token in Local Storage
      window.localStorage.setItem("token", token);
      navigate("/home");
    } else {
      alert("Invalid Credential");
    }
  };

  return (
    <div className="login-form">
      <h1>Login</h1>
      <input
        type="text"
        placeholder="Username"
        value={username}
        onChange={(e) => setUsername(e.target.value)}
      />

      <input
        type="password"
        placeholder="Password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      <button onClick={handleLogin}>Login</button>
    </div>
  );
};

export default Login;
