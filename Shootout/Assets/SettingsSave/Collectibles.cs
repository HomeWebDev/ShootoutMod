using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Collectibles {

    public static Collectibles current;
    public List<CollectibleItem> collectibleItemsList;

    public Collectibles()
    {
        collectibleItemsList = new List<CollectibleItem>();

        collectibleItemsList.Add(new CollectibleItem() { name = "ForestCleared", collected = false });
        collectibleItemsList.Add(new CollectibleItem() { name = "DesertCleared", collected = false });
        collectibleItemsList.Add(new CollectibleItem() { name = "CastleGroundsCleared", collected = false });
        collectibleItemsList.Add(new CollectibleItem() { name = "CastleCleared", collected = false });
        collectibleItemsList.Add(new CollectibleItem() { name = "MountainCleared", collected = false });
        collectibleItemsList.Add(new CollectibleItem() { name = "HellCleared", collected = false });
    }
}
