import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Navbar from "./components/common/Navbar";
import Footer from "./components/common/Footer";
import BookingPage from "./pages/BookingPage";
import "./App.css";

function App() {
  return (
    <Router>
      <div className="App">
        <Navbar />
        <main>
          <Routes>
            <Route path="/" element={<div>Home Page - Coming Soon</div>} />
            <Route path="/menu" element={<div>Menu Page - Coming Soon</div>} />
            <Route path="/booking" element={<BookingPage />} />
            <Route
              path="/about"
              element={<div>About Page - Coming Soon</div>}
            />
            <Route
              path="/login"
              element={<div>Login Page - Coming Soon</div>}
            />
            <Route
              path="/admin"
              element={<div>Admin Page - Coming Soon</div>}
            />
            <Route
              path="/account/profile"
              element={<div>Profile Page - Coming Soon</div>}
            />
          </Routes>
        </main>
        <Footer />
      </div>
    </Router>
  );
}

export default App;
