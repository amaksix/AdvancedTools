using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class PirateLogic : AgentLogic
{
    #region Static Variables
    private static float _boxPoints = 0.1f;
    private static float _boatPoints = 5.0f;
    private static float _enemyPoints = 7.0f;
    private static float minPirateWeight = 1;
    private static float maxPirateWeight = 100;
    private static float minPirateScaleMultiplier = 0.5f;
    private static float maxPirateScaleMultiplier = 1.5f;
    #endregion
    private float weight;
    private float scaleMultiplier;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Box"))
        {
            points += _boxPoints;
            Destroy(other.gameObject);
        }
    }
    
    public override void AditionalInitialization()
    {
        weight = Random.Range(minPirateWeight, maxPirateWeight);
        scaleMultiplier = Random.Range(minPirateScaleMultiplier, maxPirateScaleMultiplier);
        gameObject.transform.localScale = gameObject.transform.localScale * scaleMultiplier;
    }

    public override float RecalculateEnemyFactors(float distanceIndex, float enemyDistanceFactor, GameObject collidedObj)
    {
        return distanceIndex * enemyDistanceFactor + GetEnemyWeight();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag.Equals("Boat"))
        {
            points += _boatPoints;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag.Equals("Enemy"))
        {
            if (other.gameObject.GetComponent<PirateLogic>().GetEnemyWeight()> gameObject.GetComponent<PirateLogic>().GetEnemyWeight())
            {
                points -= _enemyPoints;
            }
            else
            {
                points += _enemyPoints;
                Destroy(other.gameObject);
            }
           
        }
    }

}
