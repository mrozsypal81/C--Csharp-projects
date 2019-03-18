using System;
using System.Drawing;
using System.Windows.Forms;

public class TTTframe : Form
{  private const int totalwidth = 1600;
   private const int totalheight = 900;

   private const int penwid = 3;
   private Label header = new Label();
   private Label announce = new Label();
   private Button startb = new Button();
   private Button quitb = new Button();
   private Pen blackpen = new Pen(Color.Black,penwid);
   private Point loc_header = new Point(600,20);
   private Point loc_startb = new Point(1215,825);
   private Point loc_quitb = new Point(1415,825);
   private int m_x = 0;
   private int m_y = 0;
   private RadioButton radX = new System.Windows.Forms.RadioButton();
   private RadioButton radO = new System.Windows.Forms.RadioButton();
   private GroupBox Players = new GroupBox();
   private int[,]ttt= new int[3,3];
   private bool startg = false;
   private winorloseclass winorloseclassframe = new winorloseclass();   
   public TTTframe()
   {
      Text = "Tic Tac Toe by Michael Rozsypal";
      System.Console.WriteLine("formwidth = {0}. formheight = {1}.",totalwidth,totalheight);
      Size = new Size(totalwidth,totalheight);
      BackColor = Color.White;
      announce.Text = "Winner -will be announced- ";
      announce.Size = new Size(200,25);
      announce.Location = new System.Drawing.Point(450,825);
      announce.BackColor = Color.Magenta;
      header.Text = "Tic Tac Toe by Michael Rozsypal";
      header.Size = new Size(300,30);
      header.Location = loc_header;
      header.BackColor = Color.White;
      startb.Text = "Start";
      startb.Size = new Size(100,50);
      startb.Location = loc_startb;
      startb.BackColor = Color.White;
      quitb.Text = "Quit";
      quitb.Size = new Size(100,50);
      quitb.Location = loc_quitb;
      quitb.BackColor = Color.White;
      radX.BackColor = Color.White;
      radO.BackColor = Color.White;
      radX.Text = "X";
      radX.Size = new System.Drawing.Size(50,20);
      radO.Text = "O";
      radO.Size = new System.Drawing.Size(50,20);
      Players.Size = new System.Drawing.Size(150,75);
      Players.Text = "Player Select";
      Players.BackColor = Color.Yellow;
      Players.Location = new System.Drawing.Point(200,825);
      radX.Location = new System.Drawing.Point(10,30);
      radO.Location = new System.Drawing.Point(70,30);
	  Players.Enabled = false;
	  startb.Enabled = true;
	  radX.Enabled = false;
	  radO.Enabled = false;
      
      Players.Controls.Add(radO);
      Players.Controls.Add(radX);											  
      Controls.Add(Players);
      Controls.Add(header);
      Controls.Add(startb);
      Controls.Add(quitb);
      Controls.Add(announce);
      
      
	  Players.Click += new EventHandler(Playerinfo); 
      startb.Click += new EventHandler(startgame);
      quitb.Click += new EventHandler(exitfromthisprogram);
      radX.Click += new EventHandler(Playerinfo);
      radO.Click += new EventHandler(Playerinfo);
   }
   protected void announcechange(int a){
       if(a == 1){
           announce.Text = "Winner is X's";
       }
       if(a == 2){
           announce.Text = "Winner is O's";
       }
       if(a == 3){
           announce.Text = "Winner -will be announced- ";
       }
       if(a == 4){
           announce.Text = "It's a tie";
       }
   }
    protected override void OnMouseDown(MouseEventArgs me){
        m_x = me.X;
        m_y = me.Y;
        System.Console.WriteLine("Mouse Click");
    }
    protected void Playerinfo(Object sender,EventArgs events){
       
        if(sender == radX){
            radX.Checked = false;
        	radX.Enabled = false;
			radO.Enabled = true;
			Players.Enabled = false;
			Invalidate();
        }
		if(sender == radO){
		    radO.Checked = false;
			radO.Enabled = false;
			radX.Enabled = true;
			Players.Enabled = false;
			Invalidate();
		}
		System.Console.WriteLine("End of Playerinfo");
        System.Console.WriteLine("PrintXorO start");
        if(m_x != 0 && m_y != 0){
		    if(sender == radX){
			    if(m_x < 510 && m_y < 250){
			    System.Console.WriteLine("Top left corner");
				    ttt[0,0] = 1;
			    }
			    if(m_x > 510 && m_x < 1090 && m_y < 250){
			    System.Console.WriteLine("mid top");
				    ttt[1,0] = 1;
			    }
			    if(m_x > 1090 && m_y < 250){
			     System.Console.WriteLine("top right corner");
				    ttt[2,0] = 1;
			    }
			    if(m_x < 510 && m_y < 500 && m_y > 250){
			     System.Console.WriteLine("mid left");
				    ttt[0,1] = 1;
			    }
			    if(m_x > 510 && m_x < 1090 && m_y < 500 && m_y > 250){
			     System.Console.WriteLine("middle square");
				    ttt[1,1] = 1;
			    }
			    if(m_x > 1090 && m_y < 500 && m_y > 250){
			     System.Console.WriteLine("mid right");
				    ttt[2,1] = 1;
			    }
			    if(m_x < 510 && m_y > 500){
			     System.Console.WriteLine("bottom left");
				    ttt[0,2] = 1;
			    }
			    if(m_x > 510 && m_x < 1090 && m_y > 500){
			     System.Console.WriteLine("mid bottom");
				    ttt[1,2] = 1;
			    }
			    if(m_x > 1090 && m_y > 500){
			     System.Console.WriteLine("bottom right");
				    ttt[2,2] = 1;
			    }
		    }
		    if(sender == radO){
			    if(m_x < 510 && m_y < 250){
			    	ttt[0,0] = 2;
			    }
			    if(m_x > 510 && m_x < 1090 && m_y < 250){
			    	ttt[1,0] = 2;
			    }
			    if(m_x > 1090 && m_y < 250){
			    	ttt[2,0] = 2;
			    }
			    if(m_x < 510 && m_y < 500 && m_y > 250){
			    	ttt[0,1] = 2;
			    }
			    if(m_x > 510 && m_x < 1090 && m_y < 500 && m_y > 250){
			    	ttt[1,1] = 2;
			    }
			    if(m_x > 1090 && m_y < 500 && m_y > 250){
				    ttt[2,1] = 2;
			    }
			    if(m_x < 510 && m_y > 500){
				    ttt[0,2] = 2;
			    }
			    if(m_x > 510 && m_x < 1090 && m_y > 500){
				    ttt[1,2] = 2;
			    }
			    if(m_x > 1090 && m_y > 500){
			    	ttt[2,2] = 2;
			    }			
		    }
		}
		System.Console.WriteLine("PrintXorO End");
	Invalidate();
	}
   protected void startgame(Object sender, EventArgs events){
	    startg = true;    
		ttt = new int[3,3];
	   	Players.Enabled = true;
	    radX.Enabled = true;
	    radO.Enabled = true;
	    		//sets mouse values to 0 so player can click where
		m_x = 0;
		m_y = 0;
		Invalidate();
   }

   protected override void OnPaint(PaintEventArgs graph)
  {  Graphics T = graph.Graphics;
      //horizontal lines
      T.DrawLine(blackpen,0,250,totalwidth,250);
	  T.DrawLine(blackpen,0,500,totalwidth,500);
      //vertical lines
	  T.DrawLine(blackpen,510,0,510,750);
	  T.DrawLine(blackpen,1090,0,1090,750);
	  T.FillRectangle(Brushes.Teal,0,750,totalwidth,150);
   if(startg){
	   // prints x's
	   if(ttt[0,0] == 1){
	    System.Console.WriteLine("top left");
		   T.DrawLine(blackpen,0,250,510,0);
		   T.DrawLine(blackpen,0,0,510,250);
	   }
	   if(ttt[1,0] == 1){
	    System.Console.WriteLine("top mid");
		   T.DrawLine(blackpen,510,250,1090,0);
		   T.DrawLine(blackpen,510,0,1090,250);
	   }
	   if(ttt[2,0] == 1){
	    System.Console.WriteLine("top right");
		   T.DrawLine(blackpen,1090,250,1600,0);
		   T.DrawLine(blackpen,1090,0,1600,250);
	   }
	   if(ttt[0,1] == 1){
	    System.Console.WriteLine("mid left");
		   T.DrawLine(blackpen,0,500,510,250);
		   T.DrawLine(blackpen,0,250,510,500);
	   }
	   if(ttt[1,1] == 1){
	    System.Console.WriteLine("middle square");
		   T.DrawLine(blackpen,510,500,1090,250);
		   T.DrawLine(blackpen,510,250,1090,500);
	   }
	   if(ttt[2,1] == 1){
	    System.Console.WriteLine("mid right");
		   T.DrawLine(blackpen,1090,500,1600,250);
		   T.DrawLine(blackpen,1090,250,1600,500);
	   }
	   if(ttt[0,2] == 1){
	    System.Console.WriteLine("bottom left");
		   T.DrawLine(blackpen,0,750,510,500);
		   T.DrawLine(blackpen,0,500,510,750);
	   }
	   if(ttt[1,2] == 1){
	    System.Console.WriteLine("bottom mid");
		   T.DrawLine(blackpen,510,750,1090,500);
		   T.DrawLine(blackpen,510,500,1090,750);
	   }
	   if(ttt[2,2] == 1){
	    System.Console.WriteLine("bottom right");
		   T.DrawLine(blackpen,1090,750,1600,500);
		   T.DrawLine(blackpen,1090,500,1600,750);
	   }
	   //pints o's
	   if(ttt[0,0] == 2){
		   T.DrawEllipse(blackpen,150,10,200,200);
	   }
	   if(ttt[1,0] == 2){
		   T.DrawEllipse(blackpen,670,10,200,200);
	   }
	   if(ttt[2,0] == 2){
		   T.DrawEllipse(blackpen,1250,10,200,200);
	   }
	   if(ttt[0,1] == 2){
		   T.DrawEllipse(blackpen,150,260,200,200);
	   }
	   if(ttt[1,1] == 2){
		   T.DrawEllipse(blackpen,670,260,200,200);
	   }
	   if(ttt[2,1] == 2){
		   T.DrawEllipse(blackpen,1250,260,200,200);
	   }
	   if(ttt[0,2] == 2){
		   T.DrawEllipse(blackpen,150,510,200,200);
	   }
	   if(ttt[1,2] == 2){
		   T.DrawEllipse(blackpen,670,510,200,200);
	   }
	   if(ttt[2,2] == 2){
		   T.DrawEllipse(blackpen,1250,510,200,200);
	   }	   
		  
   }

      int announcevariable = 0;
      announcevariable = (winorloseclassframe.winorlose(ttt));
      announcechange(announcevariable);
      Players.Enabled = true;
      base.OnPaint(graph);  
   }

   protected void exitfromthisprogram(Object sender,EventArgs events)
   {  System.Console.WriteLine("This program will end execution.");
      Close();
   }

}
