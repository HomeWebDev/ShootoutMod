using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRepresentation : MonoBehaviour {

    private int roomArrayX = 21;
    private int roomArrayZ = 21;
    private int blockedPercentage = 10;
    private int minNumberOfRooms;
    private int nrOfRoomsMultiplierPerLevel = 3;

    private int itemRoomX;
    private int itemRoomZ;

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
        ItemLevel1,
        HiddenRoom,
        EnemiesSpawned
    };
	
	private enum WallStatus
    {
        MustBeOpen,
        CantBeOpen,
        Any
    };

    // Use this for initialization
    void Start () {

    }

    public ContentType[,] ContentArray { get; set; }
    public Room[,] RoomArray { get; set; }

    public void CreateRoomArray()
    {
        GameObject progressController = GameObject.FindGameObjectWithTag("ProgressController");
        int levelID = progressController.GetComponent<ProgressController>().NextLevel - 1;

        minNumberOfRooms = levelID * nrOfRoomsMultiplierPerLevel;

        //Debug.Log("Level: " + levelID + "rooms: " + minNumberOfRooms);

        CreateRandomRoomArray();
    }

    //For test, creates simple room layout
    private void CreateBasicRoomArray()
    {
        //RoomArray = new Room[3, 3]
        //{
        //    {new Room() { NoRoom = true }, new Room(){SouthDoorOpen = true }, new Room() {SouthDoorOpen = true } },
        //    {new Room(){ EastDoorOpen = true }, new Room(){NorthDoorOpen = true, SouthDoorOpen = true, WestDoorOpen = true, EastDoorOpen = true }, new Room() {NorthDoorOpen = true, WestDoorOpen = true } },
        //    {new Room() { EastWallOpen = true }, new Room() { NorthDoorOpen = true, WestWallOpen = true }, new Room() { NoRoom = true }}
        //};

        //RoomArray = new Room[3, 3]
        //{
        //    {new Room() { NoRoom = true }, new Room(){SouthDoorOpen = true }, new Room() {SouthDoorOpen = true } },
        //    {new Room(){ EastDoorOpen = true, SouthWallOpen = true }, new Room(){NorthDoorOpen = true, SouthDoorOpen = true, WestDoorOpen = true, EastDoorOpen = true }, new Room() {NorthDoorOpen = true, WestDoorOpen = true } },
        //    {new Room() { EastWallOpen = true, NorthWallOpen = true }, new Room() { NorthDoorOpen = true, WestWallOpen = true }, new Room() { NoRoom = true }}
        //};

        RoomArray = new Room[4, 3]
        {
            {new Room() { NoRoom = true }, new Room(){SouthDoorOpen = true }, new Room() {SouthDoorOpen = true } },
            {new Room() { NoRoom = true }, new Room(){SouthDoorOpen = true }, new Room() {SouthDoorOpen = true } },
            {new Room(){ EastDoorOpen = true, SouthWallOpen = true }, new Room(){NorthDoorOpen = true, SouthDoorOpen = true, WestDoorOpen = true, EastDoorOpen = true }, new Room() {NorthDoorOpen = true, WestDoorOpen = true } },
            {new Room() { EastWallOpen = true, NorthWallOpen = true }, new Room() { NorthDoorOpen = true, WestWallOpen = true }, new Room() { NoRoom = true }}
        };
    }

    private void CreateRandomRoomArray()
    {
        int finalRoomI = 0;
        int finalRoomJ = 0;

        while (true)
        {
            ContentArray = new ContentType[roomArrayZ, roomArrayX];
            RoomArray = new Room[roomArrayZ, roomArrayX];

            //Initialize with empty rooms
            for (int i = 0; i < roomArrayZ; i++)
            {
                for (int j = 0; j < roomArrayX; j++)
                {
                    RoomArray[i, j] = new Room() { NoRoom = true };
                    ContentArray[i, j] = ContentType.NoContent;
                }
            }

            //Create first room
            RoomArray[roomArrayZ / 2, roomArrayX / 2] = GetRandomStartRoom();
            int nrOfOpenPathsFromStartRoom = GetNumberOfOpenPaths(RoomArray[roomArrayZ / 2, roomArrayX / 2]);
            //RoomArray[roomArrayZ / 2, roomArrayX / 2] = new Room() { EastWallOpen = true };

            int currentRoomX = roomArrayX / 2;
            int currentRoomZ = roomArrayZ / 2;

            int numberOfRooms = 1;

            while (true)
            {
                int nrOrOpenPaths = 0;
                for (int i = 0; i < roomArrayZ; i++)
                {
                    for (int j = 0; j < roomArrayX; j++)
                    {
                        if (!RoomArray[i, j].NoRoom)
                        {
                            if (RoomArray[i, j].NorthDoorOpen | RoomArray[i, j].NorthWallOpen)
                            {
                                if (RoomArray[i - 1, j].NoRoom)
                                {
                                    if (numberOfRooms + nrOrOpenPaths > minNumberOfRooms + nrOfOpenPathsFromStartRoom)
                                    {
                                        RoomArray[i - 1, j] = GetRandomRoomThatFits(i - 1, j, true);
                                    }
                                    else
                                    {
                                        RoomArray[i - 1, j] = GetRandomRoomThatFits(i - 1, j, false);
                                    }
                                    ContentArray[i - 1, j] = ContentType.EnemyLevel1;
                                    nrOrOpenPaths++;
                                    numberOfRooms++;
                                    finalRoomI = i - 1;
                                    finalRoomJ = j;
                                }
                            }
                            if (RoomArray[i, j].SouthDoorOpen | RoomArray[i, j].SouthWallOpen)
                            {
                                if (RoomArray[i + 1, j].NoRoom)
                                {
                                    if (numberOfRooms + nrOrOpenPaths > minNumberOfRooms + nrOfOpenPathsFromStartRoom)
                                    {
                                        RoomArray[i + 1, j] = GetRandomRoomThatFits(i + 1, j, true);
                                    }
                                    else
                                    {
                                        RoomArray[i + 1, j] = GetRandomRoomThatFits(i + 1, j, false);
                                    }
                                    ContentArray[i + 1, j] = ContentType.EnemyLevel1;
                                    nrOrOpenPaths++;
                                    numberOfRooms++;
                                    finalRoomI = i + 1;
                                    finalRoomJ = j;
                                }
                            }
                            if (RoomArray[i, j].WestDoorOpen | RoomArray[i, j].WestWallOpen)
                            {
                                if (RoomArray[i, j - 1].NoRoom)
                                {
                                    if (numberOfRooms + nrOrOpenPaths > minNumberOfRooms + nrOfOpenPathsFromStartRoom)
                                    {
                                        RoomArray[i, j - 1] = GetRandomRoomThatFits(i, j - 1, true);
                                    }
                                    else
                                    {
                                        RoomArray[i, j - 1] = GetRandomRoomThatFits(i, j - 1, false);
                                    }
                                    ContentArray[i, j - 1] = ContentType.EnemyLevel1;
                                    nrOrOpenPaths++;
                                    numberOfRooms++;
                                    finalRoomI = i;
                                    finalRoomJ = j - 1;
                                }
                            }
                            if (RoomArray[i, j].EastDoorOpen | RoomArray[i, j].EastWallOpen)
                            {
                                if (RoomArray[i, j + 1].NoRoom)
                                {
                                    if (numberOfRooms + nrOrOpenPaths > minNumberOfRooms + nrOfOpenPathsFromStartRoom)
                                    {
                                        RoomArray[i, j + 1] = GetRandomRoomThatFits(i, j + 1, true);
                                    }
                                    else
                                    {
                                        RoomArray[i, j + 1] = GetRandomRoomThatFits(i, j + 1, false);
                                    }
                                    ContentArray[i, j + 1] = ContentType.EnemyLevel1;
                                    nrOrOpenPaths++;
                                    numberOfRooms++;
                                    finalRoomI = i;
                                    finalRoomJ = j + 1;
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

        //Create boss room in last room that was created
        RoomArray[finalRoomI, finalRoomJ] = GetBossRoom(finalRoomI, finalRoomJ);
        ContentArray[finalRoomI, finalRoomJ] = ContentType.BossLevel1;

        //Create item room
        if (FindSuitableItemRoomLocation())
        {
            RoomArray[itemRoomZ, itemRoomX] = GetItemRoom(itemRoomZ, itemRoomX);
            ContentArray[itemRoomZ, itemRoomX] = ContentType.ItemLevel1;
        }

        //Create hidden pathway
        if (FindSuitableItemRoomLocation())
        {
            RoomArray[itemRoomZ, itemRoomX] = GetItemRoom(itemRoomZ, itemRoomX);
            ContentArray[itemRoomZ, itemRoomX] = ContentType.HiddenRoom;
        }
    }

    private int GetNumberOfOpenPaths(Room room)
    {
        int nrOfPaths = 0;
        
        if(room.NorthDoorOpen)
        {
            nrOfPaths++;
        }
        if (room.SouthDoorOpen)
        {
            nrOfPaths++;
        }
        if (room.WestDoorOpen)
        {
            nrOfPaths++;
        }
        if (room.EastDoorOpen)
        {
            nrOfPaths++;
        }

        return nrOfPaths;
    }

    private bool FindSuitableItemRoomLocation()
    {
        //List<Room> availableRoomList = new List<Room>();

        List<int> iList = new List<int>();
        List<int> jList = new List<int>();

        for (int i = 0; i < roomArrayZ; i++)
        {
            for (int j = 0; j < roomArrayX; j++)
            {
                if(RoomArray[i, j].NoRoom)
                {
                    if(NumberOfConnectedNormalRooms(i, j) >= 1)
                    {
                        //availableRoomList.Add(RoomArray[i, j]);
                        iList.Add(i);
                        jList.Add(j);
                    }
                }
            }
        }

        if (iList.Count > 0)
        {
            int index = rand.Next(iList.Count);
            itemRoomX = jList[index];
            itemRoomZ = iList[index];
            return true;
        }
        return false;
    }

    private int NumberOfConnectedNormalRooms(int i, int j)
    {
        int numberOfConnectedNormalRooms = 0;

        if (i > 0)
        {
            if (ContentArray[i - 1, j] == ContentType.EnemyLevel1)
            {
                numberOfConnectedNormalRooms++;
            }
        }
        if (i < roomArrayZ - 1)
        {
            if (ContentArray[i + 1, j] == ContentType.EnemyLevel1)
            {
                numberOfConnectedNormalRooms++;
            }
        }
        if (j > 0)
        {
            if (ContentArray[i, j - 1] == ContentType.EnemyLevel1)
            {
                numberOfConnectedNormalRooms++;
            }
        }
        if (j < roomArrayX - 1)
        {
            if (ContentArray[i, j + 1] == ContentType.EnemyLevel1)
            {
                numberOfConnectedNormalRooms++;
            }
        }

        //Debug.Log("numberOfConnectedNormalRooms: " + numberOfConnectedNormalRooms);

        return numberOfConnectedNormalRooms;
    }

    private Room GetItemRoom(int z, int x)
    {
        List<string> directionList = new List<string>();


        //Debug.Log("z: " + z + " x: " + x);
        if (z > 0)
        {
            if (ContentArray[z - 1, x] == ContentType.EnemyLevel1)
            {
                directionList.Add("north");
            }
        }
        if (z < roomArrayZ - 1)
        {
            if (ContentArray[z + 1, x] == ContentType.EnemyLevel1)
            {
                directionList.Add("south");
            }
        }
        if (x > 0)
        {
            if (ContentArray[z, x - 1] == ContentType.EnemyLevel1)
            {
                directionList.Add("west");
            }
        }
        if (x < roomArrayX - 1)
        {
            if (ContentArray[z, x + 1] == ContentType.EnemyLevel1)
            {
                directionList.Add("east");
            }
        }

        int randPos = rand.Next(directionList.Count);
        string direction = directionList[randPos];

        Room room = new Room();
        room.NorthDoorOpen = false;
        room.SouthDoorOpen = false;
        room.WestDoorOpen = false;
        room.EastDoorOpen = false;

        if (direction == "north")
        {
            room.NorthDoorOpen = true;
            RoomArray[z - 1, x].SouthDoorOpen = true;
        }
        if (direction == "south")
        {
            room.SouthDoorOpen = true;
            RoomArray[z + 1, x].NorthDoorOpen = true;
        }
        if (direction == "west")
        {
            room.WestDoorOpen = true;
            RoomArray[z, x - 1].EastDoorOpen = true;
        }
        if (direction == "east")
        {
            room.EastDoorOpen = true;
            RoomArray[z, x + 1].WestDoorOpen = true;
        }

        //Debug.Log("Itemroom x: " + x + " , z: " + z);

        return room;
    }

    private Room GetBossRoom(int z, int x)
    {
        List<string> directionList = new List<string>();

        if (RoomArray[z, x].NorthDoorOpen | RoomArray[z, x].NorthWallOpen)
        {
            directionList.Add("north");
            RoomArray[z - 1, x].SouthDoorOpen = false;
            RoomArray[z - 1, x].SouthWallOpen = false;
        }
        if (RoomArray[z, x].SouthDoorOpen | RoomArray[z, x].SouthWallOpen)
        {
            directionList.Add("south");
            RoomArray[z + 1, x].NorthDoorOpen = false;
            RoomArray[z + 1, x].NorthWallOpen = false;
        }
        if (RoomArray[z, x].WestDoorOpen | RoomArray[z, x].WestWallOpen)
        {
            directionList.Add("west");
            RoomArray[z, x - 1].EastDoorOpen = false;
            RoomArray[z, x - 1].EastWallOpen = false;
        }
        if (RoomArray[z, x].EastDoorOpen | RoomArray[z, x].EastWallOpen)
        {
            directionList.Add("east");
            RoomArray[z, x + 1].WestDoorOpen = false;
            RoomArray[z, x + 1].WestWallOpen = false;
        }

        int randPos = rand.Next(directionList.Count);
        string direction = directionList[randPos];

        Room room = new Room();
        room.NorthDoorOpen = false;
        room.SouthDoorOpen = false;
        room.WestDoorOpen = false;
        room.EastDoorOpen = false;

        if (direction == "north")
        {
            room.NorthDoorOpen = true;
            RoomArray[z - 1, x].SouthDoorOpen = true;
        }
        if (direction == "south")
        {
            room.SouthDoorOpen = true;
            RoomArray[z + 1, x].NorthDoorOpen = true;
        }
        if (direction == "west")
        {
            room.WestDoorOpen = true;
            RoomArray[z, x - 1].EastDoorOpen = true;
        }
        if (direction == "east")
        {
            room.EastDoorOpen = true;
            RoomArray[z, x + 1].WestDoorOpen = true;
        }

        return room;
    }

    private Room GetRandomStartRoom()
    {
        Room room = new Room();
        while(true)
        {
            room.NorthDoorOpen = rand.NextDouble() > 0.5;
            room.SouthDoorOpen = rand.NextDouble() > 0.5;
            room.WestDoorOpen = rand.NextDouble() > 0.5;
            room.EastDoorOpen = rand.NextDouble() > 0.5;

            if(room.NorthDoorOpen | room.SouthDoorOpen | room.WestDoorOpen | room.EastDoorOpen)
            {
                break;
            }
        }
        return room;
    }

    private Room GetRandomRoomThatFits(int z, int x, bool closeRoom)
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
                if(closeRoom)
                {
                    northWallStatus = WallStatus.CantBeOpen;
                }
                else
                {
                    northWallStatus = WallStatus.Any;
                }
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
                if (closeRoom)
                {
                    southWallStatus = WallStatus.CantBeOpen;
                }
                else
                {
                    southWallStatus = WallStatus.Any;
                }
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
                if (closeRoom)
                {
                    westWallStatus = WallStatus.CantBeOpen;
                }
                else
                {
                    westWallStatus = WallStatus.Any;
                }
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
                if (closeRoom)
                {
                    eastWallStatus = WallStatus.CantBeOpen;
                }
                else
                {
                    eastWallStatus = WallStatus.Any;
                }
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
