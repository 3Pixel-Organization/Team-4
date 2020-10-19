using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
#if UNITY_EDITOR
	[MenuItem("GameObject/UI/Linear Progress Bar")]
	public static void AddLinearProgressBar()
	{
		GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Linear Progress Bar"));
		obj.transform.SetParent(Selection.activeGameObject.transform, false);
	}
#endif
	public int minimum;
	public int maximum;
	public int current;
	[SerializeField] private Image mask;
	[SerializeField] private Image fill;
	[SerializeField] private Color color;
	[Range(1.0f, 0.0f)]
	[SerializeField] private float barBias = 0.3f;

	private float targetFill;
	// Start is called before the first frame update
	void Start()
	{
		
	}

	public void ValueChange()
	{
		GetCurrentFill();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void FixedUpdate()
	{
		if (targetFill != mask.fillAmount)
		{
			mask.fillAmount = Mathf.Lerp(mask.fillAmount, targetFill, barBias);
		}
	}

	void GetCurrentFill()
	{
		float currentOffset = current - minimum;
		float maximumOffset = maximum - minimum;
		float fillAmount = currentOffset / maximumOffset;
		targetFill = fillAmount;

		fill.color = color;
	}
}
