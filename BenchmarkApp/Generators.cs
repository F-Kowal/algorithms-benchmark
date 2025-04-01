using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkApp
{
    public static class Generators
    {
        private static readonly Random rand = new Random();

        public static int[] GenerateRandom(int size, int minVal, int maxVal)
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = rand.Next(minVal, maxVal + 1);
            }
            return array;
        }

        public static int[] GenerateSorted(int size, int minVal, int maxVal)
        {
            int[] array = GenerateRandom(size, minVal, maxVal);
            Array.Sort(array);
            return array;
        }

        public static int[] GenerateReversed(int size, int minVal, int maxVal)
        {
            int[] array = GenerateSorted(size, minVal, maxVal);
            Array.Reverse(array);
            return array;
        }

        public static int[] GenerateAlmostSorted(int size, int minVal, int maxVal, int swaps = 5)
        {
            int[] array = GenerateSorted(size, minVal, maxVal);
            for (int i = 0; i < swaps; i++)
            {
                int index1 = rand.Next(0, size);
                int index2 = rand.Next(0, size);
                (array[index1], array[index2]) = (array[index2], array[index1]);
            }
            return array;
        }

        public static int[] GenerateFewUnique(int size, int uniqueValues)
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = rand.Next(1, uniqueValues + 1);
            }
            return array;
        }
    }
}
