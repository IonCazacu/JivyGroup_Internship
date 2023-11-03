namespace server.Services.UserServices.Entities
{
    public class Pagination
    {
        public IEnumerable<User>? Users { get; set; }
        public long NextCursor { get; set; }
        public bool HasNextCursor { get; set; }

        public void Deconstruct(
            out IEnumerable<User>? users,
            out long nextCursor,
            out bool hasNextCursor
        ) {
            users = Users;
            nextCursor = NextCursor;
            hasNextCursor = HasNextCursor;
        }
    }
}
