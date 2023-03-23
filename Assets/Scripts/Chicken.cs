using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Herbivore
{
    // Start is called before the first frame update
    void Start()
    {
        animalAnimator.SetFloat("Speed_f", 0.0f);
        Invoke("Eat", 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
