using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SistemaTarefasDBContext _dbContext;
        public UsuarioRepositorio(SistemaTarefasDBContext sistemaTarefasDBContex)
        {
            _dbContext = sistemaTarefasDBContex;
        }
        public async Task<UsuarioModel> BuscarPorId(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }
        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario;
        }

        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioPorID = await BuscarPorId(id);

            if(usuarioPorID == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado.");
            }

            usuarioPorID.Nome = usuario.Nome;
            usuarioPorID.Email = usuario.Email;

            _dbContext.Usuarios.Update(usuarioPorID);
            await _dbContext.SaveChangesAsync();

            return usuarioPorID;
        }
        

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioPorID = await BuscarPorId(id);

            if (usuarioPorID == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado.");
            }

            _dbContext.Usuarios.Remove(usuarioPorID);
            await _dbContext.SaveChangesAsync();
            return true;
        }


        
    }
}
