using System;
using System.IO;
using System.Linq;

// Idea here is that we just need to get the first half of the smaller number and concat it to itself. then we keep incrementing that first half and concating until it's larger than the larger number. any numbers that were between the range were invalid ids.

namespace GiftShop {
  class Program {
    static void Main() {
      Console.WriteLine("Starting...");

      long sum = 0;

      (long, long)[] ranges = ReadRangesFromInput();

      foreach ((long min, long max) in ranges) {
        Console.WriteLine("{0} {1}", min, max);

        string minString = min.ToString();

        // round down to make sure not to miss numbers (998-1012 as an example)
        int halfLength = minString.Length / 2;

        // if halfLength is 0, we need to start with the smallest possible number (1) since min is smaller
        string firstHalf = halfLength == 0 ? "1" : minString[..halfLength];

        long curHalf = long.Parse(firstHalf);
        string curString = curHalf.ToString() + curHalf.ToString();
        long curNum = long.Parse(curString);

        while (curNum <= max) {
          if (curNum >= min && curNum <= max) {
            Console.WriteLine(curNum);
            sum += curNum;
          }

          curHalf++;
          curString = curHalf.ToString() + curHalf.ToString();
          curNum = long.Parse(curString);
        }

        Console.WriteLine(sum);
        Console.WriteLine("\n");
      }
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
  }
}

