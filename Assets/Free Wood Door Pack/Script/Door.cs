using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace DoorScript
{
	[RequireComponent(typeof(AudioSource))]


public class Door : MonoBehaviour {
	private bool open;
	public float smooth = 1.0f;
	float DoorOpenAngle = -90.0f;
    float DoorCloseAngle = 0.0f;
	public AudioSource asource;
	public AudioClip openDoor,closeDoor;
	private bool playerIn = false;
	public GameObject promptUIFront;
	public GameObject promptUIBack;
	public TMP_Text promptTextFront;
	public TMP_Text promptTextBack;
	public LevelManager levelManager;
	// Use this for initialization
	void Start () {
		asource = GetComponent<AudioSource> ();
			promptUIFront.SetActive(false);
			promptUIBack.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (playerIn && Input.GetKeyDown(KeyCode.E) && levelManager.doorAllowed) 
		{
			//Debug.Log("player pressed E");
			OpenDoor();
		}
		if (open)
		{
			//Debug.Log("door opening");
            var target = Quaternion.Euler (0, DoorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);
	
		}
		else
		{
			//Debug.Log("door closing");
            var target1= Quaternion.Euler (0, DoorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * 5 * smooth);
	
		}  
	}

	public void OpenDoor(){
		open =!open;
		//Debug.Log("open changed");
		asource.clip = open?openDoor:closeDoor;
		asource.Play ();
	}

	void OnTriggerEnter(Collider other)
    {
		//Debug.Log("player enter.");
        if (other.CompareTag("Player"))
        {
			playerIn = true;
        }
    }

	void OnTriggerExit(Collider other)
	{
		//Debug.Log("player out.");
		if (other.CompareTag("Player"))
        {
			playerIn = false;
			if (open)
				{
					OpenDoor();
				}
        }
	}

	public void OnInteract() 
	{ 
		promptTextFront.text = levelManager.doorAllowed? "Unlocked" : "Locked";
		promptTextBack.text = levelManager.doorAllowed? "Unlocked" : "Locked";
		StartCoroutine(DoorInteract());
	}

	System.Collections.IEnumerator DoorInteract()
	{
		promptUIFront.SetActive(true);
		promptUIBack.SetActive(true);
		yield return new WaitForSeconds(3);
		promptUIFront.SetActive(false);
		promptUIBack.SetActive(false);
	}
}
}