using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingView : IView
{
	private GameObject panelGo;
	public LoadingView(GameObject panel)
	{
		panelGo = panel;
	}

	public void FadeIn(){}
	public void FadeOut(){}

	public void Show(){}
	public void Hide(){}


	public void SetText(string message){}
	public void SetPercent(float percent){}
}
