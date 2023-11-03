import React from "react";
import { useForm, Controller } from "react-hook-form";
import signupSchema from "./SignupSchema";

import styles from "./Signup.module.scss";

const SignupView: React.FC = () => {

  const API = 'http://localhost:5263/api/user';
  
  const { control, formState: { isValid, errors }, handleSubmit, setError, watch } = useForm({ mode: "all" });

  const onSubmit = async (signupForm: any) => {

    try {

      const response = await fetch(API, {
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
        
        throw new Error(data.message);

      }
      
    } catch (error) {
      
      let msg = undefined;

      if (error instanceof TypeError) {

        msg = `Network response was not ok`;
        
      } else {

        msg = `${ error }`;
      
      }
      
      setError("root", {
        message: msg
      });
    }
  }

  return (
    <section className={styles["container"]}>
      <form className={styles["form__box-shadow"]} onSubmit={handleSubmit(onSubmit)}>
        <p
          aria-live="assertive"
          className={
            !!errors.root?.message ? "error-shown" : "hidden"
          }>{errors.root?.message}
        </p>
        <h2>Sign Up</h2>

        {signupSchema(watch).map((group, key) => {

          return (
            <fieldset key={key} className={styles["form-set"]}>
              <label className={styles["form-label"]} htmlFor={group.name}>
                {group.label}
              </label>

              <Controller
                name={group.name}
                control={control}
                defaultValue=""
                rules={group.validation}
                render={({ field, fieldState }) => {

                  return (
                    <div className={styles["form-field"]}>
                      <input
                        {...field}
                        type={group.type}
                        id={field.name}
                        autoComplete="off"
                        required={group.validation.required}
                        className={styles["form-input"]}
                        aria-describedby={group.ariaDescribedBy}
                        aria-invalid={!!fieldState.error && !!field.value}
                      />
                      <div
                        role="alert"
                        id={group.ariaDescribedBy}
                        className={
                          !!fieldState.error && !!field.value ? styles["sr-shown"] : "hidden"
                        }>
                        {group.validation.hint}
                      </div>
                    </div>
                  );

                }}
              ></Controller>

            </fieldset>
          );

        })}

        <button disabled={ !isValid }>Sign Up</button>

      </form>
    </section>
  )
};

export default SignupView;
