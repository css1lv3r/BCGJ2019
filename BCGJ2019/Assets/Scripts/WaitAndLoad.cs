using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitAndLoad : MonoBehaviour
{

    public int secondsToWait;
    public string sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitToLoad());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(secondsToWait);
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
