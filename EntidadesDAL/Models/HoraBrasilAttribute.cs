/* 
 * Atributo HoraBrasil
 * 
 * Data Annotations para Validar uma hora no formato hh:mm.
 * 
 * Visite nossa página http://www.codigoexpresso.com.br
 * 
 * by Antonio Azevedo
 *  
 * Chamada em sua Classe : 
 *   [HoraBrasil(ErrorMessage="Sua mensagem de erro", HoraRequerida=true/false, Hora24=true/false)]
 *    
 *   HoraRequerida  -  (False) - Valida horas em branco 
 *   Hora24         -  (True) Valida horas no formado hhh:mm (False) Valida Horas no Formato hh:mm (00:00 até 23:59) 
 *   
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Mvc;

public class HoraBrasilAttribute : ValidationAttribute, IClientValidatable
{

    public Boolean HoraRequerida { get; set; }
    public Boolean Hora24 { get; set; }
    public HoraBrasilAttribute()
    {
        this.ErrorMessage = "Hora inválida.";
        this.HoraRequerida = false;
        this.Hora24 = true;
    }

    protected override ValidationResult IsValid(
        object value,
        ValidationContext validationContext)
    {

        // Verifica se o Valor é nulo
        if (value == null)
        {
            value = "";
        }

        // Caso o valor informado seja nulo não é requerido retorna sem validar
        if (value.ToString() == "" && HoraRequerida == false)
        {
            return ValidationResult.Success;
        }

        Regex regExpHora;

        // Atribui expressao Regex conforme Atribuito solicitado Hora24 solicitado
        if (Hora24 == false)
        {
            regExpHora = new Regex(@"^([0-9][0-9][0-9]|[0-9][0-9]|[0-9]):([0-5][0-9])$");
        }
        else
        {
            regExpHora = new Regex(@"^([0-1][0-9]|[2][0-3]):([0-5][0-9])$");
        }

        // Valida a expressao
        Match match = regExpHora.Match(value.ToString());

        if (match.Success)
        {
            // Se não for requerida valida com sucesso
            if (HoraRequerida == false)
            {
                return ValidationResult.Success; ;
            }
            else
            {
                if (Hora24 == true)
                {
                    return ValidationResult.Success; ;
                }
            }
        }
        else
        {
            // Devolve o erro padrao se a expressao nao for valida.
            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }

        // Converte o valor para Minutos inteiros
        int Minutos = 0;

        int pos = value.ToString().IndexOf(":");
        if (pos < 0)
        {
            Minutos = 0;
        }
        else
        {
            Minutos = (ConverteInt32(value.ToString().Substring(0, pos)) * 60) + (ConverteInt32(value.ToString().Substring(pos + 1)));
        }

        // Se Hora foi requerida e não digitada retorna erro padrao
        if (HoraRequerida == true && Minutos == 0)
        {
            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }

        // Retorna com sucesso caso a converão tenha sido feita
        return ValidationResult.Success;
    }

    // Diretivas para validação do lado do Cliente, implementa com jquery.validate
    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
        ModelMetadata metadata,
        ControllerContext context)
    {
        var Rule = new ModelClientValidationRule
        {
            ValidationType = "horabrasil",
            ErrorMessage = this.FormatErrorMessage(metadata.PropertyName)

        };

        string[] array = new string[] { HoraRequerida.ToString(), Hora24.ToString() };

        Rule.ValidationParameters["params"] = string.Join(",", Array.ConvertAll(array, x => x.ToString()));

        yield return Rule;
    }


    // Função para converter uma string para inteiro, se a string nao puder ser convertida retorna 0
    private Int32 ConverteInt32(string valor)
    {
        Int32 newValor;
        try
        {
            newValor = Convert.ToInt32(valor);
        }
        catch (Exception)
        {
            return 0;
        }

        return newValor;
    }
}
