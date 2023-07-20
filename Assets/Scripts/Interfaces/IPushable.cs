using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPushable
{
    bool IsResisting { get; }

    void BeginResisting();
    void StopResisting();
}
