using Azure;
using StealAllTheCats.Models.Api;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StealAllTheCats.Models
{
    [Table("Cats")]
    public class CatEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string CatId { get; set; } = string.Empty;

        public int Width { get; set; }

        public int Height { get; set; }

        public string Image { get; set; }

        public DateTime Created { get; set; }

        public virtual List<TagEntity> Tags { get; set; } = new();
    }
}
