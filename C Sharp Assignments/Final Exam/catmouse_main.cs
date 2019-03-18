//By Michael Rozsypal
// wolfken81@gmail.com or mrozsypal81@gmail.com
// CPSC223N 
// Final Exam
//

using System;
using System.Windows.Forms;           
public class Ricochet
{  public static void Main()
   {  System.Console.WriteLine("The ricochet ball program will begin now.");
        System.Random randomgeneratorv = new System.Random();
        System.Random randomgeneratorw = new System.Random();
       double randomnumberv = randomgeneratorv.NextDouble();
       double randomnumberw = randomgeneratorw.NextDouble();
      double speed = 50.0; 
      Ricochet_interface_form ricochet_application = new Ricochet_interface_form(speed,randomnumberv,randomnumberw);
      Application.Run(ricochet_application);
      System.Console.WriteLine("This ricochet ball program has ended.  Bye.");
   }
}
