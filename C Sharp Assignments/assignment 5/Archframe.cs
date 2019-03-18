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

//This file name: Archframe.cs
//Compile this file: 
//mcs -target:library Archframe.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:Archspiral.dll -out:Archframe.dll

//Best configuration for printing: 7-point monospaced font with portrait orientation.

//Alert: This program keeps a copy of the graphic area in memory.  That means a lots of pixels are stored in memory.  When
//testing this program with frame size larger than HDTV (1920x1080) the program sometime froze, and the author of this 
//program believes the freeze is due to insufficient memory allocated to this program.  The answer lies in some research into
//the mono compiler for C# to find out how to allocate more memory to this program's run-time space.



using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Timers;

public class Spiralframe : Form          //Spiralframe inherits from Form class
{  private const int form_height = 1080; //1280;  //Vertical height measured in pixels.  Adjust this number to match your monitor.
   private const int control_region_height = 64;     //This region holds buttons and input boxes -- if any.
   private const int vertical_adjustment = 16;
   private const int graphic_area_height = form_height-control_region_height;
   private const int form_width = graphic_area_height*16/9;  //Horizontal width measured in pixels.

   //****** Declare the 3 important numbers here: linear speed, refresh rate, motion rate ****************************************
   //Declare the constant velocity.  The motion of drawing the spiral must proceed at this speed at all times.                   *
   private const double linear_velocity = 30.0;  //pixels per second.  These are not math units; these are graphical pixels.     *
   //                                                                                                                            *
   //Declare rates for the clocks                                                                                                *
   private const double refresh_rate = 30.0; //Hertz: How many times per second the bitmap is copied to the visible surface.     *
   private const double spiral_rate = 300.0;  //Hertz: How many times per second the coordinates of the brush are updated.        *
   //*****************************************************************************************************************************

   //Declare scale factor: for example, '30' means "30 to 1" or "30 pixels represent 1 mathematical unit".                       
   private const double scale_factor = 500.0;
   
   //*****************************************************************************************************************************
   //To future programmers: There is a delicate balance among the three quantities:                                              *
   //    linear velocity: the distance in pixels traveled by the brush drawing the spiral in one second                          *
   //    refresh rate  :  the number of times per second the graphic area is repainted                                           *
   //    spiral rate   :  the number of times per second the coordinates of the brush painting the spiral are updated per second.*
   //If any one of the 3 is extreme compared to the other two the result is a disconnected spiral with gaps scattered along the  *
   //spiral curve.  For instance, these values result in a disconnected spiral:                                                  *
   //    linear velocity = 105.0                                                                                                 *
   //    refresh rate = 24.0                                                                                                     *
   //    spiral rate  = 18.0                                                                                                     *
   //However, the values shown below are more 'evenly balanced', and the result is a smooth connected spiral.                    *
   //    linear velocity = 44.5                                                                                                  *
   //    refresh rate = 47.5                                                                                                     *
   //    spiral rate = 50.0                                                                                                      *
   //You will simply have to experiment with different values for the 3 quantities to find values that make a good viewing       *
   //experience.  Keep in mind that these values vary with power of the computer used to test the program.  A computer with a    *
   //more powerful CPU, video card, size of memory, etc will be able to handle a larger range of values than a lesser computer.  *
   //*****************************************************************************************************************************

   //Declare a name for the distance the drawing brush travels in one tic of the spiral_clock.  For example, if the velocity of
   //the brush drawing the spiral is 120 pixels/sec and the clock tics at the rate of 30 Hz, then the brush moves a distance of 
   //120/30 = 4 pixels per tic. 
   private const double pixel_distance_traveled_in_one_tic =  linear_velocity/spiral_rate;  //The units are in pixels.
   //Convert pixel_distance to mathematical distance.
   private const double mathematical_distance_traveled_in_one_tic = pixel_distance_traveled_in_one_tic/scale_factor;
   
   //Declare the polar origin in C# coordinates
   private int polar_origen_x = (int)System.Math.Round(form_width/2.0);
   private int polar_origen_y = (int)System.Math.Round(graphic_area_height/2.0);

   //Declare the width of the spiral curve.  Be aware that in the method Update_the_graphic_area the value will be divided by 2 
   //using integer division.  Therefore the width of the curve should be at least 2. 
   //division; namely: spiral_width/2 in the method Update_the_graphic_area.
   private const int spiral_width = 2;  //The value 1 results in a very thin barely visible curve.

   //Declare max and min sizes
   private Size maxframesize = new Size(form_width,form_height);
   private Size minframesize = new Size(form_width,form_height);

   //Declare variables for polar coordinate system
   private int unit_circle_radius = (int)System.Math.Round(scale_factor);

   //Declare variables for the spiral function r = a + b*t.
   // trying to make r = sin(4t) a = 1 b = 4
   //  x(t) = sin(4t)cos(t) y(t) = sin(4t)sin(t)
   // derivatives: 'x = 4cos(4t)cos(t)-sin(4t)sin(t) 'y = 4cos(4t)sin(t)+sin(4t)cos(t)
   //The distance between rings measured along a polar radius is d = 2bπ.
   //To achieve a distance between rings equal to 1.0 the coefficient b must be set to b = 1.0/(2π).
   //private const double initial_radius = 0.0;            //a = 0.  There is no loss in assuming a is zero.
   private const double distance_between_rings = 1.0; 
   private const double b_coefficient = 4;
   private double t = 1.0;  //t is angle between polar axis and the ray emanating from the pole to the pen; t≥0.
   private double x;        //Cartesian x-coordinate of the point drawing the spiral trace
   private double y;        //Cartesian x-coordinate of the point drawing the spiral trace

   //Variables for the scaled description of the Archimedean spiral
   private double x_scaled_double;
   private double y_scaled_double;
   
   //Variables for drawing on the bitmap
   private int x_scaled_int;
   private int y_scaled_int;

   //Variables detecting time to stop execution
   private bool spiral_too_large_vertically = false;
   private bool spiral_too_large_horizontally = false;

   //Declare buttons
   private Button start_button = new Button();
   private Button exit_button = new Button();
   private Point location_of_start_button = new Point(40,form_height-control_region_height+10);
   private Point location_of_exit_button = new Point(form_width-40-80,form_height-control_region_height+10);

   //Declare clocks
   private static System.Timers.Timer graphic_area_refresh_clock = new System.Timers.Timer();
   private static System.Timers.Timer spiral_clock = new System.Timers.Timer();
   private enum Spiral_clock_state_type{Begin,Ticking,Paused};
   private Spiral_clock_state_type spiral_state = Spiral_clock_state_type.Begin;

   //Instruments
   private Pen bic = new Pen(Color.Black,1); //The bic pen has a thickness of 1 pixel.

   //Declare pointers to the visible graphical area and and to the bitmap area of memory.
   private System.Drawing.Graphics pointer_to_graphic_surface;
   private System.Drawing.Bitmap pointer_to_bitmap_in_memory = 
                                         new Bitmap(form_width,form_height,System.Drawing.Imaging.PixelFormat.Format24bppRgb);

   //Declare a tool kit of algorithms
   Spiral_algorithms archimedes;

   //Define the constructor of this class.
   public Spiralframe()
   {   //Set the initial size of this form.
       Size = new Size(form_width,form_height);
       MaximumSize = maxframesize;
       MinimumSize = minframesize;

       //Set the title of this user interface.
       Text = "r = sin(4t) By Michael Rozsypal";

       //Give feedback to the programmer.
       System.Console.WriteLine("form_width = {0}, form_height = {1}.", form_width, form_height);
       System.Console.WriteLine("polar_origen_x = {0}, polar_origen_y = {1}.", polar_origen_x, polar_origen_y);

       //Set the initial background color of this form.
       BackColor = Color.Pink;

       //Instantiate the collection of supporting algorithms
       archimedes = new Spiral_algorithms();

       //Set initial values for the spiral in a standard mathematical cartesian coordinate system (no scaling at this time)
       t = 0.0;
       //possible importance to change for calculation
       //Declare variables for the spiral function r = a + b*t.
       // trying to make r = sin(4t) a = 1 b = 4
       //  x(t) = sin(4t)cos(t) y(t) = sin(4t)sin(t)
       // derivatives: 'x = 4cos(4t)cos(t)-sin(4t)sin(t) 'y = 4cos(4t)sin(t)+sin(4t)cos(t)       
       x = (Math.Sin(b_coefficient*t))*System.Math.Cos(t);
       y = (Math.Sin(b_coefficient*t))*System.Math.Sin(t);

       //Prepare the buttons
       start_button.Text = "Start Spiral";
       start_button.Size = new Size(80,22);
       start_button.Location = location_of_start_button;
       start_button.BackColor = Color.LimeGreen;
       exit_button.Text = "Exit";
       exit_button.Size = new Size(80,22);
       exit_button.Location = location_of_exit_button;
       exit_button.BackColor = Color.LimeGreen;

       //Prepare the refresh clock
       graphic_area_refresh_clock.Enabled = false;
       graphic_area_refresh_clock.Elapsed += new ElapsedEventHandler(Update_the_graphic_area);

       //Prepare the spiral clock
       spiral_clock.Enabled = false;
       spiral_clock.Elapsed += new ElapsedEventHandler(Update_the_position_of_the_spiral);

       //Use extra memory to make a smooth animation.
       DoubleBuffered = true;

       //Initialize the pointer used to write onto the bitmap stored in memory.
       pointer_to_graphic_surface = Graphics.FromImage(pointer_to_bitmap_in_memory);
       initialize_bitmap();

       //Make buttons visible and attach buttons to functions
       Controls.Add(start_button);
       start_button.Click += new EventHandler(Manage_spiral_clock);   //How does Start_spiral_clock receive the update rate?
       Controls.Add(exit_button);
       exit_button.Click += new EventHandler(Stoprun);

       //Start refresh clock running
       Start_graphic_clock(refresh_rate);

       System.Console.WriteLine("The constructor Spiralframe has completed its job.");

   }//End of constructor of Spiralframe class.

   protected void initialize_bitmap()        //Called one time from the constructor
   {//Explanation: The method contain 5 statements that perform a Draw action on "pointer_to_graphic_surface".  Each one of 
    //those statements is an output action onto the bitmap area in memory.  The name "pointer_to_graphic_surface" seems counter
    //intuitive, but in fact the output does go to a block in memory.
    Font numeric_label_font = new System.Drawing.Font("Arial",8,FontStyle.Regular);
    Brush numeric_label_brush = new SolidBrush(System.Drawing.Color.Black);
    pointer_to_graphic_surface.Clear(System.Drawing.Color.White);
    double numeric_label = 0.0; //Declare a variable to assist in placing numeric labels on the polar axis.

    //The next few statements provide feedback for the programmer.  These statements should be removed by the next programmer.
    System.Console.WriteLine("form_width = {0}, form_height = {1}, graphic_area_height = {2} .", 
                              form_width, form_height, graphic_area_height);
    System.Console.WriteLine("form_height/2 = {0}, scale_factor = {1}, form_height/2/scale_factor = {2} .", 
                              form_width/2, scale_factor, form_height/scale_factor/2);
    System.Console.WriteLine("polar_origen_x = {0}, polar_origen_y = {1}.", polar_origen_x, polar_origen_y);
    System.Console.WriteLine("polar_origen_x-unit_circle_radius = {0}, polar_origen_y-unit_circle_radius = {1}.", 
                              polar_origen_x-unit_circle_radius, polar_origen_y-unit_circle_radius);

    //Draw the polar axis
    bic.DashStyle = DashStyle.Solid;  //Dash, Dot, DashDot, DashDotDot
    bic.Width = 1;  //The bic pen will draw with a width = 2 pixels
    pointer_to_graphic_surface.DrawLine(bic,form_width/2,graphic_area_height/2,form_width,graphic_area_height/2);

    //Draw labels along the polar axis using a loop
    numeric_label = 0.0;
    while(numeric_label*scale_factor*2.0 < (float)form_width)
    {  pointer_to_graphic_surface.DrawString(String.Format("{0:0.0}",numeric_label),numeric_label_font,numeric_label_brush,
                                             polar_origen_x+(int)System.Math.Round(numeric_label*scale_factor-10.0),
                                             polar_origen_y+2);
       numeric_label = numeric_label + 1.0;  //Increment the loop control variable     
    }//End of while

    //Draw a pseudo-panel as a thin long rectangle along the bottom of the form.
    pointer_to_graphic_surface.FillRectangle(Brushes.Yellow,0,form_height-control_region_height,form_width,control_region_height);
   }//End of initialize_bitmap()

   protected override void OnPaint(PaintEventArgs eee)
   {   Graphics spiralgraph = eee.Graphics;
       //Copy the contents of the bitmap to the graphic surface area.
       spiralgraph.DrawImage(pointer_to_bitmap_in_memory,0,0,form_width,form_height);
       base.OnPaint(eee);
   }//End of OnPaint(PaintEventArgs eee)

   //Comments about drawing and copying of bytes;  To output data to memory we use pointer_to_graphic_surface as the object 
   //receiving the action as in the next line:
   //pointer_to_graphic_surface.Clear(System.Drawing.Color.White);pointer_to_graphic_surface.Clear(System.Drawing.Color.White);
   //However, to copy the bitmap region of memory to the visible graphical area we use the other variable
   //pointer_to_bitmap_in_memory.  For example: spiralgraph.DrawImage(pointer_to_bitmap_in_memory,0,0,form_width,form_height);

   protected void Start_graphic_clock(double refreshrate)                         //Called one time by the constructor
   {double elapsedtimebetweentics;
    if(refreshrate < 1.0) refreshrate = 1.0;  //Do not allow updates slower than 1 hertz.
    elapsedtimebetweentics = 1000.0/refreshrate;  //elapsed time between tics has units milliseconds
    graphic_area_refresh_clock.Interval = (int)System.Math.Round(elapsedtimebetweentics);
    graphic_area_refresh_clock.Enabled = true;  //Start this clock ticking.
    System.Console.WriteLine("Start_graphic_clock has terminated.");
   }//End of method Start_graphic_clock(double refreshrate)

   protected void Manage_spiral_clock(Object sender, EventArgs events)
   {switch(spiral_state)
       {case Spiral_clock_state_type.Begin: 
             double elapsed_time_between_updates_of_spiral_coordinates;
             double local_spiral_update_rate = spiral_rate;
             //In the next statement don't allow the spiral to update at a rate slower than 1.0 Hz
             if(local_spiral_update_rate < 1.0) local_spiral_update_rate = 1.0;
             elapsed_time_between_updates_of_spiral_coordinates = 1000.0/local_spiral_update_rate;
             spiral_clock.Interval = (int)System.Math.Round(elapsed_time_between_updates_of_spiral_coordinates);
             spiral_clock.Enabled = true;
             start_button.Text = "Pause";
             spiral_state = Spiral_clock_state_type.Ticking;
             graphic_area_refresh_clock.Enabled = true;
             System.Console.WriteLine("Begin case finished executing");
             break;
        case Spiral_clock_state_type.Ticking: 
             spiral_clock.Enabled = false;
             spiral_state = Spiral_clock_state_type.Paused;
             start_button.Text = "Go";
             graphic_area_refresh_clock.Enabled = false;
             System.Console.WriteLine("Ticking case finished executing");
             break;
        case Spiral_clock_state_type.Paused: 
             spiral_clock.Enabled = true;
             spiral_state = Spiral_clock_state_type.Ticking;
             start_button.Text = "Pause";
             graphic_area_refresh_clock.Enabled = true;
             System.Console.WriteLine("Paused case finished executing");
             break;
        default:
             System.Console.WriteLine("A serious error occurred in the switch statement.");
             break;
       }//End of switch
   }//End of Manage_spiral_clock

     //The next method updates the x and y coordinates of the dot that is tracing the path of the spiral.
     protected void Update_the_position_of_the_spiral(System.Object sender,ElapsedEventArgs an_event)
     {//Call a method to compute the next pair of Cartesian coordinates for the moving particle tracing the path of the spiral.

      archimedes.get_next_coordinates(
                                      b_coefficient,
                                      mathematical_distance_traveled_in_one_tic,
                                      ref t,
                                      out x,
                                      out y);

      x_scaled_double = scale_factor * x;
      y_scaled_double = scale_factor * y;
     }//End of method Update_the_position_of_the_spiral


   //The next method places the dot into the bitmapped region of memory according to its own coordinates, and then calls method
   //OnPaint to copy the bitmapped image to the graphical surface for viewing.  This occurs each time the graphic_area_refresh_clock
   //makes a tic.
   protected void Update_the_graphic_area(System.Object sender, ElapsedEventArgs even)                    //Activated by the refresh clock.
   {   x_scaled_int = (int)System.Math.Round(x_scaled_double);
       y_scaled_int = (int)System.Math.Round(y_scaled_double);

       //The if-condition below does not allow the spiral to write outside of the upper or lower boundaries of the graphic area.
       if(0 <= polar_origen_y-y_scaled_int-spiral_width/2 && polar_origen_y-y_scaled_int-spiral_width/2 < graphic_area_height)
           //The if-condition below does not allow the spiral to write outside of the left or right boundaries of the graphic area.
           if(0 <= polar_origen_x+x_scaled_int-spiral_width/2 && polar_origen_x+x_scaled_int-spiral_width/2 < form_width)
                {pointer_to_graphic_surface.FillEllipse(Brushes.Red,
                                                  polar_origen_x+x_scaled_int-spiral_width/2,
                                                  polar_origen_y-y_scaled_int-spiral_width/2,  //There is a subtraction here because the y-axis is upside down.
                                                  spiral_width,
                                                  spiral_width);
                }
           else
                {spiral_too_large_horizontally = true;
                }
       else
           {spiral_too_large_vertically = true;
           }
       Invalidate();  //This function actually calls OnPaint.  Yes, that is true.
       if(spiral_too_large_horizontally && spiral_too_large_vertically)  //It is time to stop execution
          {graphic_area_refresh_clock.Enabled = false;  //Stop refreshing the graphic area
           spiral_clock.Enabled = false;
           start_button.Enabled = false;
           System.Console.WriteLine("The graphical area is no longer refreshing.  You may close the window.");
          }
   }//End of Update_the_graphic_area

   protected void Stoprun(Object sender, EventArgs events)
   {   Close();
   }//End of stoprun


}//End of Spiralframe class

//Ruler:=1=========2=========3=========4=========5=========6=========7=========8=========9=========0=========1=========2=========3**
