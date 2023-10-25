using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Enums;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepositorio _tarefaRepositorio;

        public TarefaController(ITarefaRepositorio tarefaRepositorio)
        {
            _tarefaRepositorio = tarefaRepositorio;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TarefaModel>> BuscarPorID(int id)
        {
            TarefaModel tarefa = await _tarefaRepositorio.BuscarPorID(id);
            return Ok(tarefa);
        }

        [HttpGet("{status}")]
        public async Task<ActionResult<List<TarefaModel>>> BuscarTarefasPorStatus(StatusTarefa status)
        {
           List<TarefaModel> tarefa = await _tarefaRepositorio.BuscarTarefasPorStatus(status);
            return Ok(tarefa);
        }

        [HttpGet]
        public async Task<ActionResult<List<TarefaModel>>> BuscarTodasTarefas()
        {
            List<TarefaModel> tarefas = await _tarefaRepositorio.BuscarTodasTarefas();
            return Ok(tarefas);
        }

        [HttpPost]
        public async Task<ActionResult<TarefaModel>> Adicionar([FromBody]TarefaModel tarefa)
        {
            TarefaModel _tarefa = await _tarefaRepositorio.Adicionar(tarefa);
            return Ok(_tarefa);
        }

        [HttpPut]
        public async Task<ActionResult<TarefaModel>> AlterarStatus([FromBody] int id, StatusTarefa status)
        {
            TarefaModel tarefa = await _tarefaRepositorio.AlterarStatus(id, status);
            return Ok(tarefa);
        }

        [HttpPut]
        public async Task<ActionResult<TarefaModel>> Editar([FromBody]TarefaModel tarefa, int id)
        {
            tarefa.Id = id;
            TarefaModel _tarefa = await _tarefaRepositorio.Editar(tarefa, id);
            return Ok(tarefa);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TarefaModel>> Apagar(int id)
        {
            bool retorno = await _tarefaRepositorio.Apagar(id);
            return Ok(retorno);
        }


        
    }
}
