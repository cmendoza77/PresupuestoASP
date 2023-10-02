using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Models
{
    //Clase que representa las categorias
    public class Categoria
    {
        //Cmpos de la tabla categorias
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50, ErrorMessage = "El campo {0} no puede ser mayor a {1} caracteres")]
        public string Nombre { get; set; }
        [Display(Name = "Tipo Operación")]
        public TipoOperacion TipoOperacionId  { get; set; }
        public int UsusarioId { get; set; }
    }
}
