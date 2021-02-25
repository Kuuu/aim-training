using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController Instance;
    public float circleSpeed = 0.01f;
    public float circleSpeedVariation = 0.1f; // variation to original speed in percentage
    public float circleSize = 1.5f;
    public float circleSizeVariation = 0.1f;
    public float spawnSpeed = 0.5f;
    public GameObject circlePrefab;
    public Text scoreText;
    public Text livesText;

    public Transform leftBorder, topBorder, rightBorder, bottomBorder;

    public Color startColor, midColor, endColor;

    private Vector3 vec3 = new Vector3();
    private Quaternion quat = new Quaternion();
    private int score = 0;
    private int lives = 5;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnCircle", 0, spawnSpeed);
    }

    void SpawnCircle()
    {
        vec3.x = Random.Range(leftBorder.position.x, rightBorder.position.x);
        vec3.y = Random.Range(bottomBorder.position.y, topBorder.position.y);
        vec3.z = 0;
        Instantiate(circlePrefab, vec3, quat);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
        }
    }

    void CastRay()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            Destroy(hit.collider.gameObject);
            IncreaseScore();
        } else
        {
            DecreaseLives();
        }
    }

    public void DecreaseLives()
    {
        if (lives == 0)
        {
            FinishGame();
        }

        lives--;
        livesText.text = ""+lives;
    }

    public void IncreaseScore()
    {
        if (lives == 0) return;

        score++;
        scoreText.text = "" + score;

        if (score % 10 == 0)
        {
            circleSpeed *= 1.3f;
            Debug.Log("New Speed: " + circleSpeed);
        }
    }

    void FinishGame()
    {
        int bestScore = PlayerPrefs.GetInt("bestScore");

        PlayerPrefs.SetInt("lastScore", score);
        if (bestScore < score)
        {
            PlayerPrefs.SetInt("bestScore", score);
        }

        PlayerPrefs.Save();

        SceneManager.LoadScene(0);
    }

}
