using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingView :View
{
    Slider slider;
    Text text;
	public LoadingView(GameObject panel) : base(panel)
    {
        //Acquire ProgressBar
        slider=panel.transform.Find("ProgressBar").GetComponent<Slider>();
        text = panel.transform.Find("Text").GetComponent<Text>();
    }

    public void FadeIn(){}
	public void FadeOut(){}




	public void SetText(string message){
        if (text != null)
        {
            text.text = message;
        }
    }
    public void SetPercent(float percent)
    {
        if (slider != null)
        {
            slider.value = percent;
        }
    }
}
