import UsernameValidator from '../validators/UsernameValidator.js';
import EmailValidator from '../validators/EmailValidator.js';
import PasswordValidator from '../validators/PasswordValidator.js';
import ConfirmPasswordValidator from '../validators/PasswordValidator.js';


class RegistrationForm {
  constructor() {
    this.registrationForm = document.getElementById('form');
    this.registrationForm.addEventListener(
      'submit',
      this.handleSubmit.bind(this)
    );
  }
  
  handleSubmit($event) {
  
    $event.preventDefault();
  
    const elementsToValidate = this.getElementsToValidate();
    let password = {
      primary: null,
      secondary: null
    };
    
    for (const element of elementsToValidate) {
      
      const currentType = element.dataset.validation;
      let validator;
      
      switch (currentType) {

        case 'confirm-password': {
          
          password.secondary = element;
          validator = new ConfirmPasswordValidator(
            password.primary,
            password.secondary
          );
          break;

        }

        case 'password': {

          password.primary = element;
          validator = new PasswordValidator(password.primary);
          break;

        }

        case 'username': {

          validator = new UsernameValidator(element);
          break;

        }

        case 'email': {

          validator = new EmailValidator(element);
          break;

        }

      }
      
      const validationResult = validator.validate();
      this.setMessage(validationResult);
    }
  };

  getElementsToValidate() {
    
    // Converting from HTMLFormControlsCollection to Array
    const elements = Array.from(this.registrationForm.elements);
    
    // Types to exclude from elements Array
    const typesToExclude = new Set(['button', 'submit', 'reset']);
    
    // Elements to be validated
    return elements.filter((element) => !typesToExclude.has(element.type));
  
  }

  setMessage( { element = null, isValid = true, messages = null } ) {
    
    if (element === null) {
      return;
    }

    const formField = element.parentElement;
    const errorField = formField.querySelector('.error');
      
    formField.classList.toggle('error', !isValid);
    formField.classList.toggle('success', isValid);

    if (messages === null) {

      errorField.innerText = '';
      return;
    
    }

    if (typeof messages === 'object') {

      errorField.innerText = '';

      for (const message of messages) {
      
        const li = this.createElement('li', message);
        errorField.appendChild(li);
      
      }
    }
  }

  createElement(tagName, message) {
    
    let element = document.createElement(tagName);
    element.innerText = message;

    return element;
  }
}


document.addEventListener('DOMContentLoaded', function() {
  const registrationForm = new RegistrationForm();
});
