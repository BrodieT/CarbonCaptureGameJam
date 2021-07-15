using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance = default;

    IEnumerator LevelTimer()
    {
        while (time > 0)
        {
            time--;
            System.TimeSpan duration = System.TimeSpan.FromSeconds(time);
            Vector2Int result = new Vector2Int(duration.Minutes, duration.Seconds);

            GameUI.Instance.UpdateTimerText(result);

            GenerateNewPipeLevelPiece();

            yield return new WaitForSecondsRealtime(1);
        }

        currentDialogue++;

        Debug.Log(currentDialogue + "     " + dialogues.Count);
        if (currentDialogue <= dialogues.Count)
        {
            dialogues[currentDialogue].StartDialogue();
        }
        else
        {
            StartLevelCountdown();
        }

        Restart();
    }

    IEnumerator LevelCountdown()
    {
        int count = 4;

        while (count > 0)
        {
            count -= 1;
            GameUI.Instance.UpdateCountdownText(count);

            yield return new WaitForSeconds(1);
        }

        count = -1;

        GameUI.Instance.UpdateCountdownText(count);
        StartLevel();

        if(bossLevel)
        {
            boss = Instantiate(bossEnemy);
            boss.transform.position = PlayerController.instance.transform.position + new Vector3(12, 0, 0);
        }
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
    float time = 0;

    [SerializeField]
    Transform playerSpawn = default;

    [SerializeField]
    DialogueTrigger bossLevelDialogue = default;

    bool bossLevel = false;

    [SerializeField]
    List<DialogueTrigger> dialogues = new List<DialogueTrigger>();
    int currentDialogue = -1;

    [SerializeField]
    GameObject bossEnemy = default;
    GameObject boss = default;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        levelPieces = pipeLevelPieces;
        GeneratePipeLevel(5);

        //Reset Player
        PlayerController.instance.transform.position = playerSpawn.position;
        PlayerController.instance.PausePlayer();
    }

    public bool OnLastDialogue()
    {
        Debug.Log(currentDialogue + "     " + dialogues.Count);
        if(currentDialogue > dialogues.Count - 1 || bossLevel)
        {
            return true;
        }

        return false;
    }

    public void StartLevelCountdown()
    {
        if (currentDialogue == dialogues.Count - 2)
        {
            Debug.Log("BOSS LEVEL");
            StartBossLevel();
        }
        else
        {
            StartCoroutine(LevelCountdown());
        }

        PlayerController.instance.PausePlayer();
    }

    void StartLevel()
    {
        time = levelTime;

        StartCoroutine(LevelTimer());

        PlayerController.instance.UnPausePlayer();
    }

    void StartBossLevel()
    {
        bossLevel = true;
        levelPieces = bossLevelPieces;

        time = levelTime;
        StartCoroutine(LevelCountdown());

        PlayerController.instance.UnPausePlayer();
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

        if(spawnedPieces.Count > 50)
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
        foreach(GameObject o in spawnedPieces)
        {
            Destroy(o);
        }

        spawnedPieces.Clear();

        xPos = 0;

        if(boss != null)
        {
            Destroy(boss);
        }

        GeneratePipeLevel(5);

        //Reset Player
        PlayerController.instance.transform.position = playerSpawn.position;
    }
}
