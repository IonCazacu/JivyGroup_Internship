import React, { useState } from 'react';
import FormData from '../../../ports/FormData';
import FormError from '../../../ports/FormError';
import FormField from '../../../ports/FormField';

// import './Authentication.scss';

// Interface for form state
interface State {
  formState: {
    [key: string]: string
  };
};

// Interface for form component
interface FormProps {
  header: string;
  fields: FormField[];
  errors: FormError;
  onSubmit: (FormData: FormData) => void;
};

const Auth: React.FC<FormProps> = ({ header, fields, errors, onSubmit }) => {

  const [formState, setFormState] = useState<State>({ formState: {} });

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

  console.log('object : ', errors);

  return (
    // <section></section>
    <form className="container" onSubmit={ handleSubmit }>
      <div className='container__box-shadow'>
        <p>Server Error</p>
        <h2>{ header }</h2>
        { fields.map((field, key) => (
          <div className="form-group" key={ key }>
            <label
              className='form-label'
              htmlFor={ field.name }>
              { field.label }
            </label>
            <input
              className='form-input'
              type={ field.type }
              id={ field.name }
              name={ field.name }
              data-validation={ field.dataValidation }
              value={ formState.formState[field.name] || '' }
              onChange={ handleChange } />
            { errors.formError.errors?.[field.dataValidation] &&
              errors.formError.errors?.[field.dataValidation] !== null && 
              <ul className="error-list">
                <li className='error-item'>
                  { errors.formError.errors?.[field.dataValidation] }
                </li>
              </ul>
            }
          </div>
        ))}
        <button type="submit" id="submit" className='submit-btn'>Submit</button>
      </div>
    </form>
  )
}

export default Auth;
