using UnityEngine;

//魔法使用時にICastCommandのExecute関数に渡す構造体
public readonly struct CastContext
{
    public Vector3 Position { get; }
    public int Direction { get; }
    public int Strength { get; }

    public CastContext(Vector3 position, int direction, int strength)
    {
        Position = position;
        Direction = direction;
        Strength = strength;
    }
}
