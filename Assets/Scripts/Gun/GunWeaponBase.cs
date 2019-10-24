using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunWeaponBase : GunControllerBase {

    protected override void Start()
    {
        base.Start();
        LoadEffectAssets();
    }

    protected override void MouseButtonLeftDown()
    {
        base.MouseButtonLeftDown();
        PlayEffect();
    }

    protected abstract void LoadEffectAssets();
    protected abstract void PlayEffect();
}
