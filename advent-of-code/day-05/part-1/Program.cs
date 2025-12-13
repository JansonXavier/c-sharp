using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace Cafeteria {
  class Program {
    static readonly List<(int, int)> storedRanges = [];
    static void Main() {
      Console.WriteLine("Starting...");

      BruteForce();

      // bool isBuildingRanges = true;
      // foreach (string line in File.ReadLines("input.txt")) {
      //   if (line == "") {
      //     isBuildingRanges = false;
      //     continue;
      //   } else if (isBuildingRanges) {
      //     BuildRanges();
      //   } else {
      //     FindIdRangeIndex();
      //   }
      // }


    }

    // see if this joins with existing ranges
      // check start and end
    // if so, add to range
    // if not, add to correct position in storedRanges
    // static void BuildRanges() {

    // }

    // check if we are below the lowest start or highest end
    // binary search the starting value of the ranges with l/r pointers?
    // when l/r point at the same thing, check if we are in that range
    // (is it in the range, index)
    // static (bool, int) FindIdRangeIndex(int id) {
      
    // }

    /*
    Starting...
    Brute forcing...
    Done building: 3
    Count: 758
    total: 5, diff: 2
    */
    static void BruteForce() {
      Console.WriteLine("Brute forcing...");

      Stopwatch stopwatch = Stopwatch.StartNew();
      long buildTime = -1;
      long totalTime;

      int count = 0;
      List<(long, long)> ranges = [];

      bool isBuildingRanges = true;
      foreach (string line in File.ReadLines("input.txt")) {
        if (line == "") {
          isBuildingRanges = false;
          buildTime = stopwatch.ElapsedMilliseconds;
          Console.WriteLine("Done building: {0}", buildTime);
        } else if (isBuildingRanges) {
          string[] parts = line.Split("-");
          long min = long.Parse(parts[0]);
          long max = long.Parse(parts[1]);
          ranges.Add((min, max));
        } else {
          long id = long.Parse(line);
          foreach ((long min, long max) in ranges) {
            if (id >= min && id <= max) {
              count++;
              break;
            }
          }
        }
      }

      totalTime = stopwatch.ElapsedMilliseconds;

      Console.WriteLine("Count: {0}", count);
      Console.WriteLine("total: {0}, diff: {1}", totalTime, totalTime - buildTime);
    }
  }
}

