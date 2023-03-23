using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carnivore : Animal
{
    public GameObject prey = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
            case AnimalState.Hunting:
                return;
            case AnimalState.Eating:
            case AnimalState.Running:
            case AnimalState.Roaming:
            case AnimalState.Idle:
            case AnimalState.Sitting:
                float direction = Random.Range(-45.0f, 45.0f);
                transform.Rotate(Vector3.up, direction);
                RandomMove();
                break;
        }
    }

    private void RandomMove()
    {
        int index = Random.Range(0, 4);
        prey = null;
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
                animalState = AnimalState.Sitting;
                Sit();
                break;

        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (animalState == AnimalState.Eating)
        {
            if (other.CompareTag("Deer"))
            {
                Deer deer = other.gameObject.GetComponent<Deer>();
                if (deer.animalState == AnimalState.Dead)
                {
                    energy += deer.TakeMeat();
                }
            }
        }
        
    }

    protected void Sit()
    {
        Debug.Log(gameObject.name + " Sit");
        movementSpeed = 0.0f;
        animalAnimator.SetFloat("Speed_f", 0);
        animalAnimator.SetBool("Sit_b", true);
        StartCoroutine(FinishedSit());
    }

    IEnumerator FinishedSit()
    {
        int framesElapsed = 0;
        while (animalState == AnimalState.Sitting)
        {
            if (framesElapsed > 199)
            {
                energy -= 1;
                Debug.Log(gameObject.name + " Finished Sit");
                NextMove();
                break;
            }
            yield return new WaitForSeconds(0.04f);
            framesElapsed++;
        }
        animalAnimator.SetBool("Sit_b", false);
    }

    public void SetPrey(GameObject myPrey)
    {
        prey = myPrey;
    }

    public void Hunt()
    {
        Debug.Log(gameObject.name + " Hunt");
        movementSpeed = runSpeed * runMultiplier;
        animalAnimator.SetBool("Sit_b", false);
        animalAnimator.SetFloat("Speed_f", runSpeed);
        StartCoroutine(FinishedHunt());
    }

    IEnumerator FinishedHunt()
    {
        int framesElapsed = 0;
        while (animalState == AnimalState.Hunting)
        {
            if (prey != null)
            {
                if (prey.GetComponent<Deer>().animalState == AnimalState.Dead)
                {
                    movementSpeed = 0;
                    animalAnimator.SetFloat("Speed_f", 0);
                    animalState = AnimalState.Eating;
                    Eat();
                    energy -= Mathf.RoundToInt(20 * (framesElapsed / 200));
                    break;
                }
                else if (Vector3.Distance(gameObject.transform.position, prey.transform.position) > 3)
                {
                    prey = null;
                    animalState = AnimalState.Idle;
                    movementSpeed = 0;
                    energy -= Mathf.RoundToInt(20 * (framesElapsed / 200));
                    animalAnimator.SetFloat("Speed_f", 0);
                    NextMove();
                    break;
                }
                else if (framesElapsed > 199)
                {

                    energy -= 20;
                    Debug.Log(gameObject.name + " Finished Hunt");
                    if (energy > 9)
                    {
                        Hunt();
                    }
                    else
                    {
                        prey = null;
                        movementSpeed = 0;
                        animalAnimator.SetFloat("Speed_f", 0);
                        animalState = AnimalState.Idle;
                        NextMove();
                    }
                    break;
                }
            }

            yield return new WaitForSeconds(0.04f);
            if (prey == null) break;
            Vector3 target = new Vector3(prey.transform.position.x, transform.position.y, prey.transform.position.z);
            transform.LookAt(target);
            framesElapsed++;
        }
    }
}
