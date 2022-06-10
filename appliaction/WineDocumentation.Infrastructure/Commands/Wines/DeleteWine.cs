namespace WineDocumentation.Infrastructure.Commands.Wines
{
    public class DeleteWine : ICommand
    {
        public string Id { get; set; }
        public string __RequestVerificationToken { get; set;}
    }
}