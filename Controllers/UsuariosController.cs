﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SAF_3T.Domains;
using SAF_3T.Interfaces;
using SAF_3T.Repositories;
using SAF_3T.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAF_3T.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]

    [ApiController]
    public class UsuariosController : Controller
    {
        private IUsuarioRepository _usuarioRepository;
        public UsuariosController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_usuarioRepository.ListarTodos());
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
                throw;
            }
        }

        [HttpPost]
        public IActionResult CadastrarUsuario([FromForm] Usuario novoUsuario, IFormFile arquivo)
        {
            try
            {
                string uploadResultado;
                string[] extensoesPermitidas = { "jpg", "png", "jpeg", "gif" };
                if (arquivo != null)
                {
                    uploadResultado = Upload.UploadFile(arquivo, extensoesPermitidas);
                }
                else
                {
                    uploadResultado = null;
                }


                //if (uploadResultado == "")
                //{
                //    return BadRequest("Arquivo não encontrado");
                //}

                if (uploadResultado == "Extensão não permitida")
                {
                    return BadRequest("Extensão de arquivo não permitida");
                }

                novoUsuario.ImagemUsuario = uploadResultado;

                _usuarioRepository.Cadastrar(novoUsuario);
                return StatusCode(201, novoUsuario);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
                throw;
            }
        }

        [HttpPatch("AlterarSenha/{idRecebido}")]
        public IActionResult AlterarSenha(int idRecebido, Usuario usuarioLogadodo)
        {
            try
            {
                _usuarioRepository.AlterarSenha(idRecebido, usuarioLogadodo);
                return Ok("Senha alterada");
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
                throw;
            }
        }

        [HttpDelete]
        public IActionResult Deletar(int id)
        {
            try
            {
                _usuarioRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
                throw;
            }
        }

        [HttpGet("BuscarPorId/{idUsuario}")]
        public IActionResult BuscarPorId(int idUsuario)
        {
            try
            {
                return Ok(_usuarioRepository.BuscarPorId(idUsuario));
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
                throw;
            }
        }

        [HttpGet("/numero")]
        public IActionResult BuscarPorNumero(string numero)
        {
            try
            {
                return Ok(_usuarioRepository.BuscarPorNumero(numero));
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
                throw;
            }
        }

        [HttpGet("/cpf/{cpf}")]
        public IActionResult BuscarPorCpf(string cpf)
        {
            try
            {
                return Ok(_usuarioRepository.BuscarPorCPF(cpf));
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
                throw;
            }
        }

    }
}
