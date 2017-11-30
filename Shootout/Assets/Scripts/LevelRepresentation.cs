﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRepresentation : MonoBehaviour {

    public int roomArrayX = 11;
    public int roomArrayZ = 11;
    public int blockedPercentage = 30;
    public int minNumberOfRooms = 30;

    //public int roomArrayX = 3;
    //public int roomArrayZ = 3;
    //public int blockedPercentage = 0;
    //public int minNumberOfRooms = 3;

    private System.Random rand = new System.Random();

    public enum ContentType
    {
        NoContent,
        BossLevel1,
        EnemyLevel1,
        Treasure,
    };
	
	private enum WallStatus
    {
        MustBeOpen,
        CantBeOpen,
        Any
    };

    public LevelRepresentation()
    {
        //CreateRoomArray();

        CreateRandomRoomArray();
    }


    // Use this for initialization
    void Start () {
        
    }

    public ContentType[,] ContentArray { get; set; }
    public Room[,] RoomArray { get; set; }

    //For test, creates simple room layout
    private void CreateRoomArray()
    {
        //RoomArray = new Room[3, 3]
        //{
        //    {new Room() { NoRoom = true }, new Room(){SouthDoorOpen = true }, new Room() {SouthDoorOpen = true } },
        //    {new Room(){ EastDoorOpen = true }, new Room(){NorthDoorOpen = true, SouthDoorOpen = true, WestDoorOpen = true, EastDoorOpen = true }, new Room() {NorthDoorOpen = true, WestDoorOpen = true } },
        //    {new Room() { EastWallOpen = true }, new Room() { NorthDoorOpen = true, WestWallOpen = true }, new Room() { NoRoom = true }}
        //};

        RoomArray = new Room[3, 3]
        {
            {new Room() { NoRoom = true }, new Room(){SouthDoorOpen = true }, new Room() {SouthDoorOpen = true } },
            {new Room(){ EastDoorOpen = true, SouthWallOpen = true }, new Room(){NorthDoorOpen = true, SouthDoorOpen = true, WestDoorOpen = true, EastDoorOpen = true }, new Room() {NorthDoorOpen = true, WestDoorOpen = true } },
            {new Room() { EastWallOpen = true, NorthWallOpen = true }, new Room() { NorthDoorOpen = true, WestWallOpen = true }, new Room() { NoRoom = true }}
        };
    }

    private void CreateRandomRoomArray()
    {
        while (true)
        {
            ContentArray = new ContentType[roomArrayX, roomArrayZ];
            RoomArray = new Room[roomArrayX, roomArrayZ];

            //Initialize with empty rooms
            for (int i = 0; i < roomArrayX; i++)
            {
                for (int j = 0; j < roomArrayZ; j++)
                {
                    RoomArray[i, j] = new Room() { NoRoom = true };
                    ContentArray[i, j] = ContentType.NoContent;
                }
            }

            //Create first room
            RoomArray[roomArrayZ / 2, roomArrayX / 2] = GetRandomStartRoom();
            //RoomArray[roomArrayZ / 2, roomArrayX / 2] = new Room() { EastWallOpen = true };


            int currentRoomX = roomArrayX / 2;
            int currentRoomZ = roomArrayZ / 2;

            int numberOfRooms = 1;
            while (true)
            {
                int nrOrOpenPaths = 0;
                for (int i = 0; i < roomArrayX; i++)
                {
                    for (int j = 0; j < roomArrayZ; j++)
                    {
                        if (!RoomArray[i, j].NoRoom)
                        {
                            if (RoomArray[i, j].NorthDoorOpen | RoomArray[i, j].NorthWallOpen)
                            {
                                if (RoomArray[i - 1, j].NoRoom)
                                {
                                    RoomArray[i - 1, j] = GetRandomRoomThatFits(i - 1, j);
                                    ContentArray[i - 1, j] = ContentType.EnemyLevel1;
                                    nrOrOpenPaths++;
                                    numberOfRooms++;
                                }
                            }
                            if (RoomArray[i, j].SouthDoorOpen | RoomArray[i, j].SouthWallOpen)
                            {
                                if (RoomArray[i + 1, j].NoRoom)
                                {
                                    RoomArray[i + 1, j] = GetRandomRoomThatFits(i + 1, j);
                                    ContentArray[i + 1, j] = ContentType.EnemyLevel1;
                                    nrOrOpenPaths++;
                                    numberOfRooms++;
                                }
                            }
                            if (RoomArray[i, j].WestDoorOpen | RoomArray[i, j].WestWallOpen)
                            {
                                if (RoomArray[i, j - 1].NoRoom)
                                {
                                    RoomArray[i, j - 1] = GetRandomRoomThatFits(i, j - 1);
                                    ContentArray[i, j - 1] = ContentType.EnemyLevel1;
                                    nrOrOpenPaths++;
                                    numberOfRooms++;
                                }
                            }
                            if (RoomArray[i, j].EastDoorOpen | RoomArray[i, j].EastWallOpen)
                            {
                                if (RoomArray[i, j + 1].NoRoom)
                                {
                                    RoomArray[i, j + 1] = GetRandomRoomThatFits(i, j + 1);
                                    ContentArray[i, j + 1] = ContentType.EnemyLevel1;
                                    nrOrOpenPaths++;
                                    numberOfRooms++;
                                }
                            }
                        }
                    }
                }
                if (nrOrOpenPaths == 0)
                {
                    break;
                }
            }
            if (numberOfRooms >= minNumberOfRooms)
            {
                break;
            }
        }
    }

    private Room GetRandomStartRoom()
    {
        Room normalRoom = new Room();
        while(true)
        {
            normalRoom.NorthDoorOpen = rand.NextDouble() > 0.5;
            normalRoom.SouthDoorOpen = rand.NextDouble() > 0.5;
            normalRoom.WestDoorOpen = rand.NextDouble() > 0.5;
            normalRoom.EastDoorOpen = rand.NextDouble() > 0.5;

            if(normalRoom.NorthDoorOpen | normalRoom.SouthDoorOpen | normalRoom.WestDoorOpen | normalRoom.EastDoorOpen)
            {
                break;
            }
        }
        return normalRoom;
    }

    private Room GetRandomRoomThatFits(int z, int x)
    {
        bool northDoor = false;
        bool southDoor = false;
        bool westDoor = false;
        bool eastDoor = false;
		WallStatus northWallStatus = WallStatus.Any;
		WallStatus southWallStatus = WallStatus.Any;
		WallStatus westWallStatus = WallStatus.Any;
		WallStatus eastWallStatus = WallStatus.Any;

        //Check outer blocks
        bool northBlocked = (z <= 0);
        bool southBlocked = (z >= roomArrayZ - 1);
        bool westBlocked = (x <= 0);
        bool eastBlocked = (x >= roomArrayX - 1);

        //TODO: Check inner bounds. Needed?


        //Check which rooms fit
        if (!northBlocked)
        {
            northDoor = RoomArray[z - 1, x].SouthDoorOpen;
			if(RoomArray[z - 1, x].NoRoom)
			{
				northWallStatus = WallStatus.Any;
			}
			else if(RoomArray[z - 1, x].SouthWallOpen)
			{
				northWallStatus = WallStatus.MustBeOpen;
			}
			else
			{
				northWallStatus = WallStatus.CantBeOpen;
			}
        }
        if (!southBlocked)
        {
            southDoor = RoomArray[z + 1, x].NorthDoorOpen;
			if(RoomArray[z + 1, x].NoRoom)
			{
				southWallStatus = WallStatus.Any;
			}
			else if(RoomArray[z + 1, x].NorthWallOpen)
			{
				southWallStatus = WallStatus.MustBeOpen;
			}
			else
			{
				southWallStatus = WallStatus.CantBeOpen;
			}
        }
        if (!westBlocked)
        {
            westDoor = RoomArray[z, x - 1].EastDoorOpen;
			if(RoomArray[z, x - 1].NoRoom)
			{
				westWallStatus = WallStatus.Any;
			}
			else if(RoomArray[z, x - 1].EastWallOpen)
			{
				westWallStatus = WallStatus.MustBeOpen;
			}
			else
			{
				westWallStatus = WallStatus.CantBeOpen;
			}
        }
        if (!eastBlocked)
        {
            eastDoor = RoomArray[z, x + 1].WestDoorOpen;
			if(RoomArray[z, x + 1].NoRoom)
			{
				eastWallStatus = WallStatus.Any;
			}
			else if(RoomArray[z, x + 1].WestWallOpen)
			{
				eastWallStatus = WallStatus.MustBeOpen;
			}
			else
			{
				eastWallStatus = WallStatus.CantBeOpen;
			}
        }

        //Add additional blocks
        if (rand.Next(100) < blockedPercentage & !northDoor & (northWallStatus != WallStatus.MustBeOpen))
        {
            northBlocked = true;
        }
        if (rand.Next(100) < blockedPercentage & !southDoor & (southWallStatus != WallStatus.MustBeOpen))
        {
            southBlocked = true;
        }
        if (rand.Next(100) < blockedPercentage & !westDoor & (westWallStatus != WallStatus.MustBeOpen))
        {
            westBlocked = true;
        }
        if (rand.Next(100) < blockedPercentage & !eastDoor & (eastWallStatus != WallStatus.MustBeOpen))
        {
            eastBlocked = true;
        }

        //Get random room that fits
        Room room = GetRandomRoom(northDoor, southDoor, westDoor, eastDoor, northBlocked, southBlocked, westBlocked, eastBlocked, northWallStatus, southWallStatus, westWallStatus, eastWallStatus);

        return room;
    }

    private Room GetRandomRoom(bool northDoor, bool southDoor, bool westDoor, bool eastDoor, bool northBlocked, bool southBlocked, bool westBlocked, bool eastBlocked, WallStatus northWallStatus, WallStatus southWallStatus, WallStatus westWallStatus, WallStatus eastWallStatus)
    {
        Room room = new Room();

        if (northDoor)
        {
            room.NorthDoorOpen = true;
			room.NorthWallOpen = false;
        }
        else if (northBlocked)
        {
            room.NorthDoorOpen = false;
			room.NorthWallOpen = false;
        }
        else if (northWallStatus == WallStatus.CantBeOpen)
        {
            room.NorthDoorOpen = false;
            room.NorthWallOpen = false;
        }
        else if (northWallStatus == WallStatus.MustBeOpen)
        {
            room.NorthDoorOpen = false;
            room.NorthWallOpen = true;
        }
        else
        {
            room.NorthDoorOpen = rand.NextDouble() > 0.67;
			if(room.NorthDoorOpen)
			{
				room.NorthWallOpen = false;
			}
			else
			{
				room.NorthWallOpen = rand.NextDouble() > 0.67;
			}
        }
        if (southDoor)
        {
            room.SouthDoorOpen = true;
			room.SouthWallOpen = false;
        }
        else if (southBlocked)
        {
            room.SouthDoorOpen = false;
			room.SouthWallOpen = false;
        }
        else if (southWallStatus == WallStatus.CantBeOpen)
        {
            room.SouthDoorOpen = false;
            room.SouthWallOpen = false;
        }
        else if (southWallStatus == WallStatus.MustBeOpen)
        {
            room.SouthDoorOpen = false;
            room.SouthWallOpen = true;
        }
        else
        {
            room.SouthDoorOpen = rand.NextDouble() > 0.67;
            if (room.SouthDoorOpen)
            {
                room.SouthWallOpen = false;
            }
            else
            {
                room.SouthWallOpen = rand.NextDouble() > 0.67;
            }
        }
        if (westDoor)
        {
            room.WestDoorOpen = true;
			room.WestWallOpen = false;
        }
        else if (westBlocked)
        {
            room.WestDoorOpen = false;
			room.WestWallOpen = false;
        }
        else if (westWallStatus == WallStatus.CantBeOpen)
        {
            room.WestDoorOpen = false;
            room.WestWallOpen = false;
        }
        else if (westWallStatus == WallStatus.MustBeOpen)
        {
            room.WestDoorOpen = false;
            room.WestWallOpen = true;
        }
        else
        {
            room.WestDoorOpen = rand.NextDouble() > 0.67;
            if (room.WestDoorOpen)
            {
                room.WestWallOpen = false;
            }
            else
            {
                room.WestWallOpen = rand.NextDouble() > 0.67;
            }
        }
        if (eastDoor)
        {
            room.EastDoorOpen = true;
			room.EastWallOpen = false;
        }
        else if (eastBlocked)
        {
            room.EastDoorOpen = false;
			room.EastWallOpen = false;
        }
        else if (eastWallStatus == WallStatus.CantBeOpen)
        {
            room.EastDoorOpen = false;
            room.EastWallOpen = false;
        }
        else if (eastWallStatus == WallStatus.MustBeOpen)
        {
            room.EastDoorOpen = false;
            room.EastWallOpen = true;
        }
        else
        {
            room.EastDoorOpen = rand.NextDouble() > 0.67;
            if (room.EastDoorOpen)
            {
                room.EastWallOpen = false;
            }
            else
            {
                room.EastWallOpen = rand.NextDouble() > 0.67;
            }
        }

        return room;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
