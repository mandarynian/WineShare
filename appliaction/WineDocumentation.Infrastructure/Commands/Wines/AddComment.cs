namespace WineDocumentation.Infrastructure.Commands.Wines
{
    public class AddComment : ICommand
    {
        public string WineId {get;set;}
        public string ScoreValue {get; set;}
        public string Comment {get;set;}
        public string Author {get;set;}
        public string __RequestVerificationToken { get; set;}
    }
}