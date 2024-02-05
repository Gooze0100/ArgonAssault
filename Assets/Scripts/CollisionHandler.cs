using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] GameObject shipCollider;
    [SerializeField] GameObject weapons;

    // void OnCollisionEnter(Collision other)
    // {
    //     string collisionName = other.gameObject.name;
    //     Debug.Log(this.name + " --Collided with-- " + collisionName);
    // }

    void OnTriggerEnter(Collider other)
    {
        // string collisionName = other.gameObject.name;
        // Debug.Log($"{this.name} **Triggered by** {collisionName}");

        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        explosion.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerControls>().enabled = false;
        shipCollider.SetActive(false);
        weapons.SetActive(false);
        Invoke("ReloadLevel", loadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
