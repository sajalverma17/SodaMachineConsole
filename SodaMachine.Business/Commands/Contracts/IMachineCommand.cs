using SodaMachine.Business.Entities;

namespace SodaMachine.Business.Commands.Contracts
{
    /// <summary>
    /// Interface for Machine commands 
    /// </summary>
    public interface IMachineCommand
    {
        /// <summary>
        /// Takes a receiver object of type <c>Machine</c> and asks the receiver to perform an operation.
        /// Returns the updated instance after <c>Machine</c> performs its action
        /// </summary>
        Machine Execute(Machine machine);
    }



    

}
