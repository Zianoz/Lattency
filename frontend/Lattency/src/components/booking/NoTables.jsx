import React from "react";

const NoTables = () => {
  return (
    <div className="no-tables">
      <span className="no-tables-icon">😔</span>
      <h3>No Available Tables</h3>
      <p>
        Sorry, no tables are available for the selected date, time, and number
        of guests.
      </p>
      <p className="no-tables-suggestion">
        Please try a different time or contact us directly.
      </p>
    </div>
  );
};

export default NoTables;
