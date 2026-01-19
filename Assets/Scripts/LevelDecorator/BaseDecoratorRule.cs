using UnityEngine;

public abstract class BaseDecoratorRule : ScriptableObject
{
    internal abstract bool CanBeApplied(TileType[,] levelDorated, Room room);
    internal abstract void Apply(TileType[,] levelDecorated, Room room, Transform parent);
}
