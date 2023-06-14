import React, { useState, useEffect } from "react";
import axios from "axios";
import "./home.css";

const Home = () => {
  const [data, setData] = useState([]);

  const fetchData = async () => {
    const token = window.localStorage.getItem("token");
    const response = await axios.get(
      "https://localhost:7274/api/Auth/getproduct",
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (response.status === 200) {
      setData(response.data);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <div>
      <h1>Home Page</h1>
      <table className="data-table">
        <thead>
          <tr>
            <th>Name</th>
            <th>Description</th>
          </tr>
        </thead>
        <tbody>
          {data.map((item) => (
            <tr key={item.Id}>
              <td>{item.Name}</td>
              <td>{item.Description}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default Home;
