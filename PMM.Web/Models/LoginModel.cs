using FluentValidation.Attributes;
using PMM.Web.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMM.Web.Models
{
    [Validator(typeof(LoginValidator))]
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsRemember { get; set; }
    }
}