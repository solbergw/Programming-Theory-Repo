using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Carnivore
{
    // Start is called before the first frame update

    void Start()
    {
        walkSpeed = 0.4f;
        runSpeed = 1.0f;
        walkMultiplier = 0.5f;
        runMultiplier = 3.0f;
        animalState = AnimalState.Idle;
        prey = null;
        NextMove();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

    }
}
