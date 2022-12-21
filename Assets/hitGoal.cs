using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitGoal : MonoBehaviour
{
    public GameObject endCanva;

    private void OnTriggerEnter2D(Collider2D other)
    {
        endCanva.SetActive(true);
        Debug.Log("hit goal");
    }

    void OnCollisionEnter(Collision collision)
    {
        endCanva.SetActive(true);
        Debug.Log("hit goal");
    }
    
    
}
