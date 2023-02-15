using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoinsComponent : MonoBehaviour
{
    [SerializeField] GameObject _goldCoin;
    [SerializeField] GameObject _silverCoin;
    [Space]
    [Range(0, 10)]
    [SerializeField] int _coins;
    [Range(0, 100)]
    [SerializeField] int _goldProbabilityPercent;
    //[SerializeField] int _silverProbabilityPercent;

    private GameObject coin;
    private Rigidbody2D rb;

    private void SpawnCoin(GameObject prefab)
    {
        coin = Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
        rb = coin.GetComponent<Rigidbody2D>();
        Vector2 dir = new Vector2(Random.Range(-5f, 5f), Random.Range(1f, 10f));
        rb.AddForce(dir, ForceMode2D.Impulse);
    }
    public void SpawnCoins()
    {
        if (_coins > 10) _coins = 10;
        if (_coins > 10) _coins = 10;

        int goldGoinNum = Mathf.RoundToInt(_coins * _goldProbabilityPercent * .01f);
        int silverCoinsNum = _coins - goldGoinNum;

        for (int i = 0; i < goldGoinNum; i++)
        {
            SpawnCoin(_goldCoin);
        }
        for (int i = 0; i < silverCoinsNum; i++)
        {
            SpawnCoin(_silverCoin);
        }
    }
}
