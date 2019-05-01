using System;

namespace StarWarsSampleApp.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(Type type, int value)
            : base($"Entity of type {type.FullName} with id: {value} does not exist")
        {
            
        }
    }
}
