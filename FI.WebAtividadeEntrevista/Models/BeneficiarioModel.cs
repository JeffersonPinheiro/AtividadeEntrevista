using FI.AtividadeEntrevista.DML;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FI.WebAtividadeEntrevista.Models
{
    public class BeneficiarioModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Id Cliente
        /// </summary>
        [Required]
        public long IdCliente { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        [Required(ErrorMessage = "O campo CPF é Obrigatório.")]
        [CustomValidationCPF(ErrorMessage = "CPF inválido.")]
        public string CPF { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        public string Nome { get; set; }

        public virtual Cliente Cliente { get; set; }

        public string NormalizarCPF(string cpf)
        {
            return Regex.Replace(cpf ?? "", @"[^\d]", "");
        }
    }

    public class CustomValidationCPFAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string cpf = value as string;

            if (string.IsNullOrEmpty(cpf))
                return new ValidationResult("CPF é Obrigatório.");

            cpf = Regex.Replace(cpf, @"[^\d]", "");

            if (cpf.Length != 11)
                return new ValidationResult("CPF Inválido.");

            return ValidationResult.Success;
        }
    }
}