import React, { ChangeEventHandler } from 'react'

const useForm = () => {
  
  // Form values
  const [values, setValues] = React.useState({})
  // Errors
  const [errors, setErrors] = React.useState({})
  
  const onChange = ($event: React.ChangeEvent<HTMLInputElement>) => {
    $event.persist();

    let name = $event.target.name;
    let val = $event.target.value;

    const validate = ($event: React.ChangeEvent<HTMLInputElement>, name: any, val: any) => {

    }

    setValues({
      ...values,
      [name]: val
    })

    validate($event, name, val);
  }

  return (
    <div>useForm</div>
  )
}

export default useForm;
