using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace arcos_system.Models
{
    [Table(name:"Usuarios")]
    public class Usuario
    {
        public int Id { set; get; }

        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(20)]
        public string NomeLogin { set; get; }

        [MaxLength(10)]
        public string Senha { set; get; }

        public string Tipo { set; get; }

        public virtual ICollection<Comentario> Comentarios { set; get; }
    }
}
