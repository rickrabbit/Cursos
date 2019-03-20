using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Domain
{
    public class AlunoDTO
    {
        public int id { get; set; }
        [Required(ErrorMessage = "O nome é de preenchimento obrigatório!")]
        [StringLength(50, ErrorMessage = "Nome deve ter no mínimo 2 caracteres e no máximo 50!", MinimumLength = 2)]
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string telefone { get; set; }
        [Range(1,9999, ErrorMessage = "RA deve ser um número entre 1 e 9999!")]
        public int ra { get; set; }

    }
}