using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class GetParentValue : MonoBehaviour
{
    public Text value, title;
    void Update()
    {
        value.text = transform.parent.parent.GetComponent<CircleSlider>().value.ToString();
        title.text = transform.parent.parent.GetComponent<CircleSlider>().title;
    }
}
