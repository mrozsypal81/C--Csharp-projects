
/*
Author: Michael Rozsypal
prefered email wolfken81@gmail.com
CPSC223n
assignment 3
due October 11
This program runs a traffic light with red yellow and green lights
Assignment3Main.cs
*/


using System;
using System.Windows.Forms;
public class Trafficlight
{  public static void Main()
   {  System.Console.WriteLine("Assignment 3 will now start");
        trafficlightframe lightapplication = new trafficlightframe();
      Application.Run(lightapplication);
      System.Console.WriteLine("Assignment 3 has ended");
   }
}
