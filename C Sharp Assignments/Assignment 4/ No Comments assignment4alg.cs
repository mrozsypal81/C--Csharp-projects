


public class Twoanimatedlogic
{   private System.Random randomgenerator = new System.Random();

    public double get_random_direction_for_a()
       {
        double randomnumber = randomgenerator.NextDouble();
        randomnumber = randomnumber - 0.5;
        double ball_a_angle_radians = System.Math.PI * randomnumber;
        return ball_a_angle_radians;
       }

    public double get_random_direction_for_b()
       {
        double randomnumber = randomgenerator.NextDouble();
        randomnumber = randomnumber + 0.5;
        double ball_b_angle_radians = System.Math.PI * randomnumber;
        return ball_b_angle_radians;
       }

}