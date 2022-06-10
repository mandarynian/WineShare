namespace WineDocumentation.Infrastructure.Commands.Users
{
    public class LogInUser : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}