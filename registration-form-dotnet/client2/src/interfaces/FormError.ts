interface FormError {
  type: string;
  title: string;
  status: number;
  traceId: string;
  errors: Array<Object>;
}

export default FormError;
