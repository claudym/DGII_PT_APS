import React, { useEffect, useState } from "react";
import { tableHeaderStyle, tableCellStyle } from "../utils/tableStyle";
import { wrapperStyle } from "../utils/generalStyle";
import { fetchContribuyentes } from "../utils/requests";

const ContribuyentesList = ({ title }) => {
  const [contribuyentes, setContribuyentes] = useState([]);

  useEffect(() => {
    const getData = async () => {
      if (contribuyentes.length === 0) {
        try {
          const data = await fetchContribuyentes();
          setContribuyentes(data);
        } catch (error) {
          console.error("Error:", error);
        }
      }
    };

    getData();
  }, [contribuyentes]);

  return (
    <div style={wrapperStyle}>
      <h2>{title}</h2>

      <table style={{ borderCollapse: "collapse", width: "100%" }}>
        <thead>
          <tr>
            <th style={tableHeaderStyle}>RNC/Cédula</th>
            <th style={tableHeaderStyle}>Nombre</th>
            <th style={tableHeaderStyle}>Tipo</th>
            <th style={tableHeaderStyle}>Estatus</th>
          </tr>
        </thead>
        <tbody>
          {contribuyentes.length > 0 &&
            contribuyentes.map((item) => (
              <tr key={item.rncCedula}>
                <td style={tableCellStyle}>{item.rncCedula}</td>
                <td style={tableCellStyle}>{item.nombre}</td>
                <td style={tableCellStyle}>{item.tipo}</td>
                <td style={tableCellStyle}>{item.estatus}</td>
              </tr>
            ))}
        </tbody>
      </table>
    </div>
  );
};

export default ContribuyentesList;
