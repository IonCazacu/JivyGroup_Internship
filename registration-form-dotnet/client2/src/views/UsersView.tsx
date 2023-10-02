function Users() {

  fetch('http://localhost:5229/api/user', {
    
    method: 'GET',
    
    headers: {
      'Content-Type': 'application/json'
    }
  })
  .then((response) => {
    return response.json();
  })
  .then((data) => {
    console.log(data);
  })
  .catch((error) => {
    console.log(error);
  })

  return (
    <table className="container">
      <thead>
        <tr>
          <th>ID</th>
          <th>UUID</th>
          <th>Username</th>
          <th>Email</th>
        </tr>
      </thead>
    </table>
  )
}

export default Users;
