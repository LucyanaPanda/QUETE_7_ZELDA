using UnityEngine;

public class EnemyHiddenReveal : MonoBehaviour
{
    [SerializeField] private GameObject enemyBody;

    public void RevealBody()
    {
        enemyBody.SetActive(true);
    }
}
