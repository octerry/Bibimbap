using UnityEngine;

public class StayAtMousePosition : MonoBehaviour
{
    private Camera cam;
    
    void Start()
    {
        cam = Camera.main;
    }

    void OnGUI()
    { 
        // Je l'ai piqué à la docu Unity
        Vector3 point = new Vector3();
        Event   currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        
        transform.position = point;
    }
}
