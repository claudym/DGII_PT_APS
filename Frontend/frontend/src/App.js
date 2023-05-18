import React from "react";
import { BrowserRouter as Router, Route, Link } from "react-router-dom";
import { Routes } from "react-router-dom";
import ContribuyentesList from "./components/ContribuyentesList";
import ComprobantesList from "./components/ComprobantesList";
import TotalITBISList from "./components/TotalITBISList";
import TotalITBISDetalle from "./components/TotalITBISDetalle";
import { navStyle, ulStyle, liStyle, linkStyle } from "./utils/generalStyle";

const App = () => {
  const contribuyentesTitle = "Listado de Contribuyentes";
  const comprobantesTitle = "Listado de Comprobantes Fiscales";
  const totalITBISListTitle = "Listado de Total ITBIS por RNC/Cédula";
  const totalITBISTitle = "Total ITBIS por RNC/Cédula";

  return (
    <Router>
      <div>
        <nav style={navStyle}>
          <ul style={ulStyle}>
            <li style={liStyle}>
              <Link className="navLink" style={linkStyle} to="/Contribuyentes">
                {contribuyentesTitle}
              </Link>
            </li>
            <li style={liStyle}>
              <Link className="navLink" style={linkStyle} to="/comprobantes">
                {comprobantesTitle}
              </Link>
            </li>
            <li style={liStyle}>
              <Link
                className="navLink"
                style={linkStyle}
                to="/comprobantes/totalITBIS"
              >
                {totalITBISListTitle}
              </Link>
            </li>
            <li style={liStyle}>
              <Link
                className="navLink"
                style={linkStyle}
                to="/comprobantes/totalITBIS/detalle"
              >
                {totalITBISTitle}
              </Link>
            </li>
          </ul>
        </nav>

        <Routes>
          <Route
            path="/contribuyentes"
            element={<ContribuyentesList title={contribuyentesTitle} />}
          />
          <Route
            path="/comprobantes"
            element={<ComprobantesList title={comprobantesTitle} />}
          />
          <Route
            path="/comprobantes/totalITBIS"
            element={<TotalITBISList title={totalITBISListTitle} />}
          />
          <Route
            path="/comprobantes/totalITBIS/detalle"
            element={<TotalITBISDetalle title={totalITBISTitle} />}
          />
        </Routes>
      </div>
    </Router>
  );
};

export default App;
