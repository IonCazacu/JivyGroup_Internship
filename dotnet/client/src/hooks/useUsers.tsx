import React from "react";
import UserData from "../ports/UserData";

const useUsers = () => {

  const API = 'http://localhost:5263/api/user';

  const [users, setUsers] = React.useState<UserData[]>([]);
  const [isLoading, setIsLoading] = React.useState<boolean>(false);
  const [isError, setIsError] = React.useState<boolean>(false);
  const [error, setError] = React.useState<unknown>({});
  const [nextCursor, setNextCursor] = React.useState<number>(0);

  const getUsers = React.useCallback(async () => {
    
    const controller = new AbortController();
    const { signal } = controller;
    
    try {
      
      setIsLoading(true);
      setIsError(false);
      setError({});
      
      const queryParams = new URLSearchParams({
        cursor: nextCursor.toString(),
        limit: '50'
      });
      
      const response = await fetch(`${ API }?${ queryParams }`, {
        headers: {
          'Content-Type': 'application/json'
        },
        method: 'GET',
        signal: signal
      });
      
      const data = await response.json();
      
      setUsers((prevState: UserData[]) => [
        ...prevState,
        ...data.users
      ]);
  
      setNextCursor(data.nextCursor);
      
      setIsLoading(false);
  
    } catch (error) {
  
      setIsLoading(false);
      
      if (signal.aborted) {
        return;
      }
      
      setIsError(true);
      
      if (error instanceof TypeError) {
        setError({ message: error.message });
      }
      
    } finally {
      setIsLoading(false);
    }
  
    return () => controller.abort();
    
  }, [nextCursor]);

  return {
    users,
    isLoading,
    isError,
    error,
    getUsers
  }

};

export default useUsers;
