using System;
using WebApiVersion.Models.Domain;

namespace WebApiVersion
{
    public class CountriesData
    {
        public static List<Country> Get()
        {

            var countries = new[]
            {
                new Country {Id = 1 , Name = "Byculla"},
                new Country {Id = 2 , Name = "Lalbaug"},
                new Country {Id = 3 , Name = "Dadar"},
                new Country {Id = 4 , Name = "Kalyan"},
                new Country {Id = 5 , Name = "Dombivli"},
                new Country {Id = 6 , Name = "Badlapur"},
                new Country {Id = 7 , Name = "Mulund"},
                new Country {Id = 8 , Name = "Thane"},

            };

            return countries.Select(c => new Country { Id = c.Id, Name = c.Name }).ToList();
        }
    }
}

