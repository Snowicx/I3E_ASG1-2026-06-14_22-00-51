using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Keeping your original static score variable
    public static int score = 0;

    [Header("Door Unlock Settings")]
    public int targetScore = 5;      // The criteria requirement to unlock the door
    public GameObject doorObject;    // Drag your physical door object here in the Inspector

    // An Instance reference so the Collectible script can find this easily
    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        // Set up the instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Call this function from your Collectible script to update score and check criteria
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score Updated! Current Score: " + score);

    }
    void OnGUI()
    {
        // Your temporary on-screen display so you can see your score increase
        GUILayout.BeginArea(new Rect(10, 10, 200, 50));
        GUILayout.Label("Score: " + score, new GUIStyle { fontSize = 24, normal = { textColor = Color.white } });
        GUILayout.EndArea();
    }
}