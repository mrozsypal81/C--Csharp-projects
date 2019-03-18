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

//This file name: Archspiral.cs
//Compile this file: 
//mcs -target:library Archspiral.cs -out:Archspiral.dll

//Best configuration for printing: 7-point monospaced font with portrait orientation

//System requirements: Linux system with Bash shell and package mono-complete installed.


 
   //Declare variables for the spiral function r = a + b*t.
   // trying to make r = sin(4t) a = 1 b = 4
   //  x(t) = sin(4t)cos(t) y(t) = sin(4t)sin(t)
   // derivatives: 'x = 4cos(4t)cos(t)-sin(4t)sin(t) 'y = 4cos(4t)sin(t)+sin(4t)cos(t)
 

public class Spiral_algorithms
{   private double magnitude_of_tangent_vector_squared;
    private double magnitude_of_tangent_vector;
    //Note that all values in the method below are mathematical units.  None of the values have been scaled for output by a computer.
    public void get_next_coordinates(          //Constant 'a' in the equation r = a+b*t
                                     double b_coefficient,           //Constant 'b' in the equation r = a+b*t
                                     double distance_1_tic,          //The distance the brush will move in one tic of the spiral clock.
                                     ref double t,                   //The variable 't' in the equation r = a+b*t
                                     out double new_x,               //The next x coordinate of the spiral
                                     out double new_y)               //The next y coordinate of the spiral
       {magnitude_of_tangent_vector_squared = (b_coefficient*System.Math.Cos(b_coefficient*t)*System.Math.Cos(t)-System.Math.Sin(b_coefficient*t)*System.Math.Sin(t))*(b_coefficient*System.Math.Cos(b_coefficient*t)*System.Math.Cos(t)-System.Math.Sin(b_coefficient*t)*System.Math.Sin(t));
        magnitude_of_tangent_vector_squared = magnitude_of_tangent_vector_squared + ((b_coefficient*System.Math.Cos(b_coefficient*t)*System.Math.Sin(t)-System.Math.Sin(b_coefficient*t)*System.Math.Cos(t))*(b_coefficient*System.Math.Cos(b_coefficient*t)*System.Math.Sin(t)-System.Math.Sin(b_coefficient*t)*System.Math.Cos(t)));
        magnitude_of_tangent_vector = System.Math.Sqrt(magnitude_of_tangent_vector_squared);
        t = t + distance_1_tic/magnitude_of_tangent_vector;
        new_x = (System.Math.Sin(b_coefficient*t))*System.Math.Cos(t);
        new_y = (System.Math.Sin(b_coefficient*t))*System.Math.Sin(t);
       }//End of get_next_coordinates
       
}//End  of class Spiral_algorithms

//Ruler:=1=========2=========3=========4=========5=========6=========7=========8=========9=========0=========1=========2=========3**






//To: Students in the 223N class
//This file contains an implementation of the get_next_coordinates method used with the curve defined by r = a+b*t, which has corresponding cartesian
//coordinate functions: x = r*cos(t) = (a+b*t)*cos(t) and y = r*sin(t) = (a+b*t)*sin(t).
// (bcos(t) - (a+b*t)sin(t))^2 + (bsin(t)+(a+b*t)cos(t))^2 
//The tangent vector is r'(t) = (x'(t),y'(t)), and the magnitude_of_tangent_vector_squared = x'(t)^2+y'(t)^2 = b^2+(a+b*t)^2.
//That is the tangent vector that corresponds to the initial curve function r = a+b*t.
//You can not use the same code as found in this get_next_coordinates function because it only works with the curve: r=a+b*t.
//If you use this get_next_coordinates method with your curve function you will create a wrong program, and thereby loose a lot of points.


//You have to do the hard steps yourself:
//1. Start with your curve function r(t) = (x(t),y(t))
//2. Find the derivatives x'(t), y'(t)
//3. Find the tangent vector (x'(t),y'(t))
//4. Compute the absolute value of that tangent vector which is also known as magnitude of the tangent vector
//5. Use that magnitude as the divisor as demonstrated in the function in the class Spiral_algorithms.


