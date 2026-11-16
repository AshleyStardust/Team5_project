using UnityEngine;

public class UI_PickUpRotator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Key pick up rotates in a circle
        transform.Rotate(new Vector2(0, 90) * Time.deltaTime);
    }
}
