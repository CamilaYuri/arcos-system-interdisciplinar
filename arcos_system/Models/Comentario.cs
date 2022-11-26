using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace arcos_system.Models
{
    [Table(name: "Comentarios")]
    public class Comentario
    {
        public int Id { set; get; }
        public virtual Usuario Usuario { get; set; }
        public string Comentarios { set; get; }
        public virtual ItensDeMenu ItensDeMenu { set; get; }
    }
}
