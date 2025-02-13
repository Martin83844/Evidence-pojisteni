import React, { useEffect, useState } from "react";
import { useParams, useNavigate, Link } from "react-router-dom";
import apiClient from "../../utils/apiClient";
import deleteHomeInsurance from "../../functions/deleteHomeInsurance";
import InsuranceHomeDamageRecordsList from "../lists/damageRecordsLists/InsuranceHomeDamageRecordsList";
import { getEnumDisplayName, HomeInsuranceType, PropertyType } from "../../utils/enumUtils";
import { LoadingSpinner } from "../../utils/LoadingSpinner";
import { DeleteButton } from "../buttons/DeleteButton";

const HomeInsuranceDetail = () => {
  const { id } = useParams();
  const [insurance, setInsurance] = useState(null);
  const [confirmDelete, setConfirmDelete] = useState(false);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  const handleDelete = () => {
    deleteHomeInsurance(id, insurance.insured.id, navigate);
  };

  useEffect(() => {
    apiClient
      .get(`/homeinsurances/${id}`)
      .then((response) => {
        setInsurance(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Chyba při načítání detailu pojištění domova:", error);
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
                <p><strong>Typ pojištění:</strong> {getEnumDisplayName(HomeInsuranceType, insurance.homeInsuranceType)}</p>
                <p><strong>Pojištěný:</strong> {insurance.insured.surname} {insurance.insured.name}</p>
                <p><strong>Pojistník:</strong> {insurance.insurer.surname} {insurance.insurer.name}</p>
                <p><strong>Vlastník nemovitosti:</strong> {insurance.ownerName}</p>
                <p><strong>Kontakt na vlastníka:</strong> {insurance.ownerContact}</p>
                <p><strong>Cena:</strong> {insurance.price}</p>
                <p><strong>Platnost od:</strong> {new Date(insurance.validityFrom).toLocaleDateString('cs-CZ')}</p>
                <p><strong>Platnost do:</strong> {new Date(insurance.validityUntil).toLocaleDateString('cs-CZ')}</p>
                <p><strong>Adresa nemovitosti:</strong> {insurance.propertyAddress}</p>
                <p><strong>Typ nemovitosti:</strong> {getEnumDisplayName(PropertyType, insurance.propertyType)}</p>
                <p><strong>Rok výstavby:</strong> {insurance.yearBuilt}</p>
                <p><strong>Hodnota nemovitosti (Kč):</strong> {insurance.propertyValue}</p>
                <p><strong>Plocha nemovitosti (m<sup>2</sup>):</strong> {insurance.propertyArea}</p>
              </div>
              <div className="card-footer p-0">
                <Link to={`/edit-homeinsurance/${insurance.id}`} className="btn btn-secondary rounded-0 w-100">Upravit</Link>
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
          <InsuranceHomeDamageRecordsList insuranceId={insurance.id} />
        </div>
      </div>
    </div>
  );
};

export default HomeInsuranceDetail;
