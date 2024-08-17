using UnityEngine;

public enum Directions
{
    NONE,
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public static class DirectionsExtension
{
    public static Directions GetOpposite(this Directions direction)
    {
        switch(direction)
        {
            case Directions.UP:
                return Directions.DOWN;
            case Directions.DOWN:
                return Directions.UP;
            case Directions.LEFT:
                return Directions.RIGHT;
            case Directions.RIGHT:
                return Directions.LEFT;
            default:
                return Directions.NONE;
        }
    }

    public static Quaternion GetRotation(this Directions direction)
    {
        switch (direction)
        {
            case Directions.LEFT:
                return Quaternion.Euler(0, 0, 90);
            case Directions.DOWN:
                return Quaternion.Euler(0, 0, 180);
            case Directions.RIGHT:
                return Quaternion.Euler(0, 0, 270);
            default:
                return Quaternion.identity;
        }
    }

    public static Vector3 GetNormalizedDirection(this Directions direction)
    {
        switch (direction)
        {
            case Directions.UP:
                return Vector3.up;
            case Directions.DOWN:
                return Vector3.down;
            case Directions.LEFT:
                return Vector3.left;
            case Directions.RIGHT:
                return Vector3.right;
            default:
                return Vector3.zero;
        }
    }
}