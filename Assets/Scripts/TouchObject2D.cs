using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TouchObject2D : MonoBehaviour
{
    public UnityEvent touchDownEvent;
    private void OnMouseDown()
    {
        touchDownEvent?.Invoke();
        Debug.Log(gameObject.name + " is touch.");
    }
    //TODO события на разные типы нажатий
}
