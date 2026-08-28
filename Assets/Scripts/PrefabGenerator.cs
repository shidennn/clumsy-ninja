using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PrefabGenerator : MonoBehaviour
{


    public List<GameObject> prefabsToGenerate;
    public Transform ropeGenerationPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static PrefabGenerator Instance;
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
    public IEnumerator GeneratePrefabs()
    {
        while (PlayerController.Instance.onRope)
        {
            Debug.Log("Generating ropes...");
            yield return new WaitForSeconds(3f);
            Instantiate(prefabsToGenerate[0], ropeGenerationPoint.GetChild(0).position, ropeGenerationPoint.rotation * Quaternion.Euler(0f, 0f, 0f));
        }
    }

    public void BeginGenerating()
    {
        StartCoroutine(GeneratePrefabs());
    }
}
