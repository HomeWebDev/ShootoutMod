using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainHandler : MonoBehaviour {

    //public int TerrainId;
    private System.Random rand = new System.Random();

    // Use this for initialization
    //void Start () {

    //       GameObject terrain = new GameObject();
    //       TerrainData terraindata = new TerrainData();


    //       terrain = Terrain.CreateTerrainGameObject(terraindata);


    //       Vector3 position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
    //       GameObject terrainGameObject = Instantiate(terrain, position, Quaternion.identity);

    //   }

    private void Start()
    {
        
    }


    public void ShiftTerrain(int TerrainId)
    {
        Terrain terrain = GetComponent<Terrain>();

        TerrainData terrainData = terrain.terrainData;

        float[,,] splatmapData = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];

        //int terrainIndex = Random.Range(0, terrainData.alphamapLayers - 1);
        //Debug.Log("terrainIndex: " + terrainIndex);

        for (int y = 0; y < terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < terrainData.alphamapWidth; x++)
            {

                float[] splatWeights = new float[terrainData.alphamapLayers];
                for (int i = 0; i < splatWeights.Length; i++)
                {
                    //splatWeights[i] = 0.5f;
                }

                if(x < terrainData.alphamapWidth/2)
                {
                    //splatWeights[4] = 1.0f;
                }
                else
                {
                    //splatWeights[1] = 1.0f;
                }


                //splatWeights[1] = 0.1f;
                //splatWeights[3] = randNr;

                //splatWeights[4] = 1.0f;

                //splatWeights[9] = 1.0f;

                splatWeights[TerrainId] = 1.0f;

                

                // Sum of all textures weights must add to 1, so calculate normalization factor from sum of weights
                float z = 0f;
                foreach (float part in splatWeights)
                {
                    z += part;
                }

                // Loop through each terrain texture
                for (int i = 0; i < terrainData.alphamapLayers; i++)
                {

                    // Normalize so that sum of all texture weights = 1
                    splatWeights[i] /= z;

                    // Assign this point to the splatmap array
                    splatmapData[x, y, i] = splatWeights[i];
                }
            }
        }

        terrainData.SetAlphamaps(0, 0, splatmapData);
    }

    void testStart()
    {


        // Get the attached terrain component
        Terrain terrain = GetComponent<Terrain>();

        // Get a reference to the terrain data
        TerrainData terrainData = terrain.terrainData;

        // Splatmap data is stored internally as a 3d array of floats, so declare a new empty array ready for your custom splatmap data:
        float[,,] splatmapData = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];

        for (int y = 0; y < terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < terrainData.alphamapWidth; x++)
            {
                // Normalise x/y coordinates to range 0-1 
                float y_01 = (float)y / (float)terrainData.alphamapHeight;
                float x_01 = (float)x / (float)terrainData.alphamapWidth;

                // Sample the height at this location (note GetHeight expects int coordinates corresponding to locations in the heightmap array)
                float height = terrainData.GetHeight(Mathf.RoundToInt(y_01 * terrainData.heightmapHeight), Mathf.RoundToInt(x_01 * terrainData.heightmapWidth));

                // Calculate the normal of the terrain (note this is in normalised coordinates relative to the overall terrain dimensions)
                Vector3 normal = terrainData.GetInterpolatedNormal(y_01, x_01);

                // Calculate the steepness of the terrain
                float steepness = terrainData.GetSteepness(y_01, x_01);

                // Setup an array to record the mix of texture weights at this point
                float[] splatWeights = new float[terrainData.alphamapLayers];

                // CHANGE THE RULES BELOW TO SET THE WEIGHTS OF EACH TEXTURE ON WHATEVER RULES YOU WANT

                // Texture[0] has constant influence
                splatWeights[0] = 0.5f;

                // Texture[1] is stronger at lower altitudes
                splatWeights[1] = Mathf.Clamp01((terrainData.heightmapHeight - height));

                // Texture[2] stronger on flatter terrain
                // Note "steepness" is unbounded, so we "normalise" it by dividing by the extent of heightmap height and scale factor
                // Subtract result from 1.0 to give greater weighting to flat surfaces
                splatWeights[2] = 1.0f - Mathf.Clamp01(steepness * steepness / (terrainData.heightmapHeight / 5.0f));

                // Texture[3] increases with height but only on surfaces facing positive Z axis 
                splatWeights[3] = height * Mathf.Clamp01(normal.z);

                // Sum of all textures weights must add to 1, so calculate normalization factor from sum of weights
                float z = 0f;
                foreach (float part in splatWeights)
                {
                    z += part;
                }
                //float z = splatWeights.Sum();

                // Loop through each terrain texture
                for (int i = 0; i < terrainData.alphamapLayers; i++)
                {

                    // Normalize so that sum of all texture weights = 1
                    splatWeights[i] /= z;

                    // Assign this point to the splatmap array
                    splatmapData[x, y, i] = splatWeights[i];
                }
            }
        }

        // Finally assign the new splatmap to the terrainData:
        terrainData.SetAlphamaps(0, 0, splatmapData);
    }



    // Update is called once per frame
    void Update () {
		
	}
}
