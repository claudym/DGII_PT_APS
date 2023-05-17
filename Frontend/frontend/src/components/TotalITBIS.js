import React, { useEffect, useState } from 'react';

const TotalITBIS = ({ rncCedula }) => {
  const [total, setTotal] = useState(0);

  useEffect(() => {
    fetch(`${process.env.REACT_APP_API_URL}/comprobantesfiscales/${rncCedula}/total-itbis`)
      .then(response => response.json())
      .then(data => setTotal(data.totalITBIS))
      .catch(error => console.error(error));
  }, [rncCedula]);

  return (
    <div>
      <h2>Total ITBIS</h2>
      <p>RNC/Cédula: {rncCedula}</p>
      <p>Total ITBIS: {total}</p>
    </div>
  );
};

export default TotalITBIS;

