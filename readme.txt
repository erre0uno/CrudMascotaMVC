<h4><i class="fa-regular fa-folder-open"></i> &nbsp;Historias</h4>

<h4><i class="fa-solid fa-paw"></i> &nbsp;Mascotas</h4>

<h4><i class="fa-solid fa-user-doctor"></i> &nbsp;Veterinario</h4>

<h4><i class="fa-solid fa-notes-medical"></i> &nbsp;Visita</h4>



Scaffold-DbContext "server=(localdb)\MSSQLLocalDB; database=MVCMascotas; integrated security=true;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models


 optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB; database=MVCMascotas; integrated security=true;");


 "ConnectionStrings": {
    "conexion": "Server=(localdb)\\mssqllocaldb;Database=MVCMascotas;Trusted_Connection=True;" }



builder.Services.AddDbContext<MVCMascotasContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("conexion")));




SELECT [m].[MascotaID], [m].[Color], [m].[DuenoID], [m].[Especie], [m].[MedicoID], [m].[Nombre], [m].[Raza], [d].[DuenoID], [d].[Apellidos], [d].[Correo], [d].[Direccion], [d].[Nombres], [m0].[MedicoID], [m0].[Apellidos], [m0].[Direccion], [m0].[Nombres], [m0].[Tarjeta]
FROM [Mascotas] AS [m]
LEFT JOIN [Duenos] AS [d] ON [m].[DuenoID] = [d].[DuenoID]
INNER JOIN [Medicos] AS [m0] ON [m].[MedicoID] = [m0].[MedicoID]

corta:
SELECT [m].[MascotaID], [m].[Nombre], [m].[Especie],[m].[DuenoID], 
[d].[DuenoID],[d].[Nombres], [d].[Apellidos],  [m].[MedicoID], [m0].[MedicoID],[m0].[Nombres], [m0].[Apellidos]
FROM [Mascotas] AS [m]
LEFT JOIN [Duenos] AS [d] ON [m].[DuenoID] = [d].[DuenoID]
INNER JOIN [Medicos] AS [m0] ON [m].[MedicoID] = [m0].[MedicoID]


Medicamentos en historia ??
fechas como datetime antes de generar la bd 

add telefono En BD
model validations add ?
moldelisvalid mascota histoia


validacion modelo:
            <div asp-validation-summary="ModelOnly" class="text-danger"></div>

        public async Task<IActionResult> Create([Bind("HistoriaId,FechaCreacion,Diagnostico,Medicamentos,MascotaId")] Historia historia)
        {

            var valido =_context.Historias.FirstOrDefault(h => h.MascotaId == historia.MascotaId );
            if (valido == null)
            {
                _context.Add(historia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("","mascota ya tiene historia");
            ViewData["MascotaId"] = new SelectList(_context.Mascotas, "MascotaId", "MascotaId", historia.MascotaId);
            return View(historia);
        }