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
//Education: This program is for the students enrolled in CPSC223n.  This program and other programs in the series are your book in 
//this course.  Study the programs as you would study an assigned book made of paper.
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
//Compile this source file and link in needed library files (dll files) 
//mcs -target:library Sineframe.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:Sinecurve.dll -out:Sineframe.dll

//Hardcopy: this source code is best viewed in 7 point monospaced font using portrait orientation.
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Timers;

public class Sineframe : Form            //Sineframe inherits from Form class
{  private const int form_width = 1920;  //Horizontal width measured in pixels..
   private const int form_height = form_width*9/16;  //Vertical height measured in pixels
   //In 223n you should use the largest possible graphical area supported by your monitor and still maintain the 16:9 ratio.

   //Declare constants
   private const double scale_factor = 100.0;
   private const double offset = 960.0;  //The origin is located this many pixels to the right of the left boundary.
   private const double delta = 0.015;
   private const double refresh_rate = 90.0; //Hertz: How many times per second the bitmap is updated and copied to the visible surface.
   private const double dot_update_rate = 70.0;  //Hertz: How many times per second the coordinates of the dot are updated.
   
   private Button start_button = new Button();
   private Button exit_button = new Button();
   private Point location_of_start_button = new Point(40,1000);
   private Point location_of_exit_button = new Point(form_width-40-80,1000);


   //Declare max and min sizes
   private Size maxframesize = new Size(form_width,form_height);
   private Size minframesize = new Size(form_width,form_height);

   //Declare variables for the sine function: y = amplitude * sin (coefficient * t).
   private double amplitude = 1.0;
 //  private double period;
   private double coefficient = 4.0;

   //Variables for the Cartesian description of γ(t) = (t,Asin(Bt)), where A = amplitude, B = coefficient, x(t) = t, y(t) = A*sin(B*t)
   private const double t_initial_value = 3.14159265359;  //Start curve drawing at γ(0.0) = (0.0,0.0) = Origin
   private double t = t_initial_value;
   private double x;
   private double y;

   //Variables for the scaled description of the sine curve
   private double x_scaled_double;
   private double y_scaled_double;
   
   //Variable for drawing on the bitmap
   private int x_scaled_int;
   private int y_scaled_int;

   //Declare clocks
   private static System.Timers.Timer graphic_area_refresh_clock = new System.Timers.Timer();
   private static System.Timers.Timer dot_refresh_clock = new System.Timers.Timer();
   private enum Dot_clock_state_type{Begin,Ticking,Paused};
   private Dot_clock_state_type Dot_state = Dot_clock_state_type.Begin;
   
   //Instruments
   Pen bic = new Pen(Color.Black,1);

   //Declare visible graphical area and bitmap graphical area
   private System.Drawing.Graphics pointer_to_graphic_surface;
   private System.Drawing.Bitmap pointer_to_bitmap_in_memory = 
                                         new Bitmap(form_width,form_height,System.Drawing.Imaging.PixelFormat.Format24bppRgb);

   //Declare a tool kit of algorithms
   Sinecurve curve_algorithms;

   //Define the constructor of this class.
   public Sineframe()
   {   //Set the initial size of this form.
       Size = new Size(form_width,form_height);
       MaximumSize = maxframesize;
       MinimumSize = minframesize;
       //Set the title of this user interface.
       Text = "y = sin(4t) By Michael Rozsypal";
       //Give feedback to the programmer.
       System.Console.WriteLine("form_width = {0}, form_height = {1}.", form_width, form_height);

       //Set the initial background color of this form.
       BackColor = Color.Pink;

       /*//Obtain the amplitude from the console.
       System.Console.Write("Enter a float value for the amplitude of the sine function - the range 0.0 to 5.0 is recommended: ");
       amplitude = double.Parse(System.Console.ReadLine());
       
       //Obtain the period from the console.
       System.Console.WriteLine("Enter a float value for the period of the sine function - 6.283185307 (2π) is a nice test case: ");
       System.Console.Write("Entering 0.0 or a number close to zero may cause a divide by zero error: ");
       period = double.Parse(System.Console.ReadLine());
    
       //Compute the coefficient of t in the function: y = amplitude * sin(coefficient * t).
       coefficient = 2.0*System.Math.PI/period;
*/
       start_button.Text = "Go";
       start_button.Size = new Size(80,22);
       start_button.Location = location_of_start_button;
       start_button.BackColor = Color.LimeGreen;
       exit_button.Text = "Exit";
       exit_button.Size = new Size(80,22);
       exit_button.Location = location_of_exit_button;
       exit_button.BackColor = Color.LimeGreen;
       
       Controls.Add(start_button);
       start_button.Click += new EventHandler(Manage_dot_clock);
       Controls.Add(exit_button);
       exit_button.Click += new EventHandler(Stoprun);
       
       //Instantiate the collection of supporting algorithms
       curve_algorithms = new Sinecurve();

       //Set initial values for the sine curve in a standard mathematical cartesian coordinate system
       t = 0.0;
       x = t;
       y = amplitude * System.Math.Sin(coefficient*t);

       //Prepare the refresh clock
       graphic_area_refresh_clock.Enabled = false;
       graphic_area_refresh_clock.Elapsed += new ElapsedEventHandler(Update_the_graphic_area);

       //Prepare the dot clock
       dot_refresh_clock.Enabled = false;
       dot_refresh_clock.Elapsed += new ElapsedEventHandler(Update_the_position_of_the_dot);

       //Start both clocks running
       Start_graphic_clock(refresh_rate);
       //Start_dot_clock(dot_update_rate);

       //Use extra memory to make a smooth animation.
       DoubleBuffered = true;

       //Initialize the pointer used to write onto the bitmap stored in memory.
       pointer_to_graphic_surface = Graphics.FromImage(pointer_to_bitmap_in_memory);
       initialize_bitmap();

   }//End of constructor of Sineframe class.

   protected void initialize_bitmap()
   {Font labelfont = new System.Drawing.Font("Arial",8,FontStyle.Regular);
    Brush labelbrush = new SolidBrush(System.Drawing.Color.Black);
    pointer_to_graphic_surface.Clear(System.Drawing.Color.White);
    //Draw the vertical Y-axis.
    bic.Width = 2;
    pointer_to_graphic_surface.DrawLine(bic,40,0,40,form_height);
    //Draw the horizontal X-axis.
    bic.Width = 2;
    pointer_to_graphic_surface.DrawLine(bic,0,form_height/2,form_width,form_height/2);
    //Draw horizontal guide lines.
    bic.DashStyle = DashStyle.Dash;
    bic.Width = 1;

    //Add labels to the Y-axis.
    pointer_to_graphic_surface.DrawString("+1.0",labelfont,labelbrush,40-24,form_height/2-100-4);
    pointer_to_graphic_surface.DrawString("+2.0",labelfont,labelbrush,40-24,form_height/2-200-4);
    pointer_to_graphic_surface.DrawString("+3.0",labelfont,labelbrush,40-24,form_height/2-300-4);
    pointer_to_graphic_surface.DrawString("+4.0",labelfont,labelbrush,40-24,form_height/2-400-4);
    pointer_to_graphic_surface.DrawString("+5.0",labelfont,labelbrush,40-24,form_height/2-500-4);
    pointer_to_graphic_surface.DrawString("-1.0",labelfont,labelbrush,40-24,form_height/2+100-4);
    pointer_to_graphic_surface.DrawString("-2.0",labelfont,labelbrush,40-24,form_height/2+200-4);
    pointer_to_graphic_surface.DrawString("-3.0",labelfont,labelbrush,40-24,form_height/2+300-4);
    pointer_to_graphic_surface.DrawString("-4.0",labelfont,labelbrush,40-24,form_height/2+400-4);
    pointer_to_graphic_surface.DrawString("-5.0",labelfont,labelbrush,40-24,form_height/2+500-4);
    //Add labels to the X-axis.
    pointer_to_graphic_surface.DrawString("+π",labelfont,labelbrush,40+1*314-4,form_height/2+1);
    pointer_to_graphic_surface.DrawString("+2π",labelfont,labelbrush,40+2*314-4,form_height/2+1);
    pointer_to_graphic_surface.DrawString("+3π",labelfont,labelbrush,40+3*314-4,form_height/2+1);
    pointer_to_graphic_surface.DrawString("+4π",labelfont,labelbrush,40+4*314-4,form_height/2+1);
    pointer_to_graphic_surface.DrawString("+5π",labelfont,labelbrush,40+5*314-4,form_height/2+1);
    pointer_to_graphic_surface.DrawString("+6π",labelfont,labelbrush,40+6*314-4,form_height/2+1);
   }

   protected override void OnPaint(PaintEventArgs eee)
   {   Graphics sinegraph = eee.Graphics;
       //Copy the contents of the bitmap to the graphic surface area.
       sinegraph.DrawImage(pointer_to_bitmap_in_memory,0,0,form_width,form_height);
       base.OnPaint(eee);
   }

   protected void Start_graphic_clock(double refreshrate)
   {double elapsedtimebetweentics;
    if(refreshrate < 1.0) refreshrate = 1.0;  //Do not allow updates slower than 1 hertz.
    elapsedtimebetweentics = 1000.0/refreshrate;  //elapsed time between tics has units milliseconds
    graphic_area_refresh_clock.Interval = (int)System.Math.Round(elapsedtimebetweentics);
    graphic_area_refresh_clock.Enabled = true;  //Start this clock ticking.
    System.Console.WriteLine("Start_graphic_clock has terminated.");
   }//End of method Start_graphic_clock

   /*
   protected void Start_dot_clock(double dot_parameter_update_rate)
   {double elapsedtimebetweenchangestodotcoordinates;
    //This program does not allow a clock speed slower than one hertz.
    if(dot_parameter_update_rate < 1.0) dot_parameter_update_rate = 1.0;
    //Compute the interval in millisec between each tick of the clock.
    elapsedtimebetweenchangestodotcoordinates = 1000.0/dot_parameter_update_rate;
    dot_refresh_clock.Interval = (int)System.Math.Round(elapsedtimebetweenchangestodotcoordinates);
    dot_refresh_clock.Enabled = true;  //Start this clock ticking
    System.Console.WriteLine("Start_dot_clock has now terminated.");
   }//End of method Start_dot_clock
   */
   
   protected void Manage_dot_clock(Object sender, EventArgs events)
   {switch(Dot_state)
       {case Dot_clock_state_type.Begin: 
             double elapsed_time_between_updates_of_dot_coordinates;
             double local_dot_update_rate = dot_update_rate;
             //In the next statement don't allow the spiral to update at a rate slower than 1.0 Hz
             if(local_dot_update_rate < 1.0) local_dot_update_rate = 1.0;
             elapsed_time_between_updates_of_dot_coordinates = 1000.0/local_dot_update_rate;
             dot_refresh_clock.Interval = (int)System.Math.Round(elapsed_time_between_updates_of_dot_coordinates);
             dot_refresh_clock.Enabled = true;
             start_button.Text = "Pause";
             Dot_state = Dot_clock_state_type.Ticking;
             graphic_area_refresh_clock.Enabled = true;
             System.Console.WriteLine("Begin case finished executing");
             break;
        case Dot_clock_state_type.Ticking: 
             dot_refresh_clock.Enabled = false;
             Dot_state = Dot_clock_state_type.Paused;
             start_button.Text = "Go";
             graphic_area_refresh_clock.Enabled = false;
             System.Console.WriteLine("Ticking case finished executing");
             break;
        case Dot_clock_state_type.Paused: 
             dot_refresh_clock.Enabled = true;
             Dot_state = Dot_clock_state_type.Ticking;
             start_button.Text = "Pause";
             graphic_area_refresh_clock.Enabled = true;
             System.Console.WriteLine("Paused case finished executing");
             break;
        default:
             System.Console.WriteLine("A serious error occurred in the switch statement.");
             break;
       }//End of switch
   }//End of Manage_spiral_clock
   
   //The next method updates the x and y coordinates of the dot that is tracing the sine curve.
   protected void Update_the_position_of_the_dot(System.Object sender,ElapsedEventArgs an_event)
   {   //Call a method to compute the next pair of Cartesian coordinates for the moving particle.
       curve_algorithms.get_next_coordinates(amplitude,coefficient,delta,ref t,out x,out y);
       //Convert the Cartesian coordinates to scaled coordinates for viewing on a monitor
       x_scaled_double = scale_factor * x + offset;
       y_scaled_double = (double)form_height/2.0 - scale_factor * y;
       if(x_scaled_double > (double)(form_width-1))
          {dot_refresh_clock.Enabled = false;
           graphic_area_refresh_clock.Enabled = false;
           System.Console.WriteLine("Both clocks have stopped.  You may close the window.");
          }
   }//End of method Update_the_position_of_the_dot

   //The next method places the dot into the bitmapped region of memory according to its own coordinates, and then calls method
   //OnPaint to copy the bitmapped image to the graphical surface for viewing.  This occurs each time the graphic_area_refresh_clock
   //makes a tic.
   protected void Update_the_graphic_area(System.Object sender, ElapsedEventArgs even)
   {   x_scaled_int = (int)System.Math.Round(x_scaled_double);
       y_scaled_int = (int)System.Math.Round(y_scaled_double);
       pointer_to_graphic_surface.FillEllipse(Brushes.DarkRed,x_scaled_int,y_scaled_int,1,1);
       Invalidate();  //This function actually calls OnPaint.  Yes, that is true.
       if(x_scaled_int >= form_width)  //dot has reach the right edge of the frame
          {graphic_area_refresh_clock.Enabled = false;  //Stop refreshing the graphic area
           System.Console.WriteLine("The graphical area is no longer refreshing.  You may close the window.");
          }
   }//End of Update_the_graphic_area

   protected void Stoprun(Object sender, EventArgs events)
   {   Close();
   }//End of stoprun
   
}//End of Sineframe class
