using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance = default;

    IEnumerator LevelTimer()
    {
        while (levelTime > 0)
        {
            levelTime--;
            System.TimeSpan duration = System.TimeSpan.FromSeconds(levelTime);
            Vector2Int result = new Vector2Int(duration.Minutes, duration.Seconds);

            GameUI.Instance.UpdateTimerText(result);

            GenerateNewPipeLevelPiece();

            yield return new WaitForSecondsRealtime(1);
        }

        //Start Boss Battle
        if(bossLevel)
        {
            bossLevelDialogue.StartDialogue();
        }
        else
        {
            bossLevel = true;
            StartBossLevel();
        }
    }

    IEnumerator LevelCountdown()
    {
        int count = 4;

        while (count >= 0)
        {
            count -= 1;
            GameUI.Instance.UpdateCountdownText(count);
            yield return new WaitForSeconds(1);
        }

        StartLevel();
    }

    [SerializeField]
    [Tooltip("Level pieces for the repeating pipe levels")]
    List<GameObject> pipeLevelPieces = new List<GameObject>();

    [SerializeField]
    [Tooltip("Level pieces for the repeating boss level")]
    List<GameObject> bossLevelPieces = new List<GameObject>();

    List<GameObject> levelPieces = new List<GameObject>();
    List<GameObject> spawnedPieces = new List<GameObject>();

    GameObject levelPiece = default;

    [SerializeField]
    [Tooltip("The length of the prefab")]
    float offsetSize = 0;

    [SerializeField]
    [Tooltip("Y position within the world space")]
    float yPos = 0;

    float xPos = 0;

    [SerializeField]
    float levelTime = default;

    [SerializeField]
    Transform playerSpawn = default;

    [SerializeField]
    DialogueTrigger bossLevelDialogue = default;

    bool bossLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        levelPieces = pipeLevelPieces;
        GeneratePipeLevel(5);

        //Reset Player
        PlayerController.instance.transform.position = playerSpawn.position;
    }


    public void StartLevelCountdown()
    { 
        StartCoroutine(LevelCountdown());
    }

    void StartLevel()
    {
        StartCoroutine(LevelTimer());
    }

    void StartBossLevel()
    {
        bossLevel = true;
        levelPieces = bossLevelPieces;
        StartLevelCountdown();
    }

    public void StopLevel()
    {
        foreach(GameObject p in spawnedPieces)
        {
            Destroy(p);
        }

        spawnedPieces.Clear();
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
        levelPiece = Instantiate(levelPieces[Random.Range(0, levelPieces.Count)]);
        levelPiece.transform.position = new Vector2(xPos, yPos);
        spawnedPieces.Add(levelPiece);
        xPos += offsetSize;

        if(spawnedPieces.Count > 6)
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

    public void Restart()
    {
        CleanupPipeLevelPieces();
        GeneratePipeLevel(5);

        //Reset Player
        PlayerController.instance.transform.position = playerSpawn.position;
    }
}
