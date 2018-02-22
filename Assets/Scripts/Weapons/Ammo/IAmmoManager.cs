using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TurretTheFence.Weapons.Ammo {

    public interface IAmmoManager {

        bool CanFire();

        void OnFire();

        bool CanReload();

        void OnReload();

        string GetAmmoIndicatorText();

    }

}