using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface StateInterface
{
    string Name();
    void Begin();

    void End();
}
