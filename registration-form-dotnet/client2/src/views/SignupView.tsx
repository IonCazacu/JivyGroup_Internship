import React, { useState } from 'react';
import Auth from '../components/User/Auth/Auth';
import FormData from '../interfaces/FormData';
import FormError from '../interfaces/FormError';
import FormField from '../interfaces/FormField';

const SignupView: React.FC = () => {

  const [formErrors, setFormErrors] = useState<FormError>({
    formError: {
      type: '',
      title: '',
      status: 0,
      traceId: '',
      errors: {}
    }
  });

  const formFields: FormField[] = [
    {
      label: 'Username',
      type: 'text',
      name: 'username',
      dataValidation: 'Username'
    },
    {
      label: 'Email',
      type: 'text',
      name: 'email',
      dataValidation: 'Email'
    },
    {
      label: 'Password',
      type: 'password',
      name: 'password',
      dataValidation: 'Password'
    },
    {
      label: 'Confirm Password',
      type: 'password',
      name: 'confirmPassword',
      dataValidation: 'ConfirmPassword'
    }
  ];
  
  const onSubmit = async (formData: FormData) => {
    
    const count = formFields.filter(field => field.name in formData).length;

    try {

      if (count === formFields.length) {
      
        const response = await fetch('http://localhost:5229/api/user', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(formData)
        });

        const data = await response.json();

        setFormErrors({ formError: data });
      }

    } catch (error) {

      console.error(error);

    }

  }

  return (
    <Auth
      header="Sign Up"
      fields={ formFields }
      errors={ formErrors }
      onSubmit={ onSubmit }
    ></Auth>
    )
}

export default SignupView;
