using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMM.Core;
using PMM.Web.Models;

namespace PMM.Web.Validators
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage(CommonHelper.EmptyUsername);
            RuleFor(x => x.Password).NotEmpty().WithMessage(CommonHelper.EmptyPassword);
        }
    }
}