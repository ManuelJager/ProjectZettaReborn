#pragma warning disable CS1998

using UnityEngine;

namespace Zetta.GridSystem.Blueprints.Thumbnails
{
    public partial class ThumbnailManager
    {
        // Create thumbnail of given blueprint
        private Sprite CreateThumbnail(Blueprint blueprint)
        {
            // Instantiate ship
            var parent = Ship.InstantiateShip(blueprint, transform);

            // Get size of ship
            var bounds = BlueprintUtilities.GetBounds(blueprint);
            var highestval = bounds.extents.x > bounds.extents.y ?
                bounds.extents.x : bounds.extents.y;

            var thumbnailCameraTransform = thumbnailCamera.gameObject.transform;
            var originalPosition = thumbnailCameraTransform.position;


            // Set position
            thumbnailCameraTransform.position = originalPosition + bounds.center;

            // Set size of camera to half of the highest value in the size vector of the ship
            thumbnailCamera.orthographicSize = highestval;

            // Take snapshot and destroy parent
            var texture = RTImage(thumbnailCamera);
            Destroy(parent.gameObject);

            thumbnailCameraTransform.position = originalPosition;

            // Create sprite
            return Sprite.Create(
                texture,
                new Rect(0f, 0f, 800f, 800f),
                new Vector2(0.5f, 0.5f));
        }

        // Take a "screenshot" of a camera's Render Texture.
        private Texture2D RTImage(Camera camera)
        {
            // The Render Texture in RenderTexture.active is the one
            // that will be read by ReadPixels.
            var currentRT = RenderTexture.active;
            RenderTexture.active = camera.targetTexture;

            // Render the camera's view.
            camera.Render();

            // Make a new texture and read the active Render Texture into it.
            Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
            image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
            image.Apply();

            // Replace the original active Render Texture.
            RenderTexture.active = currentRT;
            return image;
        }
    }
}
