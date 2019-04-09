using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 5f;
    public float minY = 10f;

    public float maxY = 80f;
	public float minZ = 10f;
	public float maxZ = 80f;
	public float minX = 10f;
	public float maxX = 80f;

    private Vector3 forvardHexVec = new Vector3(Mathf.Sin(Mathf.PI / 6), 0, Mathf.Cos(Mathf.PI / 6));
    private Vector3 rightHexVec = new Vector3(Mathf.Cos(Mathf.PI / 6), 0, -Mathf.Sin(Mathf.PI / 6));
    private Vector3 cameraForvardVec = new Vector3(0, -Mathf.Cos(Mathf.PI / 4), Mathf.Cos(Mathf.PI / 4));

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
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
        }*/

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos -= cameraForvardVec * scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        transform.position = pos;
    }
}