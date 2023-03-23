using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] animalPrefabs = new GameObject[3];
    [SerializeField] private GameObject[] visionPrefabs = new GameObject[3];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            SpawnRandomAnimal();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomAnimal()
    {
        int index = Random.Range(0, 2);
        float randX = Random.Range(-10.0f, 10.0f);
        float randZ = Random.Range(-10.0f, 10.0f);
        float dir = Random.Range(0.0f, 360.0f);
        GameObject animal = Instantiate(animalPrefabs[index], new Vector3(randX, 0, randZ), animalPrefabs[index].transform.rotation);
        GameObject sight = Instantiate(visionPrefabs[index], new Vector3(randX, 0, randZ), animalPrefabs[index].transform.rotation);
        sight.GetComponent<Sight>().animalObject = animal;
        animal.transform.Rotate(Vector3.up, dir);
    }
}
