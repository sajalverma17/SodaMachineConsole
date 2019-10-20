using SodaMachine.Business.Commands.Contracts;
using SodaMachine.Business.Entities;

namespace SodaMachine.Business.Commands
{
    public class RecallCommand : IMachineCommand
    {
        public Machine Execute(Machine machine)
        {
            machine.Recall();
            return machine;
            
        }
    }
}
