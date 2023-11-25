using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class PageSelector : Button
{
    void Start()
    {
    }

    public override void OnClick()
    {
        Level.Instance.NextLevel();
    }
}
