# WinUI3_SwapChainPanel_Layered

Test transparency with layered windows by using a SwapChainPanel, which was broken since Windows app SDK 1.1.0 (with black/white background in DirectComposition window)

Only tested on Windows 10 (22H2)

From https://github.com/microsoft/microsoft-ui-xaml/issues/1247, does not work fine on Windows 11 atm (unwanted border + shadow)...

[Edit] Thanks to [michalleptuch](https://github.com/michalleptuch); I updated code for Windows 11 from his [comment](https://github.com/microsoft/microsoft-ui-xaml/issues/1247#issuecomment-1374474960)

![image](https://user-images.githubusercontent.com/22345506/232746149-ffd42cfa-9003-4b1a-9041-3f58e10f42dc.png)


