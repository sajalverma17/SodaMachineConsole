using System;
using SodaMachine.Business;

namespace SodaMachine.UI
{
    /// <summary>
    /// Responsible for validating and identifying commands and parameters from user input.
    /// Command parameter must go through validtion in IsValidCommandParameter and parsed in GetCommandParameter method for each command
    /// </summary>
    public static class MachineClientHelper
    {
        #region Client validations on types of Command and Command Parameters

        /// <summary>
        /// Validates if user's input contains relevant command in text
        /// </summary>
        /// <param name="input">User's input</param>
        /// <returns></returns>
        public static bool IsValidCommand(string input)
        {
            var inputs = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            //Since user inputs are fixed and not number based (eg. select 1 for insert money, 2 for order of soda etc...),
            //we have to assume fixed lengths of each type of command. 
            bool retValue = false;
            if (inputs.Length <= 3)
            {
                if (inputs.Length == 3)
                {
                    if (inputs[0] == "sms" && inputs[1] == "order")
                    {
                        //Sms command identified
                        retValue = true;
                    }
                }
                else if (inputs.Length == 2)
                {
                    if (inputs[0] == "insert" || inputs[0] == "order")
                    {
                        //insert or order command
                        if (inputs[0] == "insert")
                        {
                            retValue = true;
                        }
                        else if (inputs[0] == "order")
                        {
                            retValue = true;
                        }
                    }
                }
                else if (inputs.Length == 1)
                {
                    if (inputs[0] == "recall")
                    {
                        //recall command
                        retValue = true;
                    }
                }
            }
            return retValue;
        }

        /// <summary>
        ///Validates if the command parameter passed is valid for the given commandType
        /// </summary>
        /// <param name="input">User's input</param>
        /// <param name="commandType">Command type for which parameter needs to be validated</param>
        /// <returns></returns>
        public static bool IsValidCommandParameter(string input, Command commandType)
        {
            var inputs = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var parameter = inputs[inputs.Length - 1];

            bool retValue = false;
            if (commandType == Command.InsertMoney)
            {
                //Insert (money) command must have a int parameter
                int parameterInt;
                if (Int32.TryParse(parameter, out parameterInt))
                {
                    retValue = true;
                }
            }
            else if (commandType == Command.SmsOrder || commandType == Command.Order || commandType == Command.Recall)
            {
                //All other command types do not require type conversions.
                retValue = true;
            }
            return retValue;
        }

        #endregion

        #region Parsing of user input to type of Command and Command Parameter
        /// <summary>
        /// Identifies and returns the <c>Command</c> that <c>MachineManager</c> supports.
        /// Each <c>Command</c> must be uniquely identified directly from user's input in this method.
        /// </summary>
        /// <param name="input">User input</param>
        /// <returns></returns>        
        public static Command GetCommand(string input)
        {            
            var inputs = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Command commandType;

            if (inputs.Length == 3)
            {
                commandType = Command.SmsOrder;
            }
            else if (inputs.Length == 2)
            {
                if (inputs[0] == "insert")
                {
                    commandType = Command.InsertMoney;
                }
                else 
                    commandType = Command.Order;
            }
            else
            {
                commandType = Command.Recall;                
            }
            return commandType;
        }

        /// <summary>
        /// Parses parameters from user input depending on commandType and returns the parameter wrapped in <c>CommandParameter</c>.
        /// </summary>
        /// <param name="input">The user input</param>
        /// <param name="commandType">The machine command for which parameter has to be parsed</param>
        /// <returns></returns>
        public static object GetCommandParameter(string input, Command commandType)
        {
            var inputs = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            object commandParameter = null;
            
            if (commandType == Command.InsertMoney)
            {
                commandParameter = int.Parse(inputs[inputs.Length - 1]);
            }
            else if (commandType == Command.Order || commandType == Command.SmsOrder)
            {
                //Both the commands require string input parameters, i.e, the soda name, so no parsing required after user input.
                commandParameter = inputs[inputs.Length - 1];
            }

            return commandParameter;
        }

        #endregion
    }
}
