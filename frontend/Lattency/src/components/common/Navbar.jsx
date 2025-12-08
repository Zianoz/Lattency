import React from "react";
import { Link } from "react-router-dom";
import "./Navbar.css";

const Navbar = () => {
  // TODO: Add authentication state management
  const isAuthenticated = false; // Replace with actual auth state
  const isAdmin = false; // Replace with actual role check

  const handleLogout = async (e) => {
    e.preventDefault();
    // TODO: Implement logout logic
    console.log("Logout clicked");
  };

  return (
    <header className="navbar">
      <div className="container">
        <Link to="/" className="logo">
          Lattency
        </Link>
        <nav className="nav-links">
          <Link to="/">Home</Link>
          <Link to="/menu">Menu</Link>
          <Link to="/booking">Booking</Link>
          <Link to="/about">About</Link>

          {isAuthenticated ? (
            <>
              {isAdmin ? (
                <Link to="/admin">Admin</Link>
              ) : (
                <Link to="/account/profile">My Account</Link>
              )}
              <button onClick={handleLogout} className="btn-logout">
                Logout
              </button>
            </>
          ) : (
            <Link to="/login">Login</Link>
          )}
        </nav>
      </div>
    </header>
  );
};

export default Navbar;
