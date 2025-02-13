import { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate, useParams } from "react-router-dom";
import { setupValidation, validateForm } from "../../../functions/validation";
import { AddFormInput } from "../../inputs/AddFormInput";
import { FormTextArea } from "../../inputs/FormTextArea";
import { NumberFormInput } from "../../inputs/NumberFormInput";
import { SubmitButtonWithSpinner } from "../../buttons/SubmitButtonWithSpinner";

const AddHomeDamageRecordForm = () => {
    const { id: insuranceId } = useParams();
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        damagedPart: "",
        otherPartiesInvolved: "",
        description: "",
        estimatedDamageCost: "",
        approvedCompensation: "",
        date: new Date().toISOString().split("T")[0],
        insuranceId: insuranceId,
    });
    const [submitting, setSubmitting] = useState(false);

    useEffect(() => {
        setupValidation("addOrEditHomeDamageRecordForm");
    }, []);

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!validateForm("addOrEditHomeDamageRecordForm")) return;

        setSubmitting(true);
        try {
            const response = await axios.post("/api/HomeInsuranceDamageRecords", {
                ...formData,
                insuranceId: insuranceId
            });
            navigate(`/homeinsurances/${insuranceId}`);
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
                    <form id="addOrEditHomeDamageRecordForm" onSubmit={handleSubmit} noValidate>
                        <div className="card-body card-body-edit">
                            <div className="row d-flex justify-content-around">
                                <div className="col-md-4">
                                    <AddFormInput type={"date"} name={"date"} label={"Datum škody:"} value={formData.date} handleChange={handleChange} />
                                    <NumberFormInput name={"estimatedDamageCost"} label={"Odhadovaná výše škody (Kč):"} value={formData.estimatedDamageCost} handleChange={handleChange} />
                                    <NumberFormInput name={"approvedCompensation"} label={"Schválená částka náhrady (Kč):"} value={formData.approvedCompensation} handleChange={handleChange} />
                                </div>
                                <div className="col-md-4">
                                    <FormTextArea name={"damagedPart"} label={"Poškozená část:"} value={formData.damagedPart} handleChange={handleChange} rows={"3"} placeholder={"Poškozená část"} />
                                    <FormTextArea name={"description"} label={"Popis škody:"} value={formData.description} handleChange={handleChange} rows={"10"} placeholder={"Popis škody"} />
                                </div>
                            </div>
                        </div>
                        <SubmitButtonWithSpinner submitting={submitting} />
                    </form>
                </div>
            </div>
        </div>
    )
}

export default AddHomeDamageRecordForm;