import apiClient from "../api/apiClient";

export async function login(email, password, rememberMe, setUser) {
    try {
        const response = await apiClient.post("/Account/Login", {
            email,
            password,
            rememberMe,
        });
        console.log("Přihlášení úspěšné:", response.data);
        const user = await apiClient.get("/Account/me");
        setUser(user.data);

        return response.data;
    } catch (error) {
        console.error("Chyba při přihlášení:", error.response?.data || error.message);
        throw error;
    }
}
