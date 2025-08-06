using System;
using System.ComponentModel.DataAnnotations;

namespace HealthTipsPortal.Models
{
    public class HealthTip
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string TipText { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
