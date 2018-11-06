using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonScript : MonoBehaviour {
	[Tooltip("So long as stateName==clipName, the clips here will play OnClick")]
	public List<AnimationClip> clips;
	[Tooltip("The particle system representing the dragon's breath attack")]
	public GameObject breathFX;
	private Animator anim;
	private AnimationClip nextClip;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		cycleClips();
	}
	private void cycleClips(){//update nextClip, and move it from front to back of list
		nextClip = clips[0];
		clips.RemoveAt (0);
		clips.Add (nextClip);
	}


	//when user clicks model, plays next anim clip, followed by idle clip
	void OnMouseUpAsButton(){//doesnt work with just meshcollider so i added a boxcollider
		StopAllCoroutines();
		StartCoroutine("playClipThenIdle",nextClip);
		cycleClips();
	}

	IEnumerator playClipThenIdle(AnimationClip c){
		anim.Play (c.name);
		print ("playing clip " + c.name);
		if(c.name == "atk02"){//activate the particle system asssociated with the animation
			yield return new WaitForSecondsRealtime (1f);
			breathFX.SetActive (true);
			yield return new WaitForSecondsRealtime (c.averageDuration - 1.3f);
		}
		else{
			yield return new WaitForSecondsRealtime (c.averageDuration);
		}
		breathFX.SetActive (false);
		anim.Play ("idle");
	}
}
