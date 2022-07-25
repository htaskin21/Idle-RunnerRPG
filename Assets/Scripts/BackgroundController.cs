using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [Space]
    public TilemapRenderer firstGroundObject;
    public TilemapRenderer secondGroundObject;
    public TilemapRenderer outOfCameraGround;

    [Space]
    public SkyImage firstSkyImage;
    public SkyImage secondSkyImage;
    public SkyImage outOfCameraSkyImage;
    
    

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
        if (_camera.transform.position.x >=
            firstSkyImage.gameObject.transform.position.x + firstSkyImage.firstLayer.bounds.size.x)
        {
            var o = firstSkyImage.gameObject;
            o.transform.position = new Vector3(
                o.transform.position.x + (firstSkyImage.firstLayer.bounds.size.x + secondSkyImage.firstLayer.bounds.size.x),
                o.transform.position.y, o.transform.position.z);

            outOfCameraSkyImage = firstSkyImage;
        }
        
        if (_camera.transform.position.x >=
            secondSkyImage.gameObject.transform.position.x + secondSkyImage.firstLayer.bounds.size.x)
        {
            var o = secondSkyImage.gameObject;
            o.transform.position = new Vector3(
                o.transform.position.x + (firstSkyImage.firstLayer.bounds.size.x + secondSkyImage.firstLayer.bounds.size.x),
                o.transform.position.y, o.transform.position.z);

            outOfCameraSkyImage = secondSkyImage;
        }
       
        
    }
}