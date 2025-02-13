import React, { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { useUser } from "../../utils/UserProvider";

const LoginForm = () => {
  const [formData, setFormData] = useState({
    email: "",
    password: "",
  });
  const [error, setError] = useState("");
  const navigate = useNavigate();
  const { setUser } = useUser();

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post("/api/Account/Login", formData);
      const userResponse = await axios.get("/api/Account/me");
      setUser(userResponse.data);
      navigate("/home");
    } catch (error) {
      console.error("Chyba při přihlášení:", error.response?.data || error.message);
      setError(error.response?.data?.Message || "Neplatné přihlašovací údaje.");
    }
  };

  return (
    <div className="container d-flex justify-content-center align-items-center" style={{ height: "100vh" }}>
      <div className="card shadow-sm p-4" style={{ maxWidth: "400px", width: "100%" }}>
        <h2 className="text-center mb-4">Přihlášení</h2>
        {error && <div className="alert alert-danger">{error}</div>}
        <form onSubmit={handleSubmit}>
          <div className="mb-3">

            <label htmlFor="email" className="form-label">Email</label>
            <input
              type="email"
              name="email"
              id="email"
              className="form-control"
              value={formData.email}
              onChange={handleChange}
              placeholder="Zadejte email"
              required
            />
          </div>
          <div className="mb-3">

            <label htmlFor="password" className="form-label">Heslo</label>
            <input
              type="password"
              name="password"
              id="password"
              className="form-control"
              value={formData.password}
              onChange={handleChange}
              placeholder="Zadejte heslo"
              required
            />
            
          </div>
          <button type="submit" className="btn btn-primary w-100">Přihlásit se</button>
        </form>
        <div className="mt-3 text-center">
          <a href="/forgot-password" className="text-decoration-none">Zapomněli jste heslo?</a>
        </div>
      </div>
    </div>
  );
};

export default LoginForm;
