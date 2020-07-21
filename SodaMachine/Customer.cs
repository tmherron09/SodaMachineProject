using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    public class Customer
    {
        private Wallet wallet;
        private Backpack backpack;

        public Customer()
        {
            wallet = new Wallet();
            // define number of coins in wall.
            backpack = new Backpack();
        }

        // Customer can give its wallets contents.
        // Customer can check contents of Backpack.
        // Check if price of soda is affordable.
        // Make a payment, transfer coins out of wallet and into soda machine.
        // Customer can recieve a soda
        // Customer can recieve change


        public List<Coin> CheckCustomerWalletContents()
        {
            return wallet.GetCoinList();  // Returns a copy of the Coin List
        }
        public Backpack GetCustomerBackpack()
        {

            return backpack;
        }
        public bool HasEnoughMoney(double price)
        {
            throw new NotImplementedException();
        }
        public List<Coin> MakePayment(List<Coin> approvedTransaction, SodaMachine sodaMachine, Can soda) // Customer then directly inserts money
        {
            // Remove change from wallet.
            // Call soda machine and insert change.
            // Recieve soda from machine.
            // Put soda into backpack.
            throw new NotImplementedException();
        }
        public void RecieveChange(List<Coin> change)
        {
            throw new NotImplementedException();
        }
        public void RecieveSoda(Can soda)
        {
            throw new NotImplementedException();
        }

    }
}
