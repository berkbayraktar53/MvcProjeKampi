using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class HeadingValidator : AbstractValidator<Heading>
    {
        public HeadingValidator()
        {
            RuleFor(x => x.HeadingName).NotEmpty().WithMessage("Başlığı Boş Geçemezsiniz");
            RuleFor(x => x.CategoryID).NotEmpty().WithMessage("Kategoriyi Boş Geçemezsiniz");
            RuleFor(x => x.WriterID).NotEmpty().WithMessage("Yazarı Alanını Boş Geçemezsiniz");
            RuleFor(x => x.HeadingName).MinimumLength(2).WithMessage("Lütfen en az 2 karakter girişi yapın");
            RuleFor(x => x.HeadingName).MaximumLength(20).WithMessage("Lütfen 20 karakterden fazla değer girişi yapmayın");
        }
    }
}