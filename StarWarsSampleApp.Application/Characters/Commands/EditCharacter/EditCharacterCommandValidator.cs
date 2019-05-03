using System.Linq;
using FluentValidation;
using FluentValidation.Validators;
using StarWarsSampleApp.Persistence;

namespace StarWarsSampleApp.Application.Characters.Commands.EditCharacter
{
    public class EditCharacterCommandValidator : AbstractValidator<EditCharacterCommand>
    {
        private readonly StarWarsSampleAppDbContext _context;

        public EditCharacterCommandValidator(StarWarsSampleAppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty()
                .Must(BeUnique).WithMessage("The character name has to be unique");

            RuleFor(x => x.EpisodesIds)
                .NotEmpty()
                .Must(HaveAtLeastOneEpisode).WithMessage("Character has to star in at least one episode");

            RuleFor(x => x.FriendsIds)
                .NotEmpty()
                .Must(DoNotHaveRelationWithItself).WithMessage("Character cannot be friend with itself");
        }

        private bool BeUnique(EditCharacterCommand command, string name, PropertyValidatorContext ctx)
        {
            return _context.Characters.Where(x => x.Id != command.Id).All(x => x.Name != command.Name);
        }

        private bool HaveAtLeastOneEpisode(EditCharacterCommand command, int[] episodeIds, PropertyValidatorContext ctx)
        {
            return _context.Episodes.Any(x => episodeIds.Contains(x.Id));
        }

        private bool DoNotHaveRelationWithItself(EditCharacterCommand command, int[] friendIds,
            PropertyValidatorContext ctx)
        {
            return friendIds.All(x => x != command.Id);
        }
    }
}
