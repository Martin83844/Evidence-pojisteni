import React, { useEffect, useState, } from "react";
import { useParams, useNavigate, Link } from "react-router-dom";
import apiClient from "../../utils/apiClient";
import deleteInsured from "../../functions/deleteInsured";
import { LoadingSpinner } from "../../utils/LoadingSpinner";
import { DeleteButton } from "../buttons/DeleteButton";
import { InsuranceTypeButton } from "../buttons/InsuranceTypeButton";

const InsuredDetail = () => {
  const { id } = useParams();
  const [insured, setInsured] = useState(null);
  const [confirmDelete, setConfirmDelete] = useState(false);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  const handleDelete = () => {
    deleteInsured(id, navigate);
  };

  useEffect(() => {
    apiClient
      .get(`/insureds/${id}`)
      .then((response) => {
        setInsured(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Error fetching insured detail:", error);
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
                <h2>Detail pojištěného č. {insured.customerNumber}</h2>
              </div>
            </div>
            <div className="usersdetail-cardpositioning">
              <div className="card-body card-body-users">
                <p><strong>Jméno:</strong> {insured.name}</p>
                <p><strong>Příjmení:</strong> {insured.surname}</p>
                <p><strong>Email:</strong> {insured.email}</p>
                <p><strong>Telefon:</strong> {insured.phoneNumber}</p>
                <p><strong>Město:</strong> {insured.city}</p>
                <p><strong>PSČ:</strong> {insured.postcode}</p>
                <p><strong>Ulice:</strong> {insured.street}</p>
              </div>
              <div className="card-footer p-0">
                <Link to={`/edit-insured/${id}`} className="btn btn-secondary rounded-0 w-100">Upravit</Link>
                <DeleteButton
                  confirmDelete={confirmDelete}
                  setConfirmDelete={setConfirmDelete}
                  handleDelete={handleDelete}
                />
              </div>
            </div>
          </div>
        </div>
        <InsuranceTypeButton entity={insured} type="insured" />
      </div>
    </div>
  );
};

export default InsuredDetail;
