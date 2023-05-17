import React, { useEffect, useState } from 'react'

function TotalITBISList({title}) {
    const [totalITBISList, setTotalITBISList] = useState([]);
    useEffect(() => {
        fetch(`${process.env.REACT_APP_API_URL}/ComprobantesFiscales/total-itbis-por-rnc`)
        .then(response => response.json())
        .then(data => setTotalITBISList(data))
        .catch(error => console.error(error));
    }, [])
    const formatCurrency = (amount) => {
        return amount.toLocaleString('es-DO', {
          style: 'currency',
          currency: 'DOP',
        });
      };
    const tableHeaderStyle = {
        background: '#f2f2f2',
        padding: '8px',
        borderBottom: '1px solid #ddd',
        textAlign:'left'
      };
      
      const tableCellStyle = {
        padding: '8px',
        borderBottom: '1px solid #ddd',
        
      };

  return (
    <div>
        <h2>{title}</h2>
      
            
        <table style={{ borderCollapse: 'collapse', width: '100%' }}>
         <thead>
        <tr>
          <th style={tableHeaderStyle}>RNC/Cédula</th>
          <th style={tableHeaderStyle}>Nombre Contribuyente</th>
          <th style={tableHeaderStyle}>Total ITBIS</th>
        </tr>
      </thead>
      <tbody>
        {totalITBISList.map(item => (

        <tr key={item.rncCedula}>
          <td style={tableCellStyle}>{item.rncCedula}</td>
          <td style={tableCellStyle}>{item.nombreContribuyente}</td>
          <td style={tableCellStyle}>{formatCurrency(item.totalITBIS)}</td>
        </tr>
        ))
        }
       
      </tbody>
    </table>
    </div>
  )
}

export default TotalITBISList