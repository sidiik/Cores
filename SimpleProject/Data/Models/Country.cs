
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleProject.Data.Models
{
    [Table("Countries")]
    public class Country
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<City>? Cities { get; set; }

    }
}