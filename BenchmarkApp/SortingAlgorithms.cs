using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkApp
{
    public class SortingAlgorithms
    {
        private int[] data;

        [Params(10, 1000, 100000)]
        public int Size;

        [Params("Random", "Sorted", "Reversed", "AlmostSorted", "FewUnique")]
        public string DataType;

        [GlobalSetup]
        public void Setup()
        {
            switch (DataType)
            {
                case "Random":
                    data = Generators.GenerateRandom(Size, 0, 10000);
                    break;
                case "Sorted":
                    data = Generators.GenerateSorted(Size, 0, 10000);
                    break;
                case "Reversed":
                    data = Generators.GenerateReversed(Size, 0, 10000);
                    break;
                case "AlmostSorted":
                    data = Generators.GenerateAlmostSorted(Size, 0, 10000);
                    break;
                case "FewUnique":
                    data = Generators.GenerateFewUnique(Size, 10);
                    break;
            }
        }

        [Benchmark]
        public void InsertionSort() => InsertionSortAlgorithm((int[])data.Clone());

        [Benchmark]
        public void MergeSort() => MergeSortAlgorithm((int[])data.Clone());

        [Benchmark]
        public void QuickSortClassical() => QuickSortAlgorithm((int[])data.Clone(), 0, data.Length - 1);

        [Benchmark]
        public void QuickSortLibrary() => Array.Sort((int[])data.Clone());

        public static void InsertionSortAlgorithm(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int key = array[i];
                int j = i - 1;
                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = key;
            }
        }

        public static void MergeSortAlgorithm(int[] array)
        {
            if (array.Length <= 1) return;

            int mid = array.Length / 2;
            int[] left = new int[mid];
            int[] right = new int[array.Length - mid];

            Array.Copy(array, left, mid);
            Array.Copy(array, mid, right, 0, array.Length - mid);

            MergeSortAlgorithm(left);
            MergeSortAlgorithm(right);
            Merge(array, left, right);
        }

        private static void Merge(int[] array, int[] left, int[] right)
        {
            int i = 0, j = 0, k = 0;
            while (i < left.Length && j < right.Length)
            {
                if (left[i] <= right[j]) array[k++] = left[i++];
                else array[k++] = right[j++];
            }
            while (i < left.Length) array[k++] = left[i++];
            while (j < right.Length) array[k++] = right[j++];
        }

        public static void QuickSortAlgorithm(int[] array, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(array, low, high);
                QuickSortAlgorithm(array, low, pi - 1);
                QuickSortAlgorithm(array, pi + 1, high);
            }
        }

        private static int Partition(int[] array, int low, int high)
        {
            int pivot = array[high];
            int i = low - 1;
            for (int j = low; j < high; j++)
            {
                if (array[j] < pivot)
                {
                    i++;
                    (array[i], array[j]) = (array[j], array[i]);
                }
            }
            (array[i + 1], array[high]) = (array[high], array[i + 1]);
            return i + 1;
        }
    }
}
