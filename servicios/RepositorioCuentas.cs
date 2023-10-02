using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

/*Esta clase contiene un repositorio de las cuentas de un usuario y se realiza el proceso del CRUD*/

namespace ManejoPresupuesto.servicios
{
    //Se encuentra contenido el repositorio del CRUD de la tabla Cuentas
    public interface IRepositorioCuentas
    {
        Task Actualizar(CuentaCreacionViewModel cuenta);
        Task Borrar(int id);
        Task<IEnumerable<Cuenta>> Buscar(int usuarioId);
        Task Crear(Cuenta cuenta);
        Task<Cuenta> ObtenerPorId(int id, int usuarioId);
    }

    //Clase que contiene el repositorio de Cuentas
    public class RepositorioCuentas:IRepositorioCuentas
    {
        private readonly string connectionString;

        //Contiene la cadena de conexion a la base de datos
        public RepositorioCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Función para crear nueva cuenta
        public async Task Crear(Cuenta cuenta)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(
                @"INSERT INTO Cuentas (Nombre, TipoCuentaId, Descripcion, Balance)
                VALUES (@Nombre, @TipoCuentaId, @Descripcion, @Balance);

                SELECT SCOPE_IDENTITY();", cuenta);

            cuenta.Id = id;
        }

        //Busca las cuentas del usuario, el IEnumerable devuelve un listado de cuentas
        public async Task<IEnumerable<Cuenta>> Buscar(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Cuenta>(@"SELECT Cuentas.Id, Cuentas.Nombre, Balance, tc.Nombre AS TipoCuenta
                                                        FROM Cuentas
                                                        INNER JOIN TiposCuentas tc
                                                        ON tc.Id = Cuentas.TipoCuentaId
                                                        WHERE tc.UsuarioId = @UsuarioId
                                                        ORDER BY tc.Orden", new { usuarioId });
        }

        //Busca una cuenta por su Id y se asigna al objeto cuenta
        public async Task<Cuenta> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Cuenta>(
                @"SELECT Cuentas.Id, Cuentas.Nombre, Balance, Descripcion, tc.Id
                FROM Cuentas
                INNER JOIN TiposCuentas tc
                ON tc.Id = Cuentas.TipoCuentaId
                WHERE tc.UsuarioId = @UsuarioId AND Cuentas.Id = @Id", new {id, usuarioId});
        }

        //Función para actualizar información de una cuenta, teniendo como parámetro los datos de la cuenta
        public async Task Actualizar(CuentaCreacionViewModel cuenta)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Cuentas
                                    SET Nombre = @Nombre, Balance = @Balance, Descripcion = @Descripcion,
                                    TipoCuentaId = @TipoCuentaId
                                    WHERE Id = @Id;", cuenta);
        }

        //Función para eliminar una cuenta de la tabla, teniendo como parámetro el id de la cuenta a eliminar
        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE Cuentas WHERE Id = @Id", new {id});
        }
    }
}
