using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticGun : Gun
{

    public override void Update()
    {
        base.Update();
        RequestShoot();
    }
}
