using System;
using System.Collections.Generic;
using UnityEngine;

public class LayoutGeneratorRooms : MonoBehaviour
{
    [SerializeField] private int width = 64;
    [SerializeField] private int length = 64;

    [SerializeField] private int roomWidthMin = 3;
    [SerializeField] private int roomWidthMax = 5;
    [SerializeField] private int roomLengthMin = 3;
    [SerializeField] private int roomLengthMax = 5;

    [SerializeField] private GameObject levelLayoutDisplay;
    [SerializeField] private List<Hallway> openDoorways;

    private System.Random random;

    private Level level;

    [ContextMenu("Generate Level layout")]
    public void GenerateLevel()
    {
        random = new System.Random();
        openDoorways = new List<Hallway>();
        level = new Level(width, length);
        var roomRect = GetStartRoomRect();
        Debug.Log(roomRect);
        Room room = new Room(roomRect);
        List<Hallway> hallways = room.CalculateAllPossibleDoorways(room.Area.width, room.Area.height, 1);
        hallways.ForEach((h) => h.StartRoom = room);
        hallways.ForEach((h) => openDoorways.Add(h));
        level.AddRoom(room);

        Room testRoom1 = new Room(new RectInt(3, 6, 6, 10));
        Room testRoom2 = new Room(new RectInt(15, 4, 10, 12));
        Hallway testHallway = new Hallway(HallwayDirection.Right, new Vector2Int(6, 3), testRoom1);
        testHallway.EndPosition = new Vector2Int(0, 5);
        testHallway.EndRoom = testRoom2;
        level.AddRoom(testRoom1);
        level.AddRoom(testRoom2);
        level.AddHallway(testHallway);

        DrawLayout(roomRect);   
    }

    private RectInt GetStartRoomRect()
    {
        int roomWidth = random.Next(roomWidthMin, roomWidthMax);
        int availableWidthX = width/2 - roomWidth;
        int randomX = random.Next(0, availableWidthX);
        int roomX = randomX + (width / 4);

        int roomLength = random.Next(roomLengthMin, roomLengthMax);
        int availableLengthY = length / 2 - roomLength;
        int randomY = random.Next(0, availableLengthY);
        int roomY = randomY + (length / 4);

        return new RectInt(roomX, roomY, roomWidth, roomLength);
    }


    private void DrawLayout(RectInt roomCandidateRect = new RectInt())
    {
        var renderer = levelLayoutDisplay.GetComponent<Renderer>();

        var layoutTexture = (Texture2D) renderer.sharedMaterial.mainTexture;

        layoutTexture.Reinitialize(width, length);
        levelLayoutDisplay.transform.localScale = new Vector3(width, length, 1);
        layoutTexture.FillWithColor(Color.black);

        Array.ForEach(level.Rooms, room => layoutTexture.DrawRectangle(room.Area, Color.white));
        Array.ForEach(level.Hallways, hallway => layoutTexture.DrawLine(hallway.StartPositionAbsolute, hallway.EndPositionAbsolute, Color.white));

        layoutTexture.DrawRectangle(roomCandidateRect, Color.blue);

        openDoorways.ForEach(
            hallway => layoutTexture.SetPixel(
                hallway.StartPositionAbsolute.x, 
                hallway.StartPositionAbsolute.y, 
                hallway.StartDirection.GetColor()));

        layoutTexture.SaveAsset();
    }
}
