import React, { useEffect, useState } from 'react';

const ComprobantesList = ({title}) => {
  const [comprobantes, setComprobantes] = useState([]);

  useEffect(() => {
    fetch(`${process.env.REACT_APP_API_URL}/comprobantesfiscales`)
      .then(response => response.json())
      .then(data => setComprobantes(data))
      .catch(error => console.error(error));
  }, []);

  return (
    <div>
      <h2>{title}</h2>
      <ul>
        {comprobantes.map(comprobante => (
          <li key={comprobante.ncf}>
            <strong>NCF:</strong> {comprobante.ncf} - <strong>Monto:</strong> {comprobante.monto} - <strong>ITBIS:</strong> {comprobante.itbis18}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default ComprobantesList;
