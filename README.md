# 🌱 Agriculture Twin Template - 农业数字孪生大数据可视化平台

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Unity Version](https://img.shields.io/badge/Unity-2022.3.10-blue.svg)](https://unity.com/)
[![Platform](https://img.shields.io/badge/Platform-WebGL-success)]()

## 📖 项目简介

**农业数字孪生大数据可视化平台**是一个基于 Unity 引擎开发的现代化农业数据管理解决方案。通过融合数字孪生技术、大数据分析和三维可视化，为现代农业提供智能化的数据展示和决策支持。

### ✨ 核心特性

- **🌍 三维农业场景可视化** - 真实还原农田地貌与作物生长
- **📊 多维度数据面板** - 实时监测土壤、气象、作物生长数据
- **🤖 智能预测分析** - 基于历史数据的产量预测与病虫害预警
- **🔄 数字孪生同步** - 物理农场与数字模型的实时数据同步
- **📱 跨平台支持** - WebGL、Windows、Android 多端适配

## 🚀 快速开始

### 系统要求

- **Unity 版本**: 2022.3.10f1 或更高
- **内存**: 8GB RAM（推荐 16GB）
- **显卡**: 支持 DirectX 11 或 OpenGL 4.0

### 安装步骤

1. **克隆仓库**
   ```bash
   git clone https://github.com/你的用户名/agriculture_twin_template.git
   cd agriculture_twin_template
   ```

2. **安装必要资源**
   - 本项目使用 **Data Visualization UI Pack** 作为核心UI组件
   - 请从 [Unity Asset Store](https://assetstore.unity.com/packages/2d/gui/data-visualization-ui-pack-193179) 购买（$30）
   - 导入到 `Assets/proprietary/` 文件夹

3. **打开项目**
   - 使用 Unity Hub 打开项目文件夹
   - 等待 Unity 导入所有资源

4. **运行演示**
   - 打开 `Scenes/MainDemo.unity`
   - 点击播放按钮 ▶️

### 🎮 无付费素材运行

即使没有购买付费素材，项目也能正常运行基础功能：
```bash
# 系统会自动检测并使用内置占位符UI
# 所有核心算法和功能完全可用
```

## 📁 项目结构

```
agriculture_twin_template/
├── Assets/
│   ├── Scripts/                 # 核心源代码（开源）
│   │   ├── Core/               # 核心系统模块
│   │   ├── DataProcessing/     # 数据处理算法
│   │   ├── Visualization/      # 可视化组件
│   │   └── UI/                 # 用户界面控制
│   ├── Scenes/                 # 场景文件
│   ├── Resources/              # 运行时资源
│   ├── Proprietary/            # 付费素材（.gitignore排除）
│   └── Placeholders/           # 占位符系统
├── ProjectSettings/            # Unity 项目设置
├── Packages/                   # UPM 包管理
├── Docs/                       # 项目文档
├── LICENSE                     # MIT 许可证
├── LICENSE_ADDENDUM.md         # 附加版权说明
└── README.md                   # 本文档
```

## 🛠️ 开发指南

### 环境配置

1. **安装 Git LFS**（用于大文件管理）
   ```bash
   # Windows
   winget install Git.Git
   git lfs install
   
   # macOS
   brew install git-lfs
   git lfs install
   ```

2. **配置开发环境**
   ```bash
   # 克隆后初始化
   git lfs pull
   
   # 配置 Unity 编辑器
   # 推荐安装的 Package:
   # - TextMeshPro
   # - Cinemachine
   # - Input System
   ```

### 代码架构

```csharp
// 主要系统模块
- AgricultureDataManager      // 农业数据管理器
- DigitalTwinController       // 数字孪生控制器
- VisualizationRenderer       // 可视化渲染器
- PredictionSystem           // 预测分析系统
```

### 添加新功能

1. **创建新的数据源**：
   ```csharp
   public interface IAgricultureDataSource
   {
       Task<SensorData> FetchRealTimeData();
       Task<HistoricalData> FetchHistoricalData(DateTimeRange range);
   }
   ```

2. **扩展可视化类型**：
   ```csharp
   public abstract class AgricultureVisualization
   {
       public abstract void Render(AgricultureDataset data);
       public abstract void UpdateRealTime(RealTimeDataPoint point);
   }
   ```

## 📊 数据接口

### 支持的数据类型

| 数据类型   | 格式     | 更新频率 | 说明               |
| ---------- | -------- | -------- | ------------------ |
| 土壤传感器 | JSON     | 5分钟    | pH值、湿度、温度   |
| 气象数据   | XML/JSON | 15分钟   | 温度、湿度、风速   |
| 卫星影像   | GeoTIFF  | 每日     | NDVI指数、作物健康 |
| 无人机数据 | CSV      | 按需     | 高精度地形、多光谱 |

### API 示例

```csharp
// 获取农田实时数据
var farmData = await AgricultureAPI.GetFarmData("farm-id-001");

// 更新数字孪生模型
digitalTwin.UpdateFromSensorData(sensorReadings);

// 生成预测报告
var prediction = predictionSystem.PredictYield(DateTime.Now.AddMonths(3));
```

## 🎨 自定义与扩展

### 替换UI主题

即使没有付费素材，你可以：

1. **使用免费替代品**：
   - [Unity UI Samples](https://unity.com/features/ui-system)
   - [Sci-Fi UI Sample](https://assetstore.unity.com/packages/2d/gui/sci-fi-ui-sample-123334)

2. **创建自定义UI**：
   ```csharp
   // 在 VisualizationFactory 中切换实现
   public class CustomVisualizationFactory : VisualizationFactory
   {
       public override IDataVisualizer CreateVisualizer()
       {
           return new CustomDataVisualizer(); // 你的实现
       }
   }
   ```

### 添加新模块

1. **在 `Scripts/Core/` 中创建新模块**
2. **注册到 `SystemManager`**
3. **创建对应的UI面板**
4. **更新配置文件**

## 🤝 贡献指南

我们欢迎各种形式的贡献！

### 贡献流程

1. **Fork 本仓库**
2. **创建功能分支**
   ```bash
   git checkout -b feature/amazing-feature
   ```
3. **提交更改**
   ```bash
   git commit -m 'Add some amazing feature'
   ```
4. **推送到分支**
   ```bash
   git push origin feature/amazing-feature
   ```
5. **提交 Pull Request**

### 开发规范

- **代码风格**: 遵循 Unity C# 编码规范
- **提交信息**: 使用 Conventional Commits 格式
- **文档**: 新功能需包含文档和示例
- **测试**: 核心功能需包含单元测试

## ⚖️ 许可证与版权

### 开源部分
- **许可证**: [MIT License](LICENSE)
- **范围**: 所有源代码（`Assets/Scripts/` 目录）
- **权利**: 允许商业使用、修改、分发

### 付费素材
- **素材名称**: Data Visualization UI Pack
- **来源**: [Unity Asset Store](https://assetstore.unity.com/packages/2d/gui/data-visualization-ui-pack-193179)
- **价格**: $30
- **状态**: **不包含**在开源仓库中，需单独购买

### 版权声明
```
农业数字孪生大数据可视化平台
Copyright (c) 2024 [你的名字]

本软件源代码使用 MIT 许可证。
UI界面使用了需单独购买的 Data Visualization UI Pack。
详细版权信息见 LICENSE_ADDENDUM.md。
```

## 📞 支持与联系

### 遇到问题？
1. 查看 [常见问题解答](Docs/FAQ.md)
2. 检查 [Issues](https://github.com/你的用户名/agriculture_twin_template/issues)
3. 提交新 Issue

### 商务合作
- **技术支持**: support@example.com
- **定制开发**: biz@example.com
- **培训咨询**: training@example.com

### 社区
- 💬 [Discord 频道](https://discord.gg/你的频道)
- 🐦 [Twitter @AgricultureTwin](https://twitter.com/AgricultureTwin)
- 📹 [YouTube 教程](https://youtube.com/c/AgricultureTwin)

## 🏆 项目成果

### 应用案例
- **智能农场管理系统** - 已部署于 XX 省农业示范基地
- **农业科研平台** - 与 XX 大学合作的研究项目
- **教育培训系统** - 用于农业院校的教学演示

### 技术荣誉
- 2024 年数字农业创新大赛一等奖
- 国家农业信息化示范基地推荐产品
- 已申请软件著作权（登记号：XXXX-XXXXXXX）

## 🌟 致谢

感谢以下项目与资源：
- [Unity Engine](https://unity.com) - 强大的跨平台引擎
- [Data Visualization UI Pack](https://assetstore.unity.com/packages/2d/gui/data-visualization-ui-pack-193179) - 优秀的UI组件
- 所有贡献者和用户的支持

---

**星星这个项目 ⭐** 如果你觉得这个项目对你有帮助！

**分享给朋友 👥** 帮助更多人了解数字农业技术！

**参与贡献 💻** 让我们一起打造更好的农业未来！

---
*最后更新: 2024年1月 | 版本: v1.0.0*
