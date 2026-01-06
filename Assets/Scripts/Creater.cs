using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 架构说明：
/// 1. Creater.cs是专门用来创建和删除的，啥都能创建，包括图表和图标里面的各种数据
/// 2. 泽里也是存储所有Prefab的地方
/// 3. BarChartController是专门控制柱状图的东西，里面有创建数据的方法，Creater调用BarChartController的方法来创建数据
///     注意不只有barchartcontroller，以后还有其他图表，要保持类似的格式
/// 4. DataHandler是数据输入接口
/// 
/// 
/// 
/// 用法:
/// 通过DataHnadler接收数据
/// 接收的数据分四个方法：图表创建删除和数据创建删除
/// 每一个方法都会来调用Creater的对应方法
/// 
/// Creater的方法实现：
/// 1. 图表的创建：
///     先确定图表类型，找到对应的Prefab，实例化
///     这里需要保存实例化出来的图表对象，保存实例化之后的Obj和ID
///     
///     接着设置参数。这里需要调用对应图表的Controller来设置参数
/// 2. 图表的删除：
///     获取图表ID，然后找到对应的实例化对象，销毁
/// 3. 数据的创建：
///     先确定数据parentId，调用这个图表的Controller来创建数据
/// 4. 数据的删除：
///     找到数据对应的parentID，调用这个图表的Controller来删除该id的数据
///     
/// 
/// </summary>
/// 


///在创建图表的时候，使用字典来控制id和实例化对象的对应关系
///由于不同的位置很多， 所以我们用结构体来存储比如Home、虫情检测、监控系统、气象等等
///然后，再每一个结构体中，存储detail（即在具体哪个版块里）
///


///分支面板的名字要和js里面的一样，并保持与obj一一对应
///

[System.Serializable]
public class ReplaceDictionary///替代性的字典解决方案，这个就是用来做那种需要显示在Inspector面板上的字典
                              ///也就是说我需要把各个面板的gameobject托到这里来我才需要这个
                              ///而我在做保存id和obj对应关系的时候就不需要这个了，直接用Dictionary就行
{
    public string detail;
    public GameObject value;
}
[Serializable]
public class Creater : Singleton<Creater>
{
    [Header("图表的prefab")]
    public GameObject barChartPrefab;


    [Header("各大界面的值，把所有主要界面都放在这里，注意，string要和js里面的一样，并保持与obj一一对应")]
    [SerializeField] 
    //什么sbunity字典都没法显示！！！！！
    private List<ReplaceDictionary> pagesList = new List<ReplaceDictionary>();
    public GameObject GetPage(string detail)//这个是location
    {
        var item = pagesList.Find(x => x.detail == detail);
        return item?.value;
    }


    //存储所有图表和数据的字典
    [SerializeField]
    private Dictionary<string, GameObject> allChart = new Dictionary<string, GameObject>();
    [SerializeField]
    private Dictionary<string, GameObject> allData = new Dictionary<string, GameObject>();


    public void CreateChart(CreateChartDataMessage ccdm)
    //id, title, type, location, detail, maxValue,location没用了，只用detail，字典也只存储detail即可
    {
        switch (ccdm.type) {
            case "bar":

                GameObject barIns = Instantiate(barChartPrefab, GetPage(ccdm.detail).transform);
                BarChartController currentbcc = barIns.GetComponent<BarChartController>();
                currentbcc.chartTitle = ccdm.title;
                allChart.Add(ccdm.id, barIns);


                break;
            default:
                Debug.LogError("[Creater.cs]:: typeswrong");
                break;
        
        }


    }
    public void DeleteChart(DeleteChartDataMessage dcdm) 
    //id
    {
        Destroy(allChart[dcdm.id]);
    }
    public void CreateData(CreateDataMessage cdm)
    //id, parentId, title, value
    {
        switch (cdm.parentChartType) { 
            case "bar":
                GameObject fatherChart = allChart[cdm.parentId];

                fatherChart.TryGetComponent<BarChartController>(out BarChartController currentbcc);

                currentbcc.CreateData(cdm.title, cdm.value.ToString());//避免格式问题，直接转字符串

                allData.Add(cdm.id, fatherChart);
                break;
            default:
                Debug.LogError("[Creater.cs]:: parentChartType wrong");
                break;
        }
        
    }
    public void DeleteData(DeleteDataMessage ddm)
    //id, parentId
    {
        Destroy(allData[ddm.id]);
    }



}
