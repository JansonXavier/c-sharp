using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace TrashCompactor {
  class Program {
    static List<List<int>> numGrid = [];
    static List<string> ops = [];
    static void Main() {
      Console.WriteLine("TrashCompactor");

      BuildProblems();

      long sum = 0;
      for (int i = 0; i < numGrid[0].Count; i++) {
        sum += Operate(i);
      }

      Console.WriteLine(sum);
    }

    static void BuildProblems() {
      foreach (string line in File.ReadLines("input.txt")) {
        string[] parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        List<int> ints = [];
        List<string> chars = [];
        foreach (string part in parts) {
          if (int.TryParse(part, out int i)) {
            ints.Add(i);
          } else {
            chars.Add(part);
          }
        }

        if (ints.Count > 0) {
          numGrid.Add(ints);
        } else {
          ops = chars;
        }
      }
    }
  
    static long Operate(int i) {
      string op = ops[i];
      long ans = op == "*" ? 1 : 0;

      foreach (List<int> nums in numGrid) {
        if (op == "*") {
          ans *= nums[i];
        } else {
          ans += nums[i];
        }
      }

      return ans;
    }
  }
}

