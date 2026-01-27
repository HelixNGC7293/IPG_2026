using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSExample2 : MonoBehaviour
{
	const float fpsMeasureTimer = 0.5f;
	private int fpsCountInterval = 0;
	private float fpsMeasureTimerNext = 0;
	private int currentFPS;
	const string display = "{0} FPS";
	[SerializeField]
	private TextMeshProUGUI text;


	private void Start()
	{
		fpsMeasureTimerNext = Time.realtimeSinceStartup + fpsMeasureTimer;
	}


	private void Update()
	{
		fpsCountInterval++;
		if (Time.realtimeSinceStartup > fpsMeasureTimerNext)
		{
			currentFPS = (int)(fpsCountInterval / fpsMeasureTimer);
			fpsCountInterval = 0;
			fpsMeasureTimerNext += fpsMeasureTimer;
			text.text = string.Format(display, currentFPS);
		}
	}
}