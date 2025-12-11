using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace LobbyTwo {
  class Program {
    static int numBatteries = 12;
    static void Main() {
      Console.WriteLine("Starting...");
      Console.WriteLine("");

      long total = 0;

      foreach (string line in File.ReadLines("input.txt")) {
        List<int> batteries = ReadBatteries(line);
        long voltage = CalculateMaxVoltage(batteries);
        total += voltage;
      }

      Console.WriteLine("");
      Console.WriteLine("Result {0}", total);
    }

    static List<int> ReadBatteries(string line) {
      List<int> batteries = [];

      foreach (char batteryChar in line) {
        int battery = batteryChar - '0';
        batteries.Add(battery);
      }

      return batteries;
    }

    static long CalculateMaxVoltage(List<int> batteries) {
      List<int> activeBatteries = batteries[^numBatteries..];

      for (int i = batteries.Count - (numBatteries + 1); i >= 0; i--) {
        int cur = batteries[i];

        int j = 0;
        // greater than or equal to because then a smaller number can be replaced with what's currently there
        while (j < activeBatteries.Count && cur >= activeBatteries[j]) {
          (cur, activeBatteries[j]) = (activeBatteries[j], cur);

          j++;
        }
      }

      string maxVoltStr = "";

      foreach (int num in activeBatteries) {
        maxVoltStr += num.ToString();
      }

      long maxVolt = long.Parse(maxVoltStr);

      Console.WriteLine(maxVolt);

      return maxVolt;
    }
  }
}
