using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public string SceneToLoad;
    public int timeBuffer = 1;
    public SpriteRenderer buttonSprite;
    public GameObject particleObject;

    public Color player1Color;
    public Color player2Color;
    public Color bothPlayerColor;

    private bool redPlayerIn = false;
    private bool bluePlayerIn = false;
    private bool redPlayerPressed = false;
    private bool bluePlayerPressed = false;

    private bool isTriggeringCheck = false;
    private bool isTriggeringSuccess = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForButtonPress();
        
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (col.gameObject.GetComponent<MenuPlayer>().playerColor == PlayerColor.Blue) { bluePlayerIn = true; }
            if (col.gameObject.GetComponent<MenuPlayer>().playerColor == PlayerColor.Red) { redPlayerIn = true; }
        }
        UpdateButtonColor();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (col.gameObject.GetComponent<MenuPlayer>().playerColor == PlayerColor.Blue) {
                bluePlayerIn = false;
                bluePlayerPressed = false; //also reset button press
            }
            if (col.gameObject.GetComponent<MenuPlayer>().playerColor == PlayerColor.Red) {
                redPlayerIn = false;
                redPlayerPressed = false; //also reset button press
            }
        }
        UpdateButtonColor();
    }

    void CheckForButtonPress()
    {
        if (Input.GetButton("Interact_0") && bluePlayerIn) {
            bluePlayerPressed = true;
            Pulse();
        }
        if (Input.GetButton("Interact_1") && redPlayerIn) {
            redPlayerPressed = true;
            Pulse();
        }
        if (!isTriggeringCheck)
        {
            isTriggeringCheck = true;
            StartCoroutine(WaitAndCheck());
        }
    }

    void UpdateButtonColor()
    {
        if (bluePlayerIn && redPlayerIn) { buttonSprite.color = bothPlayerColor; }
        else if (bluePlayerIn) { buttonSprite.color = player1Color; }
        else if (redPlayerIn) { buttonSprite.color = player2Color; }
        else { buttonSprite.color = Color.white; }
    }

    void Pulse()
    {
        buttonSprite.gameObject.GetComponent<Animator>().SetTrigger("Pulse");
    }

    //--------------------------------------------------------------------------------------------------------------

    IEnumerator WaitAndCheck()
    {
        Debug.Log("Wait and check");
        //Wait
        yield return new WaitForSeconds(timeBuffer);
        //Check - both players are in button and both have pressed button
        Debug.Log("Check");
        if (bluePlayerIn && redPlayerIn && bluePlayerPressed && redPlayerPressed)
        {
            //if all true - success events

        }
        else if (bluePlayerIn && bluePlayerPressed && !(redPlayerIn && redPlayerPressed))
        {
            //blue jumped the gun - reset blue
            Reset(PlayerColor.Blue);
        }
        else if (redPlayerIn && redPlayerPressed && !(bluePlayerIn && bluePlayerPressed))
        {
            //red is early - reset red
            Reset(PlayerColor.Red);
        }
        //if one player is in and pressed but other is out or hasn't pressed, reset early player
        //if nobody is in, do nothing
    }

    void Reset(PlayerColor pc)
    {
        Debug.Log("Reset "+ pc);
        switch (pc)
        {
            case PlayerColor.Blue:
                bluePlayerIn = false;
                bluePlayerPressed = false;
                MainMenu.Instance.bluePlayer.Reset();
                break;
            case PlayerColor.Red:
                redPlayerIn = false;
                redPlayerPressed = false;
                MainMenu.Instance.redPlayer.Reset();
                break;
            default:
                break;
        }
        UpdateButtonColor();
    }

    IEnumerator PlaySuccessSequence()
    {
        particleObject.SetActive(true);
        //play audio
        //
        float timer = 0;
        Color buttonColor = buttonSprite.color;

        while (timer < timeBuffer)
        {
            timer += Time.deltaTime;
            buttonColor.a = ((timeBuffer-timer)/timeBuffer);
            buttonSprite.color = buttonColor;
        }
        yield return new WaitForEndOfFrame();
        SceneManager.LoadSceneAsync(SceneToLoad);
        
    }
}

