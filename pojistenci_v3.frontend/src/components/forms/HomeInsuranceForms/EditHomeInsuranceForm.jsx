import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from 'axios';
import { getEnumDisplayName, HomeInsuranceType, PropertyType } from "../../../utils/enumUtils";
import { setupValidation, validateForm } from "../../../functions/validation";
import { EditFormInput } from "../../inputs/EditFormInput";
import { NumberFormInput } from "../../inputs/NumberFormInput";
import { LoadingSpinner } from "../../../utils/LoadingSpinner";
import { SubmitButtonWithSpinner } from "../../buttons/SubmitButtonWithSpinner";

const EditHomeInsuranceForm = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [insurers, setInsurers] = useState([]);
    const [loadingInsurances, setLoadingInsurances] = useState(true);
    const [loadingInsurers, setLoadingInsurers] = useState(true);
    const [submitting, setSubmitting] = useState(false);
    const [formData, setFormData] = useState({
        insurerId: "",
        ownerName: "",
        ownerContact: "",
        price: "",
        validityUntil: "",
        propertyValue: "",
    });

    useEffect(() => {
        const fetchInsurance = async () => {
            try {
                const response = await axios.get(`/api/HomeInsurances/${id}`);
                setFormData(response.data);
                setLoadingInsurances(false);
            } catch (error) {
                console.error("Chyba při načítání pojištění:", error.response?.data || error.message);
                setLoadingInsurances(false);
            }
        };


        const fetchInsurers = async () => {
            try {
                const response = await axios.get("/api/Insurers");
                setInsurers(response.data);
                setLoadingInsurers(false);
            } catch (error) {
                console.error("Chyba při načítání pojišťovatelů:", error.response?.data || error.message);
                setLoadingInsurers(false);
            }
        };

        fetchInsurance();
        fetchInsurers();
        setupValidation("editHomeInsuranceForm");
    }, [id]);

    const handleChange = (e) => {
        let { name, value } = e.target;

        if (name === "validityUntil") {
            const validityFromDate = new Date(formData.validityFrom);
            const validityUntilDate = new Date(value);

            if (validityUntilDate < validityFromDate) {
                document.getElementById("validityUntil-error").textContent = "Datum 'Platnost do' musí být pozdější než 'Platnost od'.";
                return;
            } else {
                document.getElementById("validityUntil-error").textContent = "";
            }
        }

        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!validateForm("editHomeInsuranceForm")) return;

        setSubmitting(true);
        try {
            await axios.put(`/api/HomeInsurances/${id}`, formData);
            navigate(`/homeinsurances/${id}`)
        } catch (error) {
            console.error("Chyba při ukládání změn pojištění:", error.response?.data || error.message);
        } finally {
            setSubmitting(false);
        }
    }

    if (loadingInsurances || loadingInsurers) {
        return <LoadingSpinner />;
    }

    return (
        <div className="container-fluid px-0">
            <div className="card rounded-0">
                <div className="card-header sticky-header">
                    <h1 className="text-center mb-3">Upravit pojištění č. {formData.id}</h1>
                </div>
                <div className="cardpositioning p-0">
                    <form id="editHomeInsuranceForm" className="form" onSubmit={handleSubmit} noValidate>
                        <div className="card-body card-body-edit">
                            <div className="row d-flex justify-content-around">
                                <div className="col-md-4">
                                    <EditFormInput type={"text"} name={"insuredName"} label={"Pojištěný:"} handleChange={handleChange} value={`${formData.insured.surname} ${formData.insured.name}`} disabled={true} />
                                    <div className="form-group mt-2">
                                        <label className="form-label" htmlFor="insurerId">Pojistník:</label>
                                        <select className="form-control" name="insurerId" value={formData.insurerId} onChange={handleChange} >
                                            {insurers.map((insurer) => (
                                                <option key={insurer.id} value={insurer.id}>
                                                    {`${insurer.surname} ${insurer.name}`}
                                                </option>
                                            ))}
                                        </select>
                                    </div>
                                    <EditFormInput type={"text"} name={"ownerName"} label={"Majitel nemovitosti:"} handleChange={handleChange} value={formData.ownerName} placeholder={"Jméno Příjmení"} pattern={"^[A-ZÁ-Ž][a-zá-ž]+ [A-ZÁ-Ž][a-zá-ž]+$"} />
                                    <EditFormInput type={"text"} name={"ownerContact"} label={"Kontakt na majitele:"} handleChange={handleChange} value={formData.ownerContact} placeholder={"+420XXXXXXXXX"} pattern="^\+420[0-9]{9}$" />
                                    <NumberFormInput name={"price"} label={"Cena:"} value={formData.price} handleChange={handleChange} />
                                    <EditFormInput type={"date"} name={"validityFrom"} label={"Platnost od:"} handleChange={handleChange} value={formData.validityFrom ? new Date(formData.validityFrom).toISOString().split('T')[0] : ""} disabled={true} />
                                    <div className="form-group mt-2">
                                        <label className="form-label" htmlFor="validityUntil">Platnost do:</label>
                                        <input className="form-control" id="validityUntil" type="date" name="validityUntil" value={formData.validityUntil ? new Date(formData.validityUntil).toISOString().split('T')[0] : ""} onChange={handleChange} required min={formData.validityFrom ? new Date(formData.validityFrom).toISOString().split('T')[0] : ""} />
                                        <span className="error-message" id="validityUntil-error"></span>
                                    </div>
                                </div>
                                <div className="col-md-4">
                                    <EditFormInput type={"text"} name={"homeInsuranceType"} label={"Typ pojištění:"} handleChange={handleChange} value={getEnumDisplayName(HomeInsuranceType, formData.homeInsuranceType)} disabled={true} />
                                    <EditFormInput type={"text"} name={"propertyType"} label={"Typ nemovitosti:"} handleChange={handleChange} value={getEnumDisplayName(PropertyType, formData.propertyType)} disabled={true} />
                                    <EditFormInput type={"text"} name={"propertyAddress"} label={"Adresa nemovitosti:"} handleChange={handleChange} value={formData.propertyAddress} disabled={true} />
                                    <EditFormInput type={"text"} name={"yearBuilt"} label={"Rok výstavby:"} handleChange={handleChange} value={formData.yearBuilt} disabled={true} />
                                    <NumberFormInput name={"propertyValue"} label={"Cena nemovitosti (Kč):"} value={formData.propertyValue} handleChange={handleChange} />
                                    <EditFormInput type={"text"} name={"propertyArea"} label={"Plocha nemovitosti (m²):"} handleChange={handleChange} value={formData.propertyArea} disabled={true} />
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



export default EditHomeInsuranceForm;