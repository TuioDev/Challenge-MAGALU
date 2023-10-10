using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BrowserChecker : MonoBehaviour
{
    [SerializeField] private BoolVariable IsMobileBrowser;

    void Awake()
    {
        IsMobileBrowser.Value = GetIsMobile();
    }

    #region WebGL is on mobile

    [DllImport("__Internal")]
    private static extern bool IsMobile();

    public bool GetIsMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        return IsMobile();
#endif
        return false;
    }

    #endregion
}
