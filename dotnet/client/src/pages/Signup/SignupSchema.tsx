import DynamicFieldData from "../../types/DynamicFieldData";

const signupSchema = (watch: any): DynamicFieldData[] => {
  return [
    {
      ariaDescribedBy: "username-description",
      label: "Username",
      name: "username",
      type: "text",
      validation: {
        hint: (
          <ul>
            <li>
              Length: 8 to 20 characters.
            </li>
            <li>
              Allowed Special Characters: [
                <span aria-label="Period (.)">(.)</span>,
                <span aria-label="Underscore (_)">(_)</span>
              ].
            </li>
            <li>
              Consecutive Special Characters: No consecutive [
                <span aria-label="Period (.)">(.)</span>,
                <span aria-label="Underscore">(_)</span>
              ].
            </li>
            <li>
              Starting Character: Cannot begin with [
                <span aria-label="Period (.)">(.)</span>
              ].
            </li>
          </ul>
        ),
        pattern: {
          value: /^(?!.*[._]{2})[_a-zA-Z0-9](?!.*[._]{2})[_a-zA-Z0-9.]{6,18}[_a-zA-Z0-9]$/,
          message: ""
        },
        required: true
      }
    },
    {
      ariaDescribedBy: "email-description",
      label: "Email",
      name: "email",
      type: "email",
      validation: {
        hint: (
          <ul>
            <li>
              Invalid email address.
            </li>
          </ul>
        ),
        pattern: {
          value: /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/,
          message: ""
        },
        required: true
      }
    },
    {
      ariaDescribedBy: "password-description",
      label: "Password",
      name: "password",
      type: "password",
      validation: {
        hint: (
          <ul>
            <li>
              Length: At least 8 characters long.
            </li>
            <li>
              Must include lowercase and uppercase letters, a number and a special character.
            </li>
            <li>
              Allowed special characters: [
                <span aria-label="Exclamation mark (!)">(!)</span>,
                <span aria-label="At sign (@)">(@)</span>,
                <span aria-label="Hashtag sign (#)">(#)</span>,
                <span aria-label="Dollar sign">($)</span>,
                <span aria-label="Percent sign (%)">(%)</span>
              ].
            </li>
          </ul>
        ),
        pattern: {
          value: /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,}$/,
          message: ""
        },
        required: true
      }
    },
    {
      ariaDescribedBy: "confirm-password-description",
      label: "Confirm password",
      name: "confirmPassword",
      type: "password",
      validation: {
        hint: (
          <ul>
            <li>
              Must match the first password field.
            </li>
          </ul>
        ),
        required: true,
        validate: (value: string) => value === watch("password")
      }
    }
  ];
};
export default signupSchema;
