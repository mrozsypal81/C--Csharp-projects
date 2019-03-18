//Ruler:=1=========2=========3=========4=========5=========6=========7=========8=========9=========0=========1=========2=========3**
//Author: F. Holliday
//Author's email: holliday@fullerton.edu
//Course: CPSC223N
//Assignment number: 01
//Due date: 2018-Apr-15
//Date last updated: 2017-July-28
//Number of source files in this program: 3
//Names of source files in this program: Sinewave.cs, Sineframe.cs, Sinecurve.cs
//Purposes of this entire program:  
//1.  Show a particle traveling along the path of a sine curve 
//2.  The particle moves at constant linear speed.  That means the particle travels the same distance in any two time periods
//    of equal size.
//3.  Demonstrate how to use a bitmap in memory to hold the history of where the particle has been.
//
//Development status:  This program is finished.
//
//Development platform: Bash using Mono4.2.1.102 on Xubuntu16.4 
//
//Development safety: No proprietary software was used in the development of this program.
//
//Education: This program is for the students enrolled in CPSC223n.  This program and other programs in the series are your book in 
//this course.  Study the programs as you would study an assigned book made of paper.
//
//Inputs: The user must enter a positive float number representing amplitude.  If A is the amplitude then A in the range 
//0.0 < A < 5.0 is reasonable.  The user must enter a positive float number representing the period of the sine function.  
//A reasonable input for period is 2π = 6.2832, but other positive values may be entered.
//
//System requirements: A Linux distro.  Examples are Xubuntu, Lubuntu, and others.
//                     A video card capable of HD resolution: 1920x1080
//                     Installed C# package known as mono-complete.
//                     Other related packkages such as mono-mcs are insufficient to compile and run this program.
//
//
//Name of this file: Sinewave.cs
//Purpose of this file: Launch the window showing the form where the animation will occur.
//
//
//This program includes three source files, which should be compiled in the order specified below in order to satisfy dependencies.
//  1. Sinecurve.cs         compiles into library file Sinecurve.dll
//  2. Sineframe.cs         compiles into library file Sineframe.dll
//  3. Sinewave.cs          compiles and links with the two dll files above to create Sine.exe
//
//
//Compile (& link) this file: 
//mcs -target:exe Sinewave.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:Sineframe.dll -out:Sine.exe

//Hardcopy: this source code is best viewed in 7 point monospaced font using portrait orientation.




using System;
using System.Windows.Forms;            //Needed for "Application" near the end of Main function.
public class Sinewave
{  public static void Main()
   {  System.Console.WriteLine("The Sine Wave program has begun.");
      Sineframe sineapp = new Sineframe();
      Application.Run(sineapp);
      System.Console.WriteLine("This Sine Wave program has ended.  Bye.");
   }//End of Main function
}//End of Sinewave class




//Mathematics for the sine function.
//The general sine function is y = A*sin(B*x).  In that case A = amplitude and P = 2π/B is the period.

//The domain of y = A*sin(B*x) is taken to be the non-negative real numbers.

//Some scaling is necessary.  We map 1 mathematical unit to 100 pixels.  The result is that whereas the mathematical function 
//y = sin(x) has an amplitude of 1.0 the graphical image will have an amplitude of 100 pixels.  Likewise, the mathematical
//function y = sin(x) has a natural period of 2π = 6.2832, but the graphical image of that function will have a period of 
//2π*100 = 628.32.  Simply stated: the graphic image has been expanded by a factor of 100 in both vertical and horizontal 
//directions.

//True constant linear speed is difficult to attain.  Let γ(t) be a function defined on [a;b] and mapping into RxR.  One can impose
//restrictions that γ(t) be continuous, differentiable, smooth, etc.  Let C>0 be a positive constant.  Let t be in the domain of
//γ(t).  Then it is computationally expensive to find another value s in the domain of γ(t), s>t, such that the arc length distance
//from γ(t) to γ(s) equals C.  At this point we abandon the attempt to attain true constant linear speed.

//We can program a close approximation to constant linear speed as described here.  Let γ(t) be function defined on [a;b] and 
//mapping into RxR, and assume that the function is differentiable everywhere with non-zero derivative everywhere.  Think of 
//tracing the sine curve in real time.  We want that particle to move a fixed distance in any two time periods of equal duration.
//Here is how to accomplish this.  Select a very small positive number δ.  Assume that the tracing particle is now at a pixel at
//point γ(t) = (x,y).  The job is to find a parameter u>t such that the arc distance from γ(t) to γ(u) is δ.  As mentioned in the
//previous paragraph that calculation is not feasible.  However, we can find a u that approximates the required arc distance.  
//Specifically, let u = t + δ/|γ'(t)|.  Then the cord from γ(t) to γ(u), i.e. γ(u)-γ(t), is exactly δ.  Since the length of the
//chord from γ(t) to γ(u) is approximately equal to the length of the curve from γ(t) to γ(u) when u is close to t, we accept this
//strategy of "equal chord lengths" as a good enough approximation to "equal arc lengths".

//This program uses this technique of equal chord lengths to approximate equal arc lengths.  You, the reader of this description,
//are invited to run this program.  Try to detect any noticeable changes in speed.  Human eyes cannot detect any changes in speed.

//In this program there are three quantities each playing an important role.
//V = Linear velocity (pixels per second)
//C = Length of the chord = distance traveled between successive tics of the clock (pixels)
//U = Update rate of the coordinates of the particle (Hz)

//These values must satisfy the equation V = C*U.  Thus, if any two of the three are known then the other can be easily computed.
//Example.  Suppose V = 90 pixels be second, C = 0.8 pixels.  Then U can be computed: U = V/C = 112.5 Hz.
//We can then find the update interval = 1000ms/112.5Hz = 8.8888ms.  Since the clocks of C# can accept only a whole number of 
//milliseconds, that number must be rounded to update interval = 9ms.

