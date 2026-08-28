using UnityEngine;

public class RopeMover : MonoBehaviour
{

    public bool destroying = false;
    // Update is called once per frame
    void Update()
    {
        if (PlayerController.Instance.onRope)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 4f);
            if (!destroying)
            {
                destroying = true;
                Destroy(gameObject, 7f);
            }

        }
    }
}
