using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    // here we just Instantiate prefab so we will delete it when we won't need it because it still active when created
    // [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 4;

    ScoreBoard scoreBoard;
    GameObject parentGameObject;

    void Start()
    {
        // FindObjectOfType look through entire project and when it find we can use it
        // FindObjectOfType use it rarely because it resource resourceful, but it is more useful in start method because you search it once
        // game object is just types of components so ScoreBoard is on canvas scoreboard and it recognizes with different names like TextMeshPro - Text(UI) is same object
        scoreBoard = FindObjectOfType<ScoreBoard>();

        // so we get reference here, different way
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidbody();
    }

    void AddRigidbody()
    {
        // we don't need to get component thats why we can turn of gravity without getting component
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();

        // var rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints < 1)
        {
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        // searching position in the world
        // vfx.transform.parent = parent;
        vfx.transform.parent = parentGameObject.transform;
        hitPoints--;
        scoreBoard.IncreaseScore(scorePerHit);
    }

    void KillEnemy()
    {
        scoreBoard.IncreaseScore(scorePerHit);

        // this is how we clone Particle System as GameObject on enemy objects
        // Quaternion.identity means no rotation
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        // here we say that entity we Instantiate has a parent and belongs to it
        // vfx.transform.parent = parent;
        // fx is because it is just two effects particle and sound so not just visual
        fx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }
}
