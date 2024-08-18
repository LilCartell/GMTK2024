public class Coordinates
{
    public float X;
    public float Y;

    public Coordinates(float x, float y)
    {
        X = x;
        Y = y;
    }

    public Coordinates GetCoordinatesInDirection(Directions direction)
    {
        switch(direction)
        {
            case Directions.LEFT:
                return new Coordinates(X - 1.0f, Y);
            case Directions.RIGHT:
                return new Coordinates(X + 1.0f, Y);
            case Directions.UP:
                return new Coordinates(X, Y - 1.0f);
            case Directions.DOWN:
                return new Coordinates(X, Y + 1.0f);
            default:
                return this;
        }
    }
}
