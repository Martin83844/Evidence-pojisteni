import React, { useState, useEffect } from "react";
import axios from "axios";
import { useParams, useNavigate } from "react-router-dom";
import { setupValidation, validateForm } from "../../../functions/validation";
import { EditFormInput } from "../../inputs/EditFormInput";
import { LoadingSpinner } from "../../../utils/LoadingSpinner";
import { SubmitButtonWithSpinner } from "../../buttons/SubmitButtonWithSpinner";

const EditInsurerForm = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    email: "",
    name: "",
    surname: "",
    phoneNumber: "",
    city: "",
    postcode: "",
    street: "",
  });

  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);

  useEffect(() => {
    const fetchInsurer = async () => {
      try {
        const response = await axios.get(`/api/Insurers/${id}`);
        setFormData(response.data);
        setLoading(false);
      } catch (error) {
        console.error("Chyba při načítání pojistitele:", error.response?.data || error.message);
        setLoading(false);
      }
    };

    fetchInsurer();
    setupValidation("editInsurerAndInsuredForm");
  }, [id]);

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validateForm("editInsurerAndInsuredForm")) return;

    setSubmitting(true);
    try {
      await axios.put(`/api/Insurers/${id}`, formData);
      navigate(`/insurers/${id}`);
    } catch (error) {
      console.error("Chyba při ukládání změn pojistitele:", error.response?.data || error.message);
    } finally {
      setSubmitting(false);
    }
  };

  if (loading) {
    return <LoadingSpinner />;
  }

  return (
    <div className="container-fluid px-0">
      <div className="card rounded-0">
        <div className="card-header sticky-header">
          <h1 className="text-center mb-3">Upravit pojistitele</h1>
        </div>
        <div className="cardpositioning p-0">
          <form id="insurerForm" className="form" onSubmit={handleSubmit} noValidate>
            <div className="card-body card-body-edit">
              <div className="row d-flex justify-content-around">
                <div className="col-md-4">
                  <EditFormInput type={"text"} name={"name"} label={"Jméno:"} handleChange={handleChange} placeholder={"Jméno"} pattern={"^[A-ZÁ-Ža-zá-ž]{2,50}$"} value={formData.name} />
                  <EditFormInput type={"email"} name={"email"} label={"Email:"} handleChange={handleChange} placeholder={"example@domain.com"} value={formData.email} />
                  <EditFormInput type={"text"} name={"city"} label={"Město:"} handleChange={handleChange} placeholder={"Město"} pattern="^[A-ZÁ-Ža-zá-ž0-9]+([ \-][A-ZÁ-Ža-zá-ž0-9]+)*$" value={formData.city} />
                  <EditFormInput type={"text"} name={"postcode"} label={"PSČ:"} handleChange={handleChange} placeholder={"12345"} pattern={"^\\d{3} ?\\d{2}$"} value={formData.postcode} />
                </div>

                <div className="col-md-4">
                  <EditFormInput type={"text"} name={"surname"} label={"Příjmení:"} handleChange={handleChange} placeholder={"Příjmení"} pattern={"^[A-ZÁ-Ža-zá-ž]{2,50}$"} value={formData.surname} />
                  <EditFormInput type={"text"} name={"phoneNumber"} label={"Telefonní číslo:"} handleChange={handleChange} placeholder={"+420XXXXXXXXX"} pattern={"^\\+420[0-9]{9}$"} value={formData.phoneNumber} />
                  <EditFormInput type={"text"} name={"street"} label={"Ulice a číslo:"} handleChange={handleChange} placeholder={"Ulice a číslo"} pattern="^[A-ZÁ-Ža-ž0-9]+(?:[ \-\/][A-ZÁ-Ža-ž0-9]+)* ?\d+[A-Za-z]?\/?\d*[A-Za-z]?$" value={formData.street} />
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

export default EditInsurerForm;
