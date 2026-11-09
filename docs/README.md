# Fase 2: captura de asistentes, agenda, acuerdos y tareas

## Archivos añadidos
- `Database/002_minuta_detalle.sql`: tablas relacionales para asistentes, temas, acuerdos y tareas.
- `Domain/Entities/MinutaDetalle.cs`: modelos de detalle.
- `Domain/Repositories/IMinutaDetalleRepository.cs`: contrato de persistencia.
- `Application/Services/MinutaDetalleService.cs`: validación de detalle antes de guardar.
- `DataAccess/Entities/MinutaDetalleEntities.cs`: mapeos SQLSugar.
- `DataAccess/Repositories/MinutaDetalleRepository.cs`: lectura y guardado transaccional.
- `UserInterface/MinutaDetalleForm.cs`: formulario WinForms con pestañas y grillas editables.

## Instalación
1. Respaldar la base de datos y ejecutar `Database/002_minuta_detalle.sql` después de haber instalado el esquema base `MINUTAS`.
2. Abrir la solución en Visual Studio y restaurar los paquetes NuGet.
3. Compilar y revisar las referencias de SQLSugar 5.0.3.3 y MySql.Data 8.0.32.
4. En el composition root, crear `MinutasComposition` con el `SqlSugarClient` configurado y abrir el formulario así:

```csharp
var form = new Jaeger.Minutas.UserInterface.MinutaDetalleForm(
    minutaId,
    usuarioAutenticadoId,
    composition.MinutaDetalleService,
    tituloMinuta);
form.ShowDialog(this);
```

Los identificadores de usuario deben provenir de la sesión corporativa autenticada. La selección de asistentes y responsables ahora consume el catálogo corporativo `_user`; se muestran únicamente registros activos (`_user_a = 1`). La sesión del usuario que registra la minuta sigue siendo responsabilidad del sistema anfitrión.

## Alcance y notas
- La persistencia del detalle reemplaza el conjunto actual de asistentes, temas, acuerdos y tareas de esa minuta dentro de una transacción. Por tanto, solo debe usarse para minutas en borrador/en captura; no usar sobre minutas aprobadas ni con historial legal hasta implementar versionado/auditoría.
- La relación de tarea con acuerdo y de acuerdo con tema queda sin asignar automáticamente en esta entrega. Para conservar la integridad de IDs generados, se implementará en una siguiente revisión un guardado por fases que devuelva los IDs de cada registro.
- Se incorporó control optimista de concurrencia en el guardado de detalles mediante `MINUTAS.VERSION_FILA`. El guardado actualiza la versión dentro de la misma transacción antes de reemplazar los detalles; si la versión cambió, se cancela toda la operación y se informa al usuario.
- Este control protege los guardados de detalles que utilicen esta implementación. Las futuras operaciones de edición del encabezado y las API deberán aplicar el mismo patrón de versión.
- El catálogo corporativo se consulta mediante `UsuarioCorporativoRepository`. La consulta solo recupera `_user_id`, `_user_nom`, `_user_mail` y `_user_a`; nunca carga contraseñas, tokens ni API keys. Se validan contra el catálogo los usuarios de sesión, asistentes y responsables antes de guardar. No se agregó aún una FK física a `_user`, para evitar fallos de migración si existen datos históricos inválidos o diferencias de tipos entre columnas.
- No se pudo compilar ni ejecutar contra una instancia MariaDB desde este entorno. Validar los métodos async de transacción con el proveedor exacto instalado antes de producción.


## Arranque de la interfaz
Se agregó navegación básica en `MainForm`: crear minuta y abrir detalle por ID. `Program.cs` ya inicia Windows Forms en lugar de ejecutar pruebas como si fueran el punto de entrada de producción. El ZIP recibido no incluye el inicio de sesión del sistema corporativo; por eso `SesionActual.UsuarioId` debe asignarse desde la sesión autenticada antes de habilitar operaciones. No se debe asignar un ID recibido libremente del usuario ni usar un ID fijo en producción.

## Seguridad de conexión
Revisar `App.config` antes de distribuir: la configuración original contenía credenciales de base de datos incrustadas. Usar un usuario MariaDB dedicado con permisos mínimos, cambiar las credenciales expuestas y proteger la configuración según las políticas del sistema anfitrión.


## Fase siguiente: integridad de relaciones y autenticación

Queda pendiente conservar los IDs de temas y acuerdos al guardar para enlazar `TEMA_ID` y `ACUERDO_ID` sin perder integridad referencial. También debe conectarse `SesionActual.UsuarioId` al login real del sistema anfitrión; esta solución no debe permitir que el usuario elija libremente su propio ID de sesión.

# Fase 3: catálogo corporativo de usuarios

## Integración

Se implementó lectura del catálogo existente `_user` mediante `UsuarioCorporativoRepository`. La consulta se limita a usuarios activos (`_user_a = 1`) y a los campos necesarios para mostrar identidad: `_user_id`, `_user_nom` y `_user_mail`. No se consulta ni se mapea `_user_psw`, `_user_tkn` o `_user_apikey`.

- Asistentes: selector de usuario activo; nombre y correo se copian al registro de asistencia como instantánea de la minuta.
- Temas: selector opcional de responsable.
- Tareas: selector de responsable.
- Guardado: valida que el usuario de sesión, asistentes y responsables existan y estén activos.
- Arquitectura: contrato de repositorio en `Domain`, servicio en `Application`, mapeo y repositorio en `DataAccess`.

## Instalación y configuración

1. Conservar la tabla `_user` proporcionada por el sistema corporativo. No ejecutar scripts que la creen o alteren.
2. Configurar `ConnectionString` en `App.config` con la cadena del entorno; el paquete no contiene credenciales. Usar un usuario MariaDB dedicado y con permisos mínimos.
3. Restaurar paquetes NuGet y abrir `Jaeger.Minutas.sln` en Visual Studio.
4. Confirmar que el usuario de base de datos de la aplicación tenga permisos `SELECT` sobre `_user` y los permisos requeridos por las tablas de minutas. No necesita permisos para leer las columnas de contraseña/token porque la aplicación no las consulta.
5. El sistema anfitrión debe asignar `SesionActual.UsuarioId` después de autenticar al usuario. No asignar un ID fijo ni permitir que el operador lo escriba como sustituto del login.
6. Probar con usuarios activos, inactivos, asistentes duplicados y responsables inexistentes.

## Limitaciones conocidas

- No se implementó autenticación. La tabla `_user` incluye campos de credenciales, pero no se debe inferir el protocolo de login únicamente a partir de comentarios sobre MD5/Base64. La autenticación existente debe proporcionar el ID de sesión autenticado.
- La relación de tareas con acuerdos y de acuerdos con temas todavía requiere un guardado por fases que conserve los IDs generados.
- La validación del catálogo se hace antes de la transacción que guarda los detalles; si el sistema permite desactivar usuarios concurrentemente, debe reforzarse con una política de transacción o restricciones de integridad tras auditar datos históricos.
- El proyecto no pudo compilarse ni ejecutarse contra MariaDB en este entorno; validar en Visual Studio y con una base de pruebas antes de producción.

# Fase 4: mejora del detalle de minuta

## Cambios

- El formulario `MinutaDetalleForm` utiliza un diseño de tres filas: resumen general fijo, pestañas de captura y pie fijo. El botón `Guardar detalle` permanece en el pie y se recoloca al redimensionar la ventana.
- El resumen muestra folio, título, tipo, fecha, horario, lugar/plataforma, estado y objetivo en campos de solo lectura.
- Se agregaron selectores para relacionar acuerdos con temas y tareas con acuerdos, incluyendo elementos recién capturados.
- El repositorio realiza sincronización transaccional de los detalles: conserva los identificadores de las filas existentes, genera IDs para filas nuevas y elimina las filas retiradas en orden compatible con las claves foráneas.
- Se mantiene el control de concurrencia optimista mediante `MINUTAS.VERSION_FILA`.
- Se conserva el usuario que registró originalmente un acuerdo o creó una tarea durante las actualizaciones posteriores.

## Instalación y validación

No se agregan tablas nuevas en esta fase. Se requiere tener instaladas las tablas de `MINUTAS` y el script `Database/002_minuta_detalle.sql` de la fase anterior.

El código se revisó estáticamente y el ZIP se validará por integridad. No se pudo compilar ni probar contra MariaDB en este entorno; compilar en Visual Studio y realizar una prueba de guardado/recarga antes de desplegar.

# Fase 5: edición, flujo de estados y protección de minutas

## Funcionalidad

- Se incorporó `AdministrarMinutaService` para actualizar datos generales, cambiar estados y realizar eliminación lógica.
- `MinutaEdicionForm` permite editar folio, título, tipo, fecha, horario, lugar, plataforma y objetivo.
- El formulario de detalle muestra acciones de edición y cambio de estado; la información se vuelve a cargar después de cada operación.
- Flujo permitido: `Borrador -> Preparada -> EnReunion -> PendienteAprobacion -> Aprobada`. Se permite cancelar desde estados no terminales y devolver `PendienteAprobacion` a `EnReunion` para correcciones.
- `Aprobada` y `Cancelada` son estados terminales. Se bloquean la edición de datos generales, el detalle y la eliminación.
- La eliminación es lógica (`ELIMINADO=1`); no borra físicamente la minuta ni sus detalles.
- Actualizaciones generales, cambios de estado y eliminación usan `VERSION_FILA` para concurrencia optimista.
- Las operaciones SQL incluyen filtros de estado terminal para reforzar las reglas también en persistencia.

## Consideraciones

- No se requiere una tabla nueva si `MINUTAS` ya contiene `ESTADO`, `ELIMINADO`, `VERSION_FILA` y `FECHA_ACTUALIZACION`.
- El folio se valida contra otras minutas no eliminadas.
- Las reglas de transición son de negocio y deben ajustarse si la organización requiere otro flujo de aprobación.
- Esta versión no incorpora todavía auditoría de cambios ni registro de quién aprobó/canceló; se recomienda como siguiente fase.

## Pruebas recomendadas

1. Editar y volver a abrir una minuta en borrador.
2. Avanzar por los estados permitidos y comprobar que no se permitan saltos de estado.
3. Aprobar una minuta y confirmar que datos generales, detalle y eliminación quedan bloqueados.
4. Cancelar una minuta y confirmar el mismo bloqueo.
5. Intentar guardar desde dos ventanas y verificar el mensaje de conflicto de versión.
6. Eliminar lógicamente una minuta no aprobada y comprobar que no se puede volver a abrir mediante `ObtenerPorIdAsync`.

No se pudo compilar ni ejecutar la aplicación contra MariaDB en este entorno; validar en Visual Studio y en una base de pruebas antes de desplegar.

# Fase 6 - Bitácora de auditoría

## Instalación

1. Conserva una copia de seguridad de la base de datos.
2. Ejecuta `Database/003_minuta_auditoria.sql` después de instalar las tablas base de `MINUTAS`.
3. Restaura NuGet y compila la solución en Visual Studio.

## Eventos registrados

- `CREAR`: datos iniciales de la minuta.
- `ACTUALIZAR_DATOS`: snapshot anterior y posterior de datos generales.
- `CAMBIAR_ESTADO`: estado y snapshot anterior/posterior.
- `ELIMINAR_LOGICO`: snapshot anterior y posterior a la eliminación lógica.
- `ACTUALIZAR_DETALLE`: conteos e identificadores de asistentes, temas, acuerdos y tareas antes y después.

Las operaciones principales escriben el evento en la misma transacción que el cambio de datos; si no se puede registrar la auditoría, se revierte la operación. La tabla de auditoría no tiene relación foránea con `_user` para evitar acoplar el registro histórico a cambios del catálogo corporativo.

La auditoría de detalle conserva los valores de asistentes, temas, acuerdos y tareas antes y después de guardar. La siguiente mejora puede agregar una pantalla de consulta con filtros por minuta, usuario, acción y fecha.

## Consideraciones

La bitácora está pensada como historial de aplicación. Para auditoría regulatoria o evidencia inmutable se requiere además limitar permisos de actualización/eliminación sobre `MINUTA_AUDITORIA`, respaldos y controles de acceso a nivel de base de datos.

## Trazabilidad y seguimiento (migración 005)

La barra de herramientas de Minutas incluye **Crear seguimiento** y **Trazabilidad**.

- **Crear seguimiento** está disponible cuando la minuta seleccionada tiene estado `Aprobada`. Permite editar el título, objetivo, fecha, hora y ubicación de la nueva reunión.
- Se cargan las tareas pendientes o no concluidas de la minuta aprobada para seleccionar cuáles se trasladarán. La minuta original y sus tareas no se modifican.
- Los acuerdos seleccionados se copian a la nueva minuta y las tareas se crean con estado `Pendiente`, conservando responsable y fecha límite. Los asistentes de la minuta origen se copian como `Convocado`.
- El guardado de la nueva minuta, el folio, la relación origen-destino, las tareas copiadas y los registros de auditoría se ejecutan en una transacción.
- **Trazabilidad** presenta antecedentes y continuaciones relacionadas, y permite revisar la correspondencia entre tareas/acuerdos originales y sus copias de seguimiento.

Antes de ejecutar la aplicación, aplique `Database/005_trazabilidad_minutas.sql` en la base de datos existente. Consulte `Database/005_trazabilidad_minutas.md` para el orden y las condiciones de la migración.
