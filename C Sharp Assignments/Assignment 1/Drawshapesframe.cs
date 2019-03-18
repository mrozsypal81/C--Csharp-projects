using System;
using System.Drawing;
using System.Windows.Forms;

public class Drawshapesframe : Form
{  private const int totalwidth = 1600;
   private const int totalheight = 900;
   private const int upper_x_cord_left = 100;
   private const int upper_y_cord_left = 80;
   private const int rec_width = 1300;
   private const int rec_height = 620;
   private const int lower_x_cord_right = upper_x_cord_left + rec_width;
   private const int lower_y_cord_right = upper_y_cord_left + rec_height;
   private const int circ_x_cord = 400;
   private const int circ_y_cord = 175;
   private const int circradx = 350;
   private const int circrady = 350;


   private const int penwid = 3;
   private Label header = new Label();
   private Button drawb = new Button();
   private Button clearb = new Button();
   private Button quitb = new Button();
   private Button circleb = new Button();
   private Button triangleb = new Button();
   private Button recb = new Button();
   private Button redb = new Button();
   private Button blueb = new Button();
   private Button greenb = new Button();
   private Pen bpen = new Pen(Color.Blue,penwid);
   private Pen gpen = new Pen(Color.Green,penwid);
   private Pen rpen = new Pen(Color.Red,penwid);
   private Pen blackpen = new Pen(Color.Black,penwid);
   private bool recvisible = false;
   private bool trivisible = false;
   private bool circvisible = false;
   private bool recselect = false;
   private bool triselect = false;
   private bool circselect = false;
   private int color = 0;
   private int reccolor = 0;
   private int tricolor = 0;
   private int circcolor = 0;
   private Point loc_header = new Point(800,20);
   private Point loc_drawb = new Point(1215,800);
   private Point loc_clearb = new Point(1315,800);
   private Point loc_quitb = new Point(1415,800);
   private Point loc_blueb = new Point(1015,800);
   private Point loc_greenb = new Point(915,800);
   private Point loc_redb = new Point(815,800);
   private Point loc_triangleb = new Point(615,800);
   private Point loc_recb = new Point(515,800);
   private Point loc_circleb = new Point(415,800);
   
   public Drawshapesframe()
   {
      Text = "Shapes by Michael Rozsypal";
      System.Console.WriteLine("formwidth = {0}. formheight = {1}.",totalwidth,totalheight);
      Size = new Size(totalwidth,totalheight);
      BackColor = Color.White;
      header.Text = "Shapes by Michael Rozsypal";
      header.Size = new Size(300,30);
      header.Location = loc_header;
      header.BackColor = Color.White;
      drawb.Text = "Draw";
      drawb.Size = new Size(85,25);
      drawb.Location = loc_drawb;
      drawb.BackColor = Color.White;
      clearb.Text = "Clear";
      clearb.Size = new Size(85,25);
      clearb.Location = loc_clearb;
      clearb.BackColor = Color.White;
      quitb.Text = "Exit";
      quitb.Size = new Size(85,25);
      quitb.Location = loc_quitb;
      quitb.BackColor = Color.White;
      blueb.Text = "Blue";
      blueb.Size = new Size(85,25);
      blueb.Location = loc_blueb;
      blueb.BackColor = Color.Blue;
      greenb.Text = "Green";
      greenb.Size = new Size(85,25);
      greenb.Location = loc_greenb;
      greenb.BackColor = Color.Green;
      redb.Text = "Red";
      redb.Size = new Size(85,25);
      redb.Location = loc_redb;
      redb.BackColor = Color.Red;
      triangleb.Text = "Triangle";
      triangleb.Size = new Size(85,25);
      triangleb.Location = loc_triangleb;
      triangleb.BackColor = Color.White;
      recb.Text = "Rectangle";
      recb.Size = new Size(85,25);
      recb.Location = loc_recb;
      recb.BackColor = Color.White;
      circleb.Text = "Circle";
      circleb.Size = new Size(85,25);
      circleb.Location = loc_circleb;
      circleb.BackColor = Color.White;
      
      Controls.Add(header);
      Controls.Add(drawb);
      Controls.Add(clearb);
      Controls.Add(quitb);
      Controls.Add(redb);
      Controls.Add(greenb);
      Controls.Add(blueb);
      Controls.Add(recb);
      Controls.Add(triangleb);
      Controls.Add(circleb);
	   
      redb.Click += new EventHandler(makered);
      blueb.Click += new EventHandler(makeblue);
      greenb.Click += new EventHandler(makegreen);
      recb.Click += new EventHandler(makerec);
      triangleb.Click += new EventHandler(maketri);
      circleb.Click += new EventHandler(makecirc);
      drawb.Click += new EventHandler(showshapes);
      clearb.Click += new EventHandler(clearall);
      quitb.Click += new EventHandler(exitfromthisprogram);
   }

   protected override void OnPaint(PaintEventArgs ee)
   {  Graphics graph = ee.Graphics;
      if(recvisible){
		  if(reccolor == 1){
			  graph.DrawRectangle(rpen,upper_x_cord_left,upper_y_cord_left,rec_width,rec_height);
		  }
		  if(reccolor == 2){
			  graph.DrawRectangle(bpen,upper_x_cord_left,upper_y_cord_left,rec_width,rec_height);
		  }
		  if(reccolor == 3){
			  graph.DrawRectangle(gpen,upper_x_cord_left,upper_y_cord_left,rec_width,rec_height);
		  }
	  }
	  if(circvisible){
		  if(circcolor == 1){
			  graph.DrawEllipse(rpen,circ_x_cord,circ_y_cord,circradx,circrady);
		  }
		  if(circcolor == 2){
			  graph.DrawEllipse(bpen,circ_x_cord,circ_y_cord,circradx,circrady);
		  }
		  if(circcolor == 3){
			  graph.DrawEllipse(gpen,circ_x_cord,circ_y_cord,circradx,circrady);
		  }
	  }
	  if(trivisible){
		  if(tricolor == 1){
			  graph.DrawPolygon(rpen,new Point[3] {new Point(100,600),new Point(800,100),new Point(1500,600)});    
		  }
		  if(tricolor == 2){
			  graph.DrawPolygon(bpen,new Point[3] {new Point(100,600),new Point(800,100),new Point(1500,600)});
		  }
		  if(tricolor == 3){
			  graph.DrawPolygon(gpen,new Point[3] {new Point(100,600),new Point(800,100),new Point(1500,600)});
		  }
	  }
      base.OnPaint(ee);
   }
 	protected void makered(Object sender, EventArgs events){
	 color = 1;
 	}
 	protected void makeblue(Object sender, EventArgs events){
	 color = 2;
 	}
 	protected void makegreen(Object sender, EventArgs events){
	 color = 3;
 	}
 	protected void makerec(Object sender, EventArgs events){
		recselect = true;
		triselect = false;
		circselect = false;
	}
    protected void maketri(Object sender, EventArgs events){
		triselect = true;
		recselect = false;
		circselect = false;
	}
    protected void makecirc(Object sender, EventArgs events){
		circselect = true;
		recselect = false;
		triselect = false;
	}

   protected void showshapes(Object sender,EventArgs events){
	   if(recselect){
           reccolor = color;
		   recvisible = true;
	   }
           if(triselect){
           tricolor = color;
		   trivisible = true;
	   }
           if(circselect){
           circcolor = color;
		   circvisible = true;
	   }
      Invalidate();
      System.Console.WriteLine("You clicked on the Draw button.");
   }

   protected void clearall(Object sender,EventArgs events)
   {  recvisible = false;
	  trivisible = false;
	  circvisible = false;
	  color = 0;
	
	  
      Invalidate();
      System.Console.WriteLine("You clicked on the Clear button.");
   }

   protected void exitfromthisprogram(Object sender,EventArgs events)
   {  System.Console.WriteLine("This program will end execution.");
      Close();
   }

}
