using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using EllipticCurve;


namespace CurzCoin
{
    class Program
    {
        static void Main(string[] args)
        {
            PrivateKey key1 = new PrivateKey();
            PublicKey wallet1 = key1.publicKey();

            PrivateKey key2 = new PrivateKey();
            PublicKey wallet2 = key2.publicKey();

            Blockchain curzcoin = new Blockchain(2, 100);

            Console.WriteLine("Start the miner");
            curzcoin.MinePendingTransactions(wallet1);

            Console.WriteLine("\nBalance of wallet1 is $" + curzcoin.GetBalanceOfWallet(wallet1).ToString());

            Transaction tx1 = new Transaction(wallet1, wallet2, 10);
            tx1.SignTransaction(key1);
            curzcoin.addPendingTransaction(tx1);

            Console.WriteLine("Start the miner");
            curzcoin.MinePendingTransactions(wallet2);

            Console.WriteLine("\nBalance of wallet1 is $" + curzcoin.GetBalanceOfWallet(wallet1).ToString());
            Console.WriteLine("\nBalance of wallet2 is $" + curzcoin.GetBalanceOfWallet(wallet2).ToString());

            // curzcoin.GetLastestBlock().PreviousHash = "12345";

            string blockJSON = JsonConvert.SerializeObject(curzcoin, Formatting.Indented);
            Console.WriteLine(blockJSON);
            if (curzcoin.IsChainValid())
            {
                Console.WriteLine("Block Chain is Valid!!");

            }
            else
            {
                Console.WriteLine("Block Chain is not valid");
            }
        }
    }
    
}
