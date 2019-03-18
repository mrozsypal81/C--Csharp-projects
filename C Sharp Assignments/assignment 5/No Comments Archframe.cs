
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Timers;

public class Spiralframe : Form          
{  private const int form_height = 1080; 
   private const int control_region_height = 64;     
   private const int vertical_adjustment = 16;
   private const int graphic_area_height = form_height-control_region_height;
   private const int form_width = graphic_area_height*16/9;  


   private const double linear_velocity = 30.0;
   private const double refresh_rate = 30.0; 
   private const double spiral_rate = 300.0;                  
   private const double scale_factor = 500.0;

   private const double pixel_distance_traveled_in_one_tic =  linear_velocity/spiral_rate;  
   private const double mathematical_distance_traveled_in_one_tic = pixel_distance_traveled_in_one_tic/scale_factor;
   
   private int polar_origen_x = (int)System.Math.Round(form_width/2.0);
   private int polar_origen_y = (int)System.Math.Round(graphic_area_height/2.0);

   private const int spiral_width = 2; 

   private Size maxframesize = new Size(form_width,form_height);
   private Size minframesize = new Size(form_width,form_height);

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
   private double t = 1.0; 
   private double x;        
   private double y;        

   private double x_scaled_double;
   private double y_scaled_double;
   
   private int x_scaled_int;
   private int y_scaled_int;

   private bool spiral_too_large_vertically = false;
   private bool spiral_too_large_horizontally = false;

   private Button start_button = new Button();
   private Button exit_button = new Button();
   private Point location_of_start_button = new Point(40,form_height-control_region_height+10);
   private Point location_of_exit_button = new Point(form_width-40-80,form_height-control_region_height+10);

   private static System.Timers.Timer graphic_area_refresh_clock = new System.Timers.Timer();
   private static System.Timers.Timer spiral_clock = new System.Timers.Timer();
   private enum Spiral_clock_state_type{Begin,Ticking,Paused};
   private Spiral_clock_state_type spiral_state = Spiral_clock_state_type.Begin;

   private Pen bic = new Pen(Color.Black,1);

   private System.Drawing.Graphics pointer_to_graphic_surface;
   private System.Drawing.Bitmap pointer_to_bitmap_in_memory = 
                                         new Bitmap(form_width,form_height,System.Drawing.Imaging.PixelFormat.Format24bppRgb);

   Spiral_algorithms archimedes;

   public Spiralframe()
   { 
       Size = new Size(form_width,form_height);
       MaximumSize = maxframesize;
       MinimumSize = minframesize;

       Text = "r = sin(4t) By Michael Rozsypal";

       BackColor = Color.Pink;

       archimedes = new Spiral_algorithms();

       t = 0.0;
       //possible importance to change for calculation
       //Declare variables for the spiral function r = a + b*t.
       // trying to make r = sin(4t) a = 1 b = 4
       //  x(t) = sin(4t)cos(t) y(t) = sin(4t)sin(t)
       // derivatives: 'x = 4cos(4t)cos(t)-sin(4t)sin(t) 'y = 4cos(4t)sin(t)+sin(4t)cos(t)       
       x = (Math.Sin(b_coefficient*t))*System.Math.Cos(t);
       y = (Math.Sin(b_coefficient*t))*System.Math.Sin(t);

       start_button.Text = "Start Spiral";
       start_button.Size = new Size(80,22);
       start_button.Location = location_of_start_button;
       start_button.BackColor = Color.LimeGreen;
       exit_button.Text = "Exit";
       exit_button.Size = new Size(80,22);
       exit_button.Location = location_of_exit_button;
       exit_button.BackColor = Color.LimeGreen;
       
       graphic_area_refresh_clock.Enabled = false;
       graphic_area_refresh_clock.Elapsed += new ElapsedEventHandler(Update_the_graphic_area);

       spiral_clock.Enabled = false;
       spiral_clock.Elapsed += new ElapsedEventHandler(Update_the_position_of_the_spiral);

       DoubleBuffered = true;

       pointer_to_graphic_surface = Graphics.FromImage(pointer_to_bitmap_in_memory);
       initialize_bitmap();

       Controls.Add(start_button);
       start_button.Click += new EventHandler(Manage_spiral_clock); 
       Controls.Add(exit_button);
       exit_button.Click += new EventHandler(Stoprun);

       Start_graphic_clock(refresh_rate);

       System.Console.WriteLine("The constructor Spiralframe has completed its job.");

   }

   protected void initialize_bitmap()  
   {
    Font numeric_label_font = new System.Drawing.Font("Arial",8,FontStyle.Regular);
    Brush numeric_label_brush = new SolidBrush(System.Drawing.Color.Black);
    pointer_to_graphic_surface.Clear(System.Drawing.Color.White);
    double numeric_label = 0.0;

    System.Console.WriteLine("form_width = {0}, form_height = {1}, graphic_area_height = {2} .", 
                              form_width, form_height, graphic_area_height);
    System.Console.WriteLine("form_height/2 = {0}, scale_factor = {1}, form_height/2/scale_factor = {2} .", 
                              form_width/2, scale_factor, form_height/scale_factor/2);
    System.Console.WriteLine("polar_origen_x = {0}, polar_origen_y = {1}.", polar_origen_x, polar_origen_y);
    System.Console.WriteLine("polar_origen_x-unit_circle_radius = {0}, polar_origen_y-unit_circle_radius = {1}.", 
                              polar_origen_x-unit_circle_radius, polar_origen_y-unit_circle_radius);

    bic.DashStyle = DashStyle.Solid;
    bic.Width = 1; 
    pointer_to_graphic_surface.DrawLine(bic,form_width/2,graphic_area_height/2,form_width,graphic_area_height/2);

    numeric_label = 0.0;
    while(numeric_label*scale_factor*2.0 < (float)form_width)
    {  pointer_to_graphic_surface.DrawString(String.Format("{0:0.0}",numeric_label),numeric_label_font,numeric_label_brush,
                                             polar_origen_x+(int)System.Math.Round(numeric_label*scale_factor-10.0),
                                             polar_origen_y+2);
       numeric_label = numeric_label + 1.0;       
    }

    pointer_to_graphic_surface.FillRectangle(Brushes.Yellow,0,form_height-control_region_height,form_width,control_region_height);
   }

   protected override void OnPaint(PaintEventArgs eee)
   {   Graphics spiralgraph = eee.Graphics;
       spiralgraph.DrawImage(pointer_to_bitmap_in_memory,0,0,form_width,form_height);
       base.OnPaint(eee);
   }

   protected void Start_graphic_clock(double refreshrate)
   {double elapsedtimebetweentics;
    if(refreshrate < 1.0) refreshrate = 1.0;  
    elapsedtimebetweentics = 1000.0/refreshrate;  
    graphic_area_refresh_clock.Interval = (int)System.Math.Round(elapsedtimebetweentics);
    graphic_area_refresh_clock.Enabled = true;  
    System.Console.WriteLine("Start_graphic_clock has terminated.");
   }

   protected void Manage_spiral_clock(Object sender, EventArgs events)
   {switch(spiral_state)
       {case Spiral_clock_state_type.Begin: 
             double elapsed_time_between_updates_of_spiral_coordinates;
             double local_spiral_update_rate = spiral_rate;
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
       }
   }

     protected void Update_the_position_of_the_spiral(System.Object sender,ElapsedEventArgs an_event)
     {

      archimedes.get_next_coordinates(
                                      b_coefficient,
                                      mathematical_distance_traveled_in_one_tic,
                                      ref t,
                                      out x,
                                      out y);

      x_scaled_double = scale_factor * x;
      y_scaled_double = scale_factor * y;
     }
   protected void Update_the_graphic_area(System.Object sender, ElapsedEventArgs even)  
   {   x_scaled_int = (int)System.Math.Round(x_scaled_double);
       y_scaled_int = (int)System.Math.Round(y_scaled_double);

       if(0 <= polar_origen_y-y_scaled_int-spiral_width/2 && polar_origen_y-y_scaled_int-spiral_width/2 < graphic_area_height)
           if(0 <= polar_origen_x+x_scaled_int-spiral_width/2 && polar_origen_x+x_scaled_int-spiral_width/2 < form_width)
                {pointer_to_graphic_surface.FillEllipse(Brushes.Red,
                                                  polar_origen_x+x_scaled_int-spiral_width/2,
                                                  polar_origen_y-y_scaled_int-spiral_width/2,
                                                  spiral_width,
                                                  spiral_width);
                }
           else
                {spiral_too_large_horizontally = true;
                }
       else
           {spiral_too_large_vertically = true;
           }
       Invalidate();
       if(spiral_too_large_horizontally && spiral_too_large_vertically)
          {graphic_area_refresh_clock.Enabled = false;
           spiral_clock.Enabled = false;
           start_button.Enabled = false;
           System.Console.WriteLine("The graphical area is no longer refreshing.  You may close the window.");
          }
   }

   protected void Stoprun(Object sender, EventArgs events)
   {   Close();
   }


}
