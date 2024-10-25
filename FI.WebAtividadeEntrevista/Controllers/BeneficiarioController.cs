using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;
using System.Reflection;
using FI.WebAtividadeEntrevista.Models;
using System.Diagnostics.CodeAnalysis;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObterBeneficiarios(long id)
        {
            BoBeneficiario bo = new BoBeneficiario();

            List<Beneficiario> beneficiarios = bo.Listar(id);
            List<BeneficiarioModel> beneficiarioModels = new List<BeneficiarioModel>();

            foreach (var beneficiario in beneficiarios)
            {
                beneficiarioModels.Add(new BeneficiarioModel
                {
                    Id = beneficiario.Id,
                    CPF = beneficiario.CPF,
                    Nome = beneficiario.Nome,
                    IdCliente = beneficiario.IdCliente
                });
            }

            return Json(beneficiarioModels, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult VerificarClienteExistente(long idCliente)
        {
            BoCliente bo = new BoCliente();
            var clienteExiste = bo.Consultar(idCliente) != null; 
            return Json(clienteExiste, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IncluirBeneficiario(BeneficiarioModel model)
        {
            BoBeneficiario bo = new BoBeneficiario();
            var cpfExiste = bo.VerificarExistencia(model.CPF);

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                if (cpfExiste)
                {
                    return Json("CPF já cadastrado.");
                }
                else
                {
                    model.Id = bo.Incluir(new Beneficiario()
                    {
                        CPF = model.CPF,
                        Nome = model.Nome,
                        IdCliente = model.IdCliente,
                    });

                    return Json(model, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult AlterarBeneficiario(BeneficiarioModel model)
        {
            if (model.IdCliente <= 0)
            {
                return Json("Por favor, cadastre um cliente antes de alterar beneficiários.");
            }

            BoBeneficiario bo = new BoBeneficiario();
            Beneficiario beneficiario = bo.Consultar(model.Id);
            model.CPF = model.NormalizarCPF(model.CPF);

            var cpfExiste = bo.VerificarExistencia(model.CPF);

            if (beneficiario.Id == model.Id)
            {
                bo.Alterar(new Beneficiario()
                {
                    Id = model.Id,
                    IdCliente = model.IdCliente,
                    CPF = model.CPF,
                    Nome = model.Nome,
                });

                return Json("Beneficiário alterado com sucesso.");
            }

            return Json("Erro ao alterar beneficiário.");
        }

        [HttpPost]
        public JsonResult ExcluirBeneficiario(long id)
        {
            BoBeneficiario bo = new BoBeneficiario();
            var beneficiario = bo.Consultar(id);

            if (beneficiario == null)
            {
                return Json("Beneficiário não encontrado.");
            }   

            bo.Excluir(beneficiario.Id);

            return Json("Beneficiário excluído com sucesso.");
        }
    }
}
