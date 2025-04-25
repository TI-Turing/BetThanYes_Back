using System;

namespace BetThanYes.Domain.Models
{
    public class Country
    {
        public string Code { get; set; } // Mapeo de [code] [char](2)
        public string Name { get; set; } // Mapeo de [name] [varchar](255)
        public string FullName { get; set; } // Mapeo de [full_name] [varchar](255)
        public string Iso3 { get; set; } // Mapeo de [iso3] [char](3)
        public string Number { get; set; } // Mapeo de [number] [char](3)
        public string ContinentCode { get; set; } // Mapeo de [continent_code] [char](2)
    }
}
