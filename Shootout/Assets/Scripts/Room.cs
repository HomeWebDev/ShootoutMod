using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public bool NoRoom;
    public bool NorthDoorOpen, SouthDoorOpen, WestDoorOpen, EastDoorOpen;
    public bool NorthWallOpen, SouthWallOpen, WestWallOpen, EastWallOpen;
	public List<GameObject> ObstacleList;
    public Bounds RoomArea;

    public Room()
    {
        ObstacleList = new List<GameObject>();

    }
    /// <summary>
    /// Set the room Area
    /// </summary>
    /// <param name="Center">Center of the room</param>
    /// <param name="Size">Size in X,Y,Z plane </param>
    public void SetRoomArea(Vector3 Center,Vector3 Size)
    {

        RoomArea = new Bounds(Center, Size);
    }
    public Bounds GetRoomArea()
    {
        return RoomArea;
    }
}
