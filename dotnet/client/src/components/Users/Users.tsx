import React from "react";
import UserData from "../../ports/UserData";

import styles from "./Users.module.scss";

type Props = {
  users: UserData[];
  isLoading: boolean;
  isError: boolean;
  error: unknown;
  loadMoreRef: React.RefObject<HTMLParagraphElement>;
}

const Users: React.FC<Props> = (props: Props) => {
  return (
    <section className={styles["container"]}>
      <p
        aria-live="assertive"
        className={props.isError ? "error-shown" : "hidden"}
      >{(props.error as Error).message}</p>
      <h2>Users</h2>
      <div className={styles["table-responsive"]}>
        <table className="table" role="table" aria-label="Users">
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">Username</th>
              <th scope="col">Email</th>
            </tr>
          </thead>
          {props.users && props.users[0] !== null && (
            <tbody>
              {Object.keys(props.users).map((_, key) => (
                <tr key={ key }>
                  <th scope="row">{key + 1}</th>
                  <td>{props.users[key].username}</td>
                  <td>{props.users[key].email}</td>
                </tr>
              ))}
            </tbody>
          )}
        </table>
      </div>
      <p aria-live="assertive" ref={props.loadMoreRef}>
        {props.isLoading &&
          <span>Loading...</span>
        }
      </p>
    </section>
  )
}

export default Users;
