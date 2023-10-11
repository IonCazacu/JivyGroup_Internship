import { useEffect, useState } from "react";
import Users from "../components/Users/Users";

interface UserData {
  id: number;
  username: string;
  email: string;
}

const UsersView = () => {

  const [usersState, setUsersState] = useState<UserData[]>([]);

  useEffect(() => {

    const getUsers = async () => {

      try {

        const response = await fetch('http://localhost:5263/api/user', {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json'
          }
        });

        if (response) {

          const data = await response.json();
          setUsersState(prevState => [
            ...prevState,
            data
          ]);

        }

      } catch (error) {

        console.log(error);

      }

    }

    getUsers();

  }, []);

  return (
    <Users users={ usersState }></Users>
  )
}

export default UsersView;
