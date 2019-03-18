using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class assignment4frame : Form
{  private const int formwidth = 1600;
   private const int formheight = 900; 
   private const int ballrad = 8;
   private const int horadjustment = 30;
   private Label header = new Label();
   private Button startb = new Button();
   private Button exitb = new Button();
   private TextBox speed = new TextBox();
   private TextBox angle = new TextBox();
   private bool start_of = false;
   private bool timer_start = true;
   private double b_dis_moved_per_refresh = 1.0;  
   private double ball_real_coord_x = 800.0;
   private double ball_real_coord_y = (double)(300.0 - ballrad);
   private int ball_int_coord_x;  
   private int ball_int_coord_y;  
   private double ball_horizontal_delta;
   private double ball_vertical_delta;
   private double ball_angle = 0.0;
   private const double grefreshrate = 30.0;  
   private static System.Timers.Timer graphic_refresh_clock = new System.Timers.Timer();
   private double ball_update_rate = 30.0;  
   private static System.Timers.Timer ball_clock = new System.Timers.Timer();
   private bool ball_clock_active = false;  
   
   public assignment4frame()
   {  
      Text = "Moving Ball";
      Size = new Size(formwidth,formheight);
      BackColor = Color.White;
      header.Text = "Moving Ball by Michael Rozsypal";
      header.Size = new Size(200,20);
      header.Location = new Point(650,20);
      header.BackColor = Color.White;
      startb.Text = "Start";
      startb.Size = new Size(85,25);
      startb.Location = new Point(900,825);
      startb.BackColor = Color.White;
      exitb.Text = "Exit";
      exitb.Size = new Size(85,25);
      exitb.Location = new Point(1000,825);
      exitb.BackColor = Color.White;
	  speed.Size = new Size(85,50);
	  speed.Location = new Point(250,825);
	  speed.Text = "Enter Speed";
	  speed.AcceptsReturn = true;
	  angle.Size = new Size(85,50);
	  angle.Location = new Point(350,825);
	  angle.Text = "Enter Angle";
	  angle.AcceptsReturn = true;      

      ball_int_coord_x = (int)(ball_real_coord_x);
      ball_int_coord_y = (int)(ball_real_coord_y);
      System.Console.WriteLine("Initial coordinates: ball_int_coord_x = {0}. ball_int_coord_y = {1}.",
                               ball_int_coord_x,ball_int_coord_y);
      Controls.Add(header);
      Controls.Add(startb);
      Controls.Add(exitb);
	  Controls.Add(speed);
	  Controls.Add(angle);
	   
      exitb.Click += new EventHandler(exitfromthisprogram);
	  startb.Click += new EventHandler(startmoving);	   
	  speed.Click += new EventHandler(startmoving);
	  angle.Click += new EventHandler(startmoving);
	   
      graphic_refresh_clock.Enabled = false; 
      graphic_refresh_clock.Elapsed += new ElapsedEventHandler(Updatedisplay);  

      ball_clock.Enabled = false;
      ball_clock.Elapsed += new ElapsedEventHandler(Updateball);

   }

   protected override void OnPaint(PaintEventArgs o)
   {  Graphics graph = o.Graphics;
      graph.FillEllipse(Brushes.Yellow,ball_int_coord_x,ball_int_coord_y,2*ballrad,2*ballrad);
      base.OnPaint(o);
   }

   protected void Startgclock(double refreshrate)
   {   double elapsedtimebetweentics;
       if(refreshrate < 1.0) refreshrate = 1.0;
       elapsedtimebetweentics = 1000.0/refreshrate;
       graphic_refresh_clock.Interval = (int)System.Math.Round(elapsedtimebetweentics);
       graphic_refresh_clock.Enabled = true;
   }

   protected void Startballclock(double updaterate)
   {   double elapsedtimebetweenballmoves;
       if(updaterate < 1.0) updaterate = 1.0;
       elapsedtimebetweenballmoves = 1000.0/updaterate;
       ball_clock.Interval = (int)System.Math.Round(elapsedtimebetweenballmoves);
       ball_clock_active = true;
   }

   protected void Updatedisplay(System.Object sender, ElapsedEventArgs evt)
   {  Invalidate();
      if(!ball_clock_active)
          {graphic_refresh_clock.Enabled = false;
           System.Console.WriteLine("The graphical area is no longer refreshing.  You may close the window.");
          }
   }

   protected void Updateball(System.Object sender, ElapsedEventArgs evt)
   {  ball_real_coord_x = ball_real_coord_x + ball_horizontal_delta;
      ball_real_coord_y = ball_real_coord_y - ball_vertical_delta;  
      ball_int_coord_x = (int)System.Math.Round(ball_real_coord_x);
      ball_int_coord_y = (int)System.Math.Round(ball_real_coord_y);
      ball_horizontal_delta = b_dis_moved_per_refresh*System.Math.Cos(ball_angle/57.2958);
      ball_vertical_delta = b_dis_moved_per_refresh*System.Math.Sin(ball_angle/57.2958);
      if(ball_int_coord_x >= formwidth || ball_int_coord_x == 0 || ball_int_coord_y + 2*ballrad <= 100 || ball_int_coord_y >= 700)
          {ball_clock_active = false;
           ball_clock.Enabled = false;
		   	if(start_of && !ball_clock_active){
				ball_angle = 0.0;
				ball_update_rate = 30.0;
				start_of = false;
				timer_start = true;
				startb.Text = "Start";
				ball_real_coord_x = 800.0;
                ball_real_coord_y = (double)(300.0 - ballrad);
			}
           System.Console.WriteLine("The clock controlling the ball has stopped.");
          }
   }
    protected void startmoving(Object sender,EventArgs events){
	if(start_of && sender == startb){
		startb.Text = "Resume";
		ball_clock.Enabled = false;
		start_of = false;
	}
	else if(sender == startb){
	startb.Text = "Pause";
	start_of = true;
	ball_clock.Enabled = true;
	}
	if(speed.Text != "Enter Speed"){
	    System.Console.WriteLine("SPEED SET FUNCTION");
		ball_update_rate = double.Parse(speed.Text);
	}
	if(angle.Text != "Enter Angle"){
	    System.Console.WriteLine("ANGLE SET FUNCTION");
		ball_angle = double.Parse(angle.Text);
	}
    if(timer_start && sender == startb){ 
		Startgclock(grefreshrate);  
   	    Startballclock(ball_update_rate);	
		timer_start = false;
	}
	
   }
    protected void exitfromthisprogram(Object sender,EventArgs events)
   {  System.Console.WriteLine("This program will end execution.");
      Close();
   }

}
