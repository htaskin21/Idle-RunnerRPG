using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public TilemapRenderer firstGroundObject;

    public TilemapRenderer secondGroundObject;

    public TilemapRenderer outOfCameraGround;

    private void Update()
    {
        if (_camera.transform.position.x >=
            firstGroundObject.gameObject.transform.position.x + firstGroundObject.bounds.size.x)
        {
            var o = firstGroundObject.gameObject;
            o.transform.position = new Vector3(
                o.transform.position.x + (firstGroundObject.bounds.size.x + secondGroundObject.bounds.size.x),
                o.transform.position.y, o.transform.position.z);

            outOfCameraGround = firstGroundObject;
        }

        if (_camera.transform.position.x >=
            secondGroundObject.gameObject.transform.position.x + secondGroundObject.bounds.size.x)
        {
            var o = secondGroundObject.gameObject;
            o.transform.position = new Vector3(
                o.transform.position.x + (firstGroundObject.bounds.size.x + secondGroundObject.bounds.size.x),
                o.transform.position.y, o.transform.position.z);

            outOfCameraGround = secondGroundObject;
        }
    }
}