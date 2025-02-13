import { useEffect, useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import apiClient from "../../../utils/apiClient";
import { HomeInsuranceType, getEnumDisplayName } from "../../../utils/enumUtils";
import { LoadingSpinner } from "../../../utils/LoadingSpinner";

const InsuredHomeInsurancesList = ({ insuredId }) => {
    const [insurances, setInsurances] = useState([]);
    const [sortConfig, setSortConfig] = useState({ key: null, direction: 'asc' });
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    useEffect(() => {
        apiClient
            .get(`/homeinsurances/insured/${insuredId}`)
            .then((response) => {
                setInsurances(response.data);
                setLoading(false);
            })
            .catch((error) => {
                console.error('Error fetching insurances:', error);
                setLoading(false);
            });
    }, [insuredId]);

    const handleSort = (key) => {
        let direction = 'asc';
        if (sortConfig.key === key && sortConfig.direction === 'asc') {
            direction = 'desc';
        }
        setSortConfig({ key, direction });

        const sortedData = [...insurances].sort((a, b) => {
            if (a[key] < b[key]) return direction === 'asc' ? -1 : 1;
            if (a[key] > b[key]) return direction === 'asc' ? 1 : -1;
            return 0;
        });
        setInsurances(sortedData);
    };

    const handleRowClick = (id) => {
        navigate(`/homeinsurances/${id}`);
    };

    if (loading) {
        return <LoadingSpinner />;
    }

    return (
        <div className="container-fluid px-0">
            <div className="card rounded-0">
                <div className="card-header">
                    <div style={{ height: "110px" }}>
                        <h1 className="text-center mb-3">Pojištění nemovitosti</h1>
                        <div className="row fw-bold pb-2">
                            <div className="col-md-2 sortable" onClick={() => handleSort('id')} style={{ cursor: 'pointer' }}>
                                Číslo pojištění {sortConfig.key === 'id' ? (sortConfig.direction === 'asc' ? '↑' : '↓') : ''}
                            </div>
                            <div className="col-md-2 sortable" onClick={() => handleSort('homeInsuranceType')} style={{ cursor: 'pointer' }}>
                                Typ pojištění {sortConfig.key === 'homeInsuranceType' ? (sortConfig.direction === 'asc' ? '↑' : '↓') : ''}
                            </div>
                            <div className="col-md-2 sortable" onClick={() => handleSort('price')} style={{ cursor: 'pointer' }}>
                                Cena {sortConfig.key === 'price' ? (sortConfig.direction === 'asc' ? '↑' : '↓') : ''}
                            </div>
                            <div className="col-md-2 sortable" onClick={() => handleSort('propertyAddress')} style={{ cursor: 'pointer' }}>
                                Adresa nemovitosti {sortConfig.key === 'propertyAddress' ? (sortConfig.direction === 'asc' ? '↑' : '↓') : ''}
                            </div>
                            <div className="col-md-2 sortable" onClick={() => handleSort('insured.surname')} style={{ cursor: 'pointer' }}>
                                Pojištěný {sortConfig.key === 'insured.surname' ? (sortConfig.direction === 'asc' ? '↑' : '↓') : ''}
                            </div>
                            <div className="col-md-2 sortable" onClick={() => handleSort('insurer.surname')} style={{ cursor: 'pointer' }}>
                                Pojistník {sortConfig.key === 'insurer.surname' ? (sortConfig.direction === 'asc' ? '↑' : '↓') : ''}
                            </div>
                        </div>
                    </div>
                </div>
                <div className="list-cardpositioning p-0">
                    <div className="card-body card-body-list">
                        <div className="row">
                            {insurances.map((insurance) => (
                                <div
                                    className="col-12"
                                    key={insurance.id}
                                    onClick={() => handleRowClick(insurance.id)}
                                    style={{ cursor: 'pointer' }}
                                >
                                    <div className="row row-hover py-2 border-bottom">
                                        <div className="col-md-2">{insurance.id}</div>
                                        <div className="col-md-2">
                                            {getEnumDisplayName(HomeInsuranceType, insurance.homeInsuranceType)}
                                        </div>
                                        <div className="col-md-2">{insurance.price}</div>
                                        <div className="col-md-2">{insurance.propertyAddress}</div>
                                        <div className="col-md-2">{insurance.insured.surname} {insurance.insured.name}</div>
                                        <div className="col-md-2">{insurance.insurer.surname} {insurance.insurer.name}</div>
                                    </div>
                                </div>
                            ))}
                        </div>
                    </div>
                    <div className="card-footer p-0">
                        <Link to={`/add-homeinsurance/${insuredId}`} className="btn btn-secondary rounded-0 w-100">
                            Přidat pojištění
                        </Link>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default InsuredHomeInsurancesList;
