using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public static EnemyAttack Instance;
    public List<GameObject> projectilePrefab;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        StartCoroutine(AttackCoroutine(projectilePrefab[Random.Range(0, projectilePrefab.Count)]));
    }

    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * 3f);
    }
    protected IEnumerator AttackCoroutine(GameObject projectile)
    {
        while (transform.position.z > PlayerController.Instance.transform.position.z + 20f)
        {
        // Wait for a random duration before starting the attack
        yield return new WaitForSeconds(Random.Range(1f, 5f));

        // Perform the attack logic here
        Debug.Log("Enemy is attacking!");
        Instantiate(projectile, transform.position, Quaternion.identity);


        // Attack finished, you can add any additional logic here
        Debug.Log("Enemy finished attacking.");
        }

        Destroy(gameObject, 5f);

    }
}
