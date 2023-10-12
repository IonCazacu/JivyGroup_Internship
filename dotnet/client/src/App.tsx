import { BrowserRouter, Route, Routes } from 'react-router-dom';
import React from 'react';
import Navbar from './components/Navbar/Navbar';
import HomeView from './pages/Home/Home';
import SignupView from './pages/Signup/Signup';
import UsersView from './pages/Users/Users';

import './App.scss';

const App: React.FC = () => {

  return (
    <BrowserRouter>
      <div className="App">
        <Navbar></Navbar>
        <Routes>
          <Route path="/signup" element={ <SignupView></SignupView> }></Route>
          <Route path="/users" element={ <UsersView></UsersView> }></Route>
          <Route path="/" element={ <HomeView></HomeView> }></Route>
        </Routes>
      </div>
    </BrowserRouter>
  )
}

export default App;
