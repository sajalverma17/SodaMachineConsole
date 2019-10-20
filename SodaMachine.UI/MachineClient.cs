using System;
using SodaMachine.Business;
namespace SodaMachine.UI
{
    /// <summary>
    /// Acts as an interface to the end user and takes user inputs.
    /// Commands from user are processed and passed to an internal <c>MachineManager</c> for execution
    /// </summary>
    public class MachineClient
    {
        MachineManager machineManager;

        public MachineClient()
        {
            machineManager = new MachineManager();
        }

        /// <summary>
        /// Initializes the soda machine with an inventory of sodas.
        /// </summary>
        public void InitializeMachine()
        {
            machineManager.InitMachine();
        }

        /// <summary>
        /// Starts the soda machine.
        /// </summary>
        public void StartMachine()
        {
            while (true)
            {                
                var userInput = getUserInput(machineManager.GetMoneyInMachine());
                if(MachineClientHelper.IsValidCommand(userInput))             
                {
                    var command = MachineClientHelper.GetCommand(userInput);

                    if (MachineClientHelper.IsValidCommandParameter(userInput, command))
                    {
                        //If the command is identified and the parameter is valid for the identified command, parse the parameter
                        var commandParameter = MachineClientHelper.GetCommandParameter(userInput, command);
                        machineManager.ExecuteCommand(command, commandParameter);
                    }
                    else
                        Console.WriteLine("Invalid command option");
                }
                else
                    Console.WriteLine("Invalid command");
            }
        }

        private string getUserInput(int availableMoney)
        {
            Console.WriteLine("\n\nAvailable commands:");
            Console.WriteLine("1. insert (money) - Money put into money slot");
            Console.WriteLine("2. order (coke, sprite, fanta) - Order from machines buttons");
            Console.WriteLine("3. sms order (coke, sprite, fanta) - Order sent by sms");
            Console.WriteLine("4. recall - gives money back");
            Console.WriteLine("-------");
            Console.WriteLine("Inserted money: "+availableMoney);
            Console.WriteLine("-------\n\n");

            var input = Console.ReadLine();
            return input;
        }

    }
}
