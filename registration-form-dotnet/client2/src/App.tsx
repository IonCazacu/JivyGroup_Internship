import { BrowserRouter, Route, Routes } from 'react-router-dom';
import React from 'react';
import Navbar from './components/Navbar/Navbar';
import HomeView from './views/HomeView';
import SignupView from './views/SignupView';
import UsersView from './views/UsersView';

import './App.scss';

class App extends React.Component<{}, { previousValue: null, value: string }> {
  
  constructor(props: any) {
    super(props);
    
    this.state = {
      previousValue: null,
      value: ''
    }

    this.handleBlur = this.handleBlur.bind(this);
    this.handleChange = this.handleChange.bind(this);
  }

  handleChange($event: any) {
    this.setState({ value: $event.target.value });
  }

  handleBlur() {
    if (this.state.value.trim() === '') {
      console.log('empty input', this.state.value);
      
      this.setState({ value: 'Initial value' }, () => {
        alert(this.state.value);
        console.log('default input', this.state.value);
      });
    }
  }

  // fetch('http://localhost:5229/api/user', {
  //   method: 'GET'
  // })
  // .then((response) => {
  //   console.log(response);
  // })
  // .catch((error) => {
  //   console.log(error);
  // });

  render() {
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
}

export default App;
