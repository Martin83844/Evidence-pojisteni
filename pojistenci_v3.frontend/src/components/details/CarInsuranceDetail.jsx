import React, { useEffect, useState } from "react";
import { useParams, useNavigate, Link } from "react-router-dom";
import apiClient from "../../utils/apiClient";
import { getEnumDisplayName, CarInsuranceType, FuelType, UsageType } from "../../utils/enumUtils";
import deleteCarInsurance from "../../functions/deleteCarInsurance";
import InsuranceCarAccidentRecordsList from "../lists/damageRecordsLists/InsuranceCarAccidentRecordsList";
import { LoadingSpinner } from "../../utils/LoadingSpinner";
import { DeleteButton } from "../buttons/DeleteButton";

const CarInsuranceDetail = () => {
  const { id } = useParams();
  const [insurance, setInsurance] = useState(null);
  const [confirmDelete, setConfirmDelete] = useState(false);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  const handleDelete = () => {
    deleteCarInsurance(id, insurance.insured.id, navigate);
  };

  useEffect(() => {
    apiClient
      .get(`/carinsurances/${id}`)
      .then((response) => {
        setInsurance(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Chyba při načítání detailu pojištění vozidla:", error);
        setLoading(false);
      });
  }, [id]);

  if (loading) {
    return <LoadingSpinner />;
  }

  return (
    <div className="container-fluid px-0">
      <div className="row">
        <div className="col-md-3 p-0">
          <div className="card rounded-0">
            <div className="card-header">
              <div style={{ height: 80 }}>
                <h2>Detail pojištění č. {insurance.id}</h2>
              </div>
            </div>
            <div className="usersdetail-cardpositioning">
              <div className="card-body card-body-users">
                <p><strong>Typ pojištění:</strong> {getEnumDisplayName(CarInsuranceType, insurance.carInsuranceType)}</p>
                <p><strong>Pojištěný:</strong> {insurance.insured.surname} {insurance.insured.name}</p>
                <p><strong>Pojistník:</strong> {insurance.insurer.surname} {insurance.insurer.name}</p>
                <p><strong>Vlastník vozu:</strong> {insurance.ownerName}</p>
                <p><strong>Kontakt na vlastníka:</strong> {insurance.ownerContact}</p>
                <p><strong>Cena:</strong> {insurance.price}</p>
                <p><strong>Platnost od:</strong> {new Date(insurance.validityFrom).toLocaleDateString('cs-CZ')}</p>
                <p><strong>Platnost do:</strong> {new Date(insurance.validityUntil).toLocaleDateString('cs-CZ')}</p>
                <p><strong>Registrační značka (SPZ):</strong> {insurance.registrationNumber}</p>
                <p><strong>VIN:</strong> {insurance.vin}</p>
                <p><strong>Výrobce vozidla:</strong> {insurance.make}</p>
                <p><strong>Model vozidla:</strong> {insurance.model}</p>
                <p><strong>Rok výroby:</strong> {insurance.yearOfManufacture}</p>
                <p><strong>Objem motoru (m³):</strong> {insurance.engineSize}</p>
                <p><strong>Typ paliva:</strong> {getEnumDisplayName(FuelType, insurance.fuelType)}</p>
                <p><strong>Účel vozu:</strong> {getEnumDisplayName(UsageType, insurance.usageType)}</p>
              </div>
              <div className="card-footer p-0">
                <Link to={`/edit-carinsurance/${insurance.id}`} className="btn btn-secondary rounded-0 w-100">Upravit</Link>
                <DeleteButton
                  confirmDelete={confirmDelete}
                  setConfirmDelete={setConfirmDelete}
                  handleDelete={handleDelete}
                />
              </div>
            </div>
          </div>
        </div>
        <div className="col-md-9 p-0">
          <InsuranceCarAccidentRecordsList insuranceId={insurance.id} />
        </div>
      </div>
    </div>
  );
};

export default CarInsuranceDetail;