using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    public GameObject arrowPrefab;
    float spawn = 2.0f;
    float delta = 0.0f;

    void Start()
    {
        
    }

    void Update()
    {
        delta += Time.deltaTime;
        if(delta > spawn)
        {
            delta = 0.0f;
            GameObject Go = Instantiate(arrowPrefab) as GameObject;

            int dropPosX = Random.Range(-2, 3); // -2 ~ 2
            Go.GetComponent<ArrowController>().InitArrow(dropPosX);
        }

    }
}
