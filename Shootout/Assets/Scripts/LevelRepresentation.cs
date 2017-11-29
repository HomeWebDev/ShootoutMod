using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRepresentation : MonoBehaviour {

    public int roomArrayX = 11;
    public int roomArrayZ = 11;
    public int blockedPercentage = 50;
    public int minNumberOfRooms = 20;

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

    public LevelRepresentation()
    {
        //CreateRoomArray();

        CreateRandomRoomArray();
    }


    // Use this for initialization
    void Start () {
        
    }

    public ContentType[,] ContentArray { get; set; }
    public NormalRoom[,] NormalRoomArray { get; set; }

    //For test, creates simple room layout
    private void CreateRoomArray()
    {
        NormalRoomArray = new NormalRoom[3, 3]
        {
            {new NormalRoom() { NoRoom = true }, new NormalRoom(){SouthDoorOpen = true }, new NormalRoom() {SouthDoorOpen = true } },
            {new NormalRoom(){ EastDoorOpen = true }, new NormalRoom(){NorthDoorOpen = true, SouthDoorOpen = true, WestDoorOpen = true, EastDoorOpen = true }, new NormalRoom() {NorthDoorOpen = true, WestDoorOpen = true } },
            {new NormalRoom() { NoRoom = true }, new NormalRoom() { NorthDoorOpen = true}, new NormalRoom() { NoRoom = true }}
        };
    }

    private void CreateRandomRoomArray()
    {
        while (true)
        {
            ContentArray = new ContentType[roomArrayX, roomArrayZ];
            NormalRoomArray = new NormalRoom[roomArrayX, roomArrayZ];

            //Initialize with empty rooms
            for (int i = 0; i < roomArrayX; i++)
            {
                for (int j = 0; j < roomArrayZ; j++)
                {
                    NormalRoomArray[i, j] = new NormalRoom() { NoRoom = true };
                    ContentArray[i, j] = ContentType.NoContent;
                }
            }

            //Create first room
            NormalRoomArray[roomArrayZ / 2, roomArrayX / 2] = GetRandomStartRoom();

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
                        if (!NormalRoomArray[i, j].NoRoom)
                        {
                            if (NormalRoomArray[i, j].NorthDoorOpen)
                            {
                                if (NormalRoomArray[i - 1, j].NoRoom)
                                {
                                    NormalRoomArray[i - 1, j] = GetRandomRoomThatFits(i - 1, j);
                                    ContentArray[i - 1, j] = ContentType.EnemyLevel1;
                                    nrOrOpenDoors++;
                                    numberOfRooms++;
                                }
                            }
                            if (NormalRoomArray[i, j].SouthDoorOpen)
                            {
                                if (NormalRoomArray[i + 1, j].NoRoom)
                                {
                                    NormalRoomArray[i + 1, j] = GetRandomRoomThatFits(i + 1, j);
                                    ContentArray[i + 1, j] = ContentType.EnemyLevel1;
                                    nrOrOpenDoors++;
                                    numberOfRooms++;
                                }
                            }
                            if (NormalRoomArray[i, j].WestDoorOpen)
                            {
                                if (NormalRoomArray[i, j - 1].NoRoom)
                                {
                                    NormalRoomArray[i, j - 1] = GetRandomRoomThatFits(i, j - 1);
                                    ContentArray[i, j - 1] = ContentType.EnemyLevel1;
                                    nrOrOpenDoors++;
                                    numberOfRooms++;
                                }
                            }
                            if (NormalRoomArray[i, j].EastDoorOpen)
                            {
                                if (NormalRoomArray[i, j + 1].NoRoom)
                                {
                                    NormalRoomArray[i, j + 1] = GetRandomRoomThatFits(i, j + 1);
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
    }

    private NormalRoom GetRandomStartRoom()
    {
        NormalRoom normalRoom = new NormalRoom();
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

    private NormalRoom GetRandomRoomThatFits(int z, int x)
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

        //TODO: Check inner bounds. Needed?


        //Check which rooms fit
        if (!northBlocked)
        {
            northDoor = NormalRoomArray[z - 1, x].SouthDoorOpen;
        }
        if (!southBlocked)
        {
            southDoor = NormalRoomArray[z + 1, x].NorthDoorOpen;
        }
        if (!westBlocked)
        {
            westDoor = NormalRoomArray[z, x - 1].EastDoorOpen;
        }
        if (!eastBlocked)
        {
            eastDoor = NormalRoomArray[z, x + 1].WestDoorOpen;
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
        NormalRoom normalRoom = GetRandomRoom(northDoor, southDoor, westDoor, eastDoor, northBlocked, southBlocked, westBlocked, eastBlocked);

        return normalRoom;
    }

    private NormalRoom GetRandomRoom(bool northDoor, bool southDoor, bool westDoor, bool eastDoor, bool northBlocked, bool southBlocked, bool westBlocked, bool eastBlocked)
    {
        NormalRoom normalRoom = new NormalRoom();

        if (northDoor)
        {
            normalRoom.NorthDoorOpen = true;
        }
        else if (northBlocked)
        {
            normalRoom.NorthDoorOpen = false;
        }
        else
        {
            normalRoom.NorthDoorOpen = rand.NextDouble() > 0.5;
        }
        if (southDoor)
        {
            normalRoom.SouthDoorOpen = true;
        }
        else if (southBlocked)
        {
            normalRoom.SouthDoorOpen = false;
        }
        else
        {
            normalRoom.SouthDoorOpen = rand.NextDouble() > 0.5;
        }
        if (westDoor)
        {
            normalRoom.WestDoorOpen = true;
        }
        else if (westBlocked)
        {
            normalRoom.WestDoorOpen = false;
        }
        else
        {
            normalRoom.WestDoorOpen = rand.NextDouble() > 0.5;
        }
        if (eastDoor)
        {
            normalRoom.EastDoorOpen = true;
        }
        else if (eastBlocked)
        {
            normalRoom.EastDoorOpen = false;
        }
        else
        {
            normalRoom.EastDoorOpen = rand.NextDouble() > 0.5;
        }

        return normalRoom;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
