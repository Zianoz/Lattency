import React, { useState } from "react";
import BookingHero from "../components/booking/BookingHero";
import AvailabilityCheck from "../components/booking/AvailabilityCheck";
import TableGrid from "../components/booking/TableGrid";
import NoTables from "../components/booking/NoTables";
import Alert from "../components/booking/Alert";
import {
  checkAvailableTables,
  createBooking,
} from "../services/api/bookingService";
import "../styles/booking.css";

const BookingPage = () => {
  const [availableTables, setAvailableTables] = useState([]);
  const [reservationStart, setReservationStart] = useState("");
  const [numGuests, setNumGuests] = useState(0);
  const [searchPerformed, setSearchPerformed] = useState(false);
  const [alert, setAlert] = useState({ type: "", message: "" });
  const [loading, setLoading] = useState(false);

  const handleCheckAvailability = async (dateTime, guests) => {
    setLoading(true);
    setAlert({ type: "", message: "" });

    try {
      const tables = await checkAvailableTables(dateTime, guests);
      setAvailableTables(tables);
      setReservationStart(dateTime);
      setNumGuests(guests);
      setSearchPerformed(true);
    } catch (error) {
      setAlert({
        type: "error",
        message: "Failed to check availability. Please try again.",
      });
      setAvailableTables([]);
      setSearchPerformed(false);
    } finally {
      setLoading(false);
    }
  };

  const handleConfirmBooking = async (bookingData) => {
    setLoading(true);
    setAlert({ type: "", message: "" });

    try {
      await createBooking(bookingData);
      setAlert({
        type: "success",
        message: `Booking confirmed! Table ${bookingData.cafeTableId} for ${
          bookingData.numGuests
        } guests on ${new Date(bookingData.reservationStart).toLocaleString()}`,
      });
      // Reset the form
      setAvailableTables([]);
      setSearchPerformed(false);
      setReservationStart("");
      setNumGuests(0);
    } catch (error) {
      const errorMessage =
        error.response?.status === 401
          ? "Please sign in to create a booking."
          : "Table is not available at the requested time.";

      setAlert({
        type: "error",
        message: errorMessage,
      });
    } finally {
      setLoading(false);
    }
  };

  return (
    <>
      <BookingHero />
      <div className="booking-wrapper">
        <div className="booking-container">
          <Alert type={alert.type} message={alert.message} />

          <AvailabilityCheck onCheckAvailability={handleCheckAvailability} />

          {loading && (
            <div className="loading-spinner">
              <p>Loading...</p>
            </div>
          )}

          {!loading && availableTables.length > 0 && (
            <TableGrid
              tables={availableTables}
              reservationStart={reservationStart}
              numGuests={numGuests}
              onConfirmBooking={handleConfirmBooking}
            />
          )}

          {!loading && searchPerformed && availableTables.length === 0 && (
            <NoTables />
          )}
        </div>
      </div>
    </>
  );
};

export default BookingPage;
