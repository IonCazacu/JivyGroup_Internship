import React from "react";
import UserData from "../../ports/UserData";

import "./Users.scss";

interface Props {
  users: UserData[];
  isLoading: boolean;
  isError: boolean;
  error: unknown;
  loadMoreRef: React.RefObject<HTMLSpanElement>;
}

const Users: React.FC<Props> = (props: Props) => {
  return (
    <section className="container">
      <h2>Users</h2>
      { props.isError &&
        <p>Error: { (props.error as Error).message }</p>
      }
      <div className="table-responsive">
        <table className="table" role="table" aria-label="Users">
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">Username</th>
              <th scope="col">Email</th>
            </tr>
          </thead>
          { props.users && props.users[0] !== null && (
            <tbody>
              { Object.keys(props.users).map((_, key) => (
                <tr key={ key }>
                  <th scope="row">{ key + 1 }</th>
                  <td>{ props.users[key].username }</td>
                  <td>{ props.users[key].email }</td>
                </tr>
              ))}
            </tbody>
          )}
        </table>
      </div>
      <span ref={ props.loadMoreRef }>
        { props.isLoading &&
          <h1>Loading...</h1>
        }
      </span>
    </section>
  )
}

export default Users;
