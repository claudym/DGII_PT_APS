import React, { useEffect, useState } from "react";
import { tableHeaderStyle, tableCellStyle } from "../utils/tableStyle";
import { formatCurrency } from "../utils/formatCurrency";
import { wrapperStyle } from "../utils/generalStyle";
import { fetchComprobantes } from "../utils/requests";

const ComprobantesList = ({ title }) => {
  const [comprobantes, setComprobantes] = useState([]);

  useEffect(() => {
    const getData = async () => {
      if (comprobantes.length === 0) {
        try {
          const data = await fetchComprobantes();
          setComprobantes(data);
        } catch (error) {
          console.error("Error:", error);
        }
      }
    };

    getData();
  }, [comprobantes]);

  return (
    <div style={wrapperStyle}>
      <h2>{title}</h2>
      <table style={{ borderCollapse: "collapse", width: "100%" }}>
        <thead>
          <tr>
            <th style={tableHeaderStyle}>NCF</th>
            <th style={tableHeaderStyle}>Monto</th>
            <th style={tableHeaderStyle}>ITBIS</th>
          </tr>
        </thead>
        <tbody>
          {/* {console.log(comprobantes)} */}
          {comprobantes.length > 0 &&
            comprobantes.map((item) => (
              <tr key={item.ncf}>
                <td style={tableCellStyle}>{item.ncf}</td>
                <td style={tableCellStyle}>{formatCurrency(item.monto)}</td>
                <td style={tableCellStyle}>{formatCurrency(item.itbis18)}</td>
              </tr>
            ))}
        </tbody>
      </table>
    </div>
  );
};

export default ComprobantesList;
