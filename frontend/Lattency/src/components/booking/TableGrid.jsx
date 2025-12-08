import React, { useState } from "react";

const TableGrid = ({
  tables,
  reservationStart,
  numGuests,
  onConfirmBooking,
}) => {
  const [selectedTableId, setSelectedTableId] = useState(null);

  const handleSubmit = (e) => {
    e.preventDefault();
    if (selectedTableId) {
      onConfirmBooking({
        cafeTableId: selectedTableId,
        reservationStart,
        numGuests,
      });
    }
  };

  if (!tables || tables.length === 0) {
    return null;
  }

  return (
    <div className="booking-section tables-section">
      <div className="section-header">
        <h2 className="section-title">Available Tables</h2>
        <p className="section-subtitle">Choose your preferred table</p>
      </div>

      <form onSubmit={handleSubmit} className="booking-form">
        <div className="tables-grid">
          {tables.map((table) => (
            <label key={table.id} className="table-card">
              <input
                type="radio"
                name="cafeTableId"
                value={table.id}
                checked={selectedTableId === table.id}
                onChange={() => setSelectedTableId(table.id)}
                required
              />
              <div className="table-card-content">
                <div className="table-icon">🪑</div>
                <h3 className="table-name">Table {table.id}</h3>
                <p className="table-capacity">
                  <span className="capacity-icon">👥</span>
                  Up to {table.capacity} guests
                </p>
                <div className="table-checkmark">✓</div>
              </div>
            </label>
          ))}
        </div>

        <button type="submit" className="btn-primary btn-confirm">
          <span>Confirm Booking</span>
          <span className="btn-arrow">✓</span>
        </button>
      </form>
    </div>
  );
};

export default TableGrid;
