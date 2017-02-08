using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains the logic for (fading) platforms.
/// </summary>
public class Platform : MonoBehaviour {

	[Tooltip("Enables platform fading in and out.")]
	public bool fading;
	[Range(0,10)]
	[Tooltip("The interval until a platform fades out.")]
	public int fadeOutInterval;
	[Range(0,10)]
	[Tooltip("The interval until a platform fades back in.")]
	public int fadeInInterval;
	[Tooltip("The material used if the platform is active.")]
	public Material activeMaterial;
	[Tooltip("The material used to signal that the platform is inactive.")]
	public Material disabledMaterial;


	/// <summary>
	/// Starts the fading process if fading is enabled.
	/// </summary>
	void Start () {
		if (fading) {
			StartCoroutine (fade(true));
		}
	}

	/// <summary>
	/// This function enables and disables the platorm in alternation using the specified intervals and materials.
	/// </summary>
	/// <param name="fadeOut">If set to <c>true</c> the platform fades out.</param>
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

	/// <summary>
	/// Changes the material of the platform.
	/// </summary>
	/// <param name="active">If set to <c>true</c> it sets it to the active material.</param>
	public void setMaterial(bool active){
		Renderer renderer = GetComponent<Renderer> ();
		if (active) {
			renderer.material = activeMaterial;
		}else{
			renderer.material = disabledMaterial;
		}
	} 
}
