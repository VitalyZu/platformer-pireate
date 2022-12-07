using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmedHeroComponent : MonoBehaviour
{
    public void Armed(GameObject gameObject)
    {
        Hero hero = gameObject.GetComponent<Hero>();

        if (hero != null)
        {
            hero.ArmHero();
        }
    }
}
