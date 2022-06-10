namespace WineDocumentation.Infrastructure.Commands.Wines
{
    public class CreateWine : ICommand
    {
        public string Winename { get; set;}
        public string Speciename { get; set;}
        public string Description { get; set;}
        public string Brand { get; set;}
        // public string __RequestVerificationToken { get; set;}
    }
}