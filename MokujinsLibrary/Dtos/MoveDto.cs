namespace MokujinsLibrary.Dtos
{
    public record MoveDto
    {
        
        public string character { get; init; }
        public string moveName { get; init; }
        public string input { get; init; }
        public string damage { get; init; }
        public string framesOnBlock { get; init; }
        public string framesStartup { get; init; }
        public string hitLevel { get; init; }
        public string notes { get; init; }
        
    }
}