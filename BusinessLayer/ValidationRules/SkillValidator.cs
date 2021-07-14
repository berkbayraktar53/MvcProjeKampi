using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class SkillValidator : AbstractValidator<Skill>
    {
        public SkillValidator()
        {
            RuleFor(x => x.SkillName).NotEmpty().WithMessage("Yetenek adını boş geçemezsiniz");
            RuleFor(x => x.Percentile).NotEmpty().WithMessage("Yüzdelik dilimi boş geçemezsiniz");
            RuleFor(x => x.Value).NotEmpty().WithMessage("Değeri boş geçemezsiniz");
            RuleFor(x => x.SkillName).MinimumLength(2).WithMessage("Lütfen en az 2 karakter girişi yapın");
            RuleFor(x => x.SkillName).MaximumLength(20).WithMessage("Lütfen 20 karakterden fazla değer girişi yapmayın");
            RuleFor(x => x.Percentile.ToString()).MinimumLength(1).WithMessage("Lütfen en az 1 karakter girişi yapın");
            RuleFor(x => x.Percentile.ToString()).MaximumLength(2).WithMessage("Lütfen 2 karakterden fazla değer girişi yapmayın");
            RuleFor(x => x.Value.ToString()).MinimumLength(1).WithMessage("Lütfen en az 1 karakter girişi yapın");
            RuleFor(x => x.Value.ToString()).MaximumLength(2).WithMessage("Lütfen 2 karakterden fazla değer girişi yapmayın");
        }
    }
}