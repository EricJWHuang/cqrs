using System;

namespace Cqrs.Commands.Abstract.Example.Commands
{
    public class InitialiseCommand : ICommand<Guid>
    {
        public InitialiseCommand(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
