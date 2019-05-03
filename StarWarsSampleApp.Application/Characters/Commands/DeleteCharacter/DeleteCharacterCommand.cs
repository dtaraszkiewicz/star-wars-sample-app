using MediatR;

namespace StarWarsSampleApp.Application.Characters.Commands.DeleteCharacter
{
    public class DeleteCharacterCommand : IRequest
    {
        public int Id { get; set; }
    }
}
