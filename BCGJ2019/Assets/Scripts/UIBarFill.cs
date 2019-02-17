using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBarFill : MonoBehaviour
{
    private int lowThreshold;
    private Color normalColor;
    private Color lowColor;

    [SerializeField] Image fillBar;
    [SerializeField] Text fillPercent;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDisplay(float newValue)
    {
        fillBar.gameObject.transform.localScale = new Vector3(newValue / 100f, 1f, 1f);
        fillPercent.text = Mathf.Round(newValue) + "%";
    }
}
