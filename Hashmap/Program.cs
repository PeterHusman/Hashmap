using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashmap
{
    class Program
    {
        static void Main(string[] args)
        {
            int length = 5254857;
            int[] lookupTimes = new int[length];
            int[] lookupTimesLater = new int[length];
            Hashmap<string, int> hashmap = new Hashmap<string, int>();

            Random rand = new Random();
            for (int i = 0; i < length - 1; i++)
            {
                string input;
                do
                {
                    input = new string(new char[] { (char)rand.Next(1, 127), (char)rand.Next(1, 127), (char)rand.Next(1, 127) });
                } while (hashmap.Contains(input));
                hashmap.Insert(input, i);

                int j = hashmap[input];
                lookupTimes[i] = hashmap.lastLookupTimeTEMP;
                //Console.WriteLine(hashmap.lastLookupTimeTEMP);
            }
            Console.WriteLine($"Average lookup time was {lookupTimes.Average()}\nMaximum was {lookupTimes.Max()}\nOver {length} trials\nNote: Lookup times were recorded immediately after each entry was added, so they may not reflect the lookup times for the same entry now that the hashmap has finished being constructed.\nEnter a three-character ASCII string to see the lookup time in the completed hashmap.");
            while(true)
            {
                string s = Console.ReadLine();
                int val = hashmap[s];
                Console.WriteLine($"Lookup time was {hashmap.lastLookupTimeTEMP}\nValue was {val}");
            }
        }
    }
}
