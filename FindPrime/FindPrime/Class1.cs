using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindPrime
{
    internal class Class1
    {
        static int Number = 500000;
        bool[] map = new bool[Number];

        public Class1()
        {
            map[0] = false;

            for (int i = 1; i < Number; i++)
            {
                map[i] = true;


            }



        }

        public bool Sequential(int n)
        {
            for (int i = 2; i < n; i++)
            {
                if (n % i == 0)
                {

                    return false;

                }

            }

            return true;

        }

        public bool Root(int n)
        {
            int m = (int)Math.Sqrt(n);

            for (int i = 2; i < m + 1; i++)
            {
                if (n % i == 0)
                {

                    return false;

                }

            }

            return true;
        }

        public void Sieve(int n)
        {
            int m = (int)Math.Sqrt(n);
            int j;
            for (int i = 2; i < m; i++)
            {
                
                if (map[i-1]) {

                    j = 2*i;

                    while (j <= Number) 
                    {
                        map[j-1] = false;
                        j += i;

                    }

                
                }

            }



        }
        static void Main(string[] args)
        {
            var c1 = new Class1();
            var sw = new System.Diagnostics.Stopwatch();

            int count = 0;

            var secPrime = new List<int>();

            sw.Start();

            for(int i = 2; i < Number; i++)
            {

                if (c1.Sequential(i))
                {
                    count++;
                    secPrime.Add(i);

                }

            }
            sw.Stop();

            Console.WriteLine($"判定法1(逐次除算法) N = 500000 \nCount:{count}, Time:{sw.ElapsedMilliseconds}[ms]");
            
            count = 0;
            var rootPrime = new List<int>();

            sw.Restart();

            for (int i = 2; i < Number; i++)
            {

                if (c1.Root(i))
                {
                    count++;
                    rootPrime.Add(i);

                }

            }
            sw.Stop();

            Console.WriteLine($"判定法2(平方根除算法) N = 500000 \nCount:{count}, Time:{sw.ElapsedMilliseconds}[ms]");

            count = 0;
            var sievePrime = new List<int>();

            sw.Restart();

            c1.Sieve(Number);

           
            sw.Stop();


            for (int i = 0; i < Number; i++)
            {

                if (c1.map[i])
                {

                    count++;
                    sievePrime.Add(i + 1);


                }
            }

            Console.WriteLine($"判定法3(エラストテネスの篩) N = 500000 \nCount:{count}, Time:{sw.ElapsedMilliseconds}[ms]");
        }



    }
}
