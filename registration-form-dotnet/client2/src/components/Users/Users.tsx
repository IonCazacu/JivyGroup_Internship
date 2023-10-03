import React from "react";

import "./Users.scss";

// interface UserData {
//   id: number;
//   username: string;
//   email: string;
// }

const Users: React.FC<any> = ( users: any ) => {

  // for (const user in users.users[0]) {
  //   console.log(users.users[0][user]);
  // }

  // console.log('users', users.users[0]); // object

  return (
    <section className="container">
      <h2>Users</h2>
      <div className="table-responsive">
        <table className="table" role="table" aria-label="Users">
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">Username</th>
              <th scope="col">Email</th>
            </tr>
          </thead>
          { users.users[0] &&
            users.users[0] !== null &&
            <tbody>
              { Object.keys(users.users[0]).map((user, key) => (
                <tr key={ key }>
                  <th scope="row">{ users.users[0][user].id }</th>
                  <td>{ users.users[0][user].username }</td>
                  <td>{ users.users[0][user].email }</td>
                </tr>
              ))}
            </tbody>
          }
        </table>
      </div>
    </section>
  )
}

export default Users