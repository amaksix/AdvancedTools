using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class PirateLogic : AgentLogic
{
    #region Static Variables
    private static float _boxPoints = 0.1f;
    private static float _boatPoints = 5.0f;
    private static float _enemyPoints = 7.0f;

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Box"))
        {
            points += _boxPoints;
            Destroy(other.gameObject);
        }
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
