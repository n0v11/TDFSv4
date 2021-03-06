using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TDFSv4.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int TypeId { get; set; }
        [ForeignKey(nameof(TypeId))]
        public Type Type { get; set; } // Тип (ЮЛ / ИП)

        public virtual List<Founder> Founders { get; set; }

        public long Tin { get; set; } //Tax­pay­er Iden­ti­fi­ca­tion Num­ber (инн)

        [Display(Name = "Creation Date")]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Update Date")]
        [DataType(DataType.Date)]
        public DateTime UpdateDate { get; set; }
    }
}
