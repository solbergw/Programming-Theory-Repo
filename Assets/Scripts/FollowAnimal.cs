using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAnimal : MonoBehaviour
{
    [SerializeField] private GameObject animal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = animal.transform.position + new Vector3(0.5f, 0.5f, 0.0f);
    }
}
