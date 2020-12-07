using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using UnityEngine.SceneManagement;

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
    // tugas 2
    private int nyawa = 2;
    public Text skorGameTeks;
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

		if(roundScore == roundTargetScore)
		{
			playFX(0);
			StartCoroutine(rondeBaru());
			roundScore = 0;
			roundTargetScore += scoreIncrement;
		}

        if(tembakPerRonde == 0)
		{
			peluru[0].SetActive(false);
			StartCoroutine(hilangNyawa());
			tembakPerRonde = 3;
		}

        hitungTeks.text = playerScore.ToString();
	}

    private IEnumerator hilangNyawa(){
		yield return new WaitForSeconds(2);
		nyawa--;

		if(nyawa==0)
		{
			GUITembak.SetActive(false);
			playFX(2);
			GUIGameOverPanel.SetActive(true);
            skorGameTeks.text=playerScore.ToString();
			nyawa = 0;
		} else{
            GUITembak.SetActive(false);
			playFX(2);
			GUIAnjing.SetActive(true);
			yield return new WaitForSeconds(2);
			GUIAnjing.SetActive(false);
			GUITembak.SetActive(true);
			tembakPerRonde = 3;
        }
        hitungNyawa.text = nyawa.ToString();
	}

    public IEnumerator playRound(){
		yield return new WaitForSeconds(2f);
		targetTeks.text="Tembak "+tembakPerRonde+" burung";
        playFX(0);

        StartCoroutine(hideTeksRonde());
	}

    private IEnumerator hideTeksRonde(){
		yield return new WaitForSeconds(4);
		GUITeksRonde.SetActive(false);
	}

	public void showItems()
	{
		GuiTeksSkor.SetActive(true);
		GUITeksNyawa.SetActive(true);
		GUITargetBidikan.SetActive(true);
		GUITembak.SetActive(true);
		// GUIAnjing.SetActive(true);
		// GUITeksRonde.SetActive(true);
        GUIGameOverPanel.SetActive(true);
        // hideStartPanel();
        Terrain.SetActive(true);
        GUIGun.SetActive(true);

        tampilPeluru();
	}

	public void hideItems()
	{
		GuiTeksSkor.SetActive(false);
		GUITeksNyawa.SetActive(false);
		GUITargetBidikan.SetActive(false);
		GUITembak.SetActive(false);
		// GUIAnjing.SetActive(false);
		// GUITeksRonde.SetActive(false);
        GUIGameOverPanel.SetActive(false);
        // hideStartPanel();
        Terrain.SetActive(false);
        GUIGun.SetActive(false);
	}

    public void tampilPeluru(){
		if(tembakPerRonde == 3){
			peluru[0].SetActive(true);
			peluru[1].SetActive(true);
			peluru[2].SetActive(true);
		}else if(tembakPerRonde == 2){
			peluru[0].SetActive(true);
			peluru[1].SetActive(true);
			peluru[2].SetActive(false);
		}else if(tembakPerRonde == 1){
			peluru[0].SetActive(true);
			peluru[1].SetActive(false);
			peluru[2].SetActive(false);
		}
	}

    private IEnumerator rondeBaru(){
		yield return new WaitForSeconds(1);
		ronde++;
		GUITeksRonde.SetActive(true);
		// targetTeks.text= "Tembak "+roundTargetScore+" burung";
		teksJmlRonde.text = ronde.ToString();
		// StartCoroutine(hideRoundText());
	}
    public void restart(){
		hideItems();
		nyawa=2;
		hitungNyawa.text=nyawa.ToString();
		playerScore = 0;
		hitungTeks.text=playerScore.ToString();
		roundTargetScore=3;
		skorGameTeks.text="0";
		ronde=1;
		teksJmlRonde.text = ronde.ToString();
		GUIGameOverPanel.SetActive(false);
		StartCoroutine(playRound());
	}

    private void playFX(int sound){
		audio.clip = clips[sound];
		audio.Play();
	}

    public void quit()
	{
		SceneManager.LoadScene("intro");
	}
}
