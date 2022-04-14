using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SAF_3T.Domains;
using SAF_3T.Interfaces;
using SAF_3T.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAF_3T.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CheckListController : ControllerBase   
    {
        private ICheckListRepository _checklistRepository { get; set; }

        public CheckListController()
        {
            _checklistRepository = new CheckListRepository();
        }

        [HttpGet]
        public ActionResult<IEnumerable<CheckList>> ListarChecklists()
        {
            try
            {
            return Ok(_checklistRepository.ListarTodas());
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
                throw;
            }
        }

        [HttpGet("{idCheckList}")]
        public ActionResult<IEnumerable<CheckList>> ListarPorId(int idCheckList)
        {
            try
            {
            return Ok(_checklistRepository.BuscarPorId(idCheckList));
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
                throw;
            }
        }

        [HttpGet("Veiculo/{idVeiculo}")]
        public ActionResult<IEnumerable<CheckList>> ListarMinhas(int idVeiculo)
        {
            try
            {
            return Ok(_checklistRepository.ListarMinhas(idVeiculo));
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
                throw;
            }
        }

        [HttpPost]
        public ActionResult<CheckList> CadastrarCheckList(CheckList novaChecklist)
        {
            try
            {
           _checklistRepository.Cadastrar(novaChecklist);
            return StatusCode(201);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
                throw;
            }
        }

        [HttpDelete("{idChecklist}")]
        public ActionResult<CheckList> DeletarCheckList(int idChecklist)
        {
            try
            {
            _checklistRepository.Deletar(idChecklist);
            return StatusCode(204);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
                throw;
            }
        }
    }
}
