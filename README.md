# 🌱 Agriculture Twin Template - 农业数字孪生大数据可视化平台

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)[![Unity Version](https://img.shields.io/badge/Unity-2022.3.10-blue.svg)](https://unity.com/)[![Node.js Version](https://img.shields.io/badge/Nodejs-v24.11.0-purple.svg)](https://nodejs.org/en) [![Platform](https://img.shields.io/badge/Platform-WebGL-success)]()

## 📖 项目简介

**农业数字孪生大数据可视化平台**是一个基于 Unity 引擎开发的现代化农业数据管理解决方案。通过融合数字孪生技术、大数据分析和三维可视化，为现代农业提供智能化的数据展示和决策支持。

***请留意，该项目只作数据传输和接口的演示，不作完全内容展示项目。***

- **🌍 三维农业场景可视化** - 真实还原农田地貌与作物生长
- **📊 多维度数据面板** - 实时监测土壤、气象、作物生长数据
- **🤖 智能预测分析** - 基于历史数据的产量预测与病虫害预警
- **🔄 数字孪生同步** - 物理农场与数字模型的实时数据同步

## 🚀 快速开始

**安装必要资源**

- 本项目使用免费素材[City package](https://assetstore.unity.com/packages/3d/environments/urban/city-package-107224) 和 [Low-Poly Simple Nature Pack](https://assetstore.unity.com/packages/3d/environments/landscapes/low-poly-simple-nature-pack-162153) 
- 本项目使用付费素材 **Data Visualization UI Pack** 作为核心UI组件，请从 [Unity Asset Store](https://assetstore.unity.com/packages/2d/gui/data-visualization-ui-pack-193179) 购买（$29.9）
- 安装好所有素材后，将以上所有素材导入到 `Assets/proprietary/` 文件夹即可正常使用项目。

## 🏠架构说明

上传的根目录为unity素材。

- 其中/agritwin.com/文件夹下的内容是需要部署服务器，在服务器存储的内容。存储改内容后，可以访问服务器网页，进入数据后台。
- 如果想在自己的平台中部署unity生成的大数据面板，请将unity构建方式改为Webgl，并将生成的文件夹放在agritwin服务器文件夹下。
- 我已经将可运行的demo系统放到了【releases】下。大家可以直接访问。其中agritwin/unitywebgl是大数据页面，agritwin/manager/是数据处理页面
- 请留意：该项目使用Node.js技术，请提前开启Nodejs服务并监听8080端口。或者修改相关代码改变监听端口以实现正常项目逻辑。

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
5. **提交 Pull equest**

### 开发规范

- **代码风格**: 遵循 Unity C# 编码规范
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
- **价格**: $29.9
- **状态**: **不包含**在开源仓库中，需单独购买

## 🌟 致谢

感谢以下项目与资源：
- [Unity Engine](https://unity.com) - 强大的跨平台引擎
- [Data Visualization UI Pack](https://assetstore.unity.com/packages/2d/gui/data-visualization-ui-pack-193179) - 优秀的UI组件
- 所有贡献者和用户的支持

---

**Star这个项目 ⭐** 如果你觉得这个项目对你有帮助

**分享给朋友 👥** 帮助更多人了解数字农业技术

**参与贡献 💻** 让我们一起打造更好的农业未来

---
*最后更新: 2026年1月 | 版本: v1.0.0*
