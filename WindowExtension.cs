//Solution found in the issues section of the original repository

[DllImport("user32.dll")]
private static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

[DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
private static extern IntPtr IntSetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

[DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
private static extern Int32 IntSetWindowLong(IntPtr hWnd, int nIndex, Int32 dwNewLong);

private static int IntPtrToInt32(IntPtr intPtr)
{
    return unchecked((int)intPtr.ToInt64());
}

[DllImport("kernel32.dll", EntryPoint = "SetLastError")]
public static extern void SetLastError(int dwErrorCode);

[Flags]
private enum ExtendedWindowStyles
{
    // ...
    WS_EX_TOOLWINDOW = 0x00000080,
    WS_EX_TRANSPARENT = 32
    // ...
}

private enum GetWindowLongFields
{
    // ...
    GWL_EXSTYLE = (-20),
    // ...
}

private static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
{
    int error = 0;
    IntPtr result = IntPtr.Zero;
    // Win32 SetWindowLong doesn't clear error on success
    SetLastError(0);

    if (IntPtr.Size == 4)
    {
        // use SetWindowLong
        Int32 tempResult = IntSetWindowLong(hWnd, nIndex, IntPtrToInt32(dwNewLong));
        error = Marshal.GetLastWin32Error();
        result = new IntPtr(tempResult);
    }
    else
    {
        // use SetWindowLongPtr
        result = IntSetWindowLongPtr(hWnd, nIndex, dwNewLong);
        error = Marshal.GetLastWin32Error();
    }

    if ((result == IntPtr.Zero) && (error != 0))
    {
        throw new System.ComponentModel.Win32Exception(error);
    }

    return result;
}

// This changes clickability
public static void CaptureMouseClick(this Window window, bool condition)
{
    try
    {
        var hwnd = WindowNative.GetWindowHandle(window);

        if (condition)
        {
            int windowLong = (int)WindowInteropHelper.GetWindowLong(hwnd, (int)GetWindowLongFields.GWL_EXSTYLE);
            WindowInteropHelper.SetWindowLong(hwnd, (int)GetWindowLongFields.GWL_EXSTYLE, (IntPtr)(windowLong & -33));
        }
        else
        {
            int windowLong = (int)WindowInteropHelper.GetWindowLong(hwnd, (int)GetWindowLongFields.GWL_EXSTYLE);
            WindowInteropHelper.SetWindowLong(hwnd, (int)GetWindowLongFields.GWL_EXSTYLE, (IntPtr)(windowLong | (int)ExtendedWindowStyles.WS_EX_TRANSPARENT));
        }
    }
    catch (Exception ex)
    {
        Debug.WriteLine(ex.Message);
    }
}
