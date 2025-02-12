import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Navbar from './components/navbar/Navbar';

import LoginForm from './components/forms/LoginForm';

import InsurersList from './components/lists/InsurersList';
import InsurerDetail from './components/details/InsurerDetail';
import EditInsurerForm from './components/forms/InsurerForms/EditInsurer';
import AddInsurerForm from './components/forms/InsurerForms/AddInsurerForm';

import InsuredsList from './components/lists/InsuredsList';
import InsuredDetail from './components/details/InsuredDetail';
import EditInsuredForm from './components/forms/InsuredForms/EditInsured';
import AddInsuredForm from './components/forms/InsuredForms/AddInsuredForm';

import HomeInsurancesList from './components/lists/insurancesLists/HomeInsuranceList';
import HomeInsuranceDetail from './components/details/HomeInsuranceDetail';
import EditHomeInsuranceForm from './components/forms/HomeInsuranceForms/EditHomeInsuranceForm';
import AddHomeInsuranceForm from './components/forms/homeInsuranceForms/AddHomeInsuranceForm';

import CarInsurancesList from './components/lists/insurancesLists/CarInsurancesList';
import CarInsuranceDetail from './components/details/CarInsuranceDetail';
import EditCarInsuranceForm from './components/forms/carInsuranceForms/EditCarInsuranceForm';
import AddCarInsuranceForm from './components/forms/carInsuranceForms/AddCarInsuranceForm';

import CarAccidentRecordDetail from './components/details/CarAccidentRecordDetail';
import AddCarAccidentRecordForm from './components/forms/carAccidentRecordForms/AddCarAccidentRecordForm';
import EditCarAccidentRecord from './components/forms/carAccidentRecordForms/EditCarAccidentRecordForm';

import HomeDamageRecordDetail from './components/details/HomeDamageRecordDetail';
import AddHomeDamageRecordForm from './components/forms/homeDamageRecordForms/AddHomeDamageRecordForm';
import EditHomeDamageRecordForm from './components/forms/homeDamageRecordForms/EditHomeDamageRecordForm';

const App = () => {
  return (
    <Router>
      <Navbar />
      <Routes>
        <Route path='/' element={<LoginForm />} />
        <Route path="/login" element={<LoginForm />} />

        <Route path="/insurers" element={<InsurersList />} />
        <Route path="/insurers/:id" element={<InsurerDetail />} />
        <Route path="/add-insurer" element={<AddInsurerForm />} />
        <Route path="/edit-insurer/:id" element={<EditInsurerForm />} />

        <Route path="/insureds" element={<InsuredsList />} />
        <Route path="/insureds/:id" element={<InsuredDetail />} />
        <Route path="/add-insured" element={<AddInsuredForm />} />
        <Route path="/edit-insured/:id" element={<EditInsuredForm />} />

        <Route path="/homeinsurances" element={<HomeInsurancesList />} />
        <Route path="/homeinsurances/:id" element={<HomeInsuranceDetail />} />
        <Route path="/add-homeinsurance/:id" element={<AddHomeInsuranceForm />} />
        <Route path="/edit-homeinsurance/:id" element={<EditHomeInsuranceForm />} />

        <Route path="/carinsurances" element={<CarInsurancesList />} />
        <Route path="/carinsurances/:id" element={<CarInsuranceDetail />} />
        <Route path="/add-carinsurance/:id" element={<AddCarInsuranceForm />} />
        <Route path="/edit-carinsurance/:id" element={<EditCarInsuranceForm />} />

        <Route path="/carinsuranceaccidentrecord/:id" element={<CarAccidentRecordDetail />} />
        <Route path="/add-carinsuranceaccidentrecord/:id" element={<AddCarAccidentRecordForm />} />
        <Route path="/edit-carinsuranceaccidentrecord/:id" element={<EditCarAccidentRecord />} />

        <Route path="/homeinsurancedamagerecords/:id" element={<HomeDamageRecordDetail />} />
        <Route path="/add-homeinsurancedamagerecords/:id" element={<AddHomeDamageRecordForm />} />
        <Route path="/edit-homeinsurancedamagerecords/:id" element={<EditHomeDamageRecordForm />} />
      </Routes>
    </Router>
  );
};

export default App;