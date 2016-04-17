using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
    /* Třída která mění polohu kamery, podle dvou odlišných módů
     * 
     * Vyžaduje třídu [ThirdPersonCamera]
     */

public class CameraMode : MonoBehaviour {
    public GameObject cameraTarget; //Objekt který sleduje hlavní kamera
    public GameObject camera;       //Objekt hlavní kamery která snímá hráče
    public GameObject gui;          //Objekt který vykresluje aktuální mód v UI
    public GameObject player;       //Objekt hráče

    float cilovaPoziceKamery = 1;
    public float vyskaKamery = 1.5f;
    float rozdil = 0;

    float cooldown = 5.5f;

    void Start() {
        civilMode();
    }

    void Update() {
        //Přepínání módů kamery pomocí kláves
        if (Input.GetKey(KeyCode.Q))
            civilMode();
        if (Input.GetKey(KeyCode.R))
            actionMode();

        //Nastaví herní mód na akční
        if(camera.GetComponent<ThirdPersonCamera>().distance != cilovaPoziceKamery) {
            rozdil = camera.GetComponent<ThirdPersonCamera>().distance - cilovaPoziceKamery;
            /*Podmínka, pokud bude hodnota aktualní pozice kamery odlišná od té cílové,
            *dojde k přesunu kamery na požadovanou pozici*/
            if (rozdil > 0) {
                camera.GetComponent<ThirdPersonCamera>().distance = camera.GetComponent<ThirdPersonCamera>().distance - rozdil / 20;
            }

            if (rozdil < 0)
            {
                camera.GetComponent<ThirdPersonCamera>().distance = camera.GetComponent<ThirdPersonCamera>().distance - rozdil / 20;
            }
        }
        if (player.GetComponent<PlayerControl>().actionMode == true)
            actionMode();
    }

    //Metoda pro civilní mód
    void civilMode() {
        gui.GetComponent<Text>().text = "Civil";   //Nastavení textu na UI
        cameraTarget.transform.localPosition = new Vector3(0.3f, vyskaKamery, 0);  //Nastavení pozice objektu targetCam
        this.cilovaPoziceKamery = 1.5f;
        player.GetComponent<PlayerControl>().actionMode = false;
    }

    //Metoda pro akční mód
    void actionMode()
    {
        gui.GetComponent<Text>().text = "Action";   //Nastavení textu na UI
        cameraTarget.transform.localPosition = new Vector3(0.3f, vyskaKamery, 0);  //Nastavení pozice objektu targetCam
        this.cilovaPoziceKamery = 3.0f;
        player.GetComponent<PlayerControl>().actionMode = true;
    }

    //Metoda vracející aktuální pozici kamery a nastavující požadovanou pozici kamery
    float posunKamery(float dis) {
        float aktualniPoziceKamery = camera.GetComponent<ThirdPersonCamera>().distance;
        float cilovaPoziceKamery = dis;
        if(aktualniPoziceKamery < cilovaPoziceKamery) {
            while(aktualniPoziceKamery == cilovaPoziceKamery) {
                aktualniPoziceKamery -= 0.1f;
            }
        }
        if (aktualniPoziceKamery > cilovaPoziceKamery)
        {
            while (aktualniPoziceKamery == cilovaPoziceKamery)
            {
                aktualniPoziceKamery += 0.1f;
            }
        }
        return (aktualniPoziceKamery);
    }
}
