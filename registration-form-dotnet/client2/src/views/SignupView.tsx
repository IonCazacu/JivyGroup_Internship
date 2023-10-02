import React, { useState } from 'react';
import Signup from '../components/Auth/Signup/Signup';
import FormData from '../interfaces/FormData';
import FormField from '../interfaces/FormField';
// import FormError from '../interfaces/FormError';

const SignupView: React.FC = () => {

  const formFields: FormField[] = [
    { label: 'Username', type: 'text', name: 'username', dataValidation: 'Username' },
    { label: 'Email', type: 'text', name: 'email', dataValidation: 'Email' },
    { label: 'Password', type: 'password', name: 'password', dataValidation: 'Password' },
    { label: 'Confirm Password', type: 'password', name: 'confirmPassword', dataValidation: 'ConfirmPassword' }
  ];
  
  const [formErrors, setFormErrors] = useState({
    type: '',
    title: '',
    status: 0,
    traceId: '',
    errors: []
  });

  const onSubmit = (formData: FormData) => {
    
    let count = 0;
    
    for (const field of formFields) {
      if (field.name in formData) {
        count++;
      }
    }

    if (count === formFields.length) {
      
      fetch('http://localhost:5229/api/user', {
        
        method: 'POST',
        
        headers: {
          'Content-Type': 'application/json'
        },
        
        body: JSON.stringify(formData)
      
      })
      
      .then((response) => {
      
        return response.text();
      
      })

      .then((data) => {

        const response = JSON.parse(data);
        setFormErrors(response);
        
        console.log('SignupView', formErrors);
      })
      
      .catch((error) => {
      
        console.error('error', error.message);
      
      });

    }
    
    // console.log('Form data : ', formData['confirmPassword']);
  
  }

  return (
    <Signup fields={ formFields } errors={ formErrors } onSubmit={ onSubmit }></Signup>
    )
}

export default SignupView;
