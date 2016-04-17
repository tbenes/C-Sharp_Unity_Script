using UnityEngine;
using System.Collections;

/* Hodnota určující ID teleportů, určuje na jaký teleport je navázaný,
 * stejné ID mohou mít pouze dva na sebe navázané objekty */

public class Teleport : MonoBehaviour {
    public int id;              //ID teleportů
    public bool casovac;        //zda má mít teleport větší odpočet
    float disableTimer = 0;     //Pormněná pro odpočet před dalším použitím teleportu

    void Update()
    {
        //Podmínka počítající čas pro další přenos
        if (disableTimer > 0)
            disableTimer -= Time.deltaTime;

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player" && disableTimer <= 0) {

            foreach (Teleport tp in FindObjectsOfType<Teleport>()) {

                if (tp.code == code && tp != this) {
                    if(!casovac)
                        tp.disableTimer = 5;                                //Nastaví čas pro další použití teleportu
                    Vector3 position = tp.gameObject.transform.position;    //vytvoří promněnou do které uloží pozici druhého teleportu
                    position.y +=2;                                         //zvýší Y pozici o 2
                    collider.gameObject.transform.position = position;      //Přemístí objekt hráče na pozici určenou promněnou pozice
                }
            }
        }        
    }
}
