using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidLessDate : ValidationAttribute
    {
        public ValidLessDate() {
            //ErrorMessageString = string.Format("{0} is less than 1-jan-1900", "BirthDate");
        }
        public ValidLessDate(string errorMessage) : base(errorMessage) { }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            if (value != null)
            {
                DateTime _birthJoin = Convert.ToDateTime(value);
                if (_birthJoin < new  DateTime(1900,1,1))
                {
                  //  return new ValidationResult("Birth date can not be less than 1-jan-1900");
                    return new ValidationResult(ErrorMessageString);
                }
            }
            return ValidationResult.Success;
        }
    }
}