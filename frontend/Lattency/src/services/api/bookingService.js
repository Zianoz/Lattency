import axios from 'axios';

const API_BASE_URL = 'https://localhost:7159';

const bookingAPI = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
  withCredentials: true, // For cookie-based authentication
});

// Check available tables
export const checkAvailableTables = async (reservationStart, numGuests) => {
  try {
    const response = await bookingAPI.get('/Booking/CheckAvailableTables', {
      params: {
        reservationStart,
        numGuests,
      },
    });
    return response.data;
  } catch (error) {
    console.error('Error checking available tables:', error);
    throw error;
  }
};

// Create booking
export const createBooking = async (bookingData) => {
  try {
    const formData = new FormData();
    formData.append('CafeTableId', bookingData.cafeTableId);
    formData.append('ReservationStart', bookingData.reservationStart);
    formData.append('NumGuests', bookingData.numGuests);

    const response = await bookingAPI.post('/Booking/CreateBooking', formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data;
  } catch (error) {
    console.error('Error creating booking:', error);
    throw error;
  }
};

export default bookingAPI;
