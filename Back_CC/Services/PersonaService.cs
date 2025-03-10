using Microsoft.EntityFrameworkCore;
using Back_CC.Models;
using System;
using Back_CC.DTOs.PersonaDTO;

namespace Back_CC.Services

{
    public class PersonaService
    {
        private readonly AppDbContext _context;

        public PersonaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool, string)> ValidarDatosPersona(PersonaDto personaDto)
        {
            if (string.IsNullOrWhiteSpace(personaDto.Nombre))
                return (false, "El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(personaDto.Dni) || personaDto.Dni.Length < 6)
                return (false, "El DNI debe tener 7 caracteres minimo.");

            if (!personaDto.Email.Contains("@") || !personaDto.Email.Contains("."))
                return (false, "El email no es válido.");

            var dniExistente = await _context.Personas.AnyAsync(p => p.Dni == personaDto.Dni);
            if (dniExistente)
                return (false, "El DNI ya está registrado.");

            return (true, "Validación exitosa.");
        }

        public async Task<Persona> AltaPersona(PersonaDto personaDto)
        {
            var persona = new Persona
            {
                Nombre = personaDto.Nombre,
                Dni = personaDto.Dni,
                Email = personaDto.Email,
                Telefono = personaDto.Telefono,
                FechaNacimiento = personaDto.FechaNacimiento,
                //FechaAlta = DateTime.UtcNow,
                Activo = true
            };

            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            return persona;
        }

        public async Task<(bool, string)> InactivarPersona(int id, bool activo)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
                return (false, "Persona no encontrada.");

            persona.Activo = activo;
            persona.FechaBaja = activo ? null : DateTime.UtcNow;

            _context.Personas.Update(persona);
            await _context.SaveChangesAsync();

            return (true, "Estado de la persona actualizado.");
        }

        public async Task<(bool, string)> ModificarPersona(int id, PersonaModificarDto dto, string username)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
                return (false, "Persona no encontrada.");

            // Verificar el rol del usuario
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == username);
            if (usuario == null)
                return (false, "Usuario no encontrado.");

            if (usuario.IdTipoUsuario != 1) // Verificamos si tiene rol idTipoUsuario == 1
                return (false, "El usuario no tiene permisos suficientes.");

            // Actualizar los datos de la persona
            persona.Nombre = dto.Nombre;
            persona.Dni = dto.Dni;
            persona.Email = dto.Email;
            persona.Telefono = dto.Telefono;
            persona.FechaNacimiento = dto.FechaNacimiento;

            _context.Personas.Update(persona);
            await _context.SaveChangesAsync();

            return (true, "Persona actualizada correctamente.");
        }


        public async Task<List<PersonaListarDto>> ListarPersonasActivas()
        {
            return await _context.Personas
                .Where(p => p.Activo) // Solo trae personas activas
                .Select(p => new PersonaListarDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Dni = p.Dni,
                    Email = p.Email,
                    Telefono = p.Telefono
                })
                .ToListAsync();
        }

        public async Task<List<PersonaListarDto>> ListarPersonas()
        {
            return await _context.Personas
                .Select(p => new PersonaListarDto
                {
                Id = p.Id,
                Nombre = p.Nombre,
                Dni = p.Dni,
                Email = p.Email,
                Telefono = p.Telefono
                })
                .ToListAsync();
        }

    }





}
