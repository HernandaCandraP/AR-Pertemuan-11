﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastController : MonoBehaviour
{
    public float maxDistanceRay = 100f;
    public static RaycastController instance;
    public Text birdName;
    public Transform gunFlashTarget;
    public float fireRate = 10f;
    private bool nextShot = true;
    private string objName = "";
    public AudioClip tembak;

    AudioSource audio;
    public AudioClip[] clips;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnNewBird());
        audio = GetComponent<AudioSource>();
        // playSound(2);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake(){
        if(instance == null){
            instance = this;
        }
    }

    public void playSound(int sound){
        audio.clip = clips[sound];
        audio.Play();
    }

    private IEnumerator spawnNewBird(){
        yield return new WaitForSeconds(3f);

        GameObject newBird = Instantiate(Resources.Load("Bird_Asset", typeof(GameObject))) as GameObject;

        newBird.transform.parent = GameObject.Find("ImageTarget").transform;
        
        newBird.transform.localScale = new Vector3(10f, 10f, 10f);

        Vector3 temp;
        temp.x = Random.Range(-48f, 48f);
        temp.y = Random.Range(10f, 50f);
        temp.z = Random.Range(-48f, 48f);
        newBird.transform.position = new Vector3(temp.x, temp.y, temp.z);
    }

    public void Fire(){

        if(nextShot){
            StartCoroutine(takeShot());
            nextShot = true;
        }
    }

    private IEnumerator takeShot(){
        // tugas 1 pertemuan 14
        audio.clip = tembak;
        audio.Play();
        
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        GameController.instance.tembakPerRonde--;

        int layer_mask = LayerMask.GetMask("bird_layer");
        if(Physics.Raycast(ray,out hit, maxDistanceRay, layer_mask)){
            objName = hit.collider.gameObject.name;
            birdName.text = objName;

            if(objName == "BirdAsset(Clone)"){
                Destroy(hit.collider.gameObject);

                // spawnNewBird();

                StartCoroutine(spawnNewBird());
				GameController.instance.tembakPerRonde = 3;
				GameController.instance.playerScore++;
				GameController.instance.roundScore++;
            }else{
                // tugas 2 pertemuan 14
                print("Tembakan tidak tepat");
            }
        }

        GameObject gunFlash = Instantiate(Resources.Load("gunFlashSmoke", typeof(GameObject))) as GameObject;
		gunFlash.transform.position = gunFlashTarget.position;

        yield return new WaitForSeconds(fireRate);
        
        nextShot = false;
    }


}
