using UnityEngine;
using UnityEngine.SceneManagement;

public class buttons : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void OnButtonClick(string sceneName)
    {

        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
        //SceneManager.LoadScene(sceneName);
        // Update is called once per frame
    }
        void Update()
    {
        
    }
}
