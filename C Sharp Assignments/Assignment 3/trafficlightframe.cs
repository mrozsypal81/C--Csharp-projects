
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class trafficlightframe : Form{  
	private const int totalwidth = 1600;
   private const int totalheight = 900;  
   private const int penwidth = 3;
   private int clock_iden = 1; // which state the clock was in either fast medium or slow
   private int re_or_pau = 1; //if the button is supposed to pause or resume
   private Label header = new Label();
   private Button startb = new Button();
   private Button pauseb = new Button();
   private Button exitb = new Button();
   private Pen bkpen = new Pen(Color.Black,penwidth);
   private RadioButton fastb = new RadioButton();
   private RadioButton medb = new RadioButton();
   private RadioButton slowb = new RadioButton();
   private GroupBox speed = new GroupBox();
   private SolidBrush rbrush = new SolidBrush(Color.Red);
   private SolidBrush ybrush = new SolidBrush(Color.Yellow);
   private SolidBrush gbrush = new SolidBrush(Color.DarkGreen);
   private string color;

   private static System.Timers.Timer light_clock_slow = new System.Timers.Timer();
   private static System.Timers.Timer light_clock_fast = new System.Timers.Timer();
   private static System.Timers.Timer light_clock_medium = new System.Timers.Timer();
   private int light_counter = 0;
   
   public trafficlightframe(){ 
      Size = new Size(totalwidth,totalheight);
      BackColor = Color.Green;
      header.Text = "Traffic Light by Michael Rozsypal";
      header.Size = new Size(200,20);
      header.Location = new Point(650,20);
      header.BackColor = Color.White;
      startb.Text = "Start";
      startb.Size = new Size(85,25);
      startb.Location = new Point(500,825);
      startb.BackColor = Color.White;
      pauseb.Text = "Pause";
      pauseb.Size = new Size(85,25);
      pauseb.Location = new Point(900,825);
      pauseb.BackColor = Color.White;
      exitb.Text = "Exit";
      exitb.Size = new Size(85,25);
      exitb.Location = new Point(1000,825);
      exitb.BackColor = Color.White;
	  speed.Text = "Speed Setting";
	  speed.Size = new Size(275,50);
	  speed.BackColor = Color.White;
	  speed.Location = new Point(600,825);
	  speed.Enabled = false;      
	  fastb.Text = "Fast";
	  fastb.Size = new Size(50,25);
	  fastb.Location = new Point(20,25);
	  fastb.BackColor = Color.White;
	  fastb.Enabled = false;
	  medb.Text = "Medium";
	  medb.Size = new Size(75,25);
	  medb.Location = new Point(90,25);
	  medb.BackColor = Color.White;
	  medb.Enabled = false;
	  slowb.Text = "Slow";
	  slowb.Size = new Size(50,25);
	  slowb.Location = new Point(190,25);
	  slowb.BackColor = Color.White;
	  slowb.Enabled = false;

	  Controls.Add(speed); 
	  speed.Controls.Add(fastb);
	  speed.Controls.Add(medb);
	  speed.Controls.Add(slowb);
      Controls.Add(header);
      Controls.Add(startb);
      Controls.Add(pauseb);
      Controls.Add(exitb);
	   
      exitb.Click += new EventHandler(exitfromthisprogram);
	  startb.Click += new EventHandler(Manage_lights);
	  pauseb.Click += new EventHandler(Pause_Resume);
	  fastb.Click += new EventHandler(Manage_lights);
	  medb.Click += new EventHandler(Manage_lights);
	  slowb.Click += new EventHandler(Manage_lights); 
	   
      light_clock_slow.Enabled = false;
      light_clock_slow.Elapsed += new ElapsedEventHandler(Manage_lights_slow);
      light_clock_slow.Interval = 1;
	   
	  light_clock_medium.Enabled = false;
	  light_clock_medium.Elapsed += new ElapsedEventHandler(Manage_lights_medium);
	  light_clock_medium.Interval = 1;
	   
	  light_clock_fast.Enabled = false;
	  light_clock_fast.Elapsed += new ElapsedEventHandler(Manage_lights_fast);
	  light_clock_fast.Interval = 1;
   }

   protected override void OnPaint(PaintEventArgs g)
   {  Graphics graph = g.Graphics;
      graph.FillRectangle(Brushes.Yellow,0,800,totalwidth,200);
	  graph.FillRectangle(Brushes.Teal,0,0,totalwidth,75);
	  g.Graphics.FillEllipse(Brushes.Gray,650,150,200,200);
	  g.Graphics.FillEllipse(Brushes.Gray,650,350,200,200);
	  g.Graphics.FillEllipse(Brushes.Gray,650,550,200,200);
	  if(color == "Red"){
		  g.Graphics.FillEllipse(rbrush,650,150,200,200);
	  }
	  if(color == "Yellow"){
		  g.Graphics.FillEllipse(ybrush,650,350,200,200);
	  }
	  if(color == "Green"){
		  g.Graphics.FillEllipse(gbrush,650,550,200,200);
	  }
      base.OnPaint(g);
   }

   protected void Manage_lights_slow(Object sender, ElapsedEventArgs evt){
	   clock_iden = 1;
	   speed.Enabled = true;
	   fastb.Enabled = true;
	   medb.Enabled = true;
	   slowb.Enabled = true;
	   switch(light_counter)
       {case 0: light_clock_slow.Interval = (int)4000;//red
				color = "Red";
                break;
        case 1: light_clock_slow.Interval = (int)1000;//yellow
				color = "Yellow";
                break;
        case 2: light_clock_slow.Interval = (int)3000;//green
				color = "Green";
                break;
       }
    light_counter = (light_counter+1)%3;
    Invalidate();
   }
   protected void Manage_lights_medium(Object sender, ElapsedEventArgs evt){
	   clock_iden = 2;
	   switch(light_counter)
       {case 0: light_clock_medium.Interval = (int)2000;//red
				color = "Red";
                break;
        case 1: light_clock_medium.Interval = (int)500;//yellow
				color = "Yellow";
                break;
        case 2: light_clock_medium.Interval = (int)1500;//green
				color = "Green";
                break;
       }
    light_counter = (light_counter+1)%3;
    Invalidate();
   }
   protected void Manage_lights_fast(Object sender, ElapsedEventArgs evt){
	   clock_iden = 3;
	   switch(light_counter)
       {case 0: light_clock_fast.Interval = (int)1000;//red
				color = "Red";
                break;
        case 1: light_clock_fast.Interval = (int)250;//yellow
				color = "Yellow";
                break;
        case 2: light_clock_fast.Interval = (int)750;//green
				color = "Green";
                break;
       }
    light_counter = (light_counter+1)%3;
    Invalidate();
   }
protected void Pause_Resume(Object sender,EventArgs events){	
        if(re_or_pau == 1){
	   		re_or_pau = 2;
			pauseb.Text = "Resume";
	        light_clock_slow.Enabled = false;
	        light_clock_medium.Enabled = false;
	        light_clock_fast.Enabled = false;			
		}			
	   		else if(re_or_pau == 2){
				if(clock_iden == 1){
					re_or_pau = 1;
					pauseb.Text = "Pause";
					slowb.PerformClick();
				}
				if(clock_iden == 2){
					re_or_pau = 1;
					pauseb.Text = "Pause";
					medb.PerformClick();
				}
				if(clock_iden == 3){
					re_or_pau = 1;
					pauseb.Text = "Pause";
					fastb.PerformClick();
				}
			}
   }
   protected void Manage_lights(Object sender,EventArgs events){
       if(sender == slowb || sender == startb){
	   light_clock_slow.Enabled = true;
	   light_clock_medium.Enabled = false;
	   light_clock_fast.Enabled = false;
       }
       if(sender == medb){
       light_clock_medium.Enabled = true;
	   light_clock_slow.Enabled = false;
	   light_clock_fast.Enabled = false;       
       }
       if(sender == fastb){
       light_clock_fast.Enabled = true;
       light_clock_slow.Enabled = false;
	   light_clock_medium.Enabled = false;       
       }
   }

   protected void exitfromthisprogram(Object sender,EventArgs events)
   {  System.Console.WriteLine("This program will end execution.");
      Close();
   }

}



