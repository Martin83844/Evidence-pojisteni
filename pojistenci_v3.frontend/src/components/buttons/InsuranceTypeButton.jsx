import React, { useState } from 'react';
import InsuredHomeInsurancesList from "../lists/insuredsLists/InsuredHomeInsurancesList";
import InsuredCarInsurancesList from "../lists/insuredsLists/InsuredCarInsuranceList";
import InsurerHomeInsuranceList from "../lists/insurersLists/InsurerHomeInsuranceList";
import InsurerCarInsuranceList from "../lists/insurersLists/InsurerCarInsuranceList";

export function InsuranceTypeButton({ entity, type }) {
    const [activeList, setActiveList] = useState('home');

    return (
        <div className="col-md-9 p-0">
            <div className="btn-group w-100" role="group">
                <button
                    className={`btn rounded-0 border-0 ${activeList === "home" ? "btn-secondary" : "btn-outline-secondary"}`}
                    onClick={() => setActiveList("home")}
                >
                    Pojištění domácnosti
                </button>
                <button
                    className={`btn rounded-0 border-0 ${activeList === "car" ? "btn-secondary" : "btn-outline-secondary"}`}
                    onClick={() => setActiveList("car")}
                >
                    Pojištění vozu
                </button>
            </div>

            <div>
                {activeList === "home" ? (
                    type === 'insured' ? (
                        <InsuredHomeInsurancesList insuredId={entity.id} />
                    ) : (
                        <InsurerHomeInsuranceList insurerId={entity.id} />
                    )
                ) : (
                    type === 'insured' ? (
                        <InsuredCarInsurancesList insuredId={entity.id} />
                    ) : (
                        <InsurerCarInsuranceList insurerId={entity.id} />
                    )
                )}
            </div>
        </div>
    );
}
