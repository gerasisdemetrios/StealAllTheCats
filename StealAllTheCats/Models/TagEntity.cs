using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StealAllTheCats.Models
{
    [Table("Tags")]
    public class TagEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime Created { get; set; }

        public virtual List<CatEntity> Cats { get; } = new();
    }
}
