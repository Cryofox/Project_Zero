using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuView : IView
{
	private GameObject panelGo;
	public MainMenuView(GameObject panel)
	{
		panelGo = panel;
	}
	
	public void FadeIn(){}
	public void FadeOut(){}
}
