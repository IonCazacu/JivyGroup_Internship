interface FormError {
  formError: {
    type: string;
    title: string;
    status: number;
    traceId: string;
    errors: {
      [key: string]: Array<string>;
    }
  }
}

export default FormError;
