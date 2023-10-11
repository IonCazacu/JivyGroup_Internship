import React from "react";

const useUsers = (cursor: number = 0) => {
  const [users, setUsers] = React.useState<any>([]);
  const [isLoading, setIsLoading] = React.useState<boolean>(false);
  const [isError, setIsError] = React.useState<boolean>(false);
  const [error, setError] = React.useState<any>({});
  const [nextCursor, setNextCursor] = React.useState<number>(0);

  const getUsers = React.useCallback(async () => {
    
    const controller = new AbortController();
    const { signal } = controller;

    const queryParams = new URLSearchParams({
      cursor: cursor.toString(),
      limit: '10'
    });
      
    try {
      
      setIsLoading(true);
      setIsError(false);
      setError({});
      
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
      
      setUsers((prevState: any) => [
        ...prevState,
        ...data.users
      ]);
      
      console.log(users);
      
      setNextCursor(data.nextCursor);
      
      console.log(nextCursor);
      
      setIsLoading(false);

    } catch (error) {

      setIsLoading(false);
      
      if (signal.aborted) { return; }
      
      setIsError(true);
      
      if (error instanceof TypeError) { setError({ message: error.message }); }
      
      console.log(error);
      
    }

    return () => controller.abort();
    
  }, [cursor]);

  React.useEffect(() => {    
    
    getUsers();

  }, [getUsers]);

  return { users, isLoading, isError, error, nextCursor };
}

export default useUsers;
