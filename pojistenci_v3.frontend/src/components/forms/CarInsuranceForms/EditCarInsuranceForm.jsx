import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from 'axios';
import { getEnumDisplayName, CarInsuranceType, FuelType, UsageType } from "../../../utils/enumUtils";
import { setupValidation, validateForm } from "../../../functions/validation";
import { EditFormInput } from "../../inputs/EditFormInput";
import { NumberFormInput } from "../../inputs/NumberFormInput";
import { LoadingSpinner } from "../../../utils/LoadingSpinner";
import { SubmitButtonWithSpinner } from "../../buttons/SubmitButtonWithSpinner";

const EditCarInsuranceForm = () => {
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
        registrationNumber: "",
        usageType: ""
    });

    useEffect(() => {
        const fetchInsurance = async () => {
            try {
                const response = await axios.get(`/api/CarInsurances/${id}`);
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
        setupValidation("editCarInsuranceForm");
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
        if (!validateForm("editCarInsuranceForm")) return;

        setSubmitting(true);
        try {
            await axios.put(`/api/CarInsurances/${id}`, formData);
            navigate(`/carinsurances/${id}`)
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
                    <form id="editCarInsuranceForm" className="form" onSubmit={handleSubmit} noValidate>
                        <div className="card-body card-body-edit" >
                            <div className="row d-flex justify-content-around">
                                <div className="col-md-4">
                                    <EditFormInput type="text" name="insuredName" label="Pojištěný:" handleChange={handleChange} value={`${formData.insured?.surname} ${formData.insured?.name}`} disabled />
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
                                    <EditFormInput type="text" name="ownerName" label="Majitel vozu:" handleChange={handleChange} value={formData.ownerName} placeholder="Jméno Příjmení" pattern="^[A-ZÁ-Ž][a-zá-ž]+ [A-ZÁ-Ž][a-zá-ž]+$" />
                                    <EditFormInput type="text" name="ownerContact" label="Kontakt na majitele:" handleChange={handleChange} value={formData.ownerContact} placeholder="+420XXXXXXXXX" pattern="^\+420[0-9]{9}$" />
                                    <NumberFormInput name="price" label="Cena:" value={formData.price} handleChange={handleChange} />
                                    <EditFormInput type="date" name="validityFrom" label="Platnost od:" handleChange={handleChange} value={formData.validityFrom ? new Date(formData.validityFrom).toISOString().split('T')[0] : ""} disabled />
                                    <div className="form-group mt-2">
                                        <label className="form-label" htmlFor="validityUntil">Platnost do:</label>
                                        <input className="form-control" id="validityUntil" type="date" name="validityUntil" value={formData.validityUntil ? new Date(formData.validityUntil).toISOString().split('T')[0] : ""} onChange={handleChange} required min={formData.validityFrom ? new Date(formData.validityFrom).toISOString().split('T')[0] : ""} />
                                        <span className="error-message" id="validityUntil-error"></span>
                                    </div>
                                </div>
                                <div className="col-md-4">
                                    <EditFormInput type="text" name="carInsuranceType" label="Typ pojištění:" handleChange={handleChange} value={getEnumDisplayName(CarInsuranceType, formData.carInsuranceType)} disabled />
                                    <div className="form-group mt-2">
                                        <label className="form-label" htmlFor="usageType">Účel vozu:</label>
                                        <select className="form-control" name="usageType" value={formData.usageType} onChange={handleChange} required >
                                            {Object.entries(UsageType).map(([key, value]) => (
                                                <option key={key} value={key}>
                                                    {value}
                                                </option>
                                            ))}
                                        </select>
                                    </div>
                                    <EditFormInput type="text" name="registrationNumber" label="Registrační značka (SPZ):" handleChange={handleChange} value={formData.registrationNumber} placeholder="0A00000" pattern="^[0-9]{1}[A-Z0-9]{1,2}\d{1,4}[A-Z0-9]{1,2}$" />
                                    <EditFormInput type={"text"} name={"VIN"} label={"VIN:"} value={formData.vin} handleChange={handleChange} disabled={true} />
                                    <EditFormInput type={"text"} name={"make"} label={"Výrobce vozidla:"} value={formData.make} handleChange={handleChange} disabled={true} />
                                    <EditFormInput type={"text"} name={"model"} label={"Model vozidla:"} value={formData.model} handleChange={handleChange} disabled={true} />
                                    <EditFormInput type={"number"} name={"yearOfManufacture"} label={"Rok výroby:"} value={formData.yearOfManufacture} handleChange={handleChange} disabled={true} />
                                    <EditFormInput type={"number"} name={"engineSize"} label={"Objem motoru (cm³):"} value={formData.engineSize} handleChange={handleChange} disabled={true} />
                                    <EditFormInput type="text" name="fuelType" label="Typ paliva:" handleChange={handleChange} value={getEnumDisplayName(FuelType, formData.fuelType)} disabled />
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



export default EditCarInsuranceForm;