using FluentValidation;
using Microservice.Admin.FrontEnd.Models.ViewModels;

namespace Microservice.Admin.FrontEnd.Validation
{
    public class CreateUserValidation:AbstractValidator<AddUserViewModel>
    {
        public CreateUserValidation()
        {
            RuleFor(user => user.FullName)
           .NotEmpty().WithMessage("نام و نام خانوادگی را وارد کنید.")
           .MaximumLength(100).WithMessage("نام و نام خانوادگی باید کمتر از 100 کاراکتر باشد.");

            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("نام کاربری الزامی می باشد.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("ایمیل باید وارد شود.")
                .EmailAddress().WithMessage("ایمیل وارد شده فرمت مناسبی ندارد.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("رمز عبور را وارد کنید.");


        }
    }
}
