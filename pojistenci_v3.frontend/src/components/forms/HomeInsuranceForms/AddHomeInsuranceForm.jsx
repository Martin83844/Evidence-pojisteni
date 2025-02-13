import { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { useParams } from "react-router-dom";
import { PropertyType, HomeInsuranceType } from "../../../utils/enumUtils";
import { AddFormInput } from "../../inputs/AddFormInput";
import { NumberFormInput } from "../../inputs/NumberFormInput";
import { setupValidation, validateForm } from "../../../functions/validation";
import { SubmitButtonWithSpinner } from "../../buttons/SubmitButtonWithSpinner";

const AddHomeInsuranceForm = () => {
    const { id: insuredId } = useParams();
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        type: "HomeInsurance",
        propertyAddress: "",
        propertyType: "",
        yearBuilt: "",
        propertyValue: "",
        propertyArea: "",
        homeInsuranceType: "",
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
        setupValidation("addHomeInsuranceForm");
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
        if (!validateForm("addHomeInsuranceForm")) return;

        setSubmitting(true);
        try {
            const response = await axios.post("/api/HomeInsurances", {
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
                    <h1 className="text-center mb-3">Přidat pojištění nemovitosti</h1>
                </div>
                <div className="cardpositioning p-0">
                    <form id="addHomeInsuranceForm" onSubmit={handleSubmit} noValidate>
                        <div className="card-body card-body-edit">
                            <div className="row d-flex justify-content-around">
                                <div className="col-md-4">
                                    <AddFormInput type={"text"} name={"ownerName"} label={"Majitel nemovitosti:"} handleChange={handleChange} value={formData.ownerName} placeholder={"Jméno Příjmení"} pattern={"^[A-ZÁ-Ž][a-zá-ž]+ [A-ZÁ-Ž][a-zá-ž]+$"} />
                                    <AddFormInput type={"text"} name={"ownerContact"} label={"Kontakt na majitele:"} handleChange={handleChange} value={formData.ownerContact} placeholder={"+420XXXXXXXXX"} pattern="^\+420[0-9]{9}$" />
                                    <NumberFormInput name={"price"} label={"Cena:"} handleChange={handleChange} value={formData.price} />
                                    <AddFormInput type={"date"} name={"validityFrom"} label={"Platnost od:"} handleChange={handleChange} value={formData.validityFrom} />
                                    <div className="form-group mt-2">
                                        <label className="form-label" htmlFor="validityUntil">Platnost do:</label>
                                        <input className="form-control" id="validityUntil" type="date" name="validityUntil" value={formData.validityUntil} onChange={handleChange} required min={formData.validityFrom} />
                                        <span className="error-message" id="validityUntil-error"></span>
                                    </div>
                                </div>
                                <div className="col-md-4">
                                    <div className="form-group mt-2">
                                        <label className="form-label" htmlFor="homeInsuranceType">Typ pojištění:</label>
                                        <select className="form-control" name="homeInsuranceType" id="homeInsuranceType" value={formData.homeInsuranceType} onChange={handleChange} required >
                                            <option value="">Vyberte typ pojištění</option>
                                            {Object.entries(HomeInsuranceType).map(([key, value]) => (
                                                <option key={key} value={key}>
                                                    {value}
                                                </option>
                                            ))}
                                        </select>
                                        <span className="error-message" id="homeInsuranceType-error"></span>
                                    </div>
                                    <div className="form-group mt-2">
                                        <label className="form-label" htmlFor="propertyType">Typ Nemovitosti:</label>
                                        <select className="form-control" id="propertyType" name="propertyType" value={formData.propertyType} onChange={handleChange} required >
                                            <option value="">Vyberte typ nemovitosti</option>
                                            {Object.entries(PropertyType).map(([key, value]) => (
                                                <option key={key} value={key}>
                                                    {value}
                                                </option>
                                            ))}
                                        </select>
                                        <span className="error-message" id="propertyType-error"></span>
                                    </div>
                                    <AddFormInput type={"text"} name={"propertyAddress"} label={"Adresa nemovitosti:"} placeholder={"Např. Praha, Karlova 12"} handleChange={handleChange} value={formData.propertyAddress} pattern={"^[A-ZÁ-Ž][a-zá-ž]*(?:\\s[A-ZÁ-Ža-zá-ž]+)*(?:-[A-ZÁ-Ža-zá-ž]+)?,\\s[A-ZÁ-Ž][a-zá-ž]*(?:\\s[A-ZÁ-Ža-zá-ž.]+)*(?:-[A-ZÁ-Ža-zá-ž]+)?\\s\\d{1,4}(\\/\\d{1,4})?$"} />
                                    <AddFormInput type={"text"} name={"yearBuilt"} label={"Rok výstavby:"} handleChange={handleChange} value={formData.yearBuilt} />
                                    <NumberFormInput name={"propertyValue"} label={"Cena nemovitosti (Kč):"} handleChange={handleChange} value={formData.propertyValue} />
                                    <NumberFormInput name={"propertyArea"} label={"Plocha nemovitosti (m²):"} handleChange={handleChange} value={formData.propertyArea} />
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

export default AddHomeInsuranceForm;