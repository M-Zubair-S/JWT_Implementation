import React from "react";
import Login from "./Login";
import { Routes, Route } from "react-router-dom";
import "./App.css";
import Home from "./components/Home";

function App() {
  return (
    <div className="App">
      <Routes>
        <Route path="/" element={<Login />}></Route>
        <Route path="/home" element={<Home />}></Route>
      </Routes>
    </div>
  );
}

export default App;
