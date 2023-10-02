using AutoMapper;
using ManejoPresupuesto.Models;

namespace ManejoPresupuesto.servicios
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Cuenta, CuentaCreacionViewModel>();
        }
    }
}
