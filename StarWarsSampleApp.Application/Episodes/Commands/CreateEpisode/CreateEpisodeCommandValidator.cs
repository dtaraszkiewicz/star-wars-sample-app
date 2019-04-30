using System.Linq;
using FluentValidation;
using FluentValidation.Validators;
using StarWarsSampleApp.Persistence;

namespace StarWarsSampleApp.Application.Episodes.Commands.CreateEpisode
{
    public class CreateEpisodeCommandValidator : AbstractValidator<CreateEpisodeCommand>
    {
        private readonly StarWarsSampleAppDbContext _context;

        public CreateEpisodeCommandValidator(StarWarsSampleAppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty()
                .Must(BeUnique).WithMessage("The episode name has to be unique");
        }

        private bool BeUnique(CreateEpisodeCommand command, string name, PropertyValidatorContext ctx)
        {
            return _context.Episodes.All(x => x.Name != command.Name);
        }
    }
}
