using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : ScriptableObject {

    public bool NoRoom;
    public bool NorthDoorOpen, SouthDoorOpen, WestDoorOpen, EastDoorOpen;
    public bool NorthWallOpen, SouthWallOpen, WestWallOpen, EastWallOpen;
	public List<GameObject> ObstacleList;
    public BoundsInt RoomArea;
    public int RoomParentId;

    public Room()
    {
        ObstacleList = new List<GameObject>();

    }
    /// <summary>
    /// Set the room Area
    /// </summary>
    /// <param name="Position">Center of the room</param>
    /// <param name="Size">Size in X,Y,Z plane </param>
    public void SetRoomArea(Vector3Int Position, Vector3Int Size)
    {

        RoomArea = new BoundsInt(Position, Size);
    }
    public BoundsInt GetRoomArea()
    {
        return RoomArea;
    }
}
