using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace TrashCompactor {
  class Program {
    static List<List<int>> numGrid = [];
    static List<char> ops = [];
    static void Main() {
      Console.WriteLine("TrashCompactor");

      BuildProblems();

      long sum = 0;
      for (int i = 0; i < ops.Count; i++) {
        sum += Operate(i);
      }

      Console.WriteLine(sum);
    }

    static void BuildProblems() {
      string[] lines = File.ReadAllLines("input.txt");
      string opLine = lines[^1];

      int curIdx = 0;
      char curOp;

      int problemIdx = -1;
      while (curIdx < opLine.Length) {
        if (opLine[curIdx] != ' ') {
          curOp = opLine[curIdx];
          problemIdx++;

          numGrid.Add([]);
          ops.Add(curOp);
        }

        string numStr = "";
        for (int i = 0; i < lines.Length - 1; i++) {
          numStr += lines[i][curIdx];
        }
        numStr = numStr.Trim();

        if (numStr != "") {
          numGrid[problemIdx].Add(int.Parse(numStr));
        }
        curIdx++;
      }
    }
  
    static long Operate(int i) {
      List<int> nums = numGrid[i];
      char op = ops[i];

      long ans = op == '*' ? 1 : 0;

      foreach (int num in nums) {
        Console.Write("{0} ", num);

        if (op == '*') {
          ans *= num;
        } else {
          ans += num;
        }
      }

      Console.Write("{0} {1}\n", op, ans);
      return ans;
    }
  }
}

