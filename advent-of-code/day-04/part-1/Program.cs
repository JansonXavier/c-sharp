using System;
using System.Collections.Generic;
using System.IO;

namespace PrintingDepartment {
  class Program {
    static void Main() {
      int total = 0;
      List<List<int>> grid = BuildGrid();

      for (int i = 0; i < grid.Count; i++) {
        for (int j = 0; j < grid[i].Count; j++) {
          if (grid[i][j] == 1 && IsAccessable(grid, i, j)) {
            total++;
            // Console.WriteLine("i:{0} j:{1} total:{2}", i, j, total);
          }
        }
      }

      Console.WriteLine(total);
    }

    static List<List<int>> BuildGrid() {
      string[] lines = File.ReadAllLines("input.txt");
      List<List<int>> grid = [];

      foreach (string line in lines) {
        List<int> row = [];
        foreach (char cell in line) {
          if (cell == '.') {
            row.Add(0);
          } else {
            row.Add(1);
          }
        }
        grid.Add(row);
      }

      return grid;
    }

    static bool IsAccessable(List<List<int>> grid, int i, int j) {
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
            count++;
          }
        }
      }

      return count < 4;
    }
  }
}
