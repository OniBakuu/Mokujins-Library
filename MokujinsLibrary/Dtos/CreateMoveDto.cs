using System.ComponentModel.DataAnnotations;

namespace MokujinsLibrary.Dtos
{
    public record CreateMoveDto
    {
        [Required]
        public string character { get; init; }
        [Required]
        public string moveName { get; init; }
        [Required]
        public string input { get; init; }
        [Required]
        public string damage { get; init; }
        [Required]
        public string framesOnBlock { get; init; }
        [Required]
        public string framesStartup { get; init; }
        [Required]
        public string hitLevel { get; init; }
        public string notes { get; init; }
    }
}