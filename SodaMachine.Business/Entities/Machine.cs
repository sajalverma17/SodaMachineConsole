using System.Collections.Generic;
using System.Linq;
using System;
namespace SodaMachine.Business.Entities
{
    /// <summary>
    /// Business entity : Business object which acts as the Receiver of requests by <c>IMachineCommand</c>s 
    /// </summary>
    public class Machine
    {
        public List<Soda> Sodas { get; set; }

        public int Money { get; set; }

    #region Business methods
        public void InsertMoney(int moneyToInsert)
        {
            Money = Money + moneyToInsert;
            Console.WriteLine("Added "+moneyToInsert+" to credit");
        }

        public void Order(string soda)
        {
            string invalidResult = "";
            if (IsOrderValid(soda, out invalidResult))
            {
                //Get the soda to order from Sodas
                var sodaQuery = from sodaItem in Sodas
                                where sodaItem.Name == soda
                                select new Soda { Price = sodaItem.Price, Nr = sodaItem.Nr, Name = sodaItem.Name };
                var sodaToOrder = sodaQuery.First();

                //Place order by reducing the Nr of that soda item
                Sodas.RemoveAll(s => s.Name == sodaToOrder.Name);
                Sodas.Add(new Soda { Name = sodaToOrder.Name, Nr = sodaToOrder.Nr - 1, Price = sodaToOrder.Price });
                Console.WriteLine("Giving out a " + sodaToOrder.Name);

                //Give out change
                var change = Money - sodaToOrder.Price;
                Money = 0;
                Console.WriteLine("Returning " + change + " in change");
            }
            else
            {
                Console.WriteLine(invalidResult);
            }
        }

        public void SmsOrder(string soda)
        {
            string invalidResult = "";
            if (IsOrderValid(soda, out invalidResult))
            {
                //Send sms for the order
                Console.WriteLine("Giving out a " + soda);
            }
            else
            {
                Console.WriteLine(invalidResult);
            }
        }

        public void Recall()
        {
            Console.WriteLine("Returning "+Money+" to customer");
            Money = 0;
        }
    #endregion 


    #region Private methods
        private bool IsOrderValid(string sodaToOrder, out string invalidOrderReason)
        {
            invalidOrderReason = "";
            var validationResult = true;
            if (Sodas.Any(s => s.Name == sodaToOrder))
            {
                //Ouery for the soda ordered.
                var sodaQuery = from soda in Sodas
                                where soda.Name == sodaToOrder
                                select new Soda { Price = soda.Price, Nr = soda.Nr, Name = soda.Name };
                var orderedSoda = sodaQuery.First();
                if (orderedSoda.Price > Money)
                {
                    invalidOrderReason = "Insert " + (orderedSoda.Price - Money) + " more";
                    validationResult = false;
                }
                else if (orderedSoda.Nr < 1)
                {
                    invalidOrderReason = "No " + orderedSoda.Name + " left in the machine";
                    validationResult = false;
                }
            }
            else
            {
                validationResult = false;
                invalidOrderReason = "This soda is not available in the machine";
            }
            
            return validationResult;
        }
    #endregion


    }
}
