using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public bool NoRoom;
    public bool NorthDoorOpen, SouthDoorOpen, WestDoorOpen, EastDoorOpen;
    public bool NorthWallOpen, SouthWallOpen, WestWallOpen, EastWallOpen;
	public List<GameObject> ObstacleList;

    public Room()
    {
        ObstacleList = new List<GameObject>();
    }
}
