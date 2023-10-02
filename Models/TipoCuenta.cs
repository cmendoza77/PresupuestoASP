using ManejoPresupuesto.Validaciones;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Models
{
    //Validacion por el modelo (IValidaTableObjet)
    public class TipoCuenta //: IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [PrimeraLetraMayuscula]
        //Viene del Json de verificacion tipo cuenta
        [Remote(action: "VerificarExisteTipoCuenta", controller: "TiposCuentas")]
        public string Nombre { get; set; }
        public int  UsuarioId { get; set; }
        public int Orden { get; set; }

        //Validaciones por medio del modelo, solo para referencia
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if(Nombre != null && Nombre.Length > 0)
        //    {
        //        var PrimeraLetra = Nombre[0].ToString();
        //        if(PrimeraLetra != PrimeraLetra.ToUpper())
        //        {
        //            yield return new ValidationResult("La primera letra debe ser mayúscula",
        //                new[] {nameof(Nombre)});
        //        }
        //    }
        //}
    }
}
