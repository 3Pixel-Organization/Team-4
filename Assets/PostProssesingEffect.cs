using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class PostProssesingEffect : MonoBehaviour
{
	private Volume volume;

	private Bloom bloom;

	[SerializeField] private AnimationCurve curve;
	[SerializeField] private float lenth = 2;

	float timer = 0;
	// Start is called before the first frame update
	void Start()
	{
		volume = GetComponent<Volume>();
		volume.profile.TryGet(out bloom);
		
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.X))
		{
			//StartCoroutine(Animation());
		}
	}

	IEnumerator Animation()
	{
		timer = 0;
		while (timer < lenth)
		{
			timer += Time.deltaTime;
			bloom.intensity.value = curve.Evaluate(timer);
			yield return new WaitForEndOfFrame();
		}
	}
}
