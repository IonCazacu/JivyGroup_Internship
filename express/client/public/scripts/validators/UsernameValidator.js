export default class UsernameValidator {

  /*
    /^(?!.*[._]{2})[_a-zA-Z0-9](?!.*[._]{2})[_a-zA-Z0-9.]{6,18}[_a-zA-Z0-9]$/
    
    ^: Start of the string.

    (?!.*[._]{2}): Negative lookahead assertion to ensure there are no consecutive underscores or dots anywhere in the string.
    
    [_a-zA-Z0-9]: Matches an alphanumeric character or an underscore (valid start).
    
    (?!.*[._]{2}): Negative lookahead assertion to ensure there are no consecutive underscores or dots.
    
    [_a-zA-Z0-9.]{6,18}: Matches between 6 and 18 occurrences of alphanumeric characters, dots, or underscores (middle part).
    
    [_a-zA-Z0-9]: Matches an alphanumeric character or an underscore at the end of the string.
    
    $: End of the string.
  */

  static REGEX = /^(?!.*[._]{2})[_a-zA-Z0-9](?!.*[._]{2})[_a-zA-Z0-9.]{6,18}[_a-zA-Z0-9]$/;

  static ERROR_MESSAGES = [
    "Username cannot contain consecutive underscores or dots",
    "Username require at least 8 characters long",
  ];

  constructor(username = null, regex = UsernameValidator.REGEX) {

    try {

      if (username === null) {
      
        throw new Error("Incorrect class initialization");
      
      }
      
      this.username = username;
      this.regex = regex;
    
    } catch (error) {
    
      console.error(error);
    
    }
    
  }

  validate() {
    let returnMessage = {
      element: this.username,
      isValid: true,
      messages: null
    };

    const isMatch = this.username.value.trim().match(this.regex);

    if (!isMatch) {

      returnMessage.isValid = false;
      returnMessage.messages = UsernameValidator.ERROR_MESSAGES;
    
    }

    return returnMessage;
  };
}
