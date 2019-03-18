
/*
Author: Michael Rozsypal
prefered email wolfken81@gmail.com
CPSC223n
assignment 1
due september 11
This program will draw 3 different shapes a rectangle a circle and a triangle in three different colors red blue and green
Assignment1Main.cs
*/


using System;
using System.Windows.Forms;
public class Drawshapes
{  public static void Main()
   {  System.Console.WriteLine("Assignment 1 will now start");
      Drawshapesframe shapeapplication = new Drawshapesframe();
      Application.Run(shapeapplication);
      System.Console.WriteLine("Assignment 1 has ended");
   }
}
