namespace WineDocumentation.Infrastructure.Commands.Users
{
    public class UpdateUser
    {
        public string Id { get; set; }
        public string Email { get; set;}
        public string Password { get; set;}
        public string Username { get; set;}
        public string Role { get; set; }        
    }
}