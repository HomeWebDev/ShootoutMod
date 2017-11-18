using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRepresentation : MonoBehaviour {

    public int roomArrayX = 15;
    public int roomArrayZ = 15;
    public int blockedPercentage = 50;
    public int minNumberOfRooms = 30;

    //public int roomArrayX = 3;
    //public int roomArrayZ = 3;
    //public int blockedPercentage = 0;
    //public int minNumberOfRooms = 3;

    private System.Random rand = new System.Random();

    public enum RoomType
    {
        NoRoom,
        NorthOpenSouthOpenWestOpenEastOpen,
        NorthOpenSouthOpenWestOpenEastClosed,
        NorthOpenSouthOpenWestClosedEastOpen,
        NorthOpenSouthClosedWestOpenEastOpen,
        NorthClosedSouthOpenWestOpenEastOpen,
        NorthOpenSouthOpenWestClosedEastClosed,
        NorthOpenSouthClosedWestOpenEastClosed,
        NorthClosedSouthOpenWestOpenEastClosed,
        NorthOpenSouthClosedWestClosedEastOpen,
        NorthClosedSouthOpenWestClosedEastOpen,
        NorthClosedSouthClosedWestOpenEastOpen,
        NorthOpenSouthClosedWestClosedEastClosed,
        NorthClosedSouthOpenWestClosedEastClosed,
        NorthClosedSouthClosedWestOpenEastClosed,
        NorthClosedSouthClosedWestClosedEastOpen,
        NorthClosedSouthClosedWestClosedEastClosed,
    };

    public enum ContentType
    {
        NoContent,
        BossLevel1,
        EnemyLevel1,
        Treasure,
    };

    public LevelRepresentation()
    {
        //CreateRoomArray();

        CreateRandomRoomArray();
    }


    // Use this for initialization
    void Start () {
        
    }

    public RoomType[,] RoomArray{ get; set; }
    public ContentType[,] ContentArray { get; set; }

    private void CreateRoomArray()
    {
        RoomArray = new RoomType[3, 3]
        {
            {RoomType.NoRoom, RoomType.NorthClosedSouthOpenWestClosedEastClosed, RoomType.NorthClosedSouthOpenWestClosedEastClosed},
            {RoomType.NorthClosedSouthClosedWestClosedEastOpen, RoomType.NorthOpenSouthOpenWestOpenEastOpen, RoomType.NorthOpenSouthClosedWestOpenEastClosed},
            {RoomType.NoRoom, RoomType.NorthOpenSouthClosedWestClosedEastClosed, RoomType.NoRoom}
        };

        //Debug.Log("RoomArray[2,0]: " + RoomArray[2, 0].ToString());

        //RoomArray = new RoomType[3, 3]
        //{
        //    {RoomType.NorthOpenSouthOpenWestOpenEastClosed, RoomType.NorthOpenSouthOpenWestOpenEastClosed, RoomType.NorthOpenSouthOpenWestOpenEastClosed},
        //    {RoomType.NorthOpenSouthOpenWestOpenEastOpen, RoomType.NorthOpenSouthOpenWestOpenEastOpen, RoomType.NorthOpenSouthOpenWestOpenEastOpen},
        //    {RoomType.NorthOpenSouthOpenWestOpenEastOpen, RoomType.NorthOpenSouthOpenWestOpenEastOpen, RoomType.NorthOpenSouthOpenWestOpenEastOpen}
        //};
    }

    private void CreateRandomRoomArray()
    {
        while (true)
        {
            RoomArray = new RoomType[roomArrayX, roomArrayZ];
            ContentArray = new ContentType[roomArrayX, roomArrayZ];
            //Initialize with empty rooms
            for (int i = 0; i < roomArrayX; i++)
            {
                for (int j = 0; j < roomArrayZ; j++)
                {
                    RoomArray[i, j] = RoomType.NoRoom;
                    ContentArray[i, j] = ContentType.NoContent;
                }
            }

            //Create first room
            RoomArray[roomArrayZ / 2, roomArrayX / 2] = GetRandomStartRoom();
            //RoomArray[roomArrayZ / 2, roomArrayX / 2] = RoomType.NorthOpenSouthOpenWestOpenEastOpen;
            //RoomArray[roomArrayZ / 2, roomArrayX / 2] = RoomType.NorthOpenSouthClosedWestOpenEastOpen;

            int currentRoomX = roomArrayX / 2;
            int currentRoomZ = roomArrayZ / 2;

            int numberOfRooms = 1;
            while (true)
            {
                int nrOrOpenDoors = 0;
                for (int i = 0; i < roomArrayX; i++)
                {
                    for (int j = 0; j < roomArrayZ; j++)
                    {
                        if (RoomArray[i, j] != RoomType.NoRoom)
                        {
                            if (CheckIfNorthDoorOpen(RoomArray[i, j]))
                            {
                                if (RoomArray[i - 1, j] == RoomType.NoRoom)
                                {
                                    RoomArray[i - 1, j] = GetRandomRoomThatFits(i - 1, j);
                                    ContentArray[i - 1, j] = ContentType.EnemyLevel1;
                                    nrOrOpenDoors++;
                                    numberOfRooms++;
                                }
                            }
                            if (CheckIfSouthDoorOpen(RoomArray[i, j]))
                            {
                                if (RoomArray[i + 1, j] == RoomType.NoRoom)
                                {
                                    RoomArray[i + 1, j] = GetRandomRoomThatFits(i + 1, j);
                                    ContentArray[i + 1, j] = ContentType.EnemyLevel1;
                                    nrOrOpenDoors++;
                                    numberOfRooms++;
                                }
                            }
                            if (CheckIfWestDoorOpen(RoomArray[i, j]))
                            {
                                if (RoomArray[i, j - 1] == RoomType.NoRoom)
                                {
                                    RoomArray[i, j - 1] = GetRandomRoomThatFits(i, j - 1);
                                    ContentArray[i, j - 1] = ContentType.EnemyLevel1;
                                    nrOrOpenDoors++;
                                    numberOfRooms++;
                                }
                            }
                            if (CheckIfEastDoorOpen(RoomArray[i, j]))
                            {
                                if (RoomArray[i, j + 1] == RoomType.NoRoom)
                                {
                                    RoomArray[i, j + 1] = GetRandomRoomThatFits(i, j + 1);
                                    ContentArray[i, j + 1] = ContentType.EnemyLevel1;
                                    nrOrOpenDoors++;
                                    numberOfRooms++;
                                }
                            }
                        }
                    }
                }
                if (nrOrOpenDoors == 0)
                {
                    break;
                }
            }
            if (numberOfRooms >= minNumberOfRooms)
            {
                break;
            }
        }



        ////ORIGINAL IMPLEMENTATION
        ////Find first open door without connection
        //if (CheckIfNorthDoorOpen(RoomArray[currentRoomZ, currentRoomX]))
        //{
        //    RoomArray[currentRoomZ - 1, currentRoomX] = GetRandomRoomThatFits(currentRoomZ - 1, currentRoomX);
        //    //RoomArray[currentRoomX - 1, currentRoomZ] = RoomType.NorthClosedSouthOpenWestClosedEastClosed;
        //}
        //if (CheckIfSouthDoorOpen(RoomArray[currentRoomZ, currentRoomX]))
        //{
        //    RoomArray[currentRoomZ + 1, currentRoomX] = GetRandomRoomThatFits(currentRoomZ + 1, currentRoomX);
        //    //RoomArray[currentRoomX + 1, currentRoomZ] = RoomType.NorthOpenSouthClosedWestClosedEastClosed;
        //}
        //if (CheckIfWestDoorOpen(RoomArray[currentRoomZ, currentRoomX]))
        //{
        //    RoomArray[currentRoomZ, currentRoomX - 1] = GetRandomRoomThatFits(currentRoomZ, currentRoomX - 1);
        //    //RoomArray[currentRoomX, currentRoomZ - 1] = RoomType.NorthClosedSouthClosedWestClosedEastOpen;
        //}
        //if (CheckIfEastDoorOpen(RoomArray[currentRoomZ, currentRoomX]))
        //{
        //    RoomArray[currentRoomZ, currentRoomX + 1] = GetRandomRoomThatFits(currentRoomZ, currentRoomX + 1);
        //    //RoomArray[currentRoomX, currentRoomZ + 1] = RoomType.NorthClosedSouthClosedWestOpenEastClosed;
        //}


        //Check for if bondary is reached (finished) or if no more rooms can be added (restart)
    }

    private RoomType GetRandomStartRoom()
    {
        List<RoomType> roomTypeList = new List<RoomType>
        {
            RoomType.NorthOpenSouthOpenWestOpenEastOpen, RoomType.NorthOpenSouthOpenWestOpenEastClosed, RoomType.NorthOpenSouthOpenWestClosedEastOpen,
            RoomType.NorthOpenSouthClosedWestOpenEastOpen, RoomType.NorthOpenSouthOpenWestClosedEastClosed, RoomType.NorthOpenSouthClosedWestOpenEastClosed,
            RoomType.NorthOpenSouthClosedWestClosedEastOpen, RoomType.NorthOpenSouthClosedWestClosedEastClosed, RoomType.NorthClosedSouthClosedWestClosedEastClosed,
            RoomType.NorthClosedSouthClosedWestClosedEastOpen, RoomType.NorthClosedSouthClosedWestOpenEastClosed, RoomType.NorthClosedSouthClosedWestOpenEastOpen,
            RoomType.NorthClosedSouthOpenWestClosedEastClosed, RoomType.NorthClosedSouthOpenWestClosedEastOpen, RoomType.NorthClosedSouthOpenWestOpenEastClosed,
            RoomType.NorthClosedSouthOpenWestOpenEastOpen,
        };

        return roomTypeList[rand.Next(roomTypeList.Count)];
    }

    private RoomType GetRandomRoomThatFits(int z, int x)
    {
        bool northDoor = false;
        bool southDoor = false;
        bool westDoor = false;
        bool eastDoor = false;

        //Check outer blocks
        bool northBlocked = (z <= 0);
        bool southBlocked = (z >= roomArrayZ - 1);
        bool westBlocked = (x <= 0);
        bool eastBlocked = (x >= roomArrayX - 1);

        //TODO: Check inner bounds


        //Check which rooms fit
        if (!northBlocked)
        {
            northDoor = CheckIfSouthDoorOpen(RoomArray[z - 1, x]);
        }
        if (!southBlocked)
        {
            southDoor = CheckIfNorthDoorOpen(RoomArray[z + 1, x]);
        }
        if (!westBlocked)
        {
            westDoor = CheckIfEastDoorOpen(RoomArray[z, x - 1]);
        }
        if (!eastBlocked)
        {
            eastDoor = CheckIfWestDoorOpen(RoomArray[z, x + 1]);
        }

        //Add additional blocks
        if (rand.Next(100) < blockedPercentage && !northDoor)
        {
            northBlocked = true;
        }
        if (rand.Next(100) < blockedPercentage && !southDoor)
        {
            southBlocked = true;
        }
        if (rand.Next(100) < blockedPercentage && !westDoor)
        {
            westBlocked = true;
        }
        if (rand.Next(100) < blockedPercentage && !eastDoor)
        {
            eastBlocked = true;
        }

        //Get array with all fitting room types
        List<RoomType> availableRooms = GetAvailableRooms(northDoor, southDoor, westDoor, eastDoor, northBlocked, southBlocked, westBlocked, eastBlocked);

        // Return a random choice of the fitting rooms
        if (availableRooms.Count != 0)
        {
            return availableRooms[rand.Next(availableRooms.Count)];
        }
        else
        {
            return RoomType.NoRoom;
        }
    }

    private List<RoomType> GetAvailableRooms(bool northDoor, bool southDoor, bool westDoor, bool eastDoor, bool northBlocked, bool southBlocked, bool westBlocked, bool eastBlocked)
    {
        //Debug.Log("Doors:" + northDoor + southDoor + westDoor + eastDoor + " Blocks:" + northBlocked + southBlocked + westBlocked + eastBlocked);
        List<RoomType> roomTypeList = new List<RoomType>
        {
            RoomType.NorthOpenSouthOpenWestOpenEastOpen, RoomType.NorthOpenSouthOpenWestOpenEastClosed, RoomType.NorthOpenSouthOpenWestClosedEastOpen,
            RoomType.NorthOpenSouthClosedWestOpenEastOpen, RoomType.NorthOpenSouthOpenWestClosedEastClosed, RoomType.NorthOpenSouthClosedWestOpenEastClosed,
            RoomType.NorthOpenSouthClosedWestClosedEastOpen, RoomType.NorthOpenSouthClosedWestClosedEastClosed, RoomType.NorthClosedSouthClosedWestClosedEastClosed,
            RoomType.NorthClosedSouthClosedWestClosedEastOpen, RoomType.NorthClosedSouthClosedWestOpenEastClosed, RoomType.NorthClosedSouthClosedWestOpenEastOpen,
            RoomType.NorthClosedSouthOpenWestClosedEastClosed, RoomType.NorthClosedSouthOpenWestClosedEastOpen, RoomType.NorthClosedSouthOpenWestOpenEastClosed,
            RoomType.NorthClosedSouthOpenWestOpenEastOpen,
        };

        if (northDoor)
        {
            roomTypeList.Remove(RoomType.NorthClosedSouthClosedWestClosedEastClosed);
            roomTypeList.Remove(RoomType.NorthClosedSouthClosedWestClosedEastOpen);
            roomTypeList.Remove(RoomType.NorthClosedSouthClosedWestOpenEastClosed);
            roomTypeList.Remove(RoomType.NorthClosedSouthClosedWestOpenEastOpen);
            roomTypeList.Remove(RoomType.NorthClosedSouthOpenWestClosedEastClosed);
            roomTypeList.Remove(RoomType.NorthClosedSouthOpenWestClosedEastOpen);
            roomTypeList.Remove(RoomType.NorthClosedSouthOpenWestOpenEastClosed);
            roomTypeList.Remove(RoomType.NorthClosedSouthOpenWestOpenEastOpen);
        }
        if (southDoor)
        {
            roomTypeList.Remove(RoomType.NorthClosedSouthClosedWestClosedEastClosed);
            roomTypeList.Remove(RoomType.NorthClosedSouthClosedWestClosedEastOpen);
            roomTypeList.Remove(RoomType.NorthClosedSouthClosedWestOpenEastClosed);
            roomTypeList.Remove(RoomType.NorthClosedSouthClosedWestOpenEastOpen);
            roomTypeList.Remove(RoomType.NorthOpenSouthClosedWestClosedEastClosed);
            roomTypeList.Remove(RoomType.NorthOpenSouthClosedWestClosedEastOpen);
            roomTypeList.Remove(RoomType.NorthOpenSouthClosedWestOpenEastClosed);
            roomTypeList.Remove(RoomType.NorthOpenSouthClosedWestOpenEastOpen);
        }
        if (westDoor)
        {
            roomTypeList.Remove(RoomType.NorthClosedSouthClosedWestClosedEastClosed);
            roomTypeList.Remove(RoomType.NorthClosedSouthClosedWestClosedEastOpen);
            roomTypeList.Remove(RoomType.NorthClosedSouthOpenWestClosedEastClosed);
            roomTypeList.Remove(RoomType.NorthClosedSouthOpenWestClosedEastOpen);
            roomTypeList.Remove(RoomType.NorthOpenSouthClosedWestClosedEastClosed);
            roomTypeList.Remove(RoomType.NorthOpenSouthClosedWestClosedEastOpen);
            roomTypeList.Remove(RoomType.NorthOpenSouthOpenWestClosedEastClosed);
            roomTypeList.Remove(RoomType.NorthOpenSouthOpenWestClosedEastOpen);
        }
        if (eastDoor)
        {
            roomTypeList.Remove(RoomType.NorthClosedSouthClosedWestClosedEastClosed);
            roomTypeList.Remove(RoomType.NorthClosedSouthClosedWestOpenEastClosed);
            roomTypeList.Remove(RoomType.NorthClosedSouthOpenWestClosedEastClosed);
            roomTypeList.Remove(RoomType.NorthClosedSouthOpenWestOpenEastClosed);
            roomTypeList.Remove(RoomType.NorthOpenSouthClosedWestClosedEastClosed);
            roomTypeList.Remove(RoomType.NorthOpenSouthClosedWestOpenEastClosed);
            roomTypeList.Remove(RoomType.NorthOpenSouthOpenWestClosedEastClosed);
            roomTypeList.Remove(RoomType.NorthOpenSouthOpenWestOpenEastClosed);
        }
        if (northBlocked)
        {
            roomTypeList.Remove(RoomType.NorthOpenSouthClosedWestClosedEastClosed);
            roomTypeList.Remove(RoomType.NorthOpenSouthClosedWestClosedEastOpen);
            roomTypeList.Remove(RoomType.NorthOpenSouthClosedWestOpenEastClosed);
            roomTypeList.Remove(RoomType.NorthOpenSouthClosedWestOpenEastOpen);
            roomTypeList.Remove(RoomType.NorthOpenSouthOpenWestClosedEastClosed);
            roomTypeList.Remove(RoomType.NorthOpenSouthOpenWestClosedEastOpen);
            roomTypeList.Remove(RoomType.NorthOpenSouthOpenWestOpenEastClosed);
            roomTypeList.Remove(RoomType.NorthOpenSouthOpenWestOpenEastOpen);
        }
        if (southBlocked)
        {
            roomTypeList.Remove(RoomType.NorthClosedSouthOpenWestClosedEastClosed);
            roomTypeList.Remove(RoomType.NorthClosedSouthOpenWestClosedEastOpen);
            roomTypeList.Remove(RoomType.NorthClosedSouthOpenWestOpenEastClosed);
            roomTypeList.Remove(RoomType.NorthClosedSouthOpenWestOpenEastOpen);
            roomTypeList.Remove(RoomType.NorthOpenSouthOpenWestClosedEastClosed);
            roomTypeList.Remove(RoomType.NorthOpenSouthOpenWestClosedEastOpen);
            roomTypeList.Remove(RoomType.NorthOpenSouthOpenWestOpenEastClosed);
            roomTypeList.Remove(RoomType.NorthOpenSouthOpenWestOpenEastOpen);
        }
        if (westBlocked)
        {
            roomTypeList.Remove(RoomType.NorthClosedSouthClosedWestOpenEastClosed);
            roomTypeList.Remove(RoomType.NorthClosedSouthClosedWestOpenEastOpen);
            roomTypeList.Remove(RoomType.NorthClosedSouthOpenWestOpenEastClosed);
            roomTypeList.Remove(RoomType.NorthClosedSouthOpenWestOpenEastOpen);
            roomTypeList.Remove(RoomType.NorthOpenSouthClosedWestOpenEastClosed);
            roomTypeList.Remove(RoomType.NorthOpenSouthClosedWestOpenEastOpen);
            roomTypeList.Remove(RoomType.NorthOpenSouthOpenWestOpenEastClosed);
            roomTypeList.Remove(RoomType.NorthOpenSouthOpenWestOpenEastOpen);
        }
        if (eastBlocked)
        {
            roomTypeList.Remove(RoomType.NorthClosedSouthClosedWestClosedEastOpen);
            roomTypeList.Remove(RoomType.NorthClosedSouthClosedWestOpenEastOpen);
            roomTypeList.Remove(RoomType.NorthClosedSouthOpenWestClosedEastOpen);
            roomTypeList.Remove(RoomType.NorthClosedSouthOpenWestOpenEastOpen);
            roomTypeList.Remove(RoomType.NorthOpenSouthClosedWestClosedEastOpen);
            roomTypeList.Remove(RoomType.NorthOpenSouthClosedWestOpenEastOpen);
            roomTypeList.Remove(RoomType.NorthOpenSouthOpenWestClosedEastOpen);
            roomTypeList.Remove(RoomType.NorthOpenSouthOpenWestOpenEastOpen);
        }

        return roomTypeList;
    }


    private bool CheckIfNorthDoorOpen(RoomType roomType)
    {
        if (roomType == RoomType.NorthOpenSouthOpenWestOpenEastOpen | roomType == RoomType.NorthOpenSouthOpenWestOpenEastClosed | roomType == RoomType.NorthOpenSouthOpenWestClosedEastOpen |
                                roomType == RoomType.NorthOpenSouthClosedWestOpenEastOpen | roomType == RoomType.NorthOpenSouthOpenWestClosedEastClosed | roomType == RoomType.NorthOpenSouthClosedWestOpenEastClosed |
                                roomType == RoomType.NorthOpenSouthClosedWestClosedEastOpen | roomType == RoomType.NorthOpenSouthClosedWestClosedEastClosed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckIfSouthDoorOpen(RoomType roomType)
    {
        if (roomType == RoomType.NorthOpenSouthOpenWestOpenEastOpen | roomType == RoomType.NorthOpenSouthOpenWestOpenEastClosed | roomType == RoomType.NorthOpenSouthOpenWestClosedEastOpen |
                                roomType == RoomType.NorthClosedSouthOpenWestOpenEastOpen | roomType == RoomType.NorthOpenSouthOpenWestClosedEastClosed | roomType == RoomType.NorthClosedSouthOpenWestOpenEastClosed |
                                roomType == RoomType.NorthClosedSouthOpenWestClosedEastOpen | roomType == RoomType.NorthClosedSouthOpenWestClosedEastClosed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckIfWestDoorOpen(RoomType roomType)
    {
        if (roomType == RoomType.NorthOpenSouthOpenWestOpenEastOpen | roomType == RoomType.NorthOpenSouthOpenWestOpenEastClosed | roomType == RoomType.NorthOpenSouthClosedWestOpenEastOpen |
                                roomType == RoomType.NorthClosedSouthOpenWestOpenEastOpen | roomType == RoomType.NorthOpenSouthClosedWestOpenEastClosed | roomType == RoomType.NorthClosedSouthOpenWestOpenEastClosed |
                                roomType == RoomType.NorthClosedSouthClosedWestOpenEastOpen | roomType == RoomType.NorthClosedSouthClosedWestOpenEastClosed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckIfEastDoorOpen(RoomType roomType)
    {
        if (roomType == RoomType.NorthOpenSouthOpenWestOpenEastOpen | roomType == RoomType.NorthOpenSouthOpenWestClosedEastOpen | roomType == RoomType.NorthOpenSouthClosedWestOpenEastOpen |
                                roomType == RoomType.NorthClosedSouthOpenWestOpenEastOpen | roomType == RoomType.NorthOpenSouthClosedWestClosedEastOpen | roomType == RoomType.NorthClosedSouthOpenWestClosedEastOpen |
                                roomType == RoomType.NorthClosedSouthClosedWestOpenEastOpen | roomType == RoomType.NorthClosedSouthClosedWestClosedEastOpen)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
