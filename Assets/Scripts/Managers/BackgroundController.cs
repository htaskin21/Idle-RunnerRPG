using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace Managers
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private Image fadeImage;

        [Space]
        public Transform groundObjectParent;

        public Transform skyImageParent;

        [Space]
        public TilemapRenderer firstGroundObject;

        public TilemapRenderer secondGroundObject;
        public TilemapRenderer outOfCameraGround;

        [Space]
        public SkyImage firstSkyImage;

        public SkyImage secondSkyImage;
        public SkyImage outOfCameraSkyImage;

        private CancellationTokenSource _backgroundCts;

        public async UniTask SetBackgrounds(SkyImage skyImage, TilemapRenderer tilemapRenderer)
        {
            _backgroundCts = new CancellationTokenSource();

            fadeImage.gameObject.SetActive(true);
            await fadeImage.DOColor(new Color(0, 0, 0, 1), .65f).SetEase(Ease.OutQuad).AsyncWaitForCompletion();

            SetSkyImages(skyImage);
            SetGroundObject(tilemapRenderer);

            await UniTask.Delay(100);
            
            await fadeImage.DOColor(new Color(0, 0, 0, 0), 1f).SetEase(Ease.OutQuad)
                .OnComplete(() => fadeImage.gameObject.SetActive(false)).AsyncWaitForCompletion();

            _backgroundCts.Cancel();
        }

        private void SetSkyImages(SkyImage skyImage)
        {
            if (firstSkyImage != null)
            {
                Destroy(firstSkyImage.gameObject);
                Destroy(secondSkyImage.gameObject);
            }

            firstSkyImage = Instantiate(skyImage, skyImageParent, true);
            secondSkyImage = Instantiate(skyImage, skyImageParent, true);

            secondSkyImage.transform.position =
                new Vector3(firstSkyImage.transform.position.x + firstSkyImage.firstLayer.bounds.size.x, 0, 0);
        }

        private void SetGroundObject(TilemapRenderer tilemapRenderer)
        {
            if (firstGroundObject != null)
            {
                Destroy(firstGroundObject.gameObject);
                Destroy(secondGroundObject.gameObject);
            }

            firstGroundObject = Instantiate(tilemapRenderer, groundObjectParent, true);
            secondGroundObject = Instantiate(tilemapRenderer, groundObjectParent, true);

            secondGroundObject.transform.position =
                new Vector3((secondGroundObject.transform.position.x + firstGroundObject.bounds.size.x),
                    secondGroundObject.transform.position.y, secondGroundObject.transform.position.z);
        }

        //Update'de ground'un ve arka plan image'inin kamera'dan çıktığunı kontrol ediyor.
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
                    o.transform.position.x +
                    (firstSkyImage.firstLayer.bounds.size.x + secondSkyImage.firstLayer.bounds.size.x),
                    o.transform.position.y, o.transform.position.z);

                outOfCameraSkyImage = firstSkyImage;
            }

            if (_camera.transform.position.x >=
                secondSkyImage.gameObject.transform.position.x + secondSkyImage.firstLayer.bounds.size.x)
            {
                var o = secondSkyImage.gameObject;
                o.transform.position = new Vector3(
                    o.transform.position.x +
                    (firstSkyImage.firstLayer.bounds.size.x + secondSkyImage.firstLayer.bounds.size.x),
                    o.transform.position.y, o.transform.position.z);

                outOfCameraSkyImage = secondSkyImage;
            }
        }
    }
}