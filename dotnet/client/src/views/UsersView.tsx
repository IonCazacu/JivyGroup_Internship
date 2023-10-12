import React from "react";
import Users from "../components/Users/Users";
import UserData from "../Ports/UserData";

import '../components/Users/Users.scss';

const UsersView = () => {
  
  const [users, setUsers] = React.useState<UserData[]>([]);
  const [isLoading, setIsLoading] = React.useState<boolean>(false);
  const [isError, setIsError] = React.useState<boolean>(false);
  const [error, setError] = React.useState<any>({});
  const [nextCursor, setNextCursor] = React.useState<number>(0);
  const loadMoreRef = React.useRef<HTMLSpanElement>(null);

  const getUsers = React.useCallback(async () => {
    
    const controller = new AbortController();
    const { signal } = controller;
    
    try {
      
      setIsLoading(true);
      setIsError(false);
      setError({});
      
      const queryParams = new URLSearchParams({
        cursor: nextCursor.toString(),
        limit: '10'
      });
      
      const response = await fetch(`http://localhost:5263/api/user?${ queryParams }`, {
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
      
    }

    return () => controller.abort();
    
  }, [nextCursor]);

  const handleObserver = React.useCallback((entries: any) => {

    const [target] = entries;
    if (target.isIntersecting) {
      getUsers();
    }

  }, [getUsers]);

  React.useEffect(() => {

    const option = {
      root: null,
      rootMargin: '0px',
      threshold: 1.0
    };

    const observer = new IntersectionObserver(handleObserver, option);

    let observerRefValue: HTMLSpanElement;

    if (loadMoreRef.current) {
      observer.observe(loadMoreRef.current);
      observerRefValue = loadMoreRef.current;
    }

    return () => {
      if (observerRefValue) {
        observer.unobserve(observerRefValue);
      }
    };

  }, [handleObserver]);

  return (
    <Users
      users={ users }
      isLoading={ isLoading }
      isError={ isError }
      error={ error }
      nextCursor={ nextCursor }
      loadMoreRef={ loadMoreRef }
    ></Users>
  )
}

export default UsersView;
