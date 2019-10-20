using SodaMachine.Business.Commands.Contracts;
using SodaMachine.Business.Entities;

namespace SodaMachine.Business.Commands
{
    public class SmsOrderCommand : IMachineCommand
    {
        string _sodaToOrder;
        public SmsOrderCommand(string soda)
        {
            _sodaToOrder = soda;
        }
        public Machine Execute(Machine machine)
        {
            machine.SmsOrder(_sodaToOrder);
            return machine;
        }
    }
}
