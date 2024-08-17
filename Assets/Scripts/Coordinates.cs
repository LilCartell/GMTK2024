public class Coordinates
{
    public int X;
    public int Y;

    public Coordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Coordinates GetCoordinatesInDirection(Directions direction)
    {
        switch(direction)
        {
            case Directions.LEFT:
                return new Coordinates(X - 1, Y);
            case Directions.RIGHT:
                return new Coordinates(X + 1, Y);
            case Directions.UP:
                return new Coordinates(X, Y - 1);
            case Directions.DOWN:
                return new Coordinates(X, Y + 1);
            default:
                return this;
        }
    }
}
