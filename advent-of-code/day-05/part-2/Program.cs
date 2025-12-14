using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CafeteriaTwo {
  class Program {
    static readonly List<(long, long)> ranges = [];
    static void Main() {
      BuildRanges();

      ranges.Sort((a, b) => a.Item1.CompareTo(b.Item1));

      RemoveEncapsulatedRanges();

      LogRanges();

      long count = 0;
      foreach ((long, long) range in ranges) {
        count += range.Item2 - range.Item1 + 1; // +1 to account for ranges being inclusive
      }
      Console.WriteLine(count);
    }

    // 340471279829426
    // 340471279829523

    // 343143696885053
    // 345486116638844
    static void BuildRanges() {
      Console.WriteLine("building...");
      Stopwatch stopwatch = Stopwatch.StartNew();

      foreach (string line in File.ReadLines("input.txt")) {
        if (line == "") break;

        string[] parts = line.Split("-");
        long min = long.Parse(parts[0]);
        long max = long.Parse(parts[1]);

        int minRange;
        int maxRange;
        try {
          minRange = FindInRanges(min);
          maxRange = FindInRanges(max);
        } catch {
          Console.WriteLine("Failed to find range: {0} {1}", min, max);
          break;
        }

        if (minRange == -1 && maxRange == -1) {
          ranges.Add((min, max));
        } else if (minRange == maxRange) {
          // they are fully handled by an existing range
          continue;
        } else if (minRange == -1) {
          try {
            long existingMax = ranges[maxRange].Item2;
            ranges[maxRange] = (min, existingMax);
          } catch {
            Console.WriteLine("Failed 3: {0} {1} {2} {3}", min, max, minRange, maxRange);
            break;
          }
        } else if (maxRange == -1) {
          try {
            long existingMin = ranges[minRange].Item1;
            ranges[minRange] = (existingMin, max);
          } catch {
            Console.WriteLine("Failed 4: {0} {1} {2} {3}", min, max, minRange, maxRange);
            break;
          }
        } else {
          try {
          // min and max are in different ranges
          long newMin = ranges[minRange].Item1;
          long newMax = ranges[maxRange].Item2;

          if (minRange < maxRange) {
            (minRange, maxRange) = (maxRange, minRange);
          }

          ranges.RemoveAt(minRange);
          ranges.RemoveAt(maxRange);

          ranges.Add((newMin, newMax));
          } catch {
            Console.WriteLine("{0}, {1}", ranges[0], ranges[1]);
            Console.WriteLine("Failed 5: {0} {1} {2} {3}", min, max, minRange, maxRange);
            break;
          }
        }
      }

      long time = stopwatch.ElapsedMilliseconds;
      stopwatch.Stop();
      Console.WriteLine("Done building: {0}", time);
    }

    static void RemoveEncapsulatedRanges() {
      Console.WriteLine("Removing duplicates: {0}", ranges.Count);
      Stopwatch stopwatch = Stopwatch.StartNew();

      for (int i = ranges.Count - 1; i >= 0; i--) {
        (long min, long max) = ranges[i];

        int minRange = FindInRanges(min);
        int maxRange = FindInRanges(max);

        if (minRange != i && maxRange != i) {
          Console.WriteLine(i);
          ranges.RemoveAt(i);
          i++;
        } else if (minRange != i || maxRange != i) {
          Console.WriteLine("How {0} {1} {2} {3} {4}", min, max, minRange, maxRange, i);
        }
      }

      long doneTime = stopwatch.ElapsedMilliseconds;
      stopwatch.Stop();
      Console.WriteLine("Removed duplicates: {0} {1}", ranges.Count, doneTime);
    }

    static int FindInRanges(long id) {
      for (int i = 0; i < ranges.Count; i++) {
        (long min, long max) = ranges[i];

        if (id >= min && id <= max) {
          return i;
        }
      }

      return -1;
    }
  
    static void LogRanges() {
      Console.WriteLine("");
      long prevMax = 0;
      foreach ((long min, long max) in ranges) {
        if (min - prevMax <= 0) {
          Console.WriteLine("{0} - {1} | {2} : {3}", min, max, prevMax, min - prevMax);
        }
        prevMax = max;
      }
      Console.WriteLine("");
    }
  }
}


