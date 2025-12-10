using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace GiftShop {
  class Program {
    static void Main() {
      Console.WriteLine("Starting...");

      long sum = 0;

      (long, long)[] ranges = ReadRangesFromInput();

      foreach ((long, long) range in ranges) {
        (long min, long max) = range;
        long curSum = SumOfInvalidIdsInRange(min, max);

        sum += curSum;
      }

      Console.WriteLine(sum);
    }

    static (long, long)[] ReadRangesFromInput() {
      Console.WriteLine("Reading input");

      // read file and split ranges
      string rawInput = File.ReadAllText("input.txt");
      string[] rawRanges = rawInput.Split(",");

      // parse ranges into long
      (long, long)[] ranges = rawRanges.Select(range => {
        string[] bounds = range.Split("-");
        return (long.Parse(bounds[0]), long.Parse(bounds[1]));
      }).ToArray();

      return ranges;
    }

    // build invalid ids by going from 1 to ("num" + "num" > max)
    // for each num, repeat it until it is at least min.length and then keep adding to the repeat until it is larger than max
    // for each final num, if in range, add to sum
    // prevent duplicates by tracking seen numbers
    static long SumOfInvalidIdsInRange(long min, long max) {
      long sum = 0;
      HashSet<long> seen = [];

      string curNum = "1";
      long curFinalNum = long.Parse(curNum + curNum);

      while (curFinalNum <= max) {
        while (curFinalNum <= max) {

          if (!seen.Contains(curFinalNum)
              && curFinalNum >= min
              && curFinalNum <= max) {
            seen.Add(curFinalNum);
            sum += curFinalNum;
          }

          curFinalNum = long.Parse(curFinalNum.ToString() + curNum);
        }

        long nextNum = long.Parse(curNum) + 1;
        curNum = nextNum.ToString();
        curFinalNum = long.Parse(curNum + curNum);
      }

      return sum;
    }
  }
}

