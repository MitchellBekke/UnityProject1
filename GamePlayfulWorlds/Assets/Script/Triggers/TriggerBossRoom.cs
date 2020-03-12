using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerBossRoom : MonoBehaviour
{
    // Start is called before the first frame update4

    public GameObject timeLine;
    public GameObject spawner;
    private Weapon weapon;

    public void Start()
    {
        weapon = GameObject.Find("Tommygun").GetComponent<Weapon>();
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayableDirector pd = timeLine.GetComponent<PlayableDirector>();
        if (pd != null)
        {
            spawner.SetActive(false);
            weapon.anim.CrossFade("Holder", 0f);//Ga snel voordat je de fps controller disabled naar een temp anim om alle rotatie en postie te resetten
            weapon.anim.Update(0f);
            weapon.HOLDNAARSPAWN = false;//DEZE BOOL IS ZODAT IK VANAF DE TEMP ANIM NAAR DE SPAWN ANIM TERUG KAN
            pd.Play();
            weapon.HOLDNAARSPAWN = true;//DEZE BOOL IS ZODAT IK VANAF DE TEMP ANIM NAAR DE SPAWN ANIM TERUG KAN
            Destroy(gameObject);
        }
    }
}
