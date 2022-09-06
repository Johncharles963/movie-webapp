using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoviesAppTest.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Name")]
        public string name { get; set; }
        public string? Director { get; set; }
        [StringLength(5, ErrorMessage ="Ratings cannot excede 5 characters")]
        public string? Rating { get; set; }
        public string? Description { get; set; }
        [Required]
        [DisplayName("Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? ImgUrl { get; set; }
    }
}
