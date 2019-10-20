using SodaMachine.Business.Commands.Contracts;
using SodaMachine.Business.Entities;

namespace SodaMachine.Business.Commands
{
    public class InsertMoneyCommand : IMachineCommand
    {
        int _moneyToInsert = 0;

        public InsertMoneyCommand(int moneyToInsert)
        {
            _moneyToInsert = moneyToInsert;
        }
        public Machine Execute(Machine machine)
        {
            machine.InsertMoney(_moneyToInsert);
            return machine;
        }
    }
}
