using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionIcon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = this.transform.position;

        position.y += 0.03f;

        this.transform.position = position;


    }
}
