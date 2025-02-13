import React, { createContext, useState, useEffect, useContext } from "react";
import { getCurrentUser } from "./getCurrentUser";
import { logout } from "./logout";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        async function fetchUser() {
            const currentUser = await getCurrentUser();
            setUser(currentUser);
            setLoading(false);
        }
        fetchUser();
    }, []);

    const handleLogout = async () => {
        await logout();
        setUser(null);
    };

    return (
        <AuthContext.Provider value={{ user, loading, logout: handleLogout }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);