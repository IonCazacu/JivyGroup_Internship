import React, { useEffect, useRef, useState } from "react";
// import useUsers from "../hooks/useUsers";
// import Users from "../components/Users/Users";

import '../components/Users/Users.scss';

interface UserData {
  id: number;
  username: string;
  email: string;
}

const UsersView = () => {
  const [users, setUsers] = React.useState<UserData[]>([]);
  const [nextCursor, setNextCursor] = React.useState<number>(0);

  const controller = new AbortController();
  const { signal } = controller;

  const getUsers = async () => {

    const queryParams = new URLSearchParams({
      cursor: nextCursor.toString(),
      limit: '100'
    });

    try {

      const response = await fetch(
        'http://localhost:5263/api/user' + '?' + queryParams, {
        signal: signal,
        method: 'GET',
        headers: {
          'Content-Type': 'application/json'
        }
      });

      console.log(response);
      
      const data = await response.json();
      
      console.log(data);

      setUsers((prevState: any) => [
        ...prevState,
        ...data.users
      ]);
      
      setNextCursor(data.nextCursor);

      console.log(nextCursor);

    } catch (error) {

      if (signal.aborted) {
        return;
      }

      console.log(error);

    }

  }

  React.useEffect(() => {
    
    getUsers();

  }, []);

  const handleScroll = () => {
    const bottom = Math.ceil(window.innerHeight + window.scrollY) >= document.documentElement.scrollHeight;

    if (bottom) {
      getUsers();
    }
  }

  useEffect(() => {
    window.addEventListener('scroll', handleScroll);
    return () => {
      window.removeEventListener('scroll', handleScroll);
    };
  }, [users]);
  
  // const {
  //   users,
  //   isLoading,
  //   isError,
  //   error,
  //   nextCursor } = useUsers(currentCursor);

  return (
    <section className="container">
      <h2>Users</h2>
      
      {/* { isError &&
        <p>Error: { error.message }</p>
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
          { users !== undefined &&
            users[0] !== null &&
            users[0] &&
            <tbody>
              { Object.keys(users).map((user, key) => (
                <tr key={ key }>
                  <th scope="row">{ key + 1 }</th>
                  <td>{ users[key].username }</td>
                  <td>{ users[key].email }</td>
                </tr>
              ))}
            </tbody>
          }
        </table>
      </div>

      {/* { isLoading &&
        <p>Loading More Posts</p>
      } */}

    </section>

    // <Users
    //   users={ users }
    //   isLoading={ isLoading }
    //   isError={ isError }
    //   error={ error }
    //   nextCursor={ nextCursor }
    // ></Users>
  )
}

export default UsersView;
