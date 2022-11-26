using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace arcos_system.Models
{

    [Table(name: "ItensDeMenu")]
    public class ItensDeMenu
    {
        public int Id { set; get; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Video é obrigatório")]
        public string Video { set; get; }

        public virtual List<Comentario> Comentarios { set; get; }
    }
}
