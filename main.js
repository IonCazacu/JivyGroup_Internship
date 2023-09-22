class RegistrationForm {
    constructor() {
        const registrationForm = document.getElementById('form');
        registrationForm.addEventListener('submit', this.handleSubmit);

        const name = document.getElementById('name');
        name.addEventListener('blur', this.handleNameValidation);

        const email = document.getElementById('email');
        email.addEventListener('blur', this.handleEmailValidation);

        const formInputPassword = document.getElementById('form-input-password');
        const password = document.getElementById('password');
        
        const formInputConfirmPassword = document.getElementById('form-input-confirm-password');
        const confirmPassword = document.getElementById('confirm-password');

        const error = document.getElementById('error');
        
        const eyeFillBtn = document.getElementById('eye-fill-btn');
        const eyeSlashFillBtn = document.getElementById('eye-slash-fill-btn');
    }

    handleNameValidation($event) {

    };

    handleEmailValidation($event) {
        $event.preventDefault();
        const emailRegex = "/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/";
        const emailValue = $event.target.value;
        if (emailValue.trim() === '') {
            email.classList.add('error');
            error.innerHTML = 'Email is required';
            return;
        }

        if (!emailValue.match(emailRegex)) {
            error.innerHTML = 'Invalid email address';
            return;
        }
    };

    handleSubmit($event) {
        $event.preventDefault();
        console.log('object');
    };

    onBlur($event) {
        $event.preventDefault();
        const target = $event.target.value;
        console.log(target);
        const name = target.name;
        const value = target.value;
    };
}
