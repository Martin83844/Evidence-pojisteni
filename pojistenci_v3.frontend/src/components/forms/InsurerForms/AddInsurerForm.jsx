import React, { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { setupValidation, validateForm } from "../../../functions/validation";
import { AddFormInput } from "../../inputs/AddFormInput";
import { SubmitButtonWithSpinner } from "../../buttons/SubmitButtonWithSpinner";

const AddInsurerForm = () => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    email: "",
    password: "",
    confirmPassword: "",
    name: "",
    surname: "",
    phoneNumber: "",
    city: "",
    postcode: "",
    street: "",
  });
  const [submitting, setSubmitting] = useState(false);

  useEffect(() => {
    setupValidation("addInsurerOrInsuredForm");
  }, []);

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validateForm("addInsurerOrInsuredForm")) return;

    if (formData.password !== formData.confirmPassword) {
      return;
    }

    setSubmitting(true);
    try {
      await axios.post("/api/Account/register-insurer", {
        ...formData,
        role: "Insurer",
      });
      navigate("/insurers");
    } catch (error) {
      console.error("Chyba při registraci pojistitele:", error.response?.data || error.message);
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="container-fluid px-0">
      <div className="card rounded-0">
        <div className="card-header sticky-header">
          <h1 className="text-center mb-3">Přidat pojistitele</h1>
        </div>
        <div className="cardpositioning p-0">
          <form id="addInsurerOrInsuredForm" className="form" onSubmit={handleSubmit} noValidate>
            <div className="card-body card-body-edit">
              <div className="row d-flex justify-content-around">
                <div className="col-md-4">
                  <AddFormInput type="text" name="name" label="Jméno:" handleChange={handleChange} placeholder="Jméno" pattern="^[A-ZÁ-Ža-zá-ž]{2,50}$" value={formData.name} />
                  <AddFormInput type="email" name="email" label="Email:" handleChange={handleChange} placeholder="example@domain.com" value={formData.email} />
                  <AddFormInput type="password" name="password" label="Heslo:" handleChange={handleChange} placeholder="Heslo" pattern="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$" value={formData.password} />
                  <AddFormInput type="text" name="city" label="Město:" handleChange={handleChange} placeholder="Město" pattern="^[A-ZÁ-Ža-zá-ž0-9]+([ \-][A-ZÁ-Ža-zá-ž0-9]+)*$" value={formData.city} />
                  <AddFormInput type="text" name="postcode" label="PSČ:" handleChange={handleChange} placeholder="12345" pattern="^\d{3} ?\d{2}$" value={formData.postcode} />
                </div>
                <div className="col-md-4">
                  <AddFormInput type="text" name="surname" label="Příjmení:" handleChange={handleChange} placeholder="Příjmení" pattern="^[A-ZÁ-Ža-zá-ž]{2,50}$" value={formData.surname} />
                  <AddFormInput type="text" name="phoneNumber" label="Telefonní číslo:" handleChange={handleChange} placeholder="+420XXXXXXXXX" pattern="^\+420[0-9]{9}$" value={formData.phoneNumber} />
                  <AddFormInput type="password" name="confirmPassword" label="Potvrzení hesla:" handleChange={handleChange} placeholder="Potvrzení hesla" pattern="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$" value={formData.confirmPassword} />
                  <AddFormInput type="text" name="street" label="Ulice a číslo:" handleChange={handleChange} placeholder="Ulice a číslo" pattern="^[A-ZÁ-Ža-ž0-9]+(?:[ \-\/][A-ZÁ-Ža-ž0-9]+)* ?\d+[A-Za-z]?\/?\d*[A-Za-z]?$" value={formData.street} />
                </div>
              </div>
            </div>
            <SubmitButtonWithSpinner submitting={submitting} />
          </form>
        </div>
      </div>
    </div>
  );
};

export default AddInsurerForm;