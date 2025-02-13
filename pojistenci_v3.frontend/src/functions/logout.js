import axios from "axios";

const logout = async () => {
    try {
        const response = await axios.post("/api/Account/Logout");
        console.log("Odhlášení úspěšné:", response.data);
    } catch (error) {
        console.error("Chyba při odhlášení:", error.response?.data || error.message);
        throw error;
    }
};

export default logout;