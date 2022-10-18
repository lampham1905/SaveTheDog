
using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CointManager : MonoBehaviour
{
    public static CointManager ins;
    //References
    [SerializeField] GameObject coinPrefabParent;
    [SerializeField] GameObject cointPrefab;
    [SerializeField] RectTransform target;
    [SerializeField] RectTransform coinPosition; 
    [SerializeField] int maxCoint;
    Queue<GameObject> conitsQueue = new Queue<GameObject>();

    [SerializeField] [Range (0.5f, 0.9f)] float minAnimDuration;
	[SerializeField] [Range (0.9f, 2f)] float maxAnimDuration;

	[SerializeField] Ease easeType;
	[SerializeField] float spread;

	Vector3 targetPosition;
    private int _coint;

    private void Awake()
    {
        ins = this;
        targetPosition = target.position;

        //prepare pool
        PrepareCoins();
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        _coint = PlayerPrefs.GetInt(Variable.cointCounter);
    }
    void PrepareCoins(){
        GameObject coin;
        for(int i = 0; i < maxCoint; i++){
            coin = Instantiate(cointPrefab);
            coin.transform.parent = transform;
            coin.SetActive(false);
            conitsQueue.Enqueue(coin);      
        }
    }
    public int Coints{
        get{ return _coint;}
        set{
            _coint = value;
            // update UI text when Coint varialbe is changed
            UIManager.ins.UpdateCoinsWhenChanged(Coints);
        }
    }
    public void AddCoint(int cointCounterAdd){
        
        // int cointCounterCurrent = PlayerPrefs.GetInt(Variable.cointCounter);
        // cointCounterCurrent = cointCounterCurrent + cointCounterAdd;
        // PlayerPrefs.SetInt(Variable.cointCounter, cointCounterCurrent);
        // PlayerPrefs.Save();
        AnimationCoins(coinPosition.position, cointCounterAdd);
    }
    private void AnimationCoins(Vector3 positionCoins, int coinsAmount){
        for(int i = 0; i < coinsAmount; i++){
            // Check if there's coins in the pool
            GameObject coin = conitsQueue.Dequeue();
            coin.SetActive(true);

            // move coint to the collected coin pos
            coin.transform.position = positionCoins + new Vector3(Random.Range(-spread, spread), 0f, 0f);

            // animate coin to target pos
            float duration = Random.Range(minAnimDuration, maxAnimDuration);
            coin.transform.DOMove(targetPosition, duration).SetEase(easeType)
            .OnComplete(() => {
                // executes whenwever coin reach target postions
                coin.SetActive(false);
                conitsQueue.Enqueue(coin);

                Coints++;
            });
        }
    }
     
    
}

