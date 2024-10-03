using System.ComponentModel.DataAnnotations;

namespace VálasztásWebApp.Models
{
    public class Part
    {
        [Key]
        public string RovidNev { get; set; }
        public string? TeljesNev { get; set; }
        public virtual ICollection<Jelolt> Jeloltek { get; set; }
    }
}
