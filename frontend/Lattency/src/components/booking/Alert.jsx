import React from "react";

const Alert = ({ type, message, onClose }) => {
  if (!message) return null;

  const iconMap = {
    success: "✓",
    error: "⚠",
  };

  return (
    <div className={`alert alert-${type}`}>
      <span className="alert-icon">{iconMap[type]}</span>
      <span>{message}</span>
    </div>
  );
};

export default Alert;
