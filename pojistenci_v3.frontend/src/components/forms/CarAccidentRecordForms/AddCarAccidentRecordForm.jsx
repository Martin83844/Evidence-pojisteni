import { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate, useParams } from "react-router-dom";
import { setupValidation, validateForm } from "../../../functions/validation";
import { AddFormInput } from "../../inputs/AddFormInput";
import { NumberFormInput } from "../../inputs/NumberFormInput";
import { FormTextArea } from "../../inputs/FormTextArea";
import { SubmitButtonWithSpinner } from "../../buttons/SubmitButtonWithSpinner";

const AddCarAccidentRecordForm = () => {
  const { id: insuranceId } = useParams();
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    damagedParts: "",
    otherPartiesInvolved: "",
    description: "",
    estimatedDamageCost: "",
    approvedCompensation: "",
    date: new Date().toISOString().split("T")[0],
    insuranceId: insuranceId,
  });
  const [submitting, setSubmitting] = useState(false);

  useEffect(() => {
    setupValidation("addOrEditCarAccidentRecordForm");
  }, []);

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validateForm("addOrEditCarAccidentRecordForm")) return;

    setSubmitting(true);
    try {
      await axios.post("/api/CarInsuranceAccidentRecord", {
        ...formData,
        insuranceId: insuranceId,
      });
      navigate(`/carinsurances/${insuranceId}`);
    } catch (error) {
      console.error("Chyba při tvorbě události:", error.response?.data || error.message);
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="container-fluid px-0">
      <div className="card rounded-0">
        <div className="card-header sticky-header">
          <h1 className="text-center mb-3">Přidat pojistnou událost</h1>
        </div>
        <div className="cardpositioning p-0">
          <form id="addOrEditCarAccidentRecordForm" onSubmit={handleSubmit} noValidate>
            <div className="card-body card-body-edit">
              <div className="row d-flex justify-content-around">
                <div className="col-md-4">
                  <AddFormInput type={"date"} name={"date"} label={"Datum škody:"} value={formData.date} handleChange={handleChange} />
                  <NumberFormInput name={"estimatedDamageCost"} label={"Odhadovaná výše škody (Kč):"} value={formData.estimatedDamageCost} handleChange={handleChange} />
                  <NumberFormInput name={"approvedCompensation"} label={"Schválená částka náhrady (Kč):"} value={formData.approvedCompensation} handleChange={handleChange} />
                </div>
                <div className="col-md-4">
                  <FormTextArea name={"damagedParts"} label={"Poškozené části:"} value={formData.damagedParts} handleChange={handleChange} rows={"3"} placeholder={"Poškozené části"} />
                  <FormTextArea name={"otherPartiesInvolved"} label={"Další zúčastněné strany:"} value={formData.otherPartiesInvolved} handleChange={handleChange} rows={"3"} placeholder={"Ostatní zůčastněné strany"} />
                  <FormTextArea name={"description"} label={"Popis škody:"} value={formData.description} handleChange={handleChange} rows={"10"} placeholder={"Popis škody"} />
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

export default AddCarAccidentRecordForm;