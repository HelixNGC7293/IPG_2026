using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSExample1 : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI text;
	float deltaTime = 0;
	void Update()
	{
		deltaTime += Time.deltaTime;
		deltaTime /= 2f;
		var fps = 1 / deltaTime;
		text.text = fps.ToString();
	}
}
