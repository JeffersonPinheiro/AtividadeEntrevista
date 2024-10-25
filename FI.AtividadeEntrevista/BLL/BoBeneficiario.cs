using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        public long Incluir(DML.Beneficiario beneficiario)
        {
            DAL.Beneficiarios.DaoBeneficiarios ben = new DAL.Beneficiarios.DaoBeneficiarios();
            return ben.Incluir(beneficiario);
        }

        public List<DML.Beneficiario> Listar(long id)
        {
            DAL.Beneficiarios.DaoBeneficiarios ben = new DAL.Beneficiarios.DaoBeneficiarios();
            return ben.Listar(id);
        }

        public void Alterar(DML.Beneficiario beneficiario)
        {
            DAL.Beneficiarios.DaoBeneficiarios ben = new DAL.Beneficiarios.DaoBeneficiarios();
            ben.Alterar(beneficiario);
        }

        public void Excluir(long Id)
        {
            DAL.Beneficiarios.DaoBeneficiarios ben = new DAL.Beneficiarios.DaoBeneficiarios();
            ben.Excluir(Id);
        }

        public DML.Beneficiario Consultar(long id)
        {
            DAL.Beneficiarios.DaoBeneficiarios ben = new DAL.Beneficiarios.DaoBeneficiarios();
            return ben.Consultar(id);
        }

        public bool VerificarExistencia(string CPF)
        {
            DAL.DaoCliente ben = new DAL.DaoCliente();
            return ben.VerificarExistencia(CPF);
        }
    }
}
