import PasswordValidator from '../validators/PasswordValidator.js';


export default class ConfirmPasswordValidator extends PasswordValidator {
  constructor(password, confirmPassword = null) {
    
    super(password);
    
    try {
      
      if (confirmPassword === null) {
      
        throw new Error("Incorrect class initialization");
      
      }

      this.confirmPassword = confirmPassword;
  
    } catch (error) {
  
      console.error(error);
  
    }

  }

  validate() {
    let returnMessage = {
      element: this.confirmPassword,
      isValid: true,
      messages: null
    };

    const isConfirmPasswordValid = this.validateConfirmPassword();

    if (!isConfirmPasswordValid) {

      returnMessage.isValid = false;
      returnMessage.messages = ["Passwords do not match"];

    }

    return returnMessage;
  }

  validateConfirmPassword() {
    return this.confirmPassword.value.trim() === this.password.value.trim();
  };
}
