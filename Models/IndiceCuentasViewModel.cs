namespace ManejoPresupuesto.Models
{
    public class IndiceCuentasViewModel
    {
        public string TipoCuenta { get; set; }
        //Contiene el listado de las cuentas filtrado por TipoCuenta
        public IEnumerable<Cuenta> Cuentas { get; set; }
        //Realiza la sumatoria de los balances
        public decimal Balance => Cuentas.Sum(x => x.Balance);
    }
}
