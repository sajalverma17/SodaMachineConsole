using SodaMachine.Business.Commands.Contracts;
using SodaMachine.Business.Commands;
using SodaMachine.Business.Entities;
using System.Collections.Generic;


namespace SodaMachine.Business
{
    /// <summary>
    /// Manages a <c>Machine</c> instance and provides methods for clients to perform on the machine
    /// </summary>
    public class MachineManager
    {
        private Machine _machine;

        /// <summary>
        /// Create and initialize an instance of <c>Machine</c>.
        /// </summary>
        public void InitMachine()
        {
            var sodaInventory = new List<Soda>() {
                new Soda { Name = "coke", Nr = 5, Price = 20 },
                new Soda { Name = "sprite", Nr = 3, Price = 10 },
                new Soda { Name = "fanta", Nr = 1, Price = 5 } };

            _machine = new Machine { Money = 10, Sodas = sodaInventory };
        }

        /// <summary>
        /// Returns the money available in <c>Machine</c>
        /// </summary>
        /// <returns></returns>
        public int GetMoneyInMachine()
        {
            return _machine.Money;
        }

        /// <summary>
        /// Invokes a commands on the <c>Machine</c> instance
        /// </summary>
        /// <param name="commandType">One of the <c>Command</c> enum</param>
        /// <param name="commandInput">Dynamic Input recieved from MachineClient. 
        /// Manager must cast it to the type the command requires, trusting the validations in client to filter out incorrect command parameters entered by user</param>
        public void ExecuteCommand(Command commandType, object commandInput)
        {
            IMachineCommand machineCommand;
            switch (commandType)
            {
                case Command.InsertMoney:
                    machineCommand = new InsertMoneyCommand((int)commandInput);
                    break;
                case Command.Order:
                    machineCommand = new OrderCommand((string)commandInput);
                    break;
                case Command.SmsOrder:
                    machineCommand = new SmsOrderCommand((string)commandInput);
                    break;
                case Command.Recall:
                    machineCommand = new RecallCommand();
                    break;
                default: return;    //No command will be executed                    
            }
            _machine = machineCommand.Execute(_machine);
        }
    }

    /// <summary>
    /// Command types supported by the <c>MachineManager</c> 
    /// </summary>
    public enum Command
    {
        InsertMoney,
        Order,
        SmsOrder,
        Recall
    }
}
