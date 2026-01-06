using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BarChartController : MonoBehaviour
{
    //输入数据内容：
    //1. 数据数量
    //2. 数据标题
    //3. 数据值


    //需要：struct规定数据标题+数据值
    //获得柱状图的obj
    //根据数据数量，复制obj


    //需要实现细节数据的创建和销毁
    //再创建的时候要记录数据的id和obj对应关系

    [Header("Unity中配置的内容：")]
    [SerializeField]private GameObject barPrefab; // 在Inspector中拖入预制件
    public Transform barContainer; // 柱状图的父对象
    public Text[] text;//这个是坐标轴
    [HideInInspector]public float totalValue;
    [SerializeField] private Text titleText;

    [Header("数据接入：")]//重要！！所有的数据接入变量
    public float maxValue;//数据的最大值
    public string chartTitle;//图标标题
    [Serializable]
    public struct BarData//定义了数据内容
    {
        public string title;
        public float value;
        public string id;
        
    }
    public List<BarData> barDataList = new List<BarData>();//他的长度就是数据个数

    public void CreateData(string title, string value) //接收到string之后记得转换
    {
        float parsedValue = float.Parse(value);
        float roundedValue = (float)Math.Round(parsedValue, 2);// 保留两位小数
        barDataList.Add(new BarData
        {
            title = title,
            value = roundedValue
        });
    }


    private void OnEnable()
    {
        gameObject.tag = "BarChart";


        titleText.text = chartTitle;
        totalValue = 0;
        maxValue = 0;

        barContainer.DetachChildren();
        if (barDataList != null)
        {
            foreach (var data in barDataList)
            {

                float b = data.value;
                if (b > maxValue)
                {
                    maxValue = b;
                }
            }


            for (int i = 0; i <= 4; i++)
            {
                float value = maxValue - i * (maxValue / 4);
                text[i].text = value % 1 == 0 ? value.ToString("0") : value.ToString("0.00");
            }

            ///下面就是生成数据了
            foreach (var data in barDataList)
            {
                GameObject newBar = Instantiate(barPrefab, barContainer);
                newBar.GetComponent<CircleSlider>().value = data.value;

                newBar.GetComponent<CircleSlider>().maxValue = maxValue;
                newBar.GetComponent<CircleSlider>().title = data.title;
                totalValue += data.value;
            }
            titleText.text += "：" + totalValue.ToString("0.00");

        }
        else { 
            Debug.LogError("[BarChartController.cs]:: No data in barDataList!");
        }

    }
}
