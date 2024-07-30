using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InoueLab;

namespace HitandBlow
{
    internal class Class1
    {
        // Max Number
        static int Number = 10000;
        //Possible Answer
        bool[] suggestMap = new bool[Number];


        int ans;

        Class1()
        {
            // set True
            for(int i = 0; i < Number; i++)
            {

                suggestMap[i] = true;

            }

            // genetate answer
            RandomMT rand = new RandomMT();
            ans = rand.Int(Number);



        }

        //Main Func
        static void Main(string[] args)
        {
            var c1 = new Class1();

            int trial = 1;
            //Call Judging Func
            while (c1.PrintResult(trial) == false){ trial++;}


            return;
        }

        //Print Func 
        bool PrintResult(int trial) {

            Console.Write($"[{trial}] Input No.(0000~9999): ");
            var input = int.Parse(Console.ReadLine());
            if(input/10000 != 0)
            {
                Console.Write($"[{trial}again] Input No.(0000~9999)!! : ");
                input = int.Parse(Console.ReadLine());

            }
            var Count = Judge(input);

            if (Count[0] == 4)
            {
                Console.WriteLine($">> {Count[0]} Hit(s), {Count[1]} Blow(s) \nCorrect!! ");
                return true;

            }
            else
            {

                Console.WriteLine($">> {Count[0]} Hit(s), {Count[1]} Blow(s) ");
                ShowSuggest(input, Count);

                return false;
            }

        }
        // Judge Func return int [] = {hitCount, blowCount}
        int[] Judge(int input)
        {
            int[] inputNum = { input % 10 , input / 10 - (input / 100)*10 , input / 100 - (input / 1000)*10 , input / 1000 };
            int[] ansNum = { ans % 10, ans / 10 - (ans / 100)*10, ans / 100 - (ans / 1000)*10, ans / 1000 };
            int blowCount = 0;
            int hitCount = 0;



            //judge
            for (int i = 0; i < 4; i++)
            {
                if (ansNum[i] == inputNum[i])
                {
                    ansNum[i] = 100;
                    inputNum[i] = 200;
                    hitCount++;

                }
            }
            for (int i = 0; i < 4; i++)
            { 
                for (int j = 0; j < 4; j++)
                    {
                    if (i != j && ansNum[i] == inputNum[j])
                    {
                        blowCount++;
                        ansNum[i] = 100;
                        inputNum[j] = 200;

                        break;
                    }
                }
            }

            int[] a = new int[2] { hitCount, blowCount };

            return a;
        }

        int[] Judge(int input, int input2)
        {
            int[] inputNum = { input % 10, input / 10 - (input / 100) * 10, input / 100 - (input / 1000) * 10, input / 1000 };
            int[] ansNum = { input2 % 10, input2 / 10 - (input2 / 100) * 10, input2 / 100 - (input2 / 1000) * 10, input2 / 1000 };
            int blowCount = 0;
            int hitCount = 0;



            //judge
            for (int i = 0; i < 4; i++)
            {
                if (ansNum[i] == inputNum[i])
                {
                    ansNum[i] = 100;
                    inputNum[i] = 200;
                    hitCount++;

                }
            }
            for (int i = 0; i < 4; i++)
            {

                for (int j = 0; j < 4; j++)
                {
                    if (i != j && ansNum[i] == inputNum[j])
                    {
                        blowCount++;
                        ansNum[i] = 100;
                        inputNum[j] = 200;

                        break;
                    }


                }

            }

            int[] a = new int[2] { hitCount, blowCount };

            return a;
        }

        //Show Suggest Func
        void ShowSuggest(int input,int[] Count)
        {
            int candNum = Number;
            suggestMap[input] = false;

            int[] A;
            for (int i = 0; i < Number; i++)
            {
                
                if (suggestMap[i] == true)
                {
                    A = Judge(i, input);


                        if ((A[0] != Count[0]) || (A[1] != Count[1]))
                        {

                            suggestMap[i] = false;
                            candNum--;

                        }
                    
                }
                else
                {

                    candNum--;
                }

            }


            //Consider Suggestion Number


            int suggestNum = Sugegst(candNum);

            
            //Show Suggestion
            if (candNum <= 6)
            {
                if (candNum == 1)
                {
                    Console.Write($"{candNum} Left, Suggest: ");
                    for (int i = 0; i < Number; i++)
                    {
                        if (suggestMap[i] == true)
                        {

                            Console.Write($"{i}");

                        }

                    }

                }
                else
                {
                    Console.WriteLine($"{candNum} Left, Suggest: {suggestNum}");
                    for (int i = 0; i < Number; i++)
                    {
                        if (suggestMap[i] == true)
                        {

                            Console.Write($"{i}, ");

                        }

                    }
                }

                Console.WriteLine($"");
            }
            else
            {

                Console.WriteLine($"{candNum} Left, Suggest: {suggestNum}");

            }


        }

       int Sugegst(int candNum)
        {
            int suggestNum=10;
            double Entropy=0;
            double TempEnt;

            int[,] Aggregate = new int[5,5];

            int[] A;

            for (int i = 0; i < Number; i++)
            {

                TempEnt = 0;

                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {

                        Aggregate[j, k] = 0;

                    }

                }

                if (suggestMap[i] == true)
                {
                    for (int j = 0; j < Number; j++)
                    {
                        if (suggestMap[j] == true)
                        {
                            A = Judge(i, j);

                            Aggregate[A[0], A[1]] += 1;

                        }
                    }
                    for (int j = 0; j < 5; j++)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            if (Aggregate[j, k] != 0)
                            {
                                TempEnt += ((double)Aggregate[j, k] / candNum) * Math.Log2((double)candNum / Aggregate[j, k]);
                               
                            }
                            
                        }

                    }
                    

                }

                if (Entropy < TempEnt)
                {
                    suggestNum = i;
                    Entropy = TempEnt;

                }

            }

            return suggestNum;
        }
    }
}
