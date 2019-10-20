using SodaMachine.Business.Commands.Contracts;
using SodaMachine.Business.Entities;

namespace SodaMachine.Business.Commands
{
    public class OrderCommand : IMachineCommand
    {
        string _sodaToOrder;
        public OrderCommand(string soda)
        {
            _sodaToOrder = soda;
        }
        public Machine Execute(Machine machine)
        {
            machine.Order(_sodaToOrder);
            return machine;
        }
    }
}
