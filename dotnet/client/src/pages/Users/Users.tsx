import Users from "../../components/Users/Users";
import useUsers from "../../hooks/useUsers";
import useInfiniteScroll from "../../hooks/useInfiniteScroll";

const UsersView = () => {

  const { users, isLoading, isError, error, getUsers } = useUsers();
  const loadMoreRef  = useInfiniteScroll(getUsers);

  return (
    <Users
      users={ users }
      isLoading={ isLoading }
      isError={ isError }
      error={ error }
      loadMoreRef={ loadMoreRef }
    ></Users>
  )
}

export default UsersView;
