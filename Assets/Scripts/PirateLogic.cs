using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class PirateLogic : AgentLogic
{
    #region Static Variables
    private static float _boxPoints = 0.1f;
    private static float _boatPoints = 5.0f;
    private static float _enemyPoints = 7.0f;
    private static float minPirateWeight = 50;
    private static float maxPirateWeight = 150;
    private static float minPirateScaleMultiplier = 0.5f;
    private static float maxPirateScaleMultiplier = 1.5f;
    #endregion
    public float Weight;
    public float SceleMultiplier;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Box"))
        {
            Debug.Log("BoxTaken");
            points += _boxPoints;
            Destroy(other.gameObject);
        }
    }
    
    public override void AditionalInitialization()
    {
        Weight = Random.Range(minPirateWeight, maxPirateWeight);
        SceleMultiplier =Weight/100;
        gameObject.transform.localScale = gameObject.transform.localScale * SceleMultiplier;
    }

    public override float RecalculateEnemyFactors(float distanceIndex, float enemyDistanceFactor, GameObject collidedObj)
    {
        if(Weight>collidedObj.GetComponent<PirateLogic>().Weight)
        {
            return distanceIndex * enemyDistanceFactor + GetEnemyWeight();
        }
        else
        {
            if (GetEnemyWeight()>0)
            {
                return distanceIndex * enemyDistanceFactor - GetEnemyWeight();
            }
            else
            {
                return distanceIndex * enemyDistanceFactor + GetEnemyWeight();
            }
        }
        return distanceIndex * enemyDistanceFactor + GetEnemyWeight();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag.Equals("Boat"))
        {
            points += _boatPoints;
            Debug.Log("Killed boat");
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag.Equals("Enemy"))
        {
            if (other.gameObject.GetComponent<PirateLogic>().Weight> gameObject.GetComponent<PirateLogic>().Weight)
            {
                points -= _enemyPoints;
            }
            else
            {
                Debug.Log("Killed pirate");
                points += _enemyPoints;
                Destroy(other.gameObject);
            }
           
        }
    }

}
