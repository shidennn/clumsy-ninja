using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnterRopeState : MonoBehaviour
{
    public GameObject menu;

  
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the rope state.");
            PlayerController.Instance.onRope = true;
            Debug.Log("onrope:" + PlayerController.Instance.onRope);

            Camera.main.fieldOfView = 80f;
            other.transform.position = new Vector3(0f, other.transform.position.y, other.transform.position.z);
            other.transform.rotation = Quaternion.LookRotation(Vector3.forward);
            //Puts the character to the center of rope.
            PrefabGenerator.Instance.BeginGenerating();
            EnemySpawner.Instance.StartSpawning(EnemySpawner.Instance.enemyPrefab);

            //This also will change the animations of player to rope walking animation. It will be added when the animations are ready.
            menu.SetActive(false);
            gameObject.SetActive(false);
        }
    }

}
