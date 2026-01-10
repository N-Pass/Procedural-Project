using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Room level Layout", menuName = "Custom/Procedural Generation/ Room Level Layout Configuration")]
public class RoomLevelLayoutConfiguration : ScriptableObject
{
    [Header("Max Map Size")]
    [SerializeField] private int width = 64;
    [SerializeField] private int length = 64;

    [Header("Room Config")]
    [SerializeField] private RoomTemplate[] roomTemplates;
    [SerializeField] private int doorDistanceFromEdge = 1;

    [Header("Hallway Length")]
    [SerializeField] private int minHallwayLength = 3;
    [SerializeField] private int maxHallwayLength = 5;

    [Header("Room Amount")]
    [SerializeField] private int maxRoomCount = 5;
    [SerializeField] private int minRoomDistance = 1;

    public int Width { get => width; }
    public int Length { get => length; }

    public RoomTemplate[] RoomTemplates { get => roomTemplates; }

    public int DoorDistanceFromEdge { get => doorDistanceFromEdge; }

    public int MinHallwayLength { get => minHallwayLength; }
    public int MaxHallwayLength { get => maxHallwayLength; }

    public int MaxRoomCount { get => maxRoomCount; }
    public int MinRoomDistance { get => minRoomDistance; }

    public Dictionary<RoomTemplate, int> GetAvailiableRooms()
    {
        Dictionary<RoomTemplate, int> availableRooms = new Dictionary<RoomTemplate, int>();
        for (int i = 0; i < roomTemplates.Length; i++)
        {
            availableRooms.Add(roomTemplates[i], roomTemplates[i].NumberOfRooms);
        }

        availableRooms = availableRooms.Where(kvp =>  kvp.Value > 0).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        return availableRooms;
    }

}
