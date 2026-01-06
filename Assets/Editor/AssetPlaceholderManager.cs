#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class AssetPlaceholderManager : EditorWindow
{
    private static Dictionary<string, string> paidAssets = new Dictionary<string, string>()
    {
        // Data Visualization UI Pack 的关键组件
        {"DataVisualizationUI", "https://assetstore.unity.com/packages/2d/gui/data-visualization-ui-pack-193179"},
        {"UI_Prefabs/", "包含图表、仪表盘等预制件"},
        {"UI_Scripts/", "数据可视化脚本"},
        {"UI_Sprites/", "UI 素材和图标"},
        // 添加其他付费素材...
    };
    
    [MenuItem("Tools/检查付费素材依赖")]
    public static void CheckDependencies()
    {
        bool hasMissingAssets = false;
        
        foreach (var asset in paidAssets)
        {
            string assetPath = $"Assets/Purchased/{asset.Key}";
            if (!Directory.Exists(assetPath) && !File.Exists(assetPath))
            {
                Debug.LogWarning($"缺失付费素材: {asset.Key}");
                Debug.Log($"   下载链接: {asset.Value}");
                hasMissingAssets = true;
            }
        }
        
        if (!hasMissingAssets)
        {
            Debug.Log("所有付费素材已就位");
        }
        else
        {
            Debug.Log("请在 Asset Store 购买: Data Visualization UI Pack");
            Debug.Log("价格: $29.9 - 构成大数据展台核心");
        }
    }
    
    [MenuItem("Tools/生成占位符场景")]
    public static void GeneratePlaceholderScene()
    {
        // 创建一个演示场景，展示项目功能
        // 即使没有付费素材也能运行
        Debug.Log("创建演示场景...");
    }
}
#endif