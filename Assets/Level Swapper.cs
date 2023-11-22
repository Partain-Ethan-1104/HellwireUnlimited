using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwapper : MonoBehaviour
{
    
    public static LevelSwapper Instance;

    private void Awake ()
    {

        if (Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sceneToMoveTo2()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void sceneToMoveTo3()
    {
        SceneManager.LoadScene("Level 3");
    }
}
