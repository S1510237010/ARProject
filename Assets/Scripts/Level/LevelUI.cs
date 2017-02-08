using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


/// <summary>
/// This class searches for Textmeshes with specific tags and fills them with the appropriate content.
/// </summary>
public class LevelUI : MonoBehaviour {
	[Tooltip("The player object that supplies the data.")]
	public Player player;
	TextMesh currentScore;
	TextMesh currentDeaths;
	TextMesh currentTime;
	TextMesh currentLives;

	/// <summary>
	/// Searches for the components on startup
	/// </summary>
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
		
	/// <summary>
	/// Updates the timer at a fixed interval if one is available
	/// </summary>
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
		
	/// <summary>
	/// Updates all other fields (lives, score)
	/// </summary>
	void Update () {
		if (player != null) {
			
			if(currentScore != null)
				currentScore.text = player.Score.ToString();
			
			if (currentDeaths != null)
				currentDeaths.text = player.Deaths + " Deaths";
			
			if (currentLives != null) {
				currentLives.text = "";
				int i = 0;
				for (; i < player.maxLives - player.Deaths; i++) {
					currentLives.text += "♥";
				}
				currentLives.text += "<color=grey>";
				for (; i< player.maxLives; i++){
					currentLives.text += "♡";
				}
				currentLives.text += "</color>";
			}
		}
	}
}
