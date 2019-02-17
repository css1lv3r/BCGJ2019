using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;
    public MenuPlayer bluePlayer;
    public MenuPlayer redPlayer;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    
}
