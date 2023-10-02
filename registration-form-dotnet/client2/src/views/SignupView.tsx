import React from 'react';
import Signup from '../components/Auth/Signup/Signup';
import FormField from '../interfaces/FormField';
import FormData from '../interfaces/FormData';

class SignupView extends React.Component {

  formFields: FormField[];

  constructor(props: any) {
    super(props);
    
    this.formFields = [
      { label: 'Username', type: 'text', name: 'username' },
      { label: 'Email', type: 'email', name: 'email' },
      { label: 'Password', type: 'password', name: 'password' },
      { label: 'Confirm Password', type: 'password', name: 'confirmPassword' },
    ];
  }

  onSubmit = (formData: FormData) => {

    let count = 0;
    
    for (const field of this.formFields) {
      if (field.name in formData) {
        count++;
      }
    }

    if (count === this.formFields.length) {
      fetch('http://localhost:5229/api/user', {
        
        method: 'POST',
        
        headers: {
          'Content-Type': 'application/json'
        },
        
        body: JSON.stringify(formData)
      
      })
      
      .then((response) => {
      
        return response.json();
      
      })

      .then((data) => {

        console.log('data', data.message);

      })
      
      .catch((error) => {
      
        console.error('error', error.message);
      
      });

    }
    
    // console.log('Form data : ', formData['confirmPassword']);
  
  }

  render(): React.ReactNode {
    return (
      <Signup fields={ this.formFields } onSubmit={ this.onSubmit }></Signup>
      )
  }
}

export default SignupView;
