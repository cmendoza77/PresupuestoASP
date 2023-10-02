using ManejoPresupuesto.Models;
using ManejoPresupuesto.servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManejoPresupuesto.Controllers
{
    public class TransaccionesController: Controller
    {
        private readonly IServicioUsuarios servicioUsuarios;
        private readonly IRepositorioCuentas repositorioCuentas;

        public TransaccionesController(IServicioUsuarios servicioUsuarios,
            IRepositorioCuentas repositorioCuentas)
        {
            this.servicioUsuarios = servicioUsuarios;
            this.repositorioCuentas = repositorioCuentas;
        }

        public async Task<IActionResult> Crear()
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            //Pasa los campos de la tabla Transacciones al objeto modelo
            var modelo = new TransaccionCreacionViewModel();
            //Añade al objeto las cuentas del usuario
            modelo.Cuentas = await ObtenerCuentas(usuarioId);
            return View(modelo);
        }

        //Metodo privado que devuelve un listado de cuentas de usuario
        private async Task<IEnumerable<SelectListItem>> ObtenerCuentas(int usuarioId)
        {
            var cuentas = await repositorioCuentas.Buscar(usuarioId);
            return cuentas.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()));
        }
    }
}
