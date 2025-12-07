using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class NextLevel : MonoBehaviour
{
    private int currentSceneIndex;
    private int nextSceneIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = currentSceneIndex + 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        SceneManager.LoadScene(nextSceneIndex);
    }
}
