using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SyringeCheck : MonoBehaviour
{

    public GameObject startingWalls;
    public Light playerLight;
    public int syringeCount = 0;
    private float interactDistance = 2;
    public TextMeshProUGUI syringeInteractText;
    public TextMeshProUGUI collectSyringes;
    public TextMeshProUGUI ramDoor;
    public TextMeshProUGUI timer;
    private GameObject[] syringes;
    public bool noSyringes;
    public float syringeSpeed;
    private float currentTime;
    public AudioSource pickUpSound;
    public GameObject finalSyringe;
    public GameObject doors;
    public bool timerRunning;
    private Movement movement;
    public TextMeshProUGUI winText;

    // Start is called before the first frame update
    void Start()
    {
        startingWalls.SetActive(true);
        syringeInteractText.gameObject.SetActive(false);
        collectSyringes.gameObject.SetActive(false);
        ramDoor.gameObject.SetActive(false);
        syringes = GameObject.FindGameObjectsWithTag("Syringe");
        movement = FindObjectOfType<Movement>();
        winText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(syringeCount == 0)
        {
            startingWalls.SetActive(true);
            collectSyringes.gameObject.SetActive(false);
            noSyringes = true;
            syringeSpeed = 10;
        }
        else if(syringeCount == 11)
        {
            finalSyringe.SetActive(true);
            collectSyringes.text = "Collect the Killer's Syringe";
        }
        else if(syringeCount == 12)
        {
            collectSyringes.gameObject.SetActive(false);
            ramDoor.gameObject.SetActive(true);
        }
        else
        {
            startingWalls.SetActive(false);
            noSyringes = false;
            collectSyringes.gameObject.SetActive(true);
            collectSyringes.text = "Collect Syringes:" + syringeCount + "/11";
            ramDoor.gameObject.SetActive(false);
            finalSyringe.SetActive(false);
        }

        if (collectSyringes.gameObject.activeSelf)
        {
            timerRunning = true;
        }
        if (!doors.gameObject.activeSelf)
        {
            timerRunning = false;
        }

        if (timerRunning)
        {
            currentTime += Time.deltaTime;

            int mins = Mathf.FloorToInt(currentTime / 60);
            int sec = Mathf.FloorToInt(currentTime % 60);
            int millisec = Mathf.FloorToInt((currentTime * 100) % 100);
            timer.text = string.Format("{0:00}:{1:00}:{2:00}", mins, sec,millisec);
        }

        PickUp();
        if(movement.win)
        {
            winText.gameObject.SetActive(true);
            timer.gameObject.SetActive(false);
            ramDoor.gameObject.SetActive(false);
            winText.text = "You Escaped!\n\n" + timer.text;
        }
    }

    void PickUp()
    {
        bool syringeNearby = false;

        foreach (GameObject syringe in syringes)
        {
            if (syringe.activeInHierarchy)
            {

                float distance = Vector3.Distance(transform.position, syringe.transform.position);

                if (distance <= interactDistance)
                {
                    syringeNearby = true;
                    syringeInteractText.gameObject.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        syringeCount++;
                        syringe.SetActive(false);
                        syringeNearby = false;
                        syringeSpeed = syringeSpeed + 1;
                        playerLight.range += 1;
                        playerLight.intensity += .2f;
                        pickUpSound.Play();
                    }

                    break;
                }
            }
        }
        if (!syringeNearby)
        {
            syringeInteractText.gameObject.SetActive(false);
        }
    }
}
