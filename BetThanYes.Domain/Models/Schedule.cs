using System;

namespace BetThanYes.Domain.Models
{
    public class Schedule
    {
        public Guid Id { get; set; }                          // Clave primaria
        public long Name { get; set; }                  // Número interno incremental
        public int Days { get; set; } = string.Empty;
    }
}

