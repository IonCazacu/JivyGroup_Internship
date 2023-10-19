namespace server.Services.UserServices.Model
{
    public class Pagination
    {
        public IEnumerable<User>? Users { get; set; }
        public long NextCursor { get; set; }
    }
}
