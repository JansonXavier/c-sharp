using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/*

*/

namespace Lobby {
  class Program {
    private const string INPUT_FILE_PATH = "input.txt";

    static void Main() {
      Console.WriteLine("Starting...");
      Console.WriteLine("");

      int totalVoltage = 0;

      foreach (string line in File.ReadLines(INPUT_FILE_PATH)) {
        List<int> batteries = SeparateBatteries(line);
        int maxVoltage = CalculateMaxVoltage(batteries);
        totalVoltage += maxVoltage;
      }

      Console.WriteLine("");
      Console.WriteLine("Result: {0}", totalVoltage);
    }

    static List<int>SeparateBatteries(string line) {
      List<int> batteries = [];

      foreach (char batteryChar in line) {
        int battery = batteryChar - '0';
        batteries.Add(battery);
      }

      return batteries;
    }

    static int CalculateMaxVoltage(List<int> batteries) {
      // start with the 2 numbers on the right, track your largest digit

      // check each number to the left 1 at a time and see if it is larger than your left digit. if so, make that the left digit
        // check also if your prev left digit should now become your right digit

      int left = batteries[^2];
      int right = batteries[^1];

      // start to the left of the left digit
      for (int i = batteries.Count - 3; i >=0; i--) {
        int cur = batteries[i];

        if (cur >= left) {
          (left, cur) = (cur, left);
          
          if (cur > right) {
            right = cur;
          }
        }
      }

      string numStr = left.ToString() + right.ToString();
      Console.WriteLine(numStr);
      return int.Parse(numStr);
    }
  }
}

