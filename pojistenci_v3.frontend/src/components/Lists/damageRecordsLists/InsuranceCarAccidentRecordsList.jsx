import { useEffect, useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import apiClient from "../../../utils/apiClient";
import { LoadingSpinner } from "../../../utils/LoadingSpinner";

const InsuranceCarAccidentRecordsList = ({ insuranceId }) => {
    const [damageRecords, setDamageRecords] = useState([]);
    const [sortConfig, setSortConfig] = useState({ key: null, direction: 'asc' });
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    useEffect(() => {
        apiClient
            .get(`/carinsuranceaccidentrecord/insurance/${insuranceId}`)
            .then((response) => {
                setDamageRecords(response.data);
                setLoading(false);
            })
            .catch((error) => {
                console.error('Error fetching accident records:', error);
                setLoading(false);
            });
    }, [insuranceId]);

    const handleSort = (key) => {
        let direction = 'asc';
        if (sortConfig.key === key && sortConfig.direction === 'asc') {
            direction = 'desc';
        }
        setSortConfig({ key, direction });

        const sortedData = [...damageRecords].sort((a, b) => {
            if (a[key] < b[key]) return direction === 'asc' ? -1 : 1;
            if (a[key] > b[key]) return direction === 'asc' ? 1 : -1;
            return 0;
        });
        setDamageRecords(sortedData);
    };

    const handleRowClick = (id) => {
        navigate(`/carinsuranceaccidentrecord/${id}`, { state: { insuranceId } });
    };

    if (loading) {
        return <LoadingSpinner />;
    }

    return (
        <div className="container-fluid px-0">
            <div className="card rounded-0">
                <div className="card-header">
                    <div style={{ height: "146px" }}>
                        <h1 className="text-center mb-3">Pojistné události</h1>
                        <div className="row fw-bold pb-2">
                            <div className="col-md-2 sortable" onClick={() => handleSort('id')} style={{ cursor: 'pointer' }} >
                                Číslo události {sortConfig.key === 'id' ? (sortConfig.direction === 'asc' ? '↑' : '↓') : ''}
                            </div>
                            <div className="col-md-2 sortable" onClick={() => handleSort('damagedParts')} style={{ cursor: 'pointer' }} >
                                Poškozené části {sortConfig.key === 'damagedParts' ? (sortConfig.direction === 'asc' ? '↑' : '↓') : ''}
                            </div>
                            <div className="col-md-2 sortable" onClick={() => handleSort('otherPartiesInvolved')} style={{ cursor: 'pointer' }} >
                                Další účastníci {sortConfig.key === 'otherPartiesInvolved' ? (sortConfig.direction === 'asc' ? '↑' : '↓') : ''}
                            </div>
                            <div className="col-md-2 sortable" onClick={() => handleSort('date')} style={{ cursor: 'pointer' }} >
                                Datum {sortConfig.key === 'date' ? (sortConfig.direction === 'asc' ? '↑' : '↓') : ''}
                            </div>
                            <div className="col-md-2 sortable" onClick={() => handleSort('estimatedDamageCost')} style={{ cursor: 'pointer' }} >
                                Předpokládaná cena opravy {sortConfig.key === 'estimatedDamageCost' ? (sortConfig.direction === 'asc' ? '↑' : '↓') : ''}
                            </div>
                            <div className="col-md-2 sortable" onClick={() => handleSort('approvedCompensation')} style={{ cursor: 'pointer' }} >
                                Potvrzená kompenzace {sortConfig.key === 'approvedCompensation' ? (sortConfig.direction === 'asc' ? '↑' : '↓') : ''}
                            </div>
                        </div>
                    </div>
                </div>
                <div className="list-cardpositioning p-0">
                    <div className="card-body card-body-list">
                        <div className="row">
                            {damageRecords.map((damageRecord) => (
                                <div
                                    className="col-12"
                                    key={damageRecord.id}
                                    onClick={() => handleRowClick(damageRecord.id)}
                                    style={{ cursor: 'pointer' }}
                                >
                                    <div className="row row-hover py-2 border-bottom">
                                        <div className="col-md-2">{damageRecord.id}</div>
                                        <div className="col-md-2">{damageRecord.damagedParts}</div>
                                        <div className="col-md-2">{damageRecord.otherPartiesInvolved}</div>
                                        <div className="col-md-2">{new Date(damageRecord.date).toLocaleDateString('cs-CZ')}</div>
                                        <div className="col-md-2">{damageRecord.estimatedDamageCost}</div>
                                        <div className="col-md-2">{damageRecord.approvedCompensation}</div>
                                    </div>
                                </div>
                            ))}
                        </div>
                    </div>
                    <div className="card-footer p-0">
                        <Link to={`/add-carinsuranceaccidentrecord/${insuranceId}`} className="btn btn-secondary rounded-0 w-100">
                            Přidat událost
                        </Link>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default InsuranceCarAccidentRecordsList;
