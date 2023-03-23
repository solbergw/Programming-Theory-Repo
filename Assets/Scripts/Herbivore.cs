using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herbivore : Animal
{

    public GameObject preditor = null;
    public float distanceFromPreditor;
    public int health = 1000;
    private int meat = 1000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Dog") && animalState != AnimalState.Dead) InflictDamage();
    }

    public void InflictDamage()
    {
        health -= 1;
        if (health < 1)
        {
            animalState = AnimalState.Dead;
            animalAnimator.SetFloat("Speed_f", 0);
            movementSpeed = 0.0f;
            Debug.Log("I am Dead!!");
            this.transform.position = new Vector3(transform.position.x, 0.05f, transform.position.z);
            this.gameObject.transform.Rotate(gameObject.transform.forward, 90.0f);
            
        }

    }

    public int TakeMeat() {
        int meatToGive = 0;
        if (meat > 1)
        {
            meatToGive = 2;
            meat -= 2;
        }else {
            meatToGive = Mathf.Max(0, meat);
            meat = 0;
        }
        if (meat == 0) Destroy(gameObject);
        return meatToGive;
    }

    protected override void NextMove()
    {
        if (energy < 10)
        {
            animalState = AnimalState.Idle;
            Idle();
            return;
        }
        switch (animalState)
        {
            
            case AnimalState.Fleeing:
                return;
            case AnimalState.Running:
            case AnimalState.Roaming:
            case AnimalState.Eating:
            case AnimalState.Idle:
                if (energy > 100)
                {
                    float direction = Random.Range(-45.0f, 45.0f);
                    transform.Rotate(Vector3.up, direction);
                    RandomMove();
                }
                else
                {
                    animalState = AnimalState.Eating;
                    Eat();
                }

                break;
        }
    }

    private void RandomMove()
    {
        int index = Random.Range(0, 4);
        preditor = null;
        switch (index)
        {
            case 0:
                animalState = AnimalState.Idle;
                Idle();
                break;
            case 1:
                animalState = AnimalState.Roaming;
                Roam();
                break;
            case 2:
                animalState = AnimalState.Running;
                Run();
                break;
            case 3:
                animalState = AnimalState.Eating;
                Eat();
                break;
        }
    }

    public void SetPreditor(GameObject myPreditor)
    {
        preditor = myPreditor;
    }

    public void Flee()
    {
        Debug.Log(gameObject.name + " Flee");
        movementSpeed = runSpeed * runMultiplier;
        animalAnimator.SetFloat("Speed_f", runSpeed);
        StartCoroutine(FinishedFlee());
    }

    IEnumerator FinishedFlee()
    {
        int framesElapsed = 0;
        while (animalState == AnimalState.Fleeing)
        {
            if (framesElapsed > 399)
            {
                if (preditor != null) distanceFromPreditor = Vector3.Distance(transform.position, preditor.transform.position);
                energy -= 20;
                Debug.Log(gameObject.name + " Finished Flee");
                if (Vector3.Distance(gameObject.transform.position, preditor.transform.position) < 3)
                {
                    Flee();
                }
                else
                {
                    preditor = null;
                    animalState = AnimalState.Idle;
                    NextMove();
                }
                
                break;
            }
            yield return new WaitForSeconds(0.04f);
            if (preditor == null) break;
            Vector3 target = new Vector3(preditor.transform.position.x, transform.position.y, preditor.transform.position.z);
            transform.LookAt(target);
            
            transform.Rotate(Vector3.up, 180f);
            framesElapsed++;
        }
    }
}
