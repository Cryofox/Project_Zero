using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : IView
{
	private GameObject panelGo;
	public GameView(GameObject panel)
	{
		panelGo = panel;
	}
	
	public void FadeIn(){}
	public void FadeOut(){}
}
