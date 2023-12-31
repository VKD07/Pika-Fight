using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class MushroomPosionManager : MonoBehaviour
{
    [SerializeField] GameObject [] mushroomPoints;
    [SerializeField] float poisonDamage = 5f;
    [SerializeField] float damageInterval = 2f;
    [SerializeField] float poisonCloudDuration = 5f;
    [SerializeField] float minActivatePoisonTime = 5f;
    [SerializeField] float maxActivatePoisonTime = 10f;
    int randomPoint;
    float randomTime;

    private void Start()
    {
        SetMushroomPointsData();
        StartCoroutine(ActivatePosionRandomly());
    }

    IEnumerator ActivatePosionRandomly()
    {
        while (true)
        {
            randomTime = Random.Range(minActivatePoisonTime, maxActivatePoisonTime);
            randomPoint = Random.Range(0, mushroomPoints.Length);
            yield return new WaitForSeconds(randomTime);
            mushroomPoints[randomPoint].GetComponent<MushroomPoint>().TriggerPoison();
        }
    }

    void SetMushroomPointsData()
    {
        foreach (var mushrooms in mushroomPoints)
        {
            mushrooms.GetComponent<MushroomPoint>().Damage = poisonDamage;
            mushrooms.GetComponent<MushroomPoint>().DamageInterval = damageInterval;
            mushrooms.GetComponent<MushroomPoint>().PoisonDuration = poisonCloudDuration;
        }
    }
}
