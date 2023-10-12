import React from "react";
import UserData from "../../Ports/UserData";

import "./Users.scss";

interface Props {
  users: UserData[];
  isLoading: boolean;
  isError: boolean;
  error: object;
  nextCursor: number;
  loadMoreRef: React.RefObject<HTMLSpanElement>;
}

const Users: React.FC<Props> = (props: Props) => {
  
  console.log(props);

  return (
    <section className="container">
      <h2>Users</h2>
      
      {/* { props.isError &&
        <p>Error: { props.error.message }</p>
      } */}

      <div className="table-responsive">
        <table className="table" role="table" aria-label="Users">
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">Username</th>
              <th scope="col">Email</th>
            </tr>
          </thead>
          { props.users !== undefined &&
            props.users[0] !== null &&
            props.users[0] &&
            <tbody>
              { Object.keys(props.users).map((user, key) => (
                <tr key={ key }>
                  <th scope="row">{ key + 1 }</th>
                  <td>{ props.users[key].username }</td>
                  <td>{ props.users[key].email }</td>
                </tr>
              ))}
            </tbody>
          }
        </table>
      </div>
      <span ref={ props.loadMoreRef }>
        { props.isLoading &&
          <h1>Loading More Posts</h1>
        }
      </span>
    </section>
  )
}

export default Users;
