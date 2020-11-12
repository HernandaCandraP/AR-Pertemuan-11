using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdController : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform targetFocus;

    void Start()
    {
        targetFocus = GameObject.FindGameObjectWithTag("target").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = targetFocus.position - this.transform.position;
        Debug.Log(target.magnitude);

        if(target.magnitude < 1){
            targetcollider.instance.moveTarget();
        }

        transform.LookAt(targetFocus.transform);
        float speed = Random.Range(0.3f,0.7f);
        transform.Translate(0,0,speed*Time.deltaTime);
    }
}
