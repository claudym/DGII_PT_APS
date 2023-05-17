import React, {  useState } from 'react';

const TotalITBISDetalle = ({title}) => {
  const [total, setTotal] = useState(0);
  const [rncCedula, setRncCedula] = useState('');

  const handleChange = (event) => {
    setRncCedula(event.target.value);
    setTotal(0)
  };

  const handleKeyDown = (event) => {
    if (event.key === 'Enter') {
      fetchTotalITBIS();
    }
  };

const fetchTotalITBIS =  () => {
   fetch(`${process.env.REACT_APP_API_URL}/comprobantesfiscales/${rncCedula}/total-itbis`)
  .then(response => response.json())
  .then(data => setTotal(data.totalITBIS))
  .catch(error => console.error(error));
}

  return (
    <div style={{
      display: 'flex',
      flexDirection: 'column',
      alignItems: 'center',
      justifyContent: 'center'
      
    }}>
      <h2>{title}</h2>
      <div>
      <input
        type="text"
        value={rncCedula}
        onChange={handleChange}
        onKeyDown={handleKeyDown}
        style={{
          padding: '8px',
          borderRadius: '4px',
          border: '1px solid #ccc',
          marginRight: '8px',
        }}
       
      />
      <button type='button' onClick={fetchTotalITBIS} style={{
        padding: '8px 16px',
        borderRadius: '4px',
        backgroundColor: '#4CAF50',
        color: 'white',
        border: 'none',
        cursor: 'pointer',
      }}>Buscar</button>
      </div>
      <div>
      <p>RNC/Cédula: {rncCedula}</p>
      <p>Total ITBIS: {total}</p>
      </div>
    </div>
  );
};

export default TotalITBISDetalle;

