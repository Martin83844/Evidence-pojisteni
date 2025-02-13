import axios from "axios";

const deleteDamageRecord = async (id, insuranceId, navigate) => {
  try {
    const response = await axios.delete(`/api/CarInsuranceAccidentRecord/${id}`);
    alert("Událost byla úspěšně smazána.");
    navigate(`/CarInsurances/${insuranceId}`);
  } catch (error) {
    console.error("Chyba při mazání události:", error.response?.data || error.message);
    alert("Chyba při mazání události.");
  }
};

export default deleteDamageRecord;