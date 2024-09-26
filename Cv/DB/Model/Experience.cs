using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cv.DB.Model;

[Table("Experiences")]
[PrimaryKey(nameof(Id))]
public class Experience
{
    [DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.Identity)]
    [Key]
    public Guid Id { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public string Company { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IList<string> Skills { get; set; } = [];
    public Guid CandidateId { get; set; }
    public virtual Candidate Candidate { get; set; } = null!;
}