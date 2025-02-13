import React, { useEffect, useState } from "react";
import { useParams, useNavigate, Link, useLocation } from "react-router-dom";
import apiClient from "../../utils/apiClient";
import deleteDamageRecord from "../../functions/deleteDamageRecord";
import { LoadingSpinner } from "../../utils/LoadingSpinner";
import { DeleteButton } from "../buttons/DeleteButton";

const HomeDamageRecordDetail = () => {
  const { id } = useParams();
  const { state } = useLocation();
  const insuranceId = state?.insuranceId;
  const [damageRecord, setDamageRecord] = useState(null);
  const [confirmDelete, setConfirmDelete] = useState(false);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  const handleDelete = () => {
    deleteDamageRecord(id, insuranceId, navigate);
  };

  useEffect(() => {
    apiClient
      .get(`/homeinsurancedamagerecords/${id}`)
      .then((response) => {
        setDamageRecord(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Chyba při načítání detailu:", error);
        setLoading(false);
      });
  }, [id]);

  if (loading) {
    return <LoadingSpinner />;
  }

  return (
    <div className="container-fluid px-0">
      <div className="row">
        <div className="card rounded-0">
          <div className="card-header sticky-header">
            <h1 className="text-center mb-3">Detail události č. {damageRecord.id}</h1>
          </div>
          <div className="accident-damage-cardpositioning p-0">
            <div className="card-body card-body-accident-damage ">
              <div className="row d-flex justify-content-around">
                <div className="col-md-4">
                  <p><strong>Datum škody:</strong> {new Date(damageRecord.date).toLocaleDateString('cs-CZ')}</p>
                  <p><strong>Odhadovaná výše škody (Kč):</strong> {damageRecord.estimatedDamageCost}</p>
                  <p><strong>Schválená částka náhrady (Kč):</strong> {damageRecord.approvedCompensation}</p>
                </div>
                <div className="col-md-4">
                  <p><strong>Poškozená část:</strong></p>
                  <textarea className="form-control" rows="3" readOnly value={damageRecord.damagedPart}></textarea>
                  <p><strong>Popis škody:</strong></p>
                  <textarea className="form-control" rows="10" readOnly value={damageRecord.description}></textarea>
                </div>
              </div>
            </div>
            <div className="card-footer p-0">
              <Link to={`/edit-homeinsurancedamagerecords/${id}`} className="btn btn-secondary rounded-0 w-100">Upravit</Link>
              <DeleteButton
                confirmDelete={confirmDelete}
                setConfirmDelete={setConfirmDelete}
                handleDelete={handleDelete}
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default HomeDamageRecordDetail;