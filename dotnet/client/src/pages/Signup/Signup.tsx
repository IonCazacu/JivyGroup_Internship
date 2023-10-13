import React from 'react';

import styles from './Signup.module.scss';

const USERNAME_REGEX = /^(?!.*[._]{2})[_a-zA-Z0-9](?!.*[._]{2})[_a-zA-Z0-9.]{6,18}[_a-zA-Z0-9]$/;
const EMAIL_REGEX = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
const PASSWORD_REGEX = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,}$/;

type SignupForm = {
  username: string;
  email: string;
  password: string;
  confirmPassword: string;
};

const SignupView: React.FC = () => {

  const [formState, setFormState] = React.useState({
    username: '',
    email: '',
    password: '',
    confirmPassword: '',

    validUsername: false,
    validEmail: false,
    validPassword: false,
    validConfirmPassword: false,
    
    usernameFocus: false,
    emailFocus: false,
    passwordFocus: false,
    confirmPasswordFocus: false,
    
    errorMessage: '',
    successMessage: false,
  });

  React.useEffect(() => {
    const result = USERNAME_REGEX.test(formState.username);
    
    setFormState(prevState => ({
      ...prevState,
      validUsername: result
    }));

  }, [formState.username]);

  React.useEffect(() => {
    const result = EMAIL_REGEX.test(formState.email);

    setFormState(prevState => ({
      ...prevState,
      validEmail: result
    }));

  }, [formState.email]);

  React.useEffect(() => {
    const result = PASSWORD_REGEX.test(formState.password);
    
    setFormState(prevState => ({
      ...prevState,
      validPassword: result
    }));

    const match = formState.password === formState.confirmPassword;
    
    setFormState(prevState => ({
      ...prevState,
      validConfirmPassword: match
    }));

  }, [formState.password, formState.confirmPassword]);

  React.useEffect(() => {

    setFormState(prevState => ({
    
      ...prevState,
      errorMessage: ''
    
    }));

  }, [formState.username, formState.email, formState.password, formState.confirmPassword]);

  const handleSubmit: React.FormEventHandler = async ($event: React.FormEvent<HTMLFormElement>) => {
    
    $event.preventDefault();
    
    const validUsername = USERNAME_REGEX.test(formState.username);
    const validEmail = EMAIL_REGEX.test(formState.email);
    const validPassword = PASSWORD_REGEX.test(formState.password);

    try {

      if (!validUsername || !validEmail || !validPassword) {
        throw new Error ('Please fill out all fields.');
      }

      const signupForm: SignupForm = {
        username: formState.username,
        email: formState.email,
        password: formState.password,
        confirmPassword: formState.confirmPassword
      };

      const response = await fetch('http://localhost:5263/api/user', {
        headers: {
          'Content-Type': 'application/json'
        },
        method: 'POST',
        body: JSON.stringify(signupForm)
        // body: JSON.stringify({})
      });

      const data = await response.json();
      console.log(data);

      if (!response.ok) {

        const msg = `HTTP error! status: ${ response.status }, message: ${ data.message }`;
        
        throw new Error(msg);

      }
      
    } catch (error) {
      
      if (error instanceof TypeError) {

        const msg = `A network error occurred: ${ error.message }`;

        setFormState(prevState => ({
    
          ...prevState,
          errorMessage: msg
      
        }));
      
      } else {

        console.error("An unexpected error occurred:", error);
      
      }
    }
  }

  return (
    <section className={ styles["container"] }>
      <form className={ styles["form__box-shadow"] } onSubmit={ handleSubmit }>
        <p
          aria-live="assertive"
          className={
            formState.errorMessage ? "error-shown" : "hidden"
          }>{ formState.errorMessage }
        </p>
        <h2>Sign Up</h2>
        <div className={ styles["form-group"] }>
          <label
            className={ styles["form-label"] }
            htmlFor="username">Username
          </label>
          <input
            type="text"
            id="username"
            autoComplete="off"
            aria-describedby="uidnote"
            required
            className={ styles["form-input"] }
            value={ formState.username }
            aria-invalid={ formState.validUsername ? "false" : "true" }
            onChange={
              ($event) => setFormState(prevState => ({
                ...prevState, username: $event.target.value
            }))}
            onFocus={
              () => setFormState(prevState => ({
                ...prevState, usernameFocus: true
            }))}
            onBlur={
              () => setFormState(prevState => ({
                ...prevState, usernameFocus: false
            }))} />
          <p
            id="uidnote"
            className={
              formState.usernameFocus &&
              formState.username &&
              !formState.validUsername ? styles["sr-shown"] : "hidden"
            }>
            8 to 20 characters.<br />
            Dots and underscores are allowed.<br />
            Consecutive underscores or dots are not allowed.
          </p>
        </div>

        <div className={ styles["form-group"] }>
          <label
            className={ styles["form-label"] }
            htmlFor="email">Email
          </label>
          <input
            type="email"
            id="email"
            autoComplete="off"
            aria-describedby="emlnote"
            required
            className={ styles["form-input"] }
            value={ formState.email }
            aria-invalid={ formState.validEmail ? "false" : "true" }
            onChange={
              ($event) => setFormState(prevState => ({
                ...prevState, email: $event.target.value
            }))}
            onFocus={
              () => setFormState(prevState => ({
                ...prevState, emailFocus: true
            }))}
            onBlur={
              () => setFormState(prevState => ({
                ...prevState, emailFocus: false
            }))} />
          <p
            id="emlnote"
            className={
              formState.emailFocus &&
              formState.email &&
              !formState.validEmail ? styles["sr-shown"] : "hidden"
            }>Invalid email address.
          </p>
        </div>

        <div className={ styles["form-group"] }>
          <label
            className={ styles["form-label"] }
            htmlFor="password">Password
          </label>
          <input
            id="password"
            type="password"
            aria-describedby="pwdnote"
            required
            value={ formState.password }
            className={ styles["form-input"] }
            aria-invalid={ formState.validPassword ? "false" : "true" }
            onChange={
              ($event) => setFormState(prevState => ({
                ...prevState, password: $event.target.value
            }))}
            onFocus={
              () => setFormState(prevState => ({
                ...prevState, passwordFocus: true
            }))}
            onBlur={
              () => setFormState(prevState => ({
                ...prevState, passwordFocus: false
            }))} />
          <p
            id="pwdnote"
            className={
              formState.passwordFocus &&
              formState.password &&
              !formState.validPassword ? styles["sr-shown"] : "hidden"
            }>
            At least 8 characters long.<br />
            Must include lowercase and uppercase letters, a number and a special character.<br />
            Allowed special characters: <span aria-label="exclamation mark symbol">!</span> <span aria-label="at symbol">@</span> <span aria-label="hashtag symbol">#</span> <span aria-label="dollar sign">$</span> <span aria-label="percent symbol">%</span>
          </p>
        </div>

        <div className={ styles["form-group"] }>
          <label
            className={ styles["form-label"] }
            htmlFor="username">Confirm Password
          </label>
          <input
            id="confirm_password"
            type="password"
            aria-describedby="confirmnote"
            required
            value={ formState.confirmPassword }
            className={ styles["form-input"] }
            aria-invalid={ formState.validConfirmPassword ? "false" : "true" }
            onChange={
              ($event) => setFormState(prevState => ({
                ...prevState, confirmPassword: $event.target.value
            }))}
            onFocus={
              () => setFormState(prevState => ({
                ...prevState, confirmPasswordFocus: true
            }))}
            onBlur={
              () => setFormState(prevState => ({
                ...prevState, confirmPasswordFocus: false
            }))} />
          <p
            id="confirmnote"
            className={
              formState.confirmPasswordFocus &&
              formState.confirmPassword &&
              !formState.validConfirmPassword ? styles["sr-shown"] : "hidden"
            }>Must match the first password field.
          </p>
        </div>

        <button
          disabled={
            !formState.validUsername ||
            !formState.validEmail ||
            !formState.validPassword ||
            !formState.validConfirmPassword
          }>Sign Up
        </button>
      </form>
    </section>
  )
}

export default SignupView;
