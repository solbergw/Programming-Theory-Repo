using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    public Animator animalAnimator;
    protected float walkSpeed;
    protected float runSpeed;
    protected float walkMultiplier;
    protected float runMultiplier;
    public enum AnimalState { Idle, Roaming, Running, Sitting, Eating, Barking, Hunting, Fleeing, Dead };
    protected bool actionDone = true;
    public AnimalState animalState;
    protected float movementSpeed;

    [SerializeField] protected int energy = 500;

    protected void Eat()
    {
        Debug.Log(gameObject.name + " Eat");
        animalAnimator.SetFloat("Speed_f", 0.0f);
        movementSpeed = 0.0f;
        animalAnimator.SetTrigger("Eat_t");
        StartCoroutine(FinishedEating());
    }

    IEnumerator FinishedEating()
    {
        int framesElapsed = 0;
        while (animalState == AnimalState.Eating)
        {
            if (framesElapsed > 199)
            {
                energy += 100;
                Debug.Log(gameObject.name + " Finished Eat");
                NextMove();
                break;
            }
            yield return new WaitForSeconds(0.04f);
            framesElapsed++;
        }
    }

    protected void Idle()
    {
        Debug.Log(gameObject.name + " Idle");
        animalAnimator.SetFloat("Speed_f", 0.0f);
        movementSpeed = 0.0f;
        StartCoroutine(FinishedIdle());
    }

    IEnumerator FinishedIdle()
    {
        int framesElapsed = 0;
        while (animalState == AnimalState.Idle)
        {
            if (framesElapsed > 199)
            {
                energy += 1;
                Debug.Log(gameObject.name + " Finished Idle");
                NextMove();
                break;
            }
            yield return new WaitForSeconds(0.04f);
            framesElapsed++;
        }
    }

    protected void Roam()
    {
        Debug.Log(gameObject.name + " Roam");
        movementSpeed = walkSpeed * walkMultiplier;
        animalAnimator.SetFloat("Speed_f", walkSpeed);
        StartCoroutine(FinishedRoam());
    }

    IEnumerator FinishedRoam()
    {
        int framesElapsed = 0;
        while (animalState == AnimalState.Roaming)
        {
            if (framesElapsed > 199)
            {
                energy -= 5;
                Debug.Log(gameObject.name + " Finished Roam");
                NextMove();
                break;
            }
            yield return new WaitForSeconds(0.04f);
            framesElapsed++;
        }
    }


    protected abstract void NextMove();

    protected void Run()
    {
        Debug.Log(gameObject.name + " Run");
        movementSpeed = runSpeed * runMultiplier;
        animalAnimator.SetFloat("Speed_f", runSpeed);
        StartCoroutine(FinishedRun());
    }

    IEnumerator FinishedRun()
    {
        int framesElapsed = 0;
        while (animalState == AnimalState.Running)
        {
            if (framesElapsed > 119)
            {
                energy -= 15;
                Debug.Log(gameObject.name + " Finished Run");
                NextMove();
                break;
            }
            yield return new WaitForSeconds(0.04f);
            framesElapsed++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
