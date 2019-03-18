//Ruler:=1=========2=========3=========4=========5=========6=========7=========8=========9=========0=========1=========2=========3**
//Author: F. Holliday
//Author's email: holliday@fullerton.edu
//Course: CPSC223N
//Assignment number: 01
//Due date: 2018-Apr-15
//Date last updated: 2017-July-28
//Number of source files in this program: 3
//Purpose of this entire program:  Show a particle traveling along the path of a sine curve. 
//
//Development status:  This program is finished.
//
//Name of this file: Sinewave.cs
//Purpose of this file:  Implement all algorithms needed by other parts of the program.
//
//
//The source files in this program should be compiled in the order specified below in order to satisfy dependencies.
//  1. Sinecurve.cs         compiles into library file Sinecurve.dll
//  2. Sineframe.cs         compiles into library file Sineframe.dll
//  3. Sinewave.cs          compiles and links with the two dll files above to create Sinewave.exe

//
//How to compile this file using the Mono open source compiler/translator: 
//mcs -target:library Sinecurve.cs -out:Sinecurve.dll

//Hardcopy: this source code is best viewed in 7 point monospaced font using portrait orientation.


public class Sinecurve
{   private double absolute_value_of_derivative_squared;
    private double absolute_value_of_derivative;
    
    public void get_next_coordinates(double amp, double coef, double delta, ref double dot, out double x, out double y)
       {absolute_value_of_derivative_squared = 1.0 + amp*amp * coef*coef * System.Math.Cos(coef*dot)*System.Math.Cos(coef*dot);
        absolute_value_of_derivative = System.Math.Sqrt(absolute_value_of_derivative_squared);
        dot = dot + delta/absolute_value_of_derivative;
        x = dot;
        y = amp*System.Math.Sin(coef*dot);
       }//End of get_next_coordinates
       
}//End  of class Sinecurve


//Background.  It is nearly impossible to program an animated object to move with true constant linear velocity.  If an object 
//is programmed to move in the path of a certain curve such as say y = tan(x) and no special provision is made for attaining 
//constant speed then the object will move faster when the tangent line has a steep slope and will move slower when the tangent
//line has a near horizontal slope.  The solution implemented in this program is to adjust the speed of the moving object by
//taking into account the slope of the tangent line.  

//Consider the mathematical function: f(t) = A*sin(B*t) where A is amplitude, and B is a coefficient related to the period of sine 
//via the identity period P = 2*π/B.
//The derivative is f'(t) = A*B*cos(B*t)
//The corresponding 2-dim vector-valued function is P(t) = (t,f(t)) with derivative P'(t) = (1,f'(t)).
//The magnitude of the derivative indicates speed: |P'(t)| = sqrt(1+f'(t)^2) = sqrt(1+A^2*B^2*cos(B*t)^2)
//In the method get_next_coordinates the magnitude of the derivative will be used to moderate the speed of the dot.
//This is not a mathematical formula for "perfect" constant linear speed, but empirical testing shows that the technique produces 
//a highly accurate approximation of constant linear speed. 
