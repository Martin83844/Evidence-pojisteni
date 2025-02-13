import React, { useState, useEffect } from "react";
import axios from "axios";
import { useParams, useNavigate } from "react-router-dom";
import { setupValidation, validateForm } from "../../../functions/validation";
import { EditFormInput } from "../../inputs/EditFormInput";
import { NumberFormInput } from "../../inputs/NumberFormInput";
import { FormTextArea } from "../../inputs/FormTextArea";
import { LoadingSpinner } from "../../../utils/LoadingSpinner";
import { SubmitButtonWithSpinner } from "../../buttons/SubmitButtonWithSpinner";

const EditCarAccidentRecordForm = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        damagedParts: "",
        otherPartiesInvolved: "",
        date: "",
        estimatedDamageCost: "",
        approvedCompensation: ""
    });

    const [loading, setLoading] = useState(true);
    const [submitting, setSubmitting] = useState(false);

    useEffect(() => {
        const fetchDamageRecord = async () => {
            try {
                const response = await axios.get(`/api/CarInsuranceAccidentRecord/${id}`);
                setFormData(response.data);
                setLoading(false);
            } catch (error) {
                console.error("Chyba při načítání události:", error.response?.data || error.message);
                setLoading(false);
            }
        };

        fetchDamageRecord();
        setupValidation("addOrEditCarAccidentRecordForm");
    }, [id]);

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!validateForm("addOrEditCarAccidentRecordForm")) return;

        setSubmitting(true);
        try {
            await axios.put(`/api/CarInsuranceAccidentRecord/${id}`, formData);
            navigate(`/carinsuranceaccidentrecord/${id}`)
        } catch (error) {
            console.error("Chyba při ukládání změn události:", error.response?.data || error.message);
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
                    <h1 className="text-center mb-3">Upravit událost č. {id}</h1>
                </div>
                <div className="cardpositioning p-0">
                    <form id="addOrEditCarAccidentRecordForm" className="form" onSubmit={handleSubmit} noValidate>
                        <div className="card-body card-body-edit">
                            <div className="row d-flex justify-content-around">
                                <div className="col-md-4">
                                    <EditFormInput type={"date"} name={"date"} label={"Datum škody:"} handleChange={handleChange} value={formData.date ? new Date(formData.date).toISOString().split('T')[0] : ""} />
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

export default EditCarAccidentRecordForm;
