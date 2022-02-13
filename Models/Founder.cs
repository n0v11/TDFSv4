using System;
using System.ComponentModel.DataAnnotations;

namespace TDFSv4.Models
{
    public class Founder
    {
        public int Id { get; set; }
        
        [Required]
        public string Fio { get; set; }

        public long Tin { get; set; } //Tax­pay­er Iden­ti­fi­ca­tion Num­ber (инн)\
        public Client Client { get; set; }

        [Display(Name = "Creation Date")]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Update Date")]
        [DataType(DataType.Date)]
        public DateTime UpdateDate { get; set; }
    }
}
