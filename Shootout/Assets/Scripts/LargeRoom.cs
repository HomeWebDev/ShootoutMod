using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeRoom : MonoBehaviour {

    public bool leftNorthDoorOpen, rightNorthDoorOpen, leftSouthDoorOpen, rightSouthDoorOpen, upperWestDoorOpen, lowerWestDoorOpen, upperEastDoorOpen, lowerEastDoorOpen = false;

    public LargeRoom()
    {
        leftNorthDoorOpen = true;
        rightNorthDoorOpen = true;
        leftSouthDoorOpen = true;
        rightSouthDoorOpen = true;
        upperWestDoorOpen = true;
        lowerWestDoorOpen = true;
        upperEastDoorOpen = true;
        lowerEastDoorOpen = true;
    }
}
