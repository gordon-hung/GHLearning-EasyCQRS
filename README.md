# GHLearning-EasyCQRS
[![.NET](https://github.com/gordon-hung/GHLearning-EasyCQRS/actions/workflows/dotnet.yml/badge.svg)](https://github.com/gordon-hung/GHLearning-EasyCQRS/actions/workflows/dotnet.yml) [![Ask DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/gordon-hung/GHLearning-EasyCQRS) [![codecov](https://codecov.io/gh/gordon-hung/GHLearning-EasyCQRS/graph/badge.svg?token=7GU2vKU1sT)](https://codecov.io/gh/gordon-hung/GHLearning-EasyCQRS)
## CQRS 架構概述

這個專案採用 CQRS 模式將讀取（Query）和寫入（Command）操作分離，提供清晰的職責劃分和更好的可維護性。

### 核心架構層級

**應用層（Application Layer）**
- 實作 Command 和 Query 處理器
- 包含業務邏輯和驗證規則
- 使用依賴注入管理外部服務

**基礎設施層（Infrastructure Layer）**
- 資料存取實作 [1](#1-0) 
- Entity Framework Core 整合
- 資料庫操作封裝

**API 層（WebApi Layer）**
- RESTful API 端點 [2](#1-1) 
- 請求/回應模型轉換
- HTTP 路由配置

## Command 操作實作

### 用戶密碼更新範例
Command Handler 實作展示了典型的寫入操作模式： [3](#1-2) 

處理流程包括：
1. 用戶驗證 [4](#1-3) 
2. 密碼雜湊處理 [5](#1-4) 
3. 資料庫更新 [6](#1-5) 

### 資料存取層實作
Repository 模式提供資料操作抽象： [7](#1-6) 

## API 端點設計

控制器採用依賴注入方式整合 Command/Query 處理器： [8](#1-7) 

## DevOps 與 CI/CD

專案包含完整的持續整合流程： [9](#1-8) 

自動化流程包括：
- 程式碼覆蓋率報告
- 單元測試執行
- 品質檢查

## 架構優勢

1. **職責分離**：Command 和 Query 各自獨立，便於維護
2. **可測試性**：依賴注入和介面抽象提升測試覆蓋率
3. **擴展性**：模組化設計支援功能擴展
4. **可觀測性**：整合 CI/CD 流程確保程式碼品質

## Notes

這個專案是一個完整的 CQRS 學習範例，展示了從 API 層到資料存取層的完整實作。所有程式碼都遵循 .NET 最佳實務，使用 Entity Framework Core 作為 ORM，並整合了現代化的 DevOps 流程。

Wiki pages you might want to explore:
- [Command Operations (gordon-hung/GHLearning-EasyCQRS)](/wiki/gordon-hung/GHLearning-EasyCQRS#4.1)
- [DevOps & CI/CD (gordon-hung/GHLearning-EasyCQRS)](/wiki/gordon-hung/GHLearning-EasyCQRS#7)
