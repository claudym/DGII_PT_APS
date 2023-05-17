import React from 'react';
import { BrowserRouter as Router, Route, Link } from 'react-router-dom';
import { Routes } from 'react-router-dom';
import ContribuyentesList from './components/ContribuyentesList';
import ComprobantesList from './components/ComprobantesList';
import TotalITBIS from './components/TotalITBIS';

const App = () => {
  return (
    <Router>
      <div>
        <nav>
          <ul>
            <li>
              <Link to="/Contribuyentes">Contribuyentes</Link>
            </li>
            <li>
              <Link to="/comprobantes">Comprobantes</Link>
            </li>
          </ul>
        </nav>

        <Routes>
          <Route path="/contribuyentes" element={<ContribuyentesList />} />
          <Route path="/comprobantes" element={<ComprobantesList />} />
          <Route path="/comprobantes/:rncCedula" element={<TotalITBIS />} />
        </Routes>
      </div>
    </Router>
  );
};

export default App;
