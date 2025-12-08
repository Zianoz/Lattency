import React, { useState } from "react";

const AvailabilityCheck = ({ onCheckAvailability }) => {
  const [reservationStart, setReservationStart] = useState("");
  const [numGuests, setNumGuests] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    if (reservationStart && numGuests) {
      onCheckAvailability(reservationStart, parseInt(numGuests));
    }
  };

  return (
    <div className="booking-section">
      <div className="section-header">
        <h2 className="section-title">Check Availability</h2>
        <p className="section-subtitle">Find the perfect time for your visit</p>
      </div>

      <form onSubmit={handleSubmit} className="booking-form">
        <div className="form-row">
          <div className="form-group">
            <label htmlFor="reservationStart">
              <span className="label-icon">📅</span>
              Reservation Date & Time
            </label>
            <input
              type="datetime-local"
              id="reservationStart"
              name="reservationStart"
              className="form-input"
              step="1800"
              required
              value={reservationStart}
              onChange={(e) => setReservationStart(e.target.value)}
              placeholder="Select date and time"
              title="Please select both date and time"
            />
            <small className="form-help">
              Select both date and time (30-minute intervals)
            </small>
          </div>

          <div className="form-group">
            <label htmlFor="numGuests">
              <span className="label-icon">👥</span>
              Number of Guests
            </label>
            <input
              type="number"
              id="numGuests"
              name="numGuests"
              className="form-input"
              placeholder="How many guests?"
              min="1"
              max="20"
              required
              value={numGuests}
              onChange={(e) => setNumGuests(e.target.value)}
            />
          </div>
        </div>

        <button type="submit" className="btn-primary">
          <span>Check Available Tables</span>
          <span className="btn-arrow">→</span>
        </button>
      </form>
    </div>
  );
};

export default AvailabilityCheck;
