import React from "react";
import UserData from "../types/UserData";
import UsersError from "../errors/UserError";

const useUsers = () => {

  const API = 'http://localhost:5263/api/user';

  const [users, setUsers] = React.useState<UserData[]>([]);
  const [isLoading, setIsLoading] = React.useState<boolean>(false);
  const [isError, setIsError] = React.useState<boolean>(false);
  const [error, setError] = React.useState<UsersError | null>(null);
  const [nextCursor, setNextCursor] = React.useState<number>(0);
  const [hasNextCursor, setHasNextCursor] = React.useState<boolean>(true);
  const [isRequestPending, setIsRequestPending] = React.useState<boolean>(false);

  const getUsers = React.useCallback(async () => {
    
    if (hasNextCursor) {

      if (isRequestPending) {
        return;
      }
      
      setIsRequestPending(true);
  
      const controller = new AbortController();
      const { signal } = controller;
      
      try {
        
        setIsLoading(true);
        setIsError(false);
        setError(null);
        
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
        
        if (!response.ok) {
          // For now 404 code won't get thrown
          if (response.status === 404) {
            throw new UsersError({
              isInformative: true,
              message: response.statusText,
              name: 'GET_USERS_ERROR',
              status: response.status
            });
          }
        }
        
        const data = await response.json();
  
        setUsers((prevState: UserData[]) => [
          ...prevState,
          ...data.users
        ]);
    
        setNextCursor(data.nextCursor);
        
        setHasNextCursor(data.hasNextCursor);
  
        console.log('nextCursor : ', data.nextCursor);
        console.log('hasMore : ', data.hasNextCursor);
  
        setIsLoading(false);
    
      } catch (error) {
  
        console.log('error : ', error);
        
        setIsLoading(false);
        
        if (signal.aborted) {
          return;
        }
        
        setIsError(true);
        
        if (error instanceof UsersError) {
          if (error.name === 'GET_USERS_ERROR') {
            setError(error);
          }
        }
        
      } finally {
  
        setIsLoading(false);
        setIsRequestPending(false);
  
      }
    
      return () => controller.abort();

    }
    
  }, [nextCursor, hasNextCursor, isRequestPending]);

  return {
    users,
    isLoading,
    isError,
    error,
    getUsers
  }

};

export default useUsers;
