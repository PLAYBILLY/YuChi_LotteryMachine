|Author|逾期先生|
|---|---
|E-mail|playbilly358@gmail.com

專案介紹：  
YuChi_LotteryMachine是一個基於C#語言開發的專案，它是為每年的《Indie Space》需要使用抽獎機，而產生的客製化抽獎機專案。尚有版本更新前的垃圾舊code，該專案將會持續更新。學習分享用，請勿營利。

使用介紹：  
1.主選單  
Start進入下一畫面；Exit關閉應用程式  

2.選擇紀錄頁面  
此頁面可新增/選擇/編輯/刪除各抽獎紀錄  
刪除鈕須按住數秒以啟動刪除功能(防誤觸)  

3.編輯頁面  
此為編輯獎項的頁面，於「選擇紀錄頁面」點選"編輯"、"新增"鈕皆會跳轉至此  

4.主舞台  
抽獎機主要操作頁面，本頁面有Keyboard輸入功能，為「防止遊客不小心誤觸抽獎功能，需要由工作人員解鎖方可使用」情境所設置。當點擊抽獎鈕後，會彈出得獎獎項與剩餘總數。再點擊其他區域則可關閉(基於已按住對應解鎖鍵之時)。

|輸入鍵|功能|
|---|---
|J|跳轉至獎項數量頁面
|K|按住解鎖抽獎鈕(防止遊客誤觸)
|L|按住解鎖彈出畫面的關閉功能(防止遊客誤觸)
|Escape|跳轉至主選單

5.獎項數量頁面  
本頁面可以確認獎項數量、剩餘數量、重置數量紀錄。剩餘數量預設關閉，避免給遊客看到。

已完成功能：  
1.基本抽獎功能  
2.存/讀/自定義紀錄(存於StreamingAssets；json格式)  

目標更新項目：  
1.介面客製化  
2.UX優化  
3.抽獎機表現方式  

專案工具：  
Unity3D 2018.3.3f1 C#  

使用插件：  
[DOTween (HOTween v2)](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676)  
[Quick Save](https://assetstore.unity.com/packages/tools/integration/quick-save-107676)  
[Simple UI](https://assetstore.unity.com/packages/2d/gui/icons/simple-ui-103969)  
[Odin - Inspector and Serializer](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041)
