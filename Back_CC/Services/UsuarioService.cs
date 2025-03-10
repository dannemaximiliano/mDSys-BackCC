using Microsoft.EntityFrameworkCore;
using Back_CC.DTOs;
using Back_CC.Models;
using System;
using Back_CC.DTOs.UsuarioDTO;

namespace Back_CC.Services

{
    public class UsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObtenerUsuarioPorUsername(string username)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == username);
        }

        public bool VerificarPassword(string passwordIngresada, string passwordGuardada)
        {
            return passwordIngresada == passwordGuardada; // Agregar hashing en el futuro
        }


        public async Task<(bool, string)> AltaUsuario(UsuarioAltaDto dto)
        {
            // Verificar si el Username ya existe
            var usuarioExistente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (usuarioExistente != null)
                return (false, "El username ya está en uso.");

            // Verificar si el idPersona existe
            var personaExistente = await _context.Personas
                .FirstOrDefaultAsync(p => p.Id == dto.IdPersona);

            if (personaExistente == null)
                return (false, "La persona con el id especificado no existe.");

            // Verificar si el idTipoUsuario existe en la tabla TiposUsuarios
            var tipoUsuarioExistente = await _context.TipoUsuarios
                .FirstOrDefaultAsync(tu => tu.Id == dto.IdTipoUsuario);

            if (tipoUsuarioExistente == null)
                return (false, "El tipo de usuario especificado no existe.");

            // Crear el nuevo usuario asociado a la persona existente
            var nuevoUsuario = new Usuario
            {
                Username = dto.Username,
                Password = dto.Password,
                IdTipoUsuario = dto.IdTipoUsuario,
                IdPersona = dto.IdPersona // Asociamos el idPersona a este nuevo usuario
            };

            // Agregar el nuevo usuario a la base de datos
            await _context.Usuarios.AddAsync(nuevoUsuario);
            await _context.SaveChangesAsync();

            return (true, "Usuario creado correctamente.");
        }

        public async Task<(bool, string)> InactivarUsuario(int id, bool activo)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return (false, "Usuario no encontrada.");

            usuario.Activo = activo;
            usuario.FechaBaja = activo ? null : DateTime.UtcNow;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return (true, "Estado del usuario actualizado.");
        }

        public async Task<(bool, string)> ModificarUsuario(int idUsuario, UsuarioModificarDto dto)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuario);
            if (usuario == null)
                return (false, "Usuario no encontrada.");


            // Actualizar los datos de la persona
            usuario.Username = dto.Username;
            usuario.Password = dto.Password;
            usuario.IdTipoUsuario = dto.IdTipoUsuario;


            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return (true, "Usuario actualizado correctamente.");
        }



    }





}
