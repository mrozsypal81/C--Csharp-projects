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

