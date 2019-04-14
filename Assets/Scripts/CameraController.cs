using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float panSpeed = 30f;
    [SerializeField] private float panBorderThickness = 10f;

    [SerializeField] private float scrollSpeed = 5f;
    [SerializeField] private float minY = 10f;
    [SerializeField] private float maxY = 80f;
    [SerializeField] private float minZ = 10f;
    [SerializeField] private float maxZ = 80f;
    [SerializeField] private float minX = 10f;
    [SerializeField] private float maxX = 80f;

    private Vector3 cameraForwardVec;

    private void Start()
    {
        cameraForwardVec = transform.position.normalized;
    }

    private void Update()
    {
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        var scroll = Input.GetAxis("Mouse ScrollWheel");

        var pos = transform.position;

        pos -= cameraForwardVec * scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        transform.position = pos;
    }
}