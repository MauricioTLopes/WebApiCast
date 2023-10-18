using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApiCastMVCRazor.Models
{
    public class ContaModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
    }
}
