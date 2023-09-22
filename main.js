class RegistrationForm {
    constructor() {
        this.registrationForm = document.getElementById('form');
        this.username = document.getElementById('username');
        this.email = document.getElementById('email');
        this.password = document.getElementById('password');
        this.confirmPassword = document.getElementById('confirmPassword');

        this.registrationForm.addEventListener(
            'submit',
            this.handleSubmit.bind(this)
        );
    }
    
    handleSubmit($event) {
        $event.preventDefault();
        console.log('object');
        this.handleNameValidation();
        this.handleEmailValidation();
        this.handlePasswordValidation();
        this.handleConfirmPasswordValidation();
    };

    handleNameValidation() {
        let returnMessage = {
            isValid: true,
            message: null
        };

        const isEmpty = this.isEmpty(this.username);
        
        if (isEmpty) {
            returnMessage.isValid = false;
            returnMessage.message = 'Username is required';
            console.log('handleNameValidation', returnMessage);
            return returnMessage;
        }
        
        const minLength = 3, maxLength = 18;
        const isBetween = this.isBetween(this.username, minLength, maxLength);

        if (!isBetween) {
            returnMessage.isValid = false;
            returnMessage.message = `Username must be between ${ minLength } and ${ maxLength }`;
        }

        console.log('handleNameValidation', returnMessage);
        return returnMessage;
    }

    handleEmailValidation() {
        /*
            ^: Start of the string.
            
            [a-zA-Z0-9._%+-]+: Match one or more of the following characters: letters (both upper and lower case), digits, dots, underscores, percent signs, plus signs, and hyphens.
            
            @: Match the at symbol.
            
            [a-zA-Z0-9.-]+: Match one or more of the following characters: letters (both upper and lower case), digits, dots, and hyphens. This is for the domain name.
            
            \.: Match a literal dot.
            
            [a-zA-Z]{2,}: Match at least two or more letters. This is for the top-level domain (like com, net, org, etc.).
            
            $: End of the string.
        */
        const EMAIL_REGEX = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        
        let returnMessage = {
            isValid: true,
            message: null
        };
        
        const isEmpty = this.isEmpty(this.email);

        if (isEmpty) {
            returnMessage.isValid = false;
            returnMessage.message = 'Email is required';
            console.log('handleEmailValidation', returnMessage);
            return returnMessage;
        }

        const isMatch = this.email.value.trim().match(EMAIL_REGEX);

        if (!isMatch) {
            returnMessage.isValid = false;
            returnMessage.message = 'Invalid email address';
        }

        console.log('handleEmailValidation', returnMessage);
        return returnMessage;
    }

    handlePasswordValidation() {
        let returnMessage = {
            isValid: true,
            message: null
        };

        const isEmpty = this.isEmpty(this.password);

        if(isEmpty) {
            returnMessage.isValid = false;
            returnMessage.message = 'Password is empty';
            console.log('handlePasswordValidation', returnMessage);
            return returnMessage;
        }

        const minLength = 8, maxLength = 25;
        const isBetween = this.isBetween(this.password, minLength, maxLength);

        if (!isBetween) {
            returnMessage.isValid = false;
            returnMessage.message = `Password must be between ${ minLength } and ${ maxLength }`;
        }

        console.log('handlePasswordValidation', returnMessage);
        return returnMessage;
    }

    handleConfirmPasswordValidation() {
        let returnMessage = {
            isValid: true,
            message: null
        };

        const passwordIsEmpty = this.isEmpty(this.password);
        const confirmPasswordIsEmpty = this.isEmpty(this.confirmPassword);

        if(confirmPasswordIsEmpty) {
            returnMessage.isValid = false;
            returnMessage.message = 'Confirm your password';
            console.log('handleConfirmPasswordValidation', returnMessage);
            return returnMessage;
        }

        if(passwordIsEmpty) {
            returnMessage.isValid = false;
            returnMessage.message = 'Password is empty';
            console.log('handleConfirmPasswordValidation', returnMessage);
            return returnMessage;
        }

        if (this.password !== this.confirmPassword) {
            returnMessage.isValid = false;
            returnMessage.message = 'The passwords must match';
        }

        console.log('handleConfirmPasswordValidation', returnMessage);
        return returnMessage;
    }

    isBetween(input, minLength, maxLength) {
        const inputValue = input.value.trim().length;
        return !this.isEmpty(input) && inputValue >= minLength && inputValue <= maxLength;
    }

    isEmpty(input) {
        return !input.value.trim().length;
    }
}
