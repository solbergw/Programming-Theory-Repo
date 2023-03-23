using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : Herbivore
{
    // Start is called before the first frame update
    void Start()
    {
        walkSpeed = 0.4f;
        runSpeed = 1.0f;
        walkMultiplier = 0.5f;
        runMultiplier = 2.5f;
        animalState = AnimalState.Idle;
        preditor = null;
        NextMove();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject == null) return;
        if (animalState == AnimalState.Dead)
        {
            float x = transform.eulerAngles.x;
            float y = transform.eulerAngles.z;
            transform.rotation = Quaternion.Euler(x, y, 90.0f);
        }
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }
}
