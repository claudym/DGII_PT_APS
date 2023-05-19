import axios from "axios";

const BASE_URL = process.env.REACT_APP_API_URL;

const request = axios.create({
  baseURL: BASE_URL,
});

export const fetchContribuyentes = async () => {
  try {
    const response = await request.get("/Contribuyente");
    return response.data.data;
  } catch (error) {
    throw new Error(error.response.data.message);
  }
};

export const fetchComprobantes = async () => {
  try {
    const response = await request.get("/ComprobanteFiscal");
    return response.data.data;
  } catch (error) {
    throw new Error(error.response.data.message);
  }
};

export const fetchTotalITBISList = async () => {
  try {
    const response = await request.get("/ComprobanteFiscal/ITBIS/Total");
    return response.data.data;
  } catch (error) {
    throw new Error(error.response.data.message);
  }
};

export const fetchTotalITBISDetalle = async (rncCedula) => {
  try {
    const response = await request.get(
      `/ComprobanteFiscal/${rncCedula}/ITBIS/Total`
    );
    return response.data.data;
  } catch (error) {
    throw error;
  }
};
