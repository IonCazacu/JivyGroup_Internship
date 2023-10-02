import React, { useEffect, useState } from 'react';
import FormField from '../../../interfaces/FormField';
import FormData from '../../../interfaces/FormData';
import FormError from '../../../interfaces/FormError';

import './Signup.scss';

// Interface for form state
interface State {
  formState: {
    [key: string]: string
  };
};

// Interface for form component
interface FormProps {
  fields: FormField[];
  errors: any;
  onSubmit: (FormData: FormData) => void;
};

const Signup: React.FC<FormProps> = ({ fields, errors, onSubmit }) => {

  const [formState, setFormState] = useState<State>({ formState: {} });

  useEffect(() => {
    console.log('changed', errors);
  }, [errors]);

  // const handleBlur = ($event: React.FocusEvent<HTMLInputElement>) => {
  //   const { name, value } = $event.target;
  //   setFormState(prevState => ({
  //     formState: {
  //       ...prevState.formState,
  //       [name]: value,
  //     },
  //   }));

  //   console.log('formState', formState);
  // };

  const handleChange = ($event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = $event.target;
    setFormState(prevState => ({
      formState: {
        ...prevState.formState,
        [name]: value,
      },
    }));
  };
  
  const handleSubmit = ($event: React.FormEvent<HTMLFormElement>) => {
    $event.preventDefault();
    onSubmit(formState.formState);
  };

  return (
    <form className="container" onSubmit={ handleSubmit }>
      <h2>Sign Up</h2>
      { fields.map((field, key) => (
        <div className="form-field" key={ key }>
          <label htmlFor={ field.name }>{ field.label }</label>
          <input
            type={ field.type }
            id={ field.name }
            name={ field.name }
            data-validation={ field.dataValidation }
            value={ formState.formState[field.name] || '' }
            onChange={ handleChange } />
          { errors !== null && 
            <ul className="error">{ errors.errors[field.dataValidation] }</ul>
          }
        </div>
      ))}
      <button type="submit" id="submit">Submit</button>
    </form>
  )
}

export default Signup;
