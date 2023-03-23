using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTitle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("HideTitleScreen", 10.0f);
    }

    void HideTitleScreen()
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
