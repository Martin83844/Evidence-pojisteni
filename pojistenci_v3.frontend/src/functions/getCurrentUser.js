import apiClient from "../utils/apiClient";

export async function getCurrentUser() {
    try {
        const response = await apiClient.get("/Account/me");
        console.log("Aktuální uživatel:", response.data);
        return response.data;
    } catch (error) {
        console.error("Uživatel není přihlášen:", error.response?.data || error.message);
        return null;
    }
}