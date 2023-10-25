using SistemaDeTarefas.Enums;
using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Repositorios.Interfaces
{
    public interface ITarefaRepositorio
    {
        Task<TarefaModel> BuscarPorID(int id);
        Task<List<TarefaModel>> BuscarTodasTarefas();
        Task<List<TarefaModel>> BuscarTarefasPorStatus(StatusTarefa status);
        Task<TarefaModel> Adicionar(TarefaModel tarefa);
        Task<TarefaModel> Editar(TarefaModel tarefa, int id);
        Task<TarefaModel> AlterarStatus(int id, StatusTarefa status);
        Task<bool> Apagar(int id);
    }
}
