using System;
using System.Drawing;
using System.Windows.Forms;

public class Enhanced_button: Button
{ Point p = new Point();
  public Point get_mouse_location()
  {return p;
  }
  protected override void OnMouseDown(MouseEventArgs me)
   {  p.X = me.X;
      p.Y = me.Y;
   }
}
public class winorloseclass{
public winorloseclass(){}
//System.Console.WriteLine("Just before winorlose method");
public int winorlose(int [,]ttt){
    for (int r=0;r<3;r++){
        System.Console.WriteLine("Inside first for loop");
        if((ttt[0,r] == 1) && (ttt[1,r] == 1) && (ttt[2,r] == 1)){
            return 1;
        }
        System.Console.WriteLine("Mid 1st for loop");
        if((ttt[0,r] == 2) && (ttt[1,r] == 2) && (ttt[2,r] == 2)){
            return 2;
        }
        System.Console.WriteLine("End first for loop");
		for(int c = 0;c < 3; c++){
			if((ttt[r,0] == 1) && (ttt[r,1] == 1) && (ttt[r,2] == 1)){
                return 1;
            }
        	if((ttt[r,0] == 2) && (ttt[r,1] == 2) && (ttt[r,2] == 2)){
            	return 2;
        	}            
        }
    }
    System.Console.WriteLine("Diagonal 1");
    if((ttt[0,0] == 1) && (ttt[1,1] == 1) && (ttt[2,2] == 1)){
        return 1;
    }
    System.Console.WriteLine("Diagonal 2");
    if((ttt[2,0] == 1) && (ttt[1,1] == 1) && (ttt[0,2] == 1)){
        return 1;
    }
    System.Console.WriteLine("Diagonal 3");
    if((ttt[0,0] == 2) && (ttt[1,1] == 2) && (ttt[2,2] == 2)){
        return 2;
    }
    System.Console.WriteLine("Diagonal 4");
    if((ttt[2,0] == 2) && (ttt[1,1] == 2) && (ttt[0,2] == 2)){
        return 2;
    }
    System.Console.WriteLine("checking for 0s");
    for(int i=0;i<3;i++){
        for(int x=0;x<3; x++){
            if(ttt[i,x] == 0){
                return 3;
            }
        }
    }
    System.Console.WriteLine("Tied game");

    return 4;
}
}
