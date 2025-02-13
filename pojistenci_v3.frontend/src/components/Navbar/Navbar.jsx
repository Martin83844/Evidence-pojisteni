import React from 'react';
import { Link } from 'react-router-dom';
import LogoutButton from '../buttons/LogoutButton';
import { useUser } from '../../utils/UserProvider';

const Navbar = () => {
    const { user } = useUser();

    return (
        <nav className="navbar navbar-expand navbar-light bg-light sticky-top">
            <Link className="navbar-brand ms-3" to="/home">
                Pojištěnci
            </Link>
            <div className="collapse navbar-collapse">
                <ul className="navbar-nav me-auto">
                    <li className="nav-item">
                        <Link className="nav-link" to="/insurers">Pojistníci</Link>
                    </li>
                    <li className="nav-item">
                        <Link className="nav-link" to="/insureds">Pojištění</Link>
                    </li>
                    <li className="nav-item dropdown">
                        <a
                            className="nav-link dropdown-toggle"
                            href="#"
                            id="navbarDropdown"
                            role="button"
                            data-bs-toggle="dropdown"
                            aria-expanded="false"
                        >
                            Pojistky
                        </a>
                        <ul className="dropdown-menu" aria-labelledby="navbarDropdown">
                            <li>
                                <Link className="dropdown-item" to="/homeinsurances">
                                    Pojištění nemovitostí
                                </Link>
                            </li>
                            <li>
                                <Link className="dropdown-item" to="/carinsurances">
                                    Pojištění vozidel
                                </Link>
                            </li>
                        </ul>
                    </li>
                </ul>
                <div>
                    {user ? (
                        <div className='row d-flex align-items-center '>
                            <div className='col-7 d-none d-md-inline align-items-center'>
                                {user.email}
                            </div>
                            <div className='col-5'>
                                <LogoutButton />
                            </div>
                        </div>
                    ) : (
                        <p></p>
                    )}
                </div>
            </div>
        </nav>
    );
};

export default Navbar;
