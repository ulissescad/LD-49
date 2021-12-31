using System;
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Sequences : MonoBehaviour
{
	public Transform cube;
	
	public Transform cubeRotate;
	
	public float duration = 4;

	[SerializeField]
	private AnimationCurve _curve;
	
	[SerializeField]
	private AnimationCurve _curve2;

	IEnumerator Start()
	{
		yield return new WaitForSeconds(1);
		//Sequence s = DOTween.Sequence();
		Original();
		yield return cubeRotate.DOLocalRotate(new Vector3(0, 360, 0), 1, RotateMode.FastBeyond360).SetEase(_curve).WaitForCompletion();
		
		cubeRotate.DOKill();
		
		cubeRotate.DOLocalRotate(new Vector3(0, 360, 0), 1, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
		
		//s.Append(cubeRotate.DORotate(new Vector3(0, 45, 0), 1, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear));
	
		// transform.DOLocalRotate(new Vector3(0, 45, 0), RotateMode.FastBeyond360).SetEase(Ease.Linear).WaitForCompletion();
		yield return new WaitForSeconds(5f);
		
		cubeRotate.DOKill();
		
		yield return cubeRotate.DOLocalRotate(new Vector3(0, 360, 0)*-1, 1, RotateMode.FastBeyond360).SetEase(_curve2).WaitForCompletion();

		cubeRotate.DOKill();
	}

	// void Start()
	// {
	// 	cubeRotate.DORotate(new Vector3(0, 360, 0), 1, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
	// }
	

	IEnumerator Original()
	{
		// Start after one second delay (to ignore Unity hiccups when activating Play mode in Editor)
		yield return new WaitForSeconds(1);
	
		// Create a new Sequence.
		// We will set it so that the whole duration is 6
		Sequence s = DOTween.Sequence();
		// Add an horizontal relative move tween that will last the whole Sequence's duration
		s.Append(cube.DOMoveX(6, duration).SetRelative().SetEase(Ease.InOutQuad));
		// Insert a rotation tween which will last half the duration
		// and will loop forward and backward twice
		s.Insert(0, cube.DORotate(new Vector3(0, 45, 0), duration / 2).SetEase(Ease.InQuad).SetLoops(2, LoopType.Yoyo));
		// Add a color tween that will start at half the duration and last until the end
		s.Insert(duration / 2, cube.GetComponent<Renderer>().material.DOColor(Color.yellow, duration / 2));
		// Set the whole Sequence to loop infinitely forward and backwards
		s.SetLoops(-1, LoopType.Yoyo);
	}
}
