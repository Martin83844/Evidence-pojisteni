import axios from "axios";

const deleteCarInsurance = async (id, insuredId, navigate) => {
  try {
    const response = await axios.delete(`/api/CarInsurances/${id}`);
    alert("Pojištění bylo úspěšně smazáno.");
    navigate(`/insureds/${insuredId}`);
  } catch (error) {
    console.error("Chyba při mazání pojištění:", error.response?.data || error.message);
    alert("Chyba při mazání pojištění.");
  }
};

export default deleteCarInsurance;