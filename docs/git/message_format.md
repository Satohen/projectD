# 簽入訊息格式

## 格式

- 各區塊以空白行區隔

```` message
<type>:(subject)</type>

<body>

<footer>
````

## 標籤分類

- feat : 功能
- fix : 修改錯誤
- docs : 只修改文件 
- style ： 修改不影響程式碼的內容，如修改空白、格式、分號等
- refactor ： 重構，仍保持原有功能
- perf ： 改善效能的修改
- test ： 有關測試程式的新增與修改
- ci ： 修改有關自動化、輔助工具或物件

## 如何使用

於repo資料夾底下的`.git`資料新增msg-template資料夾，並將`\docs\ci\msg-template`所有檔案複制到`.git\msg-template`路徑下，並執行`git config --local -e`設定alias

- 新增.git\msg-template資料夾

````bash
mkdir .git\msg-template
````

- 將專案提供的template內容複制到.git\msg-template\

````bash
cp .\docs\ci\msg-template\* .\.git\msg-template\
````

- 修改local config

````bash
git config --local -e
````

- 增加以下內容

````config
[alias]
        addfeat =   !git commit -t $(git rev-parse --git-dir)/msg-template/feat-template
        addfix =    !git commit -t $(git rev-parse --git-dir)/msg-template/fix-template
        addrefact = !git commit -t $(git rev-parse --git-dir)/msg-template/refactor-template
        addtest =   !git commit -t $(git rev-parse --git-dir)/msg-template/test-template
        addci =     !git commit -t $(git rev-parse --git-dir)/msg-template/ci-template
        addperf =   !git commit -t $(git rev-parse --git-dir)/msg-template/perf-template
        addstyle =  !git commit -t $(git rev-parse --git-dir)/msg-template/style-template
        adddocs =   !git commit -t $(git rev-parse --git-dir)/msg-template/docs-template
````

- 一行指令
````
mkdir -p .git/msg-tmeplate && cp -vf ./docs/ci/msg-template/* "$_" | git config --local --remove-section alias && echo "[alias]
        addfeat =   !git commit -t $(git rev-parse --git-dir)/msg-template/feat-template
        addfix =    !git commit -t $(git rev-parse --git-dir)/msg-template/fix-template
        addrefact = !git commit -t $(git rev-parse --git-dir)/msg-template/refactor-template
        addtest =   !git commit -t $(git rev-parse --git-dir)/msg-template/test-template
        addci =     !git commit -t $(git rev-parse --git-dir)/msg-template/ci-template
        addperf =   !git commit -t $(git rev-parse --git-dir)/msg-template/perf-template
        addstyle =  !git commit -t $(git rev-parse --git-dir)/msg-template/style-template
        adddocs =   !git commit -t $(git rev-parse --git-dir)/msg-template/docs-template" >> .git/config 
````

## 內容說明

- `<type>:(<subject>)`
  - `<type>`
    > 根據分類，選擇相對應的標籤，以利日後進行統計
  - `<subject>`
    > 描述此提供會影響的範圍、功能、環境
    >
    > 主要以中文呈現，除功能名稱、專用術語為英文除外
    >
    > 盡量以被動語態動詞+名詞說明

- `<body>`
    > 如果可在subject一句話表達時，body不需額外說明，此處位置為填寫完整來龍去脈，減少審核人員覆核時間，請詳細說明:why what how
    >
    > 以下為範例
    >
    > 1.變更原因／背景:發生的背景、操作情境、如何發生(bug修正必須說明)
    >
    > 2.問題：什麼原因造成錯誤，幾個問題對應幾個解決方案
    >
    > 3.解決方案：如何修正
    >
    > 4.詳細說明：詳細作法

- `<footer>`
    > 其他連結：文件、其他相關文件
    > Issue bug id
## 範例

````message
<fix>:(修正進階查詢頁數選項)</fix>

變更原因
測試的時候，發現選擇【縣市】+【學位】，右邊頁數的地方，只剩下箭頭，沒有10.50.100的數字
只要選擇【學門/學類】或者【學位】，都會出現只有箭頭沒有數字的情形

問題
DropDownList中的style='width:10px'，呈現寬度不足

解決方案
同ComplexQueryListForUniversityList相同，設定width:unset，由內容決定

詳細作法
new {@style = "width:unset;" }
````