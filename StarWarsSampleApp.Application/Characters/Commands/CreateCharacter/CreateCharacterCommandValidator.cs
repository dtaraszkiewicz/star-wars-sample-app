using System.Linq;
using FluentValidation;
using FluentValidation.Validators;
using StarWarsSampleApp.Persistence;

namespace StarWarsSampleApp.Application.Characters.Commands.CreateCharacter
{
    public class CreateCharacterCommandValidator : AbstractValidator<CreateCharacterCommand>
    {
        private readonly StarWarsSampleAppDbContext _context;

        public CreateCharacterCommandValidator(StarWarsSampleAppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty()
                .Must(BeUnique).WithMessage("The character name has to be unique");

            RuleFor(x => x.EpisodesIds)
                .NotEmpty()
                .Must(HaveAtLeastOneEpisode).WithMessage("Character has to star in at least one episode");
        }

        private bool BeUnique(CreateCharacterCommand command, string name, PropertyValidatorContext ctx)
        {
            return _context.Characters.All(x => x.Name != command.Name);
        }

        private bool HaveAtLeastOneEpisode(CreateCharacterCommand command, int[] episodeIds, PropertyValidatorContext ctx)
        {
            return _context.Episodes.Any(x => episodeIds.Contains(x.Id));
        }
    }
}
