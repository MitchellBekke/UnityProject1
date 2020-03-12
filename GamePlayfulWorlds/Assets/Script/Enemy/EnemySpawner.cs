using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform enemy;
    public Transform smallEnemy;
    public Transform player;

    public enum ChooseEnemy {normal, small }//maak een enum aan dat verschillende soorten enemies kan hebben. dit kan je aanvinken in de editor
    public ChooseEnemy chooseEnemy;//0 idee
    public Transform enemyChosen;// hier slaan we onze keuze in op van de enum

    [SerializeField] private float timer = 0;// een timer die optelt
    public float spawnTimer = 20;// define hier hoelang er tussen elke spawn moet zitten
    public int aantalEnemyPerSpawn = 3;
    public int range = 100;
    private float currentTime;
    private float offsetTimer;//de timer zal beginnen op een ander timing zodat de spawns random worden
    void Start()
    {
        offsetTimer = Random.Range(0, 15);//zorg dat de timer tussen 0 en () start zodat alle spawners apart zijn
        timer = offsetTimer;
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        switch (chooseEnemy)// een switch met 2 opties. de optie die je kiest zorgt ervoor dat onze chooseinput veranderd naar welke enemy we willen
        {
            case ChooseEnemy.normal:
                enemyChosen = enemy;
                break;
            case ChooseEnemy.small:
                enemyChosen = smallEnemy;
                break;
        }

        if (Vector3.Distance(transform.position, player.position) > range)
        {
            timer += Time.deltaTime;
            if (timer >= spawnTimer)
            {
                for (int i = 0; i < aantalEnemyPerSpawn; i++) //ENABLE DIT ALS JE DESNOODS MEERDERE ENEMIES TEGELIJK WILT SPAWNEN
                {
                    Instantiate(enemyChosen, gameObject.transform.position, gameObject.transform.rotation);

                }
                timer = 0;
            }
        }
        else
        {
            timer = currentTime;
        }
        currentTime = timer;
        
    }
}
