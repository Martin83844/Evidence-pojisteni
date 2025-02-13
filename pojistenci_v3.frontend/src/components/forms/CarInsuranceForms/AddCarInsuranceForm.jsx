import { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { useParams } from "react-router-dom";
import { CarInsuranceType, FuelType, UsageType } from "../../../utils/enumUtils";
import { AddFormInput } from "../../inputs/AddFormInput";
import { NumberFormInput } from "../../inputs/NumberFormInput";
import { setupValidation, validateForm } from "../../../functions/validation";
import { SubmitButtonWithSpinner } from "../../buttons/SubmitButtonWithSpinner";

const AddCarInsuranceForm = () => {
    const { id: insuredId } = useParams();
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        type: "CarInsurance",
        registrationNumber: "",
        VIN: "",
        make: "",
        model: "",
        yearOfManufacture: "",
        engineSize: "",
        fuelType: "",
        carInsuranceType: "",
        usageType: "",
        ownerName: "",
        ownerContact: "",
        price: "",
        validityFrom: new Date().toISOString().split("T")[0],
        validityUntil: new Date().toISOString().split("T")[0],
        insuredId: insuredId,
        insurerId: ""
    });
    const [submitting, setSubmitting] = useState(false);

    useEffect(() => {
        setupValidation("addCarInsuranceForm");
    }, []);

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

        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!validateForm("addCarInsuranceForm")) return;

        setSubmitting(true);
        try {
            const response = await axios.post("/api/CarInsurances", {
                ...formData,
                insuredId: insuredId
            });
            navigate(`/insureds/${insuredId}`);
        } catch (error) {
            console.error("Chyba při tvorbě pojištění:", error.response?.data || error.message);
        } finally {
            setSubmitting(false);
        }
    };

    return (
        <div className="container-fluid px-0">
            <div className="card rounded-0">
                <div className="card-header sticky-header">
                    <h1 className="text-center mb-3">Přidat pojištění vozu</h1>
                </div>
                <div className="cardpositioning p-0">
                    <form id="addCarInsuranceForm" onSubmit={handleSubmit} noValidate>
                        <div className="card-body card-body-edit">
                            <div className="row d-flex justify-content-around">
                                <div className="col-md-4">
                                    <AddFormInput type="text" name="ownerName" label="Majitel vozu:" handleChange={handleChange} value={formData.ownerName} placeholder="Jméno Příjmení" pattern="^[A-ZÁ-Ž][a-zá-ž]+ [A-ZÁ-Ž][a-zá-ž]+$" />
                                    <AddFormInput type="text" name="ownerContact" label="Kontakt na majitele:" handleChange={handleChange} value={formData.ownerContact} placeholder="+420XXXXXXXXX" pattern="^\+420[0-9]{9}$" />
                                    <NumberFormInput name="price" label="Cena:" value={formData.price} handleChange={handleChange} />
                                    <AddFormInput type={"date"} name={"validityFrom"} label={"Platnost od:"} handleChange={handleChange} value={formData.validityFrom} />
                                    <div className="form-group mt-2">
                                        <label className="form-label" htmlFor="validityUntil">Platnost do:</label>
                                        <input className="form-control" id="validityUntil" type="date" name="validityUntil" value={formData.validityUntil} onChange={handleChange} required min={formData.validityFrom} />
                                        <span className="error-message" id="validityUntil-error"></span>
                                    </div>
                                </div>
                                <div className="col-md-4">
                                    <div className="form-group mt-2">
                                        <label className="form-label" htmlFor="carInsuranceType">Typ pojištění:</label>
                                        <select className="form-control" name="carInsuranceType" id="carInsuranceType" value={formData.carInsuranceType} onChange={handleChange} required >
                                            <option value="">Vyberte typ pojištění</option>
                                            {Object.entries(CarInsuranceType).map(([key, value]) => (
                                                <option key={key} value={key}>
                                                    {value}
                                                </option>
                                            ))}
                                        </select>
                                        <span className="error-message" id="carInsuranceType-error"></span>
                                    </div>
                                    <div className="form-group mt-2">
                                        <label className="form-label" htmlFor="usageType">Účel vozu:</label>
                                        <select className="form-control"
                                            name="usageType" id="usageType"
                                            value={formData.usageType}
                                            onChange={handleChange}
                                            required
                                        >
                                            <option value="">Vyberte účel vozu</option>
                                            {Object.entries(UsageType).map(([key, value]) => (
                                                <option key={key} value={key}>
                                                    {value}
                                                </option>
                                            ))}
                                        </select>
                                        <span className="error-message" id="usageType-error"></span>
                                    </div>
                                    <AddFormInput type="text" name="registrationNumber" label="Registrační značka (SPZ):" handleChange={handleChange} value={formData.registrationNumber} placeholder="0A00000" pattern="^[0-9]{1}[A-Z0-9]{1,2}\d{1,4}[A-Z0-9]{1,2}$" />
                                    <AddFormInput type={"text"} name={"VIN"} label={"VIN:"} value={formData.VIN} handleChange={handleChange} pattern="^[A-HJ-NPR-Z0-9]{17}$" placeholder="Např. WAUZZZ8K4AA123456" />
                                    <AddFormInput type={"text"} name={"make"} label={"Výrobce vozidla:"} value={formData.make} handleChange={handleChange} />
                                    <AddFormInput type={"text"} name={"model"} label={"Model vozidla:"} value={formData.model} handleChange={handleChange} />
                                    <AddFormInput type={"number"} name={"yearOfManufacture"} label={"Rok výroby:"} value={formData.yearOfManufacture} handleChange={handleChange} />
                                    <AddFormInput type={"number"} name={"engineSize"} label={"Objem motoru (cm³):"} value={formData.engineSize} handleChange={handleChange} />
                                    <div className="form-group mt-2">
                                        <label className="form-label" htmlFor="fuelType">Typ paliva:</label>
                                        <select className="form-control" name="fuelType" id="fuelType" value={formData.fuelType} onChange={handleChange} required >
                                            <option value="">Vyberte typ paliva</option>
                                            {Object.entries(FuelType).map(([key, value]) => (
                                                <option key={key} value={key}>
                                                    {value}
                                                </option>
                                            ))}
                                        </select>
                                        <span className="error-message" id="fuelType-error"></span>
                                    </div>
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

export default AddCarInsuranceForm;