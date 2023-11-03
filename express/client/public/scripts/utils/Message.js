class Message {

  setMessage( { element = null, isValid = true, messages = null } ) {
  
    if (element === null) {
      return;
    }

    const formField = element.parentElement;
    const errorField = formField.querySelector('.error');
      
    // formField.classList.toggle('error', !isValid);
    // formField.classList.toggle('success', isValid);

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
}

createElement(element) {
    
  let element = document.createElement(tagName);
  element.innerText = message;

  return element;
}