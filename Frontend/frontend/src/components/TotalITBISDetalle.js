import React, { useState } from "react";
import { buttonStyle, inputStyle, wrapperStyle } from "../utils/generalStyle";
import { formatCurrency } from "../utils/formatCurrency";
import { tableCellStyle } from "../utils/tableStyle";
import ErrorMessage from "./ErrorMessage";
import { fetchTotalITBISDetalle } from "../utils/requests";

const TotalITBISDetalle = ({ title }) => {
  const [total, setTotal] = useState(0);
  const [rncCedula, setRncCedula] = useState("");
  const [computedValue, setComputedValue] = useState("-");
  const [errorMessage, setErrorMessage] = useState(null);
  const [showError, setShowError] = useState(false);

  const handleChange = (event) => {
    setRncCedula(event.target.value);
    setTotal(0);
    setComputedValue("-");
  };

  const handleKeyDown = (event) => {
    if (event.key === "Enter") {
      fetchTotalITBIS();
      setRncCedula("");
    }
  };

  const handleClick = () => {
    fetchTotalITBIS();
    setRncCedula("");
  };

  const fetchTotalITBIS = () => {
    setErrorMessage(null);
    const getData = async () => {
      try {
        const data = await fetchTotalITBISDetalle(rncCedula);
        setComputedValue(rncCedula);
        setTotal(data.totalITBIS);
      } catch (error) {
        error.response.status === 404
          ? setErrorMessage("Registro no encontrado")
          : setErrorMessage(error.message);
        showErrorMessage();
      }
    };

    getData();
  };

  const showErrorMessage = () => {
    setShowError(true);
    setTimeout(() => {
      setShowError(false);
    }, 3000);
  };

  return (
    <div style={wrapperStyle}>
      <h2>{title}</h2>
      <div>
        <input
          type="text"
          value={rncCedula}
          onChange={handleChange}
          onKeyDown={handleKeyDown}
          style={inputStyle}
        />
        <button type="button" onClick={handleClick} style={buttonStyle}>
          Buscar
        </button>
        {showError && <ErrorMessage message={errorMessage} />}
        <table>
          <tbody>
            <tr>
              <td style={tableCellStyle}>
                <b>RNC/Cédula:</b>
              </td>
              <td style={tableCellStyle}>{computedValue}</td>
            </tr>
            <tr>
              <td style={tableCellStyle}>
                <b>Total ITBIS:</b>
              </td>
              <td style={tableCellStyle}>
                {total ? formatCurrency(total) : "-"}
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default TotalITBISDetalle;
