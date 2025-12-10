using System;
using System.IO;

namespace SecretEntranceTwo {
  class Program {
    private static int curNum = 50;
    private static int count = 0;

    static void Main() {
      Console.WriteLine("Starting...");
      Console.WriteLine("");

      foreach (string line in File.ReadLines("input.txt")) {
        char direction = line[0];
        string distanceString = line[1..];
        int distance = int.Parse(distanceString);

        (int newNum, int countInc) = CalculateNewNumAndCountInc(curNum, direction, distance);

        curNum = newNum;
        count += countInc;
      }

      Console.WriteLine(count);
      Console.WriteLine("Done!");
      Console.WriteLine("");
    }

    static (int newNum, int countInc) CalculateNewNumAndCountInc(int curNum, char direction, int distance) {
      if (direction == 'L') {
        distance *= -1;
      }

      int newNum = curNum + distance;
      int countInc = 0;

      // Count landing on 0
      if (newNum == 0) {
        countInc = 1;
      }

      if (newNum < 0) {
        countInc += newNum * -1 / 100;

        // add 1 to account for passing 0 the first time only if we didn't start at 0. If we started at 0 then it would have been added to the count on the previous turn
        if (curNum != 0 ) {
          countInc += 1;
        }

        newNum %= 100;

        if (newNum != 0) {
          newNum += 100;
        }
      } else {
        countInc += newNum / 100;
        newNum %= 100;
      }

      Console.WriteLine("{0} {1} {2}", distance, newNum, countInc);
      
      return (newNum, countInc);
    }
  }
}
