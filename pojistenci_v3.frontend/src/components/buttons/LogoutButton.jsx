import React from "react";
import { useNavigate } from "react-router-dom";
import { useUser } from "../../utils/UserProvider";
import logout from "../../functions/logout";

const LogoutButton = () => {
  const navigate = useNavigate();
  const { setUser } = useUser();

  const handleLogout = async () => {
    try {
      await logout();
      setUser(null);
      navigate("/login");
    } catch (error) {
      console.error("Chyba při odhlášení:", error.message);
    }
  };

  return (
    <button onClick={handleLogout} className="btn btn-danger me-2 me-md-4 px-4 border-0 rounded-20">
      Odhlásit
    </button>
  );
};

export default LogoutButton;