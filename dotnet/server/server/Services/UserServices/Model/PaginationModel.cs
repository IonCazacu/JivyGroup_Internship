namespace server.Services.UserServices.Model
{
    public class PaginationModel
    {
        public IEnumerable<User>? Users { get; set; }
        public long NextCursor { get; set; }
    }
}
