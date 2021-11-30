using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeFade : MonoBehaviour
{
	[SerializeField] private Volume volume1;
	[SerializeField] private Volume volume2;

	[SerializeField] private AnimationCurve curve1;
	[SerializeField] private AnimationCurve curve2;
	[SerializeField] bool mirror1 = false;
	[SerializeField] private float lenth = 2;

	private float timer;
	// Start is called before the first frame update
	void Start()
	{
		
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.X))
		{
			DoEffect();
		}
	}

	public void DoEffect()
	{
		StopCoroutine(Animation());
		StartCoroutine(Animation());
	}

	IEnumerator Animation()
	{
		timer = 0;
		while (timer < lenth)
		{
			timer += Time.unscaledDeltaTime;
			if (mirror1)
			{
				volume1.weight = curve1.Evaluate(timer);
				volume2.weight = 1 - curve1.Evaluate(timer);
			}
			else
			{
				volume1.weight = curve1.Evaluate(timer);
				volume2.weight = curve2.Evaluate(timer);
			}
			yield return new WaitForEndOfFrame();
		}
	}
}
