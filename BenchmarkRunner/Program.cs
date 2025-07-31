// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using DapperKaggleProject.Services;

BenchmarkRunner.Run<GameEventsBenchmark>();