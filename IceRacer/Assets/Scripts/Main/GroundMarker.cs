using UnityEngine;

public class GroundMarker : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -100f || transform.position.x > 100f)
        {
            Destroy(this.gameObject);
        }
    }
}
