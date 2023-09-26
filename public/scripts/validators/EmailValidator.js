export default class EmailValidator {

  /*
    /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/

    ^: Start of the string.
    
    [a-zA-Z0-9._%+-]+: Match: letters (upper && lower case), digits, dots, underscores, percent signs, plus signs, and hyphens.
    
    @: Match the at symbol.
    
    [a-zA-Z0-9.-]+: Match: letters (upper && lower case), digits, dots, and hyphens. This is for the domain name.
    
    \.: Match a literal dot.
    
    [a-zA-Z]{2,}: Match at least two or more letters. This is for the top-level domain (like com, net, org, etc.).
    
    $: End of the string.
  */

  static REGEX = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

  constructor(email = null, regex = EmailValidator.REGEX) {

    try {

      if (email === null) {
      
        throw new Error("Incorrect class initialization");
      
      }
      
      this.email = email;
      this.regex = regex;
    
    } catch (error) {
    
      console.error(error);
    
    }
    
  }

  validate() {  
    let returnMessage = {
      element: this.email,
      isValid: true,
      messages: null
    };

    const isMatch = this.email.value.trim().match(this.regex);

    if (!isMatch) {

      returnMessage.isValid = false;
      returnMessage.messages = ["Invalid email address"];
    
    }

    return returnMessage;
  };
}
  