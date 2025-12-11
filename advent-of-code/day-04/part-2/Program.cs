using System;
using System.Collections.Generic;
using System.IO;

namespace PrintingDepartmentTwo {
  class Program {
    static readonly List<List<int>> grid = [];
    static readonly Queue<(int, int)> q = [];
    static int total = 0;

    static void Main() {
      BuildGrid();

      while (q.Count != 0) {
        AttemptRollRemoval();
      }

      Console.WriteLine(total);
    }

    static List<List<int>> BuildGrid() {
      string[] lines = File.ReadAllLines("input.txt");

      for (int i = 0; i < lines.Length; i++) {
        List<int> row = [];
        string line = lines[i];

        for (int j = 0; j < line.Length; j++) {
          char cell = line[j];
          if (cell == '.') {
            row.Add(0);
          } else {
            row.Add(1);
            q.Enqueue((i, j));
          }
        }

        grid.Add(row);
      }

      return grid;
    }

    static void AttemptRollRemoval() {
      // make sure roll is still there
      (int i, int j) = q.Dequeue();

      if (grid[i][j] == 0) {
        return;
      }

      // see if we can remove it
      List<(int, int)> mem = [];
      bool canRemove = IsAccessable(mem, i, j);

      if (canRemove) {
        // remove it
        grid[i][j] = 0;
        total++;

        // add adjacent roll coordinates to queue
        foreach ((int m, int n) in mem) {
          q.Enqueue((m, n));
        }
      }
    }

    static bool IsAccessable(List<(int, int)> mem, int i, int j) {
      int count = 0;

      for (int m = i - 1; m <= i + 1; m++) {
        if (m < 0 || m >= grid.Count) {
          continue;
        }

        for (int n = j - 1; n <= j + 1; n++) {
          if (
            (m == i && n == j)
            || n < 0
            || n >= grid[0].Count
          ) {
            continue;
          }

          if (grid[m][n] == 1) {
            mem.Add((m, n));
            count++;
          }
        }
      }

      return count < 4;
    }
  }
}
