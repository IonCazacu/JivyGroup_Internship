type ControlType = "text" | "email" | "password";

type DynamicFieldData = {
  ariaDescribedBy: string;
  label: string;
  name: string;
  type: ControlType;
  validation: {
    hint: JSX.Element;
    pattern?: {
      value: RegExp;
      message: string;
    },
    required: boolean;
    validate?: (value: string) => boolean;
  }
};

export default DynamicFieldData;
