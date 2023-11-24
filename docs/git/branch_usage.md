# 分支的應用

先從二個問題開始思考：
1. 在何時、何地、用什麼名稱來建立分支？
2. 在何時、從那裡合併到那裡去？

應參考git flow、github flow、trunk based、git lab flow後，建立適合團隊開發的分支策略與工作流程，以下為經驗分享。

## 短期使用分支
以「功能開發」、「問題修正」為例，此類分支以單一作業為單位且為團隊合作的一部份，在這種作業下所建立的分支屬於短期使用的分支。

### 測試新功能(functional branch)

開發新功能時，針對每一個功能都建立一個分支，作業流程如下：

1. 在master分支下建立 feat/`<function name>`。
2. 開發人員在 feat/`<function name>`本地端上開發新功能(可能多次的commit產生)。
3. optional push至remote前可使用rebase，將所有local commit整理為一個commit，使merge至master前能比較乾淨，或是保留所有commit，在remote repo呈現所有commit。(以團隊決定為主，通常建議將所有歷程整理為一個)。
4. local端測試完成後，將分支push至remote repo，並通知審查人員驗收。
5. 待審查通過後，將feat/`<function name>`merge至master分支。
6. 隨時保持master為可用與建置無誤的狀態。

- 建立分支
  - 時間點：新功能開發前
  - 從那個位置：master分支
  - 名稱：例如「feat/`<function name>`，以清楚易懂為主
- 合併分支
  - 時間點：待功能分支開發完成並完成測試
  - 到那個位置：master分支

### 修正錯誤(fixed branch)

盡量於功能驗收時完善測試或避免長週期發佈release，可避免fix分支頻繁產生與合併的困難，若線上環境發生嚴重問題需進行修補，應於release分支上建立分支並於測試完成後合併至release分支進行發佈，若問題評估可等待至下一週期發佈時進行修補，應於master分支建立並於測試完成後合併至master分支等待下一次發佈。

- 若release週期過長，可能master與release分離過久可能產生衝突問題或master在發佈新release時需進行完整測試，無法快速發佈fix更新，此時應於release建立與合併，再使用cherry pick回master更新主要維護分支。

1. 原分支建立分支 fix/123, 使用issue number或issue命名，由開發團隊制定。
2. 在fix/123分支上修正錯誤。
3. 在fix/123於local端確認測試後，整理local commit，進行push remote branch
4. 通知審查人員針對fix/123進行驗收
5. 待審查通過後，將fix/123merge至master/release分支
6. 發佈fix版本，更新版號

## 長期使用分支(專案開發和常態的版本管理)

- ~~開發分支(develop)~~

  > 不使用，人數過少，且若與master分治過久，合併時容易產生衝突

- 維護分支(master)
  
  > 保留，做為主要維護分支

- 發佈分支(release)

  > 當uat通過行政人員驗證後，合併uat並釋出版本，為線上運行版本，版本號碼為`<major>`.`<minor>`.`<patch>`

- 使用者驗收測試分支(uat)
  
  > 固定週期由master合入並釋出測試版本，為測試環境運行版本，版本號碼為`<major>`.`<minor>`.`<patch>`-`<tag>`
  - tag name
    - rc
    - pre

- uat：user acceptance testing
## 參考資料

[trunk based](https://trunkbaseddevelopment.com/)
[git lab flow](https://docs.gitlab.com/ee/topics/gitlab_flow.html)
[software version numbers](https://www.appmysite.com/blog/understanding-software-version-numbers-the-complete-guide/)