using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelUI : MonoBehaviour {

	public Player player;
	TextMesh currentScore;
	TextMesh currentDeaths;
	TextMesh currentTime;
	TextMesh currentLives;

	void Start(){
		TextMesh[] components = GetComponentsInChildren<TextMesh> ();
		foreach(TextMesh label in components){
			switch (label.name) {
			case "Score":
				currentScore = label;
				break;
			case "Deaths":
				currentDeaths = label;
				break;
			case "Time":
				currentTime = label;
				break;
			case "Lives":
				currentLives = label;
				break;
			}
		}
	}

	void FixedUpdate(){
		if(player != null){
			if (currentTime != null) {
				float time = player.PlayerTimer;
				if (time < 60f) {
					currentTime.text = time.ToString("f1");
				} else {
					currentTime.text = (System.Math.Floor (time / 60)) + ":" + (time % 60).ToString("f1");
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (player != null) {
			
			if(currentScore != null)
				currentScore.text = player.Score.ToString();
			
			if (currentDeaths != null)
				currentDeaths.text = player.Deaths + " Deaths";
			
			if (currentLives != null) {
				currentLives.text = "";
				for (int i = 0; i < player.maxLives - player.Deaths; i++) {
					currentLives.text += "♥";
				}
			}
		}
	}
}
