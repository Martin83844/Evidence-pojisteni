import React, { useEffect, useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import apiClient from '../../utils/apiClient';
import { LoadingSpinner } from "../../utils/LoadingSpinner";

const InsurersList = () => {
  const [insurers, setInsurers] = useState([]);
  const [sortConfig, setSortConfig] = useState({ key: null, direction: 'asc' });
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    apiClient
      .get('/insurers')
      .then((response) => {
        setInsurers(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error('Error fetching insurers:', error);
        setLoading(false);
      });
  }, []);

  const handleSort = (key) => {
    let direction = 'asc';
    if (sortConfig.key === key && sortConfig.direction === 'asc') {
      direction = 'desc';
    }
    setSortConfig({ key, direction });

    const sortedData = [...insurers].sort((a, b) => {
      if (a[key] < b[key]) {
        return direction === 'asc' ? -1 : 1;
      }
      if (a[key] > b[key]) {
        return direction === 'asc' ? 1 : -1;
      }
      return 0;
    });
    setInsurers(sortedData);
  };

  const handleRowClick = (id) => {
    navigate(`/insurers/${id}`);
  };

  if (loading) {
    return <LoadingSpinner />;
  }

  return (
    <div className="container-fluid p-0">
      <div className="card rounded-0">
        <div className="card-header">
          <h1 className="text-center mb-3">Seznam pojistníků</h1>
          <div className="row fw-bold pb-2">
            <div className="col-md-2 sortable" onClick={() => handleSort('surname')} style={{ cursor: 'pointer' }}>
              Příjmení {sortConfig.key === 'surname' ? (sortConfig.direction === 'asc' ? '↑' : '↓') : ''}
            </div>
            <div className="col-md-2 sortable" onClick={() => handleSort('name')} style={{ cursor: 'pointer' }}>
              Jméno {sortConfig.key === 'name' ? (sortConfig.direction === 'asc' ? '↑' : '↓') : ''}
            </div>
            <div className="col-md-3 sortable" onClick={() => handleSort('email')} style={{ cursor: 'pointer' }}>
              Email {sortConfig.key === 'email' ? (sortConfig.direction === 'asc' ? '↑' : '↓') : ''}
            </div>
            <div className="col-md-2 sortable" onClick={() => handleSort('phoneNumber')} style={{ cursor: 'pointer' }}>
              Telefon {sortConfig.key === 'phoneNumber' ? (sortConfig.direction === 'asc' ? '↑' : '↓') : ''}
            </div>
            <div className="col-md-2 sortable" onClick={() => handleSort('city')} style={{ cursor: 'pointer' }}>
              Město {sortConfig.key === 'city' ? (sortConfig.direction === 'asc' ? '↑' : '↓') : ''}
            </div>
          </div>
        </div>
        <div className="card-body" style={{ flexGrow: 1, overflowY: "auto", height: "calc(100vh - 232px)" }}>
          <div className="row">
            {insurers.map((insurer) => (
              <div
                className="col-12"
                key={insurer.id}
                onClick={() => handleRowClick(insurer.id)}
                style={{ cursor: 'pointer' }}
              >
                <div className="row row-hover py-2 border-bottom">
                  <div className="col-md-2">{insurer.surname}</div>
                  <div className="col-md-2">{insurer.name}</div>
                  <div className="col-md-3">{insurer.email}</div>
                  <div className="col-md-2">{insurer.phoneNumber}</div>
                  <div className="col-md-2">{insurer.city}</div>
                </div>
              </div>
            ))}
          </div>
        </div>
        <div className="card-footer m-0 p-0" style={{ height: "40px" }}>
          <Link to="/add-insurer" className="btn btn-secondary rounded-0 w-100">
            Přidat pojistníka
          </Link>
        </div>
      </div>
    </div>
  );
};

export default InsurersList;