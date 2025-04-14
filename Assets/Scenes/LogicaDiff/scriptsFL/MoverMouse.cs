using UnityEngine;

public class MoverMouse : MonoBehaviour
{
    private bool arrastrando = false;
    private Vector3 offset;

    void OnMouseDown()
    {
        // calcular la diferencia entre la posición del mouse y del objeto
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z);
        arrastrando = true;
    }

    void OnMouseUp()
    {
        arrastrando = false;
    }

    void Update()
    {
        if (arrastrando)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z) + offset;
        }
    }
}
