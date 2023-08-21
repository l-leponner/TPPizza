using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace TPPizza.CustomAttributes
{
    public class ListLengthAttribute : ValidationAttribute, IClientModelValidator
    {
        public int Min { get; set; }
        public int Max { get; set; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IList? liste = value as IList;
            if (liste == null)
                return new ValidationResult("Cet élément n'est pas une liste");
            if (liste.Count < Min)
                return new ValidationResult("Pas assez d'éléments choisis. Au minimum, il faut en choisir " + Min + ".");
            if (liste.Count > Max)
                return new ValidationResult("Trop d'éléments choisis. Au maximum, il faut en choisir " + Max + ".");
            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-liste", "Vous devez sélectionner entre " + Min + " et " + Max + " éléments");
            context.Attributes.Add("data-val-liste-min", Min.ToString());
            context.Attributes.Add("data-val-liste-max", Max.ToString());
        }
    }
}
