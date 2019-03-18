//****************************************************************************************************************************
//Program name: "Archimedes' Spiral".  This program shows the invisible tip of a marker drawing a spiral curve according to  *
//the equation: r = a+bθ
//Copyright (C) 2017  Floyd Holliday                                                                                         *
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License  *
//version 3 as published by the Free Software Foundation.                                                                    *
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied         *
//warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.     *
//A copy of the GNU General Public License v3 is available here:  <https://www.gnu.org/licenses/>.                           *
//****************************************************************************************************************************




//Ruler:=1=========2=========3=========4=========5=========6=========7=========8=========9=========0=========1=========2=========3**

//Author: Floyd Holliday
//Mail: holliday@fullerton.edu

//Program name: Archimedes' Spiral
//Programming language: C#
//Date project began: 2017-April-15.
//Date of last update: 2017-Nov-10.

//Purpose: This program demonstrates the drawing of the curve r = a+bθ where typically a≥0, b≥0, θ≥0.  The curve drawing action
//maintains constant speed as it is drawn.

//Special feature: The spiral curve is actually drawn into a block of memory, and that block of memory is fast copied to the
//graphical display area once with each tic of the refresh clock.

//Nice feature: The labels on the polor axis are written via a loop structure.

//Files in this program: Archmain.cs, Archframe.cs, Archspiral.cs, run.sh

//Reuse: Students of C# are invited to reuse this program to create other programs that draw the fascinating curves of other
//functions.

//Status: This program has been tested many times, and is considered done.  The author is considering a few small improvements
//such as input boxes in the control panel that allow the user to input quantities linear speed, motion clock rate, and refresh
//clock rate.  At the present time those values are hard coded in the source code.

//This file name: Archmain.cs
//Compile & link this file: 
//mcs -target:exe Archmain.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:Archframe.dll -out:Arch.exe

//Best configuration for printing: 7-point monospaced font with portrait orientation

//System requirements: Linux system with Bash shell and package mono-complete installed.





using System;
using System.Windows.Forms;            //Needed for "Application" near the end of Main function.
public class Archie
{  public static void Main()
   {  System.Console.WriteLine("The Archimedean spiral program has begun.");
      Spiralframe archimedes = new Spiralframe();
      Application.Run(archimedes);
      System.Console.WriteLine("This Archimedean spiral program has ended.  Bye.");
   }//End of Main function
}//End of Archie class







//Ruler:=1=========2=========3=========4=========5=========6=========7=========8=========9=========0=========1=========2=========3**

//The mathematics of the Archimedean Spiral.

//In polar coordinates the defining equation is: r = a+bθ where usually a≥0, b≥0, θ≥0.
//If b=0 then the result is the degenerate spiral, namely a circle of radius a.
//The value of a indicates the starting point of the spiral assuming θ≥0.  That is, the start will be at the point (a,0).
//Typically a is taken to be 0 in order to start the spiral at the pole (cartesian origin).

//In cartesian coordinates the defining equation of the spiral is
//     γ(t) = (r(t)cos(t),r(t)sin(t)) = ((a+bt)cos(t),(a+bt)sin(t)), and generally t is taken in the domain t≥0.

//Calculus tells us the tangent vector to the point γ(t) on the curve is given by γ'(t) = (bcost-(a+bt)sint,bsint+(a+bt)cost).
//From here we compute the magnitude of γ'(t) squared: |γ'(t)|^2 = b^2 + (a+bt)^2.
//For this program we will assume a to be zero because a non-zero a contributes nothing to the spiral other than a new starting 
//position.  With this assumption the equation becomes |γ'(t)|^2 = b^2 * (1+t^2).  From this quantity we compute the magnitude
//of the tangent vector, namely: |γ'(t)|, which is the key to obtaining constant speed.

//Side note: The asymptotic spiral has polar equation r = [a/(θ+1)]*θ, and where generally a>0.  As θ approaches infinity the 
//spiral approaches the circle r = a.

//Side note: The logarithmic spiral has equation r = ae^(bθ), and a≥0, b≥0.



