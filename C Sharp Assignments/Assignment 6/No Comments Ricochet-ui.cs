
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class Ricochet_interface_form : Form
{  
   private const int formwidth = 1000;    
   private const int formheight = 900;    
   private const int titleheight = 40;
   private const int graphicheight = 800;
   private const int lowerpanelheight = formheight - titleheight - graphicheight;
   private double m_x;
   private double m_y;
   private int score = 0;
   
   private const double ball_radius = 8.5;
   private double ball_linear_speed_pix_per_sec;
   private double ball_linear_speed_pix_per_tic;
   private double ball_direction_x;
   private double ball_direction_y;
   private double ball_delta_x;
   private double ball_delta_y;
   private const double ball_center_initial_coord_x = (double)formwidth/2.0;
   private const double ball_center_initial_coord_y = (double)graphicheight/2.0;
   private double ball_center_current_coord_x;
   private double ball_center_current_coord_y;
   private double ball_upper_left_current_coord_x;
   private double ball_upper_left_current_coord_y;

   
   private static System.Timers.Timer ball_motion_control_clock = new System.Timers.Timer();
   private const double ball_motion_control_clock_rate = 43.5; 


   private static System.Timers.Timer graphic_area_refresh_clock = new System.Timers.Timer();
   private const double graphic_refresh_rate = 23.3;  

   private Font style_of_message = new System.Drawing.Font("Arial",10,FontStyle.Regular);
   private String title = "Billiards by Michael Rozsypal";
   private String Score_text;
   private Brush writing_tool = new SolidBrush(System.Drawing.Color.Black);
   private Point title_location = new Point(formwidth/2-15,10);
   private Point Score_location = new Point(200,titleheight+graphicheight+6);

   private Button start_button = new Button();
   private Button newb_button = new Button();
   private Point start_location = new Point(20,titleheight+graphicheight+6);
   private Point newb_location = new Point(50,titleheight+graphicheight+6);
   private Button quitb = new Button();   
   private Point loc_quitb = new Point(100,titleheight+graphicheight+6);
   
   public Ricochet_interface_form(double speed,double v,double w)  
   {  
      Text = "Ricochet Motion";
      System.Console.WriteLine("formwidth = {0}. formheight = {1}.",formwidth,formheight);
      Size = new Size(formwidth,formheight);
      BackColor = Color.Green;
      
      score = 0;
      
      ball_linear_speed_pix_per_sec = speed;
      ball_direction_x = v;
      ball_direction_y = w;

      ball_linear_speed_pix_per_tic = ball_linear_speed_pix_per_sec/ball_motion_control_clock_rate;
      double hypotenuse_squared = ball_direction_x*ball_direction_x + ball_direction_y*ball_direction_y;
      double hypotenuse = System.Math.Sqrt(hypotenuse_squared);
      ball_delta_x = ball_linear_speed_pix_per_tic * ball_direction_x / hypotenuse;
      ball_delta_y = ball_linear_speed_pix_per_tic * ball_direction_y / hypotenuse;

      ball_center_current_coord_x = ball_center_initial_coord_x;
      ball_center_current_coord_y = ball_center_initial_coord_y;
      System.Console.WriteLine("Initial coordinates: ball_center_current_coord_x = {0}. ball_center_current_coord_y = {1}.",
                               ball_center_current_coord_x,ball_center_current_coord_y);

      ball_motion_control_clock.Enabled = false;
      ball_motion_control_clock.Elapsed += new ElapsedEventHandler(Update_ball_position);

      graphic_area_refresh_clock.Enabled = false;
      graphic_area_refresh_clock.Elapsed += new ElapsedEventHandler(Update_display);  

      start_button.Text = "Go";
      start_button.Size = new Size(60,20);
      start_button.Location = start_location;
      start_button.BackColor = Color.LimeGreen;
      
      newb_button.Text = "New ball";
      newb_button.Size = new Size(60,30);
      newb_button.Location = newb_location;
      newb_button.BackColor = Color.LimeGreen;
      
      quitb.Text = "Quit";
      quitb.Size = new Size(60,20);
      quitb.Location = loc_quitb;
      quitb.BackColor = Color.LimeGreen;
      
      Score_text = "Your Score is " + score;
      
      Controls.Add(start_button);
      Controls.Add(quitb);
      Controls.Add(newb);
      
      newb_button.Click += new EventHandler(newball);
      start_button.Click += new EventHandler(All_systems_go);
      quitb.Click += new EventHandler(exitfromthisprogram);
      
   }
   protected override void OnMouseDown(MouseEventArgs me){
        m_x = me.X;
        m_y = me.Y;
        if((Math.Sqrt(((ball_center_current_coord_x-m_x)*(ball_center_current_coord_x-m_x))+((ball_center_current_coord_y-m_y)*(ball_center_current_coord_y-m_y))))<= ball_radius){
            score++;
            newb.PerformClick();
        }
        System.Console.WriteLine("Mouse Click");
    }
   protected override void OnPaint(PaintEventArgs ee)
   {  Graphics graph = ee.Graphics;
      graph.FillRectangle(Brushes.Pink,0,0,formwidth,titleheight);
      graph.FillRectangle(Brushes.Yellow,0,titleheight+graphicheight,formwidth,titleheight+graphicheight);
      graph.DrawString(title,style_of_message,writing_tool,title_location);
      graph.DrawString(Score_text,style_of_message,writing_tool,Score_location);
      ball_upper_left_current_coord_x = ball_center_current_coord_x - ball_radius;
      ball_upper_left_current_coord_y = ball_center_current_coord_y - ball_radius;
      graph.FillEllipse(Brushes.Red,(int)ball_upper_left_current_coord_x,(int)ball_upper_left_current_coord_y,(float)(2.0*ball_radius),(float)(2.0*ball_radius));
      base.OnPaint(ee);
   }

   protected void All_systems_go(Object sender,EventArgs events)
   {
    Start_graphic_clock(graphic_refresh_rate);
    Start_ball_clock(ball_motion_control_clock_rate);
   }
   
   protected void newball(Object sender,EventArgs events)
   {
    ball_center_current_coord_x = ball_center_initial_coord_x;
    ball_center_current_coord_y = ball_center_initial_coord_y;
    graphic_area_refresh_clock.Enabled = false;
    ball_motion_control_clock.Enabled = false;
   }   

   protected void Start_graphic_clock(double refresh_rate)
   {   double actual_refresh_rate = 1.0;
       double elapsed_time_between_tics;
       if(refresh_rate > actual_refresh_rate) 
           actual_refresh_rate = refresh_rate;
       elapsed_time_between_tics = 1000.0/actual_refresh_rate;
       graphic_area_refresh_clock.Interval = (int)System.Math.Round(elapsed_time_between_tics);
       graphic_area_refresh_clock.Enabled = true;
   }

   protected void Start_ball_clock(double update_rate)
   {   double elapsed_time_between_ball_moves;
       if(update_rate < 1.0) update_rate = 1.0;
       elapsed_time_between_ball_moves = 1000.0/update_rate;
       ball_motion_control_clock.Interval = (int)System.Math.Round(elapsed_time_between_ball_moves);
       ball_motion_control_clock.Enabled = true;
   }

   protected void Update_display(System.Object sender, ElapsedEventArgs evt)
   {  Invalidate();
      if(!ball_motion_control_clock.Enabled)
          {graphic_area_refresh_clock.Enabled = false;
           System.Console.WriteLine("The graphical area is no longer refreshing.  You may close the window.");
          }
   }

   protected void Update_ball_position(System.Object sender, ElapsedEventArgs evt)
   {  ball_center_current_coord_x += ball_delta_x;
      ball_center_current_coord_y -= ball_delta_y;
      System.Console.WriteLine("The motion clock ticked and the time is {0}", evt.SignalTime);
      if((int)System.Math.Round(ball_center_current_coord_x + ball_radius) >= formwidth)
             ball_delta_x = -ball_delta_x;

      if((int)System.Math.Round(ball_center_current_coord_y + ball_radius) >= formheight - lowerpanelheight)
            ball_delta_y = -ball_delta_y;

      if((int)System.Math.Round(ball_center_current_coord_x + ball_radius) <= ball_radius)
            ball_delta_x = -ball_delta_x;

      if((int)System.Math.Round(ball_center_current_coord_y + ball_radius) <= 40+ball_radius)
            ball_delta_y = -ball_delta_y;


   }
   protected void exitfromthisprogram(Object sender,EventArgs events)
   {  System.Console.WriteLine("This program will end execution.");
      Close();
   }
}



