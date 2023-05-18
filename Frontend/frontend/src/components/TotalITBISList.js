import React, { useEffect, useState } from "react";
import { tableHeaderStyle, tableCellStyle } from "../utils/tableStyle";
import { formatCurrency } from "../utils/formatCurrency";
import { wrapperStyle } from "../utils/generalStyle";
import { fetchTotalITBISList } from "../utils/requests";

function TotalITBISList({ title }) {
  const [totalITBISList, setTotalITBISList] = useState([]);

  useEffect(() => {
    const getData = async () => {
      if (totalITBISList.length === 0) {
        try {
          const data = await fetchTotalITBISList();
          setTotalITBISList(data);
        } catch (error) {
          console.error("Error:", error);
        }
      }
    };

    getData();
  }, [totalITBISList]);
  return (
    <div style={wrapperStyle}>
      <h2>{title}</h2>

      <table style={{ borderCollapse: "collapse", width: "100%" }}>
        <thead>
          <tr>
            <th style={tableHeaderStyle}>RNC/Cédula</th>
            <th style={tableHeaderStyle}>Nombre Contribuyente</th>
            <th style={tableHeaderStyle}>Total ITBIS</th>
          </tr>
        </thead>
        <tbody>
          {totalITBISList.length > 0 &&
            totalITBISList.map((item) => (
              <tr key={item.rncCedula}>
                <td style={tableCellStyle}>{item.rncCedula}</td>
                <td style={tableCellStyle}>{item.nombreContribuyente}</td>
                <td style={tableCellStyle}>
                  {formatCurrency(item.totalITBIS18)}
                </td>
              </tr>
            ))}
        </tbody>
      </table>
    </div>
  );
}

export default TotalITBISList;
