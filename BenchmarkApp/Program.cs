﻿using BenchmarkApp;
using BenchmarkDotNet.Running;

class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<SortingAlgorithms>();
    }
}