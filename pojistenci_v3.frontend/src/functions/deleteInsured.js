import axios from "axios";

const deleteInsured = async (id, navigate) => {
  try {
    const response = await axios.delete(`/api/Insureds/${id}`);
    alert("Pojištěný byl úspěšně smazán.");
    navigate("/insureds");
  } catch (error) {
    console.error("Chyba při mazání pojištěného:", error.response?.data || error.message);
    alert("Chyba při mazání pojištěného.");
  }
};

export default deleteInsured;