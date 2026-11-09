using System;
using Jaeger.Minutas.Aplication.Services;
using Jaeger.Minutas.Application.Contracts;
using Jaeger.Minutas.DataAccess.Repositories;
using Jaeger.Minutas.Domain.Repositories;
using SqlSugar;

namespace Jaeger.Minutas.Aplication.DependencyInjection {
    public sealed class MinutasComposition {
        public CrearMinutaService CrearMinutaService { get; private set; }
        public ObtenerMinutaService ObtenerMinutaService { get; private set; }
        public AdministrarMinutaService AdministrarMinutaService { get; private set; }
        public MinutaDetalleService MinutaDetalleService { get; private set; }
        public UsuarioCorporativoService UsuarioCorporativoService { get; private set; }
        public MinutaAuditoriaService MinutaAuditoriaService { get; private set; }
        public IMinutaService MinutaService { get; private set; }
        public MinutaTrazabilidadService MinutaTrazabilidadService { get; private set; }

        public MinutasComposition(SqlSugarClient sqlSugarClient) {
            if (sqlSugarClient == null)
                throw new ArgumentNullException(nameof(sqlSugarClient));
            IMinutaRepository minutaRepository = new MinutaRepository(sqlSugarClient);
            IMinutaDetalleRepository detalleRepository = new MinutaDetalleRepository(sqlSugarClient);
            IUsuarioCorporativoRepository usuarioRepository = new UsuarioCorporativoRepository(sqlSugarClient);
            IMinutaTrazabilidadRepository trazabilidadRepository = new MinutaTrazabilidadRepository(sqlSugarClient);
            IMinutaAuditoriaRepository auditoriaRepository = new MinutaAuditoriaRepository(sqlSugarClient);
            CrearMinutaService = new CrearMinutaService(minutaRepository);
            ObtenerMinutaService = new ObtenerMinutaService(minutaRepository);
            AdministrarMinutaService = new AdministrarMinutaService(minutaRepository);
            MinutaDetalleService = new MinutaDetalleService(detalleRepository);
            UsuarioCorporativoService = new UsuarioCorporativoService(usuarioRepository);
            MinutaAuditoriaService = new MinutaAuditoriaService(auditoriaRepository);
            this.MinutaService = new MinutaService(minutaRepository);
            MinutaTrazabilidadService = new MinutaTrazabilidadService(trazabilidadRepository);
        }
    }
}
