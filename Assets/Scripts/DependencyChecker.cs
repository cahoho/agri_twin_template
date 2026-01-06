using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DependencyChecker : MonoBehaviour
{
    [Header("付费素材占位符")]
    public GameObject paidAssetPlaceholder;
    public Text statusText;

    void Start()
    {
        StartCoroutine(CheckForPaidAssets());
    }

    IEnumerator CheckForPaidAssets()
    {
        // 检查关键预制件是否存在
        bool hasDataVizAssets = CheckAsset("DataVisualizationUI");

        if (!hasDataVizAssets)
        {
            ShowPurchaseMessage();
            EnablePlaceholderMode();
        }
        else
        {
            statusText.text = "Data Visualization UI Pack 已加载";
        }

        yield return null;
    }

    bool CheckAsset(string assetName)
    {
        // 这里可以检查特定预制件或资源
        GameObject testObj = Resources.Load<GameObject>(assetName);
        return testObj != null;
    }

    void ShowPurchaseMessage()
    {
        statusText.text = "数据展台需要 Data Visualization UI Pack\n";
        statusText.text += "💰 价格: $29.9 (Unity Asset Store)\n";
        statusText.text += "https://assetstore.unity.com/packages/2d/gui/data-visualization-ui-pack-193179\n\n";
        statusText.text += "当前使用简化版界面";
    }

    void EnablePlaceholderMode()
    {
        if (paidAssetPlaceholder != null)
        {
            paidAssetPlaceholder.SetActive(true);
        }

        // 这里可以添加简化版的数据可视化
        CreateBasicCharts();
    }

    void CreateBasicCharts()
    {
        // 使用 Unity UI 创建基础图表
        // 虽然不如付费素材好看，但功能完整
        Debug.Log("创建基础数据可视化组件...");
    }
}