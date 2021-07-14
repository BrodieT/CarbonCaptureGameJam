using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance = default;

    [SerializeField]
    [Tooltip("Level pieces for the repeating pipe levels")]
    List<GameObject> pipeLevelPieces = new List<GameObject>();

    List<GameObject> spawnedPieces = new List<GameObject>();

    GameObject levelPiece = default;

    [SerializeField]
    [Tooltip("The length of the prefab")]
    float offsetSize = 0;

    [SerializeField]
    [Tooltip("Y position within the world space")]
    float yPos = 0;

    float xPos = 0;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        GeneratePipeLevel(5);
    }

    public void GeneratePipeLevel(int size)
    {
        for(int i =0; i < size; i++)
        {
            GenerateNewPipeLevelPiece();
        }
    }

    public void GenerateNewPipeLevelPiece()
    {
        levelPiece = Instantiate(pipeLevelPieces[Random.Range(0, pipeLevelPieces.Count)]);
        levelPiece.transform.position = new Vector2(xPos, yPos);
        spawnedPieces.Add(levelPiece);
        xPos += offsetSize;

        if(spawnedPieces.Count > 5)
        {
            CleanupPipeLevelPieces();
        }
    }

    public void CleanupPipeLevelPieces()
    {
        GameObject l = spawnedPieces[0];
        spawnedPieces.RemoveAt(0);
        Destroy(l);
    }
}
