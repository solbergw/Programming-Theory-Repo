using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour


{
    public GameObject animalObject;
    public bool debugThis = true;
    public int myId;
    
    // Start is called before the first frame update
    void Start()
    {
        myId = gameObject.GetInstanceID();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject == null || animalObject == null) return;
        gameObject.transform.position = animalObject.transform.position;
        gameObject.transform.rotation = animalObject.transform.rotation;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == null || animalObject == null) return;
        if (animalObject.CompareTag("Dog") && (other.gameObject.CompareTag("Chicken") || other.gameObject.CompareTag("Deer")))
        {
            if (debugThis) Debug.Log(myId + animalObject.name);

            Dog dog = animalObject.GetComponent<Dog>();
            if (debugThis) Debug.Log(myId + "" + dog.prey);
            if (dog.prey == null)
            {

                if (debugThis) Debug.Log(myId + "We Should Hunt");
                dog.prey = other.gameObject;
                dog.animalState = Animal.AnimalState.Hunting;
                dog.Hunt();

            }
        }
        else if ((animalObject.CompareTag("Chicken") || animalObject.gameObject.CompareTag("Deer")) && other.gameObject.CompareTag("Dog"))
        {
            if (debugThis) Debug.Log(myId + animalObject.name);
            if (animalObject.CompareTag("Chicken"))
            {
                if (debugThis) Debug.Log(myId + "I am Chickin");
                Chicken me = animalObject.GetComponent<Chicken>();
                if (me.animalState == Animal.AnimalState.Dead) return;
                if (debugThis) Debug.Log(myId + "Preditor: " + me);
                if (me.preditor == null)
                {

                    if (debugThis) Debug.Log(myId + "We Should Flee");
                    me.SetPreditor(other.gameObject);
                    me.animalState = Animal.AnimalState.Fleeing;
                    me.Flee();


                }
            }
            else if (animalObject.CompareTag("Deer"))
            {

                if (debugThis) Debug.Log(myId + "I am Deer and Don't want to die");
                Deer me;
                me = animalObject.GetComponent<Deer>();
                if (me.animalState == Animal.AnimalState.Dead) return;
                if (debugThis) Debug.Log(myId + "Preditor: ");
                if (debugThis) Debug.Log(myId + " " + me);
                if (me.preditor == null)
                {

                    if (debugThis) Debug.Log(myId + "We Should Flee");
                    me.SetPreditor(other.gameObject);
                    me.animalState = Animal.AnimalState.Fleeing;
                    me.Flee();


                }
            }
        }
    }
}
