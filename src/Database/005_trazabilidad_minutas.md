# Migración 005: trazabilidad de minutas

1. Realizar un respaldo de la base de datos.
2. Confirmar que existen `MNTA`, `MNTAC` y `MNTTA` con las claves indicadas en el script maestro del proyecto.
3. Ejecutar `005_trazabilidad_minutas.sql` una sola vez.
4. Restaurar los paquetes NuGet del proyecto y compilar la solución en Visual Studio.
5. Desplegar la aplicación actualizada.

La migración agrega:

- `MNTRZ`: relación entre minuta origen y minuta de seguimiento.
- `MNTRZT`: correspondencia entre tareas/acuerdos de origen y sus copias. Guarda descripciones históricas para que la trazabilidad siga siendo legible aunque una tarea de seguimiento se elimine posteriormente.

El script no modifica ni elimina registros anteriores y no cambia las minutas aprobadas.

El flujo actual solo permite crear seguimiento desde una minuta con estado `Aprobada`. Las tareas concluidas no aparecen en la selección. Las tareas seleccionadas se copian a la minuta nueva con estado `Pendiente`; las tareas originales, sus acuerdos y sus evidencias se conservan intactos. Las personas asistentes se copian como convocadas. La creación de minuta, folio, relación, tareas copiadas y auditoría se realiza dentro de una transacción.

## Estados de tarea

Se consideran concluidas las tareas cuyo estado sea `Completada`, `Completado`, `Concluida`, `Concluido`, `Finalizada`, `Finalizado`, `Cerrada`, `Cerrado`, `Cancelada`, `Cancelado`, `Resuelta`, `Resuelto`, `Terminada` o `Terminado` (sin distinguir mayúsculas/minúsculas). Los demás estados se ofrecen para seguimiento.
