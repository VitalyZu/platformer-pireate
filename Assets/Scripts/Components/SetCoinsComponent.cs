using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCoinsComponent : MonoBehaviour
{
    public void SetHeroCoins(int coins)
    {
        GameObject.Find("Player").gameObject.GetComponent<Hero>().setCoins(coins);
    }
}
