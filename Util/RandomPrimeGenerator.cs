using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SProjectServer.Util
{
    internal class RandomPrimeGenerator
    {
        public int GenerateRandomPrime()
        {
            var random = new Random();
            var randomNum = random.Next(100, 1000);
            while (!CheckIfPrime(randomNum))
            {
                randomNum = random.Next(100, 1000);
            }
            return randomNum;
        }

        private bool CheckIfPrime(int n) //to check if the random number generated is prime
        {
            var isPrime = true;
            var sqrt = Math.Sqrt(n);
            for (var i = 2; i <= sqrt; i++)
                if ((n % i) == 0) isPrime = false;
            return isPrime;
        }
    }
}
