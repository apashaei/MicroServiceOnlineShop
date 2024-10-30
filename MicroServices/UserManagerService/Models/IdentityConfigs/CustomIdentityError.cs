using Microsoft.AspNetCore.Identity;

namespace UserManagerService.Models.IdentityConfigs
{
    public class CustomIdentityError : IdentityErrorDescriber
    {
        public override IdentityError DefaultError() => new IdentityError { Code = nameof(DefaultError), Description = "یک خطای پیش‌فرض رخ داده است." };
        public override IdentityError ConcurrencyFailure() => new IdentityError { Code = nameof(ConcurrencyFailure), Description = "خطای همزمانی، داده‌ها تغییر کرده‌اند." };
        public override IdentityError PasswordMismatch() => new IdentityError { Code = nameof(PasswordMismatch), Description = "رمز عبور اشتباه است." };
        public override IdentityError InvalidToken() => new IdentityError { Code = nameof(InvalidToken), Description = "توکن نامعتبر است." };
        public override IdentityError LoginAlreadyAssociated() => new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = "یک کاربر با این نام کاربری قبلاً وجود دارد." };
        public override IdentityError InvalidUserName(string userName) => new IdentityError { Code = nameof(InvalidUserName), Description = $"نام کاربری '{userName}' نامعتبر است." };
        public override IdentityError InvalidEmail(string email) => new IdentityError { Code = nameof(InvalidEmail), Description = $"ایمیل '{email}' نامعتبر است." };
        public override IdentityError DuplicateUserName(string userName) => new IdentityError { Code = nameof(DuplicateUserName), Description = $"نام کاربری '{userName}' قبلاً گرفته شده است." };
        public override IdentityError DuplicateEmail(string email) => new IdentityError { Code = nameof(DuplicateEmail), Description = $"ایمیل '{email}' قبلاً گرفته شده است." };
        public override IdentityError InvalidRoleName(string role) => new IdentityError { Code = nameof(InvalidRoleName), Description = $"نقش '{role}' نامعتبر است." };
        public override IdentityError DuplicateRoleName(string role) => new IdentityError { Code = nameof(DuplicateRoleName), Description = $"نقش '{role}' قبلاً گرفته شده است." };
        public override IdentityError UserAlreadyHasPassword() => new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = "این کاربر قبلاً رمز عبور دارد." };
        public override IdentityError UserLockoutNotEnabled() => new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = "قفل شدن کاربر فعال نیست." };
        public override IdentityError UserAlreadyInRole(string role) => new IdentityError { Code = nameof(UserAlreadyInRole), Description = $"کاربر قبلاً در نقش '{role}' است." };
        public override IdentityError UserNotInRole(string role) => new IdentityError { Code = nameof(UserNotInRole), Description = $"کاربر در نقش '{role}' نیست." };
        public override IdentityError PasswordTooShort(int length) => new IdentityError { Code = nameof(PasswordTooShort), Description = $"رمز عبور باید حداقل {length} کاراکتر باشد." };
        public override IdentityError PasswordRequiresNonAlphanumeric() => new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "رمز عبور باید حداقل یک کاراکتر غیرالفبایی داشته باشد." };
        public override IdentityError PasswordRequiresDigit() => new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "رمز عبور باید حداقل یک عدد داشته باشد." };
        public override IdentityError PasswordRequiresLower() => new IdentityError { Code = nameof(PasswordRequiresLower), Description = "رمز عبور باید حداقل یک حرف کوچک داشته باشد." };
        public override IdentityError PasswordRequiresUpper() => new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "رمز عبور باید حداقل یک حرف بزرگ داشته باشد." };
    }
}
