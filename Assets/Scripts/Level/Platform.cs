using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	public bool fading;
	[Range(0,120)]
	public int fadeOutInterval;
	[Range(0,120)]
	public int fadeInInterval;
	public Material activeMaterial;
	public Material disabledMaterial;

	// Use this for initialization
	void Start () {
		if (fading) {
			StartCoroutine (fade(true));
		}
	}

	IEnumerator fade (bool fadeOut){
		if (fadeOut) {
			yield return new WaitForSeconds(fadeOutInterval);
		} else {
			yield return new WaitForSeconds(fadeInInterval);
		}
		if (fading) {
			StartCoroutine (fade (!fadeOut));
		}
		if(gameObject != null){
			BoxCollider collider = gameObject.GetComponent<BoxCollider> ();
			if (collider != null) {
				collider.enabled = !collider.enabled;
				setMaterial (collider.enabled);
			}
		}
	}

	public void setMaterial(bool active){
		Renderer renderer = GetComponent<Renderer> ();
		if (active) {
			renderer.material = activeMaterial;
		}else{
			renderer.material = disabledMaterial;
		}
	} 
	
	// Update is called once per frame
	void Update () {
		
	}
}
