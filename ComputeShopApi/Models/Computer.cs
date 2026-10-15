using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ComputeShopApi.Models
{
    public class Computer
    {
        [Key]
        [MaxLength(36)]
       
        public Guid Id { get; set; }
        public string Brand { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string Type { get; set; }
        [Column(TypeName = "float")]
        public double Display { get; set; }
        [Column(TypeName = "int(7)")]
        public int Memory { get; set; }
        public DateTime CreatedTime { get; set; }

        public Guid OsId { get; set; }

        [JsonIgnore]
        public Osystem Os { get; set; }
    }
}
