using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cv.DB.Model;

[Table("Candidates")]
[PrimaryKey(nameof(Id))]
public class Candidate
{
    [DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.Identity)]
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public IList<string> Skills { get; set; } = [];
    public virtual ICollection<Experience> Experiences { get; set; } = null!;
}
