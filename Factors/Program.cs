/*Program.cs
 * GDSC- Given an integer n, return all possible combinations of its factors.
 * 
 * Revision History:
 *          Jaime Sanchez, 2022.03.02 : Created
 *          
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factors
{
    class Program
    {


        static List<List<int>> factors = new List<List<int>>();

        static List<int> primes = new List<int>();

        //variable for controling the loop in the recursive function
        static bool finish = false;


        static void Main(string[] args)
        {


            string input;

            int number;

            try
            {

                while (true)
                {
                    ResetVariables();
                    Console.WriteLine("\nEnter an integer number greater than 2 to calculate the combinations of its factors.");
                    input = Console.ReadLine();
                    if (input.Equals("exit"))
                    {
                        System.Environment.Exit(0);
                    }
                    GetFactors(number = Convert.ToInt32(input));
                }
            }
            catch
            {
                Console.WriteLine("You need to write the word \"exit\" or enter an integer number greater than 2. Press any key to try again.");
                Console.ReadKey();
                Console.Clear();
                Main(args);

            }
            Console.ReadKey();
        }

        static void ResetVariables()
        {
            factors.Clear();
            primes.Clear();
            finish = false;
        }


        static void GetFactors(int number)
        {

            for (int i = 2; i < number; i++)
            {
                //if the residue is 0 then is a factor of the number
                //we are validating to not repeat the same combination of numbers
                if (number % i == 0 && !SubListContainNumber(i))
                {
                    SetPairInSubList(number, i);
                }

            }

            //If factors has a list within we proceed to calculate the prime factors
            if (factors.Count > 0)
            {
                Console.WriteLine("The all posible combinatios of its factores are : ");
                if (!IsPrime(factors[0][1]))
                {
                    primes.Add(factors[0][0]);
                    SetPrimes(factors[0][1]);

                    factors.Add(primes);

                }
            }

            //Method to show in the console the list
            factors.ForEach(delegate (List<int> item)
            {
                for (int i = 0; i < item.Count; i++)
                {
                    if (i == 0)
                    {
                        Console.Write("(");
                    }

                    Console.Write(item[i]);

                    if (i == item.Count - 1)
                    {
                        Console.Write(")");
                    }
                    else
                    {
                        Console.Write(",");
                    }
                }

            });
            Console.WriteLine();
        }



        //Method to verify if a number is in any list of factors
        static bool SubListContainNumber(int number)
        {
            for (int i = 0; i < factors.Count(); i++)
            {
                if (factors[i].Contains(number))
                {
                    return true;
                }
            }
            return false;
        }



        static void SetPrimes(int factor)
        {
            for (int i = 2; i <= factor; i++)
            {

                //if finish is true then we have to out of the method
                if (finish)
                {
                    return;
                }
                else
                {
                    if (factor % i == 0)
                    {
                        //If the number is prime add to the list of primes factors
                        if (IsPrime(i))
                        {
                            primes.Add(i);

                        }

                        //factor/i is the number who will be the next factor... If it is prime we add and finish otherwise we continue doing the same.
                        if (!IsPrime(factor / i))
                        {
                            SetPrimes(factor / i);
                        }
                        else
                        {

                            primes.Add(factor / i);
                            finish = true;
                            return;
                        }
                    }

                }

            }
        }



        //method to add a pair of factors in the list
        static void SetPairInSubList(int number, int factor)
        {

            //number/factor is the other factor that multiplied by the factor is equal to the main number
            List<int> subList = new List<int>();
            subList.Add(factor);
            subList.Add(number / factor);
            //we add the sublist(pair of factors) to the main List
            factors.Add(subList);
        }


        //method to verify if a number is prime
        static bool IsPrime(int number)
        {
            for (int i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }

            }

            return true;
        }

    }
}
