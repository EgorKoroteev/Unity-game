using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject DoorU;
    public GameObject DoorR;
    public GameObject DoorD;
    public GameObject DoorL;
}
/*public Transform[] PointSpawn;
public Enemy[] EnemiesPrefabs;

public void SpawnCubes()
{
    for (int i = 0; i < PointSpawn.Length - Random.Range(0, 3); i++)
    {
        var newEnemy = Instantiate(EnemiesPrefabs[Random.Range(0, EnemiesPrefabs.Length)]);
        PointSpawn[i].transform.position = newEnemy.transform.position;
    }
}*/