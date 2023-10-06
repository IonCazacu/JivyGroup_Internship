export default class PasswordValidator {

  /*
    ^: Start of the string.

    (?=.*[!@#$%^&*()_+\-=\[\]{}|\\:;\"'<>,.?\/~]): Positive lookahead assertion to ensure at least one special character is present.

    [\S]{8,}: Matches any non-whitespace character (including special characters) at least 8 times.

    $: End of the string.
  */

  static REGEX = /^(?=.*[!@#$%^&*()_+\-=\[\]{}|\\:;\"'<>,.?\/~])[\S]{8,}$/;

  static ERROR_MESSAGES = [
    `Password require at least 8 characters long`,
    `Password require special character (!@#$%^&*()_+-=[]{}|:;"'<>,.?/~)`,
    `Password cannot contain spaces or whitespace characters`
  ];

  constructor(password = null, regex = PasswordValidator.REGEX) {
    
    try {

      if (password === null) {
      
        throw new Error("Incorrect class initialization");
      
      }

      this.password = password;
      this.regex = regex;
    
    } catch (error) {
    
      console.error(error);
    
    }

  }

  validate() {
    
    let returnMessage = {
      element: this.password,
      isValid: true,
      messages: null
    };
    
    const isPasswordValid = this.validatePassword();

    if (!isPasswordValid) {
      
      returnMessage.isValid = false;
      returnMessage.messages = PasswordValidator.ERROR_MESSAGES;
    
    }

    return returnMessage;
  };

  validatePassword() {
    return this.password.value.trim().match(this.regex);
  };

}
