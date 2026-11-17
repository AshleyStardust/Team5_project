using UnityEngine;

public class UI_PickUpRotator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    { 
        transform.Rotate(new Vector2(0, 90) * Time.deltaTime); //Rotating animation for key inventory image in top left
    }
}
