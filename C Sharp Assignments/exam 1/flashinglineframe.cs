
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class flashinglineframe : Form{  
	private const int totalwidth = 1600;
   private const int totalheight = 900;  
   private const int penwidth = 7;
   private Label header = new Label();
   private Label name = new Label();
   private Button startb = new Button();
   private Button exitb = new Button();
   private Pen rpen = new Pen(Color.Red,penwidth);
   private Pen gpen = new Pen(Color.Green,penwidth);
   private Pen open = new Pen(Color.Orange,penwidth);
   private Pen mpen = new Pen(Color.Magenta,penwidth);
   private Pen cpen = new Pen(Color.Cyan,penwidth);
   private Pen wpen = new Pen(Color.White,penwidth);
   private RadioButton rb = new RadioButton();
   private RadioButton gb = new RadioButton();
   private RadioButton ob = new RadioButton();
   private RadioButton mb = new RadioButton();
   private GroupBox bcolor = new GroupBox();
   private int color = 0;
   private bool started = false;
   private int fx = 0;
   private int fy = 0;
   private int sx = 0;
   private int sy = 0;
   private int timer = 0;

   private static System.Timers.Timer line_clock = new System.Timers.Timer();
   
   public flashinglineframe(){ 
      Size = new Size(totalwidth,totalheight);
      BackColor = Color.White;
      name.Text = "Programmer: Michael Rozsypal";
      name.Size = new Size(200,20);
      name.Location = new Point(10,30);
      name.BackColor = Color.White;
      header.Text = "Flashing Line";
      header.Size = new Size(200,20);
      header.Location = new Point(650,20);
      header.BackColor = Color.White;
      startb.Text = "Start";
      startb.Size = new Size(85,25);
      startb.Location = new Point(500,825);
      startb.BackColor = Color.White;
      exitb.Text = "Exit";
      exitb.Size = new Size(85,25);
      exitb.Location = new Point(1000,825);
      exitb.BackColor = Color.White;
	  bcolor.Text = "Colors";
	  bcolor.Size = new Size(350,50);
	  bcolor.BackColor = Color.White;
	  bcolor.Location = new Point(600,825);
	  bcolor.Enabled = true;     
	  rb.Text = "Red";
	  rb.Size = new Size(50,25);
	  rb.Location = new Point(20,25);
	  rb.BackColor = Color.White;
	  rb.Enabled = true;
	  gb.Text = "Green";
	  gb.Size = new Size(75,25);
	  gb.Location = new Point(90,25);
	  gb.BackColor = Color.White;
	  gb.Enabled = true;
	  ob.Text = "Orange";
	  ob.Size = new Size(75,25);
	  ob.Location = new Point(190,25);
	  ob.BackColor = Color.White;
	  ob.Enabled = true;
	  mb.Text = "magenta";
	  mb.Size = new Size(75,25);
	  mb.Location = new Point(275,25);
	  mb.BackColor = Color.White;
	  mb.Enabled = true;

	  Controls.Add(bcolor); 
	  bcolor.Controls.Add(rb);
	  bcolor.Controls.Add(gb);
	  bcolor.Controls.Add(ob);
	  bcolor.Controls.Add(mb);
      Controls.Add(header);
      Controls.Add(name);
      Controls.Add(startb);
      Controls.Add(exitb);
	   
      exitb.Click += new EventHandler(exitfromthisprogram);
	  startb.Click += new EventHandler(Manage_line);
	  rb.Click += new EventHandler(Manage_line);
	  gb.Click += new EventHandler(Manage_line);
	  ob.Click += new EventHandler(Manage_line);
	  mb.Click += new EventHandler(Manage_line); 
	   
      line_clock.Enabled = false;
      line_clock.Elapsed += new ElapsedEventHandler(Manage_line_time);
      line_clock.Interval = (int)1000;
   }
    protected override void OnMouseDown(MouseEventArgs me){
        if(fx == 0 && fy == 0){
        fx = me.X;
        fy = me.Y;
        }
        else if(sx == 0 && sy == 0){
        sx = me.X;
        sy = me.Y;
        }
        System.Console.WriteLine("Mouse Click");
    }   

   protected override void OnPaint(PaintEventArgs g)
   {  Graphics graph = g.Graphics;
      graph.FillRectangle(Brushes.Yellow,0,800,totalwidth,200);
	  graph.FillRectangle(Brushes.White,0,0,totalwidth,75);
	  if(timer == 0){
	  graph.DrawLine(wpen,fx,fy,sx,sy);
	  }
	  else if(timer == 1){
	  if(color == 1){
            graph.DrawLine(rpen,fx,fy,sx,sy);
	  }
	  if(color == 2){
            graph.DrawLine(gpen,fx,fy,sx,sy);
	  }
	  if(color == 3){
            graph.DrawLine(open,fx,fy,sx,sy);
	  }
	  if(color == 4){
	        graph.DrawLine(mpen,fx,fy,sx,sy);
	  }
	  if(color == 5){
            graph.DrawLine(cpen,fx,fy,sx,sy);  
	  }
	 }
      base.OnPaint(g);
   }

   protected void Manage_line_time(Object sender, ElapsedEventArgs evt){
   if(timer == 0){
            timer = 1;
   }
   else if(timer == 1){
 	  if(color == 1){
            line_clock.Interval = (int)1000;
            timer = 0;
	  }
	  if(color == 2){
            line_clock.Interval = (int)1000;
            timer = 0;
	  }
	  if(color == 3){
            line_clock.Interval = (int)1000;
            timer = 0;
	  }
	  if(color == 4){
            line_clock.Interval = (int)1000;
            timer = 0;
	  }
	  if(color == 5){
            line_clock.Interval = (int)1000;
            timer = 0;
	  }
    }   
    Invalidate();
   }
   protected void Manage_line(Object sender,EventArgs events){
        if(sender == startb){
            if(color == 0){
                color = 5;
            }
            if(started == false){
                line_clock.Enabled = true;
                started = true;
                Invalidate();
            }
            else if(started == true){
                line_clock.Enabled = false;
                started = false;
                color = 0;
                fx = 0;
                fy = 0;
                sx = 0;
                sy = 0;
                Invalidate();
            }
        }
        if(sender == rb){
            color = 1;
        }
        if(sender == gb){
            color = 2;
        }
        if(sender == ob){
            color = 3;
        }
        if(sender == mb){
            color = 4;
        }
   }
   protected void exitfromthisprogram(Object sender,EventArgs events)
   {  System.Console.WriteLine("This program will end execution.");
      Close();
   }

}



