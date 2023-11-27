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
        print("Level 2 Loaded");
    }
    public void sceneToMoveTo3()
    {
        SceneManager.LoadScene("Level 3");
        print("Level 3 Loaded");
    }
    public void sceneToMoveTo4()
    {
        SceneManager.LoadScene("Level 4");
        print("Level 4 Loaded");
    }
    public void sceneToMoveTo5()
    {
        SceneManager.LoadScene("Level 5");
        print("Level 5 Loaded");
    }
    public void sceneToMoveTo6()
    {
        SceneManager.LoadScene("Level 6");
        print("Level 6 Loaded");
    }
    public void sceneToMoveTo7()
    {
        SceneManager.LoadScene("Level 7");
        print("Level 7 Loaded");
    }
    public void sceneToMoveTo9()
    {
        SceneManager.LoadScene("Level 9 Boss");
        print("Level 9 Loaded");
    }
}
