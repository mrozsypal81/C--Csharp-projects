
/*
Author: Michael Rozsypal
prefered email wolfken81@gmail.com
CPSC223n
assignment 2
due september 27
This program runs a tic tac toe game with Xs and Os
Assignment2 Main.cs
*/


using System;
using System.Windows.Forms;
public class TTT
{  public static void Main()
   {  System.Console.WriteLine("Assignment 2 will now start");
        TTTframe TTTapplication = new TTTframe();
      Application.Run(TTTapplication);
      System.Console.WriteLine("Assignment 2 has ended");
   }
}
