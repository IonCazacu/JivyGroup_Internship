import UsernameValidator from '../validators/UsernameValidator.js';
import EmailValidator from '../validators/EmailValidator.js';
import PasswordValidator from '../validators/PasswordValidator.js';
import ConfirmPasswordValidator from '../validators/ConfirmPasswordValidator.js';


class RegistrationForm {
  constructor() {
    this.registrationForm = document.getElementById('form');
    
    this.registrationForm.addEventListener(
      'submit',
      this.handleSubmit.bind(this)
    );

    this.username = document.getElementById('username');
    this.email = document.getElementById('email');
    this.password = document.getElementById('password');
    this.confirmPassword = document.getElementById('confirm-password');
  }
  
  handleSubmit($event) {
  
    $event.preventDefault();

    const usernameInst = new UsernameValidator(this.username);
    const emailInst = new EmailValidator(this.email);
    const passwordInst = new PasswordValidator(this.password);
    const confirmPasswordInst = new ConfirmPasswordValidator(
      this.password,
      this.confirmPassword
    );

    let success =  true;

    for (const element of [
      usernameInst,
      emailInst,
      passwordInst,
      confirmPasswordInst
    ]) {
      
      const validationResult = element.validate();

      this.setMessage(validationResult);

      if (!validationResult.isValid) {
        
        success = false;
      
      }
      
    }

    if (success) {

      const postData = {
        username: this.username.value.trim(),
        email: this.email.value.trim(),
        password: this.password.value.trim()
      };

      fetch('http://localhost:8080/api/users', {
      
        method: 'POST',
      
        headers: {
          'Content-Type': 'application/json'
        },
      
        body: JSON.stringify(postData)

      })
      .then((response) => {
        
        console.log('response', response);

      })
      .catch((error) => {
        
        console.error(error);
      
      });

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
      
    // element.classList.toggle('error');

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
