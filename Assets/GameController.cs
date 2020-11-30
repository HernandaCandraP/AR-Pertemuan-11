using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    

    // komponen GUI
    public GameObject startPanel;
    public int playerScore =0;
    public Text hitungTeks;
    public Text hitungNyawa;
    public int ronde = 1;
    public GameObject rondeTeks;
    public Text jmlRonde;
    public Text teksJmlRonde;
    public Text targetTeks;
    public int tembakPerRonde = 3;
    private int nyawa = 2;
    public GameObject[] peluru;

    // show/hide GameObject
    public GameObject GuiTeksSkor;
    public GameObject GUITeksNyawa;
    public GameObject GUITargetBidikan;
    public GameObject GUITembak;
    public GameObject GUIAnjing;
    public GameObject GUITeksRonde;
    public GameObject GUIGameOverPanel;
    public GameObject GUIStartPanel;
    public GameObject Terrain;
    public GameObject GUIGun;

    // control audio
    AudioSource audio;
    public AudioClip[] clips;

    // Aturan ROnde
    private int roundTargetScore=3;
	public int roundScore = 0;
	private int scoreIncrement = 2;
	public bool playerStarted = false;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start () {
		playerScore = int.Parse(hitungTeks.text);
		showStartPanel();
		audio = GetComponent<AudioSource>();
		hitungNyawa.text = nyawa.ToString();
	}

	private void showStartPanel(){
		startPanel.SetActive(true);
	}

	private void hideStartPanel(){
	    startPanel.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		if(DefaultTrackableEventHandler.trueFalse ==true){
			hideStartPanel();
			showItems();
            if(playerStarted == false){
				StartCoroutine(playRound());
			}
			playerStarted = true;
		} else {
			showStartPanel();
			hideItems();
		}
	}

    public IEnumerator playRound(){
		yield return new WaitForSeconds(2f);
		targetTeks.text="Tembak "+tembakPerRonde+" burung";
	}

	public void showItems()
	{
		GuiTeksSkor.SetActive(true);
		GUITeksNyawa.SetActive(true);
		GUITargetBidikan.SetActive(true);
		GUITembak.SetActive(true);
		GUIAnjing.SetActive(true);
		GUITeksRonde.SetActive(true);
        GUIGameOverPanel.SetActive(true);
        // hideStartPanel();
        Terrain.SetActive(true);
        GUIGun.SetActive(true);
	}

	public void hideItems()
	{
		GuiTeksSkor.SetActive(false);
		GUITeksNyawa.SetActive(false);
		GUITargetBidikan.SetActive(false);
		GUITembak.SetActive(false);
		GUIAnjing.SetActive(false);
		GUITeksRonde.SetActive(false);
        GUIGameOverPanel.SetActive(false);
        // hideStartPanel();
        Terrain.SetActive(false);
        GUIGun.SetActive(false);
	}
}
