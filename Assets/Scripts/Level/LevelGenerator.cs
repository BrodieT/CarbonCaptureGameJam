using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance = default;

    IEnumerator LevelTimer()
    {
        if (!bossLevel)
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

            NextDialogue();
        }
    }

    public void NextDialogue()
    {
        currentDialogue++;
        Debug.Log(currentDialogue + "     " + dialogues.Count);

        if (currentDialogue <= dialogues.Count || bossLevel)
        {
            Debug.Log("STARTED DIALOGUE");
            dialogues[currentDialogue].StartDialogue();
            StopAllCoroutines();
        }
        else
        {
            Restart(false);
            StartLevelCountdown();
        }

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

        if (bossLevel && boss == null)
        {
            boss = Instantiate(bossEnemy);
            boss.transform.position = new Vector3(CameraController.instance.transform.position.x, PlayerController.instance.transform.position.z, PlayerController.instance.transform.position.z) + new Vector3(7, 0, 0);
            finished = true;
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
    float zPos = 0;

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

    [SerializeField]
    GameObject stationaryEnemy = default;

    bool finished = false;

    [SerializeField]
    List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        levelPieces = pipeLevelPieces;
        GeneratePipeLevel(5);

        //Reset Player
        //Reset Player
        CameraController.instance.transform.position = new Vector3(playerSpawn.position.x, CameraController.instance.transform.position.y, CameraController.instance.transform.position.z);
        PlayerController.instance.transform.position = new Vector3(CameraController.instance.transform.position.x, playerSpawn.transform.position.y, playerSpawn.transform.position.z);


        PlayerController.instance.PausePlayer();
        CameraController.instance.PauseCamera();
    }

    public bool OnLastDialogue()
    {
        Debug.Log(currentDialogue + "     " + dialogues.Count);

        if(finished)
        {
            return true;
        }

        return false;
    }

    public void StartLevelCountdown()
    {

        if (currentDialogue == dialogues.Count - 2 || bossLevel)
        {
            Debug.Log("BOSS LEVEL");
            StartBossLevel();
        }
        else
        {
            StartCoroutine(LevelCountdown());
        }


        PlayerController.instance.PausePlayer();
        CameraController.instance.PauseCamera();
    }

    void StartLevel()
    {
        if (!bossLevel)
        {
            time = levelTime;

            StartCoroutine(LevelTimer());
        }

        PlayerController.instance.UnPausePlayer();
        CameraController.instance.UnPauseCamera();
    }

    void StartBossLevel()
    {
        bossLevel = true;
        levelPieces.Clear();
        levelPieces.AddRange(bossLevelPieces);

        foreach (GameObject e in enemies)
        {
            Destroy(e);
        }


        time = levelTime;
        StartCoroutine(LevelCountdown());


        AudioHandler.Instance.PlaySound(AudioBank.Audio.BOSS_MUSIC);
        PlayerController.instance.UnPausePlayer();
        CameraController.instance.UnPauseCamera();
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
        if(spawnedPieces.Count == 0)
        {
            levelPiece = Instantiate(levelPieces[0]);
        }
        else
        {
            levelPiece = Instantiate(levelPieces[Random.Range(1, levelPieces.Count)]);
        }

        levelPiece.transform.position = new Vector3(xPos, yPos, zPos);
        spawnedPieces.Add(levelPiece);
        xPos += offsetSize;

        if (!bossLevel)
        {
            foreach (Transform t in levelPiece.transform)
            {
                if (t.GetComponent<EnemySpawn>())
                {
                    Vector3 spawn = t.GetComponent<EnemySpawn>().transform.position;
                    GameObject enemy = Instantiate(stationaryEnemy);
                    enemy.transform.position = spawn;

                    enemies.Add(enemy);
                }
            }
        }

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

    public void Restart(bool playerDied)
    {
        foreach(GameObject o in spawnedPieces)
        {
            Destroy(o);
        }

        foreach(GameObject e in enemies)
        {
            Destroy(e);
        }

        spawnedPieces.Clear();
        enemies.Clear();

        xPos = 0;

        if(boss != null)
        {
            Destroy(boss);
        }

        GeneratePipeLevel(5);

        //Reset Player
        CameraController.instance.transform.position = new Vector3(playerSpawn.position.x, CameraController.instance.transform.position.y, CameraController.instance.transform.position.z);
        PlayerController.instance.transform.position = new Vector3(CameraController.instance.transform.position.x, playerSpawn.transform.position.y, playerSpawn.transform.position.z);

        if(playerDied)
        {
            Collectables.Instance.ResetScore();
        }

        time = 1000;

        StopAllCoroutines();
    }
}
