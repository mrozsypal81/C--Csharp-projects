
   //Declare variables for the spiral function r = a + b*t.
   // trying to make r = sin(4t) a = 1 b = 4
   //  x(t) = sin(4t)cos(t) y(t) = sin(4t)sin(t)
   // derivatives: 'x = 4cos(4t)cos(t)-sin(4t)sin(t) 'y = 4cos(4t)sin(t)+sin(4t)cos(t)
 

public class Spiral_algorithms
{   private double magnitude_of_tangent_vector_squared;
    private double magnitude_of_tangent_vector;

    public void get_next_coordinates(          
                                     double b_coefficient,           
                                     double distance_1_tic,          
                                     ref double t,                   
                                     out double new_x,               
                                     out double new_y)               
       {magnitude_of_tangent_vector_squared = (b_coefficient*System.Math.Cos(b_coefficient*t)*System.Math.Cos(t)-System.Math.Sin(b_coefficient*t)*System.Math.Sin(t))*(b_coefficient*System.Math.Cos(b_coefficient*t)*System.Math.Cos(t)-System.Math.Sin(b_coefficient*t)*System.Math.Sin(t));
        magnitude_of_tangent_vector_squared = magnitude_of_tangent_vector_squared + ((b_coefficient*System.Math.Cos(b_coefficient*t)*System.Math.Sin(t)-System.Math.Sin(b_coefficient*t)*System.Math.Cos(t))*(b_coefficient*System.Math.Cos(b_coefficient*t)*System.Math.Sin(t)-System.Math.Sin(b_coefficient*t)*System.Math.Cos(t)));
        magnitude_of_tangent_vector = System.Math.Sqrt(magnitude_of_tangent_vector_squared);
        t = t + distance_1_tic/magnitude_of_tangent_vector;
        new_x = (System.Math.Sin(b_coefficient*t))*System.Math.Cos(t);
        new_y = (System.Math.Sin(b_coefficient*t))*System.Math.Sin(t);
       }
       
}


