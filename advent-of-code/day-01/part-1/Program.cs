using System;
using System.IO;


namespace SecretEntrance
{
  class Program
  {
    private const string INPUT_FILE_PATH = "input.txt";
    private static int count = 0;

    static void Main()
    {
      Console.WriteLine("Starting...");

      int curNum = 50;

      foreach (string line in File.ReadLines(INPUT_FILE_PATH))
      {
        char direction = line[0];
        string distanceStr = line[1..];
        int distance = int.Parse(distanceStr);

        curNum = GetNewDialPosition(curNum, direction, distance);

        if (curNum == 0) {
          count++;
        }
      }

      Console.WriteLine(count);
    }

    static int GetNewDialPosition(int curNum, char direction, int distance)
    {
      // Update distance with direction
      if (direction == 'L')
      {
        distance *= -1;
      }

      // Move dial
      int newNum = (curNum + distance) % 100;

      // If new num is negative, 100 - newNum will give us current position
      if (newNum < 0)
      {
        newNum += 100;
      }

      return newNum;
    }
  }
}
