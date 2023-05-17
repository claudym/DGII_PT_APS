import React, { useEffect, useState } from 'react';

const ContribuyentesList = () => {
  const [contribuyentes, setContribuyentes] = useState([]);

  useEffect(() => {
    fetch(`${process.env.REACT_APP_API_URL}/contribuyentes`)
      .then(response => response.json())
      .then(data => setContribuyentes(data))
      .catch(error => console.error(error));
  }, []);

  return (
    <div>
      <h2>Listado de Contribuyentes</h2>
      <ul>
        {contribuyentes.map(contribuyente => (
          <li key={contribuyente.rncCedula}>
            <strong>{contribuyente.nombre}</strong> - {contribuyente.tipo}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default ContribuyentesList;
