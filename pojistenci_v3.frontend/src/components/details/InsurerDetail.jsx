import React, { useEffect, useState } from "react";
import { useParams, useNavigate, Link } from "react-router-dom";
import apiClient from "../../utils/apiClient";
import deleteInsurer from "../../functions/deleteInsurer";
import { LoadingSpinner } from "../../utils/LoadingSpinner";
import { DeleteButton } from "../buttons/DeleteButton";
import { InsuranceTypeButton } from "../buttons/InsuranceTypeButton";

const InsurerDetail = () => {
  const { id } = useParams();
  const [insurer, setInsurer] = useState(null);
  const [confirmDelete, setConfirmDelete] = useState(false);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  const handleDelete = () => {
    deleteInsurer(id, navigate);
  };

  useEffect(() => {
    apiClient
      .get(`/insurers/${id}`)
      .then((response) => {
        setInsurer(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Chyba při načítání detailu pojistníka:", error);
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
                <h2>Detail pojistníka</h2>
              </div>
            </div>
            <div className="usersdetail-cardpositioning">
              <div className="card-body card-body-users">
                <p><strong>Jméno:</strong> {insurer.name}</p>
                <p><strong>Příjmení:</strong> {insurer.surname}</p>
                <p><strong>Email:</strong> {insurer.email}</p>
                <p><strong>Telefon:</strong> {insurer.phoneNumber}</p>
                <p><strong>Město:</strong> {insurer.city}</p>
                <p><strong>PSČ:</strong> {insurer.postcode}</p>
                <p><strong>Ulice:</strong> {insurer.street}</p>
              </div>
              <div className="card-footer p-0">
                <Link to={`/edit-insurer/${id}`} className="btn btn-secondary rounded-0 w-100">Upravit</Link>
                <DeleteButton
                  confirmDelete={confirmDelete}
                  setConfirmDelete={setConfirmDelete}
                  handleDelete={handleDelete}
                />
              </div>
            </div>
          </div>
        </div>
        <InsuranceTypeButton entity={insurer} type="insurer" />
      </div>
    </div>
  );
};

export default InsurerDetail;
