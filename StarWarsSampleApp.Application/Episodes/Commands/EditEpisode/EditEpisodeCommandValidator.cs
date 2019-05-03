using System.Linq;
using FluentValidation;
using FluentValidation.Validators;
using StarWarsSampleApp.Persistence;

namespace StarWarsSampleApp.Application.Episodes.Commands.EditEpisode
{
    public class EditEpisodeCommandValidator : AbstractValidator<EditEpisodeCommand>
    {
        private readonly StarWarsSampleAppDbContext _context;

        public EditEpisodeCommandValidator(StarWarsSampleAppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(BeActive).WithMessage("This episode was deleted"); 

            RuleFor(x => x.Name)
                .NotEmpty()
                .Must(BeUnique).WithMessage("The episode name has to be unique");
        }

        private bool BeUnique(EditEpisodeCommand command, string name, PropertyValidatorContext ctx)
        {
            return _context.Episodes.Where(x => x.Id != command.Id).All(x => x.Name != name);
        }

        private bool BeActive(EditEpisodeCommand command, int id, PropertyValidatorContext ctx)
        {
            var entity = _context.Episodes.Find(id);
            return entity.IsActive.HasValue && entity.IsActive.Value;
        }
    }
}
