import axios from "axios";

const deleteInsurer = async (id, navigate) => {
  try {
    const response = await axios.delete(`/api/Insurers/${id}`);
    alert("Pojistník byl úspěšně smazán.");
    navigate("/insurers");
  } catch (error) {
    console.error("Chyba při mazání pojistníka:", error.response?.data || error.message);
    alert("Chyba při mazání pojistníka.");
  }
};

export default deleteInsurer;