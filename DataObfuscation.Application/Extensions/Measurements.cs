using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Console;

namespace DataObfuscation.Application.Extensions
{
    public static class Measurements
    {
        public static TimeSpan Measure(Action action)
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            action();
            sw.Stop();
            return sw.Elapsed;
        }

        public static void PrintSpeedup(TimeSpan baseTime, Dictionary<string, TimeSpan> processingTimes)
        {
            WriteLine("Rodzaj obliczeń\t\t\tCzas trwania\t\t\tPrzyspieszenie");
            foreach (var actionWithMeasure in processingTimes)
            {
                double speedupScale = baseTime.TotalMilliseconds / actionWithMeasure.Value.TotalMilliseconds;
                WriteLine($"{actionWithMeasure.Key}\t\t{actionWithMeasure.Value}\t\t{speedupScale}");
            }
        }
    }
}
