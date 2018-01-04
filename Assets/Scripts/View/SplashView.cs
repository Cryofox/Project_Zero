using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashView : IView
{
	private GameObject panelGo;
	public SplashView(GameObject panel)
	{
		panelGo = panel;
	}
	
	public void FadeIn(){}
	public void FadeOut(){}
}
