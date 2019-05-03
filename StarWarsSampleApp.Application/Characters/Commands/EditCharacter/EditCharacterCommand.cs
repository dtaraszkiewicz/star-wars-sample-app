using StarWarsSampleApp.Application.Characters.Commands.CreateCharacter;

namespace StarWarsSampleApp.Application.Characters.Commands.EditCharacter
{
    public class EditCharacterCommand : CreateCharacterCommand
    {
        public int Id { get; set; }
    }
}
