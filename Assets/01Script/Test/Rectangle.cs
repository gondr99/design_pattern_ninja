public class Rectangle : Shape
{
    public float width, height;
    public override float CalculateArea()
    {
        return width * height;
    }
}
