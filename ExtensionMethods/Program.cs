using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asgn6
{
    class Program
    {
        static void Main(string[] args)
        {
            var vals = new int[] { 5, 9, 4, 7, 15, 16, 10, 11, 14, 19, 18 };

            var resultsMax = vals.MaxOverPrevious();

            foreach(var e in resultsMax)
            {
                Console.Write(e + "  ");
            }

            Console.WriteLine();

            var resultsMaxL = vals.MaxOverPrevious(i => i/2 + 7);

            foreach (var e in resultsMaxL)
            {
                Console.Write(e + "  ");
            }

            Console.WriteLine();

            var resultsMaxima = vals.LocalMaxima();

            foreach(var e in resultsMaxima)
            {
                Console.Write(e + "  ");
            }

            Console.WriteLine();

            var resultsMaximaL = vals.LocalMaxima(i => i/2 + 7);

            foreach (var e in resultsMaximaL)
            {
                Console.Write(e + "  ");
            }

            Console.WriteLine();

            var resultAtLeastK = vals.AtLeastK(3, i => i >= 16 && i < 20);

            if (resultAtLeastK)
            {
                Console.WriteLine("The results match");
            }
            else
            {
                Console.WriteLine("The results do not match");
            }

            var resultsHalf = vals.AtLeastHalf(i => i >= 16 && i < 20);

            if (resultsHalf)
            {
                Console.WriteLine("The results match");
            }
            else
            {
                Console.WriteLine("The results do not match");
            }

            Console.ReadKey();


            //Testing another data type
            Person p1 = new Person("Sam", 43, "123331233");
            Person p2 = new Person("Ray", 12, "9098886777");
            Person p3 = new Person("Peter", 67, "5556667878");

            var people = new Person[] { p1, p2, p3 };

            var listMax = people.MaxOverPrevious();

            foreach (var e in listMax)
            {
                Console.Write(e + "  ");
            }

            var listMaxima = people.LocalMaxima();

            foreach (var e in listMaxima)
            {
                Console.Write(e + "  ");
            }

            var listHalf = people.AtLeastHalf(i => i < 5 && i > 80);
        }
    }

    public static class ExtensionMethods
    {
        public static IEnumerable<T> MaxOverPrevious<T>(this IEnumerable<T> list) where T : IComparable
        {
            
            if (list.Count() == 0)
            {
                Console.WriteLine("Cannot compute on an empty data set");
            }

            List<T> maxList = new List<T>();

            T max = list.First();
            yield return max;
           // maxList.Add(max);

            foreach (var e in list)
            {

                if (e.CompareTo(max) > 0)
                {
                    yield return e;
                    //maxList.Add(e);
                    max = e;
                }
            }

           // return maxList;
        }

        public static IEnumerable<T> MaxOverPrevious<T>(this IEnumerable<T> list, Func<T, IComparable> compare)
        {
            if (list.Count() == 0)
            {
                Console.WriteLine("Cannot compute on an empty data set");
            }

            List<T> maxList = new List<T>();

            T max = list.First();
            yield return max;
            //maxList.Add(max);

            foreach (var e in list)
            {
                IComparable curr = compare(e);

                if (curr.CompareTo(max) > 0)
                {
                    yield return e;
                    //maxList.Add(e);
                    max = e;
                }
            }

            //return maxList;
        }

        public static IEnumerable<T> LocalMaxima<T>(this IEnumerable<T> list) where T : IComparable
        {
            if (list.Count() == 0)
            {
                Console.WriteLine("Cannot compute on an empty data set");
            }

            List<T> localMaxList = new List<T>();
            int x = 0; //keeps the count of numBefore
            T numBefore = list.First();



            int y = 2; //keeps the count of numAfter
            T numAfter = list.ElementAt(2);

            for (int e = 1; e < list.Count(); e++)
            {
                if (list.ElementAt(e).CompareTo(numBefore) > 0)
                {
                    if (list.ElementAt(e).CompareTo(numAfter) > 0)
                    {
                        //return using deferred execution
                        yield return list.ElementAt(e);
                       // localMaxList.Add(list.ElementAt(e));
                    }
                }

                numBefore = list.ElementAt(x + 1);
                x = x + 1;
                if (y < (list.Count() -1))
                    {
                    numAfter = list.ElementAt(y + 1);
                    y = y + 1;
                    }
            }

            //return localMaxList;
        }

        public static IEnumerable<T> LocalMaxima<T>(this IEnumerable<T> list, Func<T, IComparable> compare)
        {
            if (list.Count() == 0)
            {
                Console.WriteLine("Cannot compute on an empty data set");
            }

            List<T> localMaxList = new List<T>();
            int x = 0; //keeps the count of numBefore
            T numBefore = list.First();



            int y = 2; //keeps the count of numAfter
            T numAfter = list.ElementAt(2);

            for (int e = 1; e < list.Count(); e++)
            {
                IComparable curr = compare(list.ElementAt(e));

                if (curr.CompareTo(numBefore) > 0)
                {
                    if (curr.CompareTo(numAfter) > 0)
                    {
                        //return using deferred execution
                        yield return list.ElementAt(e);
                        // localMaxList.Add(list.ElementAt(e));
                    }
                }

                numBefore = list.ElementAt(x + 1);
                x = x + 1;
                if (y < (list.Count() - 1))
                {
                    numAfter = list.ElementAt(y + 1);
                    y = y + 1;
                }

            }

            //return localMaxList;
        }

        public static bool AtLeastK<T>(this IEnumerable<T> vals, int k, Func<T, bool> numbersBtwn)
        {
            bool answer = false;
            List<T> isTrue = new List<T>();

            foreach (T e in vals)
            {
                if (numbersBtwn(e))
                {
                    isTrue.Add(e);
                }
            }

            if (isTrue.Count() >= k)
            {
                answer = true;
            }
            return answer;
        }

        public static bool AtLeastHalf<T>(this IEnumerable<T> vals, Func<T, bool> numbersBtwn)
        {
            bool answer = false;
            List<T> isTrue = new List<T>();

            int half = vals.Count() / 2;

            foreach (T e in vals)
            {
                if (numbersBtwn(e))
                {
                    isTrue.Add(e);
                }
            }

            if (isTrue.Count() >= half)
            {
                answer = true;
            }
            return answer;
        }

    }
}
