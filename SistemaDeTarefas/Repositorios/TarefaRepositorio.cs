using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Enums;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly SistemaTarefasDBContext _dbContext;

        public TarefaRepositorio(SistemaTarefasDBContext sistemaTarefasDBContext)
        {
            _dbContext = sistemaTarefasDBContext;
        }

        public async Task<TarefaModel> Adicionar(TarefaModel tarefa)
        {
            await _dbContext.Tarefas.AddAsync(tarefa);
            await _dbContext.SaveChangesAsync();

            return tarefa;
        }

        public async Task<TarefaModel> AlterarStatus(int id, StatusTarefa status)
        {
            TarefaModel tarefa = await BuscarPorID(id);

            if(tarefa == null)
            {
                throw new Exception($"Tarefa com ID: {id} não foi encontrado.");
            }

            tarefa.Status = status;
            
            _dbContext.Tarefas.Update(tarefa);
            await _dbContext.SaveChangesAsync();

            return tarefa;
        }

        public async Task<bool> Apagar(int id)
        {
            TarefaModel tarefa = await BuscarPorID(id);

            if(tarefa == null)
            {
                throw new Exception($"Tarefa com ID: {id} não foi encontrada.");
            }

            _dbContext.Tarefas.Remove(tarefa);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<TarefaModel> BuscarPorID(int id)
        {
            return await _dbContext.Tarefas.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TarefaModel>> BuscarTarefasPorStatus(StatusTarefa status)
        {
            return await _dbContext.Tarefas.Where(x => x.Status == status).ToListAsync();
        }

        public async Task<List<TarefaModel>> BuscarTodasTarefas()
        {
            return await _dbContext.Tarefas.ToListAsync();
        }

        public async Task<TarefaModel> Editar(TarefaModel tarefa, int id)
        {
            TarefaModel _tarefa = await BuscarPorID(id);

            if (_tarefa == null)
            {
                throw new Exception($"Tarefa com ID: {id} não foi encontrado.");
            }

            _tarefa.Status = tarefa.Status;
            _tarefa.Descricao = tarefa.Descricao;
            _tarefa.Nome = tarefa.Nome;

            _dbContext.Tarefas.Update(_tarefa);
            await _dbContext.SaveChangesAsync();

            return _tarefa;
        }
    }
}
