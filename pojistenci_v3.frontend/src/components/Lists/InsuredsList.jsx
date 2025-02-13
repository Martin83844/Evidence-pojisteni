import React, { useEffect, useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import apiClient from '../../utils/apiClient';
import { LoadingSpinner } from '../../utils/LoadingSpinner';

const InsuredsList = () => {
  const [insureds, setInsureds] = useState([]);
  const [sortConfig, setSortConfig] = useState({ key: null, direction: 'asc' });
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    apiClient
      .get('/insureds')
      .then((response) => {
        setInsureds(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error('Error fetching insureds:', error);
        setLoading(false);
      });
  }, []);

  const handleSort = (key) => {
    let direction = 'asc';
    if (sortConfig.key === key && sortConfig.direction === 'asc') {
      direction = 'desc';
    }
    setSortConfig({ key, direction });

    const sortedData = [...insureds].sort((a, b) => {
      if (a[key] < b[key]) {
        return direction === 'asc' ? -1 : 1;
      }
      if (a[key] > b[key]) {
        return direction === 'asc' ? 1 : -1;
      }
      return 0;
    });
    setInsureds(sortedData);
  };

  const handleRowClick = (id) => {
    navigate(`/insureds/${id}`);
  };

  if (loading) {
    return <LoadingSpinner />;
  }

  return (
    <div className="container-fluid px-0">
      <div className="card rounded-0">
        <div className="card-header">
          <h1 className="text-center mb-3">Seznam pojištěných</h1>
          <div className="row fw-bold pb-2">
            <div className="col-md-2 sortable" onClick={() => handleSort('customerNumber')} style={{ cursor: 'pointer' }}>
              Číslo {sortConfig.key === 'customerNumber' ? (sortConfig.direction === 'asc' ? '↑' : '↓') : ''}
            </div>
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
            <div className="col-md-1 sortable" onClick={() => handleSort('city')} style={{ cursor: 'pointer' }}>
              Město {sortConfig.key === 'city' ? (sortConfig.direction === 'asc' ? '↑' : '↓') : ''}
            </div>
          </div>
        </div>
        <div className="card-body" style={{ flexGrow: 1, overflowY: "auto", height: "calc(100vh - 232px)" }}>
            <div className="row">
              {insureds.map((insured) => (
                <div
                  className="col-12"
                  key={insured.id}
                  onClick={() => handleRowClick(insured.id)}
                  style={{ cursor: 'pointer' }}
                >
                  <div className="row row-hover py-2 border-bottom">
                    <div className="col-md-2">{insured.customerNumber}</div>
                    <div className="col-md-2">{insured.surname}</div>
                    <div className="col-md-2">{insured.name}</div>
                    <div className="col-md-3">{insured.email}</div>
                    <div className="col-md-2">{insured.phoneNumber}</div>
                    <div className="col-md-1">{insured.city}</div>
                  </div>
                </div>
              ))}
            </div>
        </div>
        <div className="card-footer row m-0 p-0" style={{ height: "40px" }}>
          <Link to="/add-insured" className="btn btn-secondary rounded-0">
            Přidat pojištěného
          </Link>
        </div>
      </div>
    </div>
  );
};

export default InsuredsList;

