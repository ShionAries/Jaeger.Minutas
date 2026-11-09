using System;
using SqlSugar;

namespace Jaeger.Minutas.DataAccess.Datbase {
    public static class SqlSugarFactory {
        public static SqlSugarClient Crear(string connectionString) {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException(
                    "La cadena de conexión es obligatoria.", nameof(connectionString));

            var config = new ConnectionConfig {
                ConnectionString = connectionString,
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            };

            var client = new SqlSugarClient(config);
            client.Aop.OnError = error => {
                Console.WriteLine($"Error en la consulta SQL: {error.Sql}");
            };

            return client;
        }
    }
}
