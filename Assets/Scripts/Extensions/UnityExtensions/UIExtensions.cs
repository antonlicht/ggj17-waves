using UnityEngine;
using UnityEngine.UI;

namespace Shared.Extensions
{
	public static class UIExtensions
	{
		public static RectTransform GetRectTransform (this Component comp)
		{
			return comp.GetComponent<RectTransform>();
		}

		public static RectTransform GetRectTransform (this MonoBehaviour comp)
		{
			return comp.GetComponent<RectTransform>();
		}

		public static void SetAnchoredX (this RectTransform t, float x)
		{
			var pos = t.anchoredPosition;
			pos.x = x;
			t.anchoredPosition = pos;
		}

		public static void SetAnchoredY (this RectTransform t, float y)
		{
			var pos = t.anchoredPosition;
			pos.y = y;
			t.anchoredPosition = pos;
		}

		public static Vector3 WorldToCanvasWorldSpace (this Canvas canvas, Vector3 worldPos, Camera camera = null)
		{
			if (camera == null) {
				camera = Camera.main;
			}

			var viewportPos = GetViewPortPosition(camera, worldPos);
			var canvasRect = canvas.GetComponent<RectTransform>();

			var corners = new Vector3[4];
			canvasRect.GetWorldCorners(corners);
			var size = corners[2] - corners[0];

			var ratio = camera.rect.max - camera.rect.min;
			viewportPos = camera.rect.min + (new Vector2(viewportPos.x * ratio.x, viewportPos.y * ratio.y));

			var x = viewportPos.x * size.x - size.x / 2 + canvas.transform.position.x;
			var y = viewportPos.y * size.y - size.y / 2 + canvas.transform.position.y;

			return new Vector3(x, y, canvas.transform.position.z);
		}
			
		public static Vector3 GetViewPortPosition (Camera camera, Vector3 worldPos)
		{
			if (camera == null) {
				camera = Camera.main;
			}

			return camera.WorldToViewportPoint(worldPos);
		}

		public static Vector3 PositionToWorldSpace (this RectTransform rectTransform, Camera camera = null, Vector3? pivot = null)
		{
			if (camera == null) {
				camera = Camera.main;

				// Extra check because all cameras might not be available if the shop opens and the other elements are disabled.
				if (camera == null) {
					return Vector3.zero;
				}
			}

			if (!pivot.HasValue) {
				pivot = Vector3.one / 2f;
			}

			var corners = new Vector3[4];
			rectTransform.GetWorldCorners(corners);

			var size = corners[2] - corners[0];
			var pos = corners[0] + new Vector3(size.x * pivot.Value.x, size.y * pivot.Value.y, size.z * pivot.Value.z);

			var canvas = rectTransform.GetComponentInParent<Canvas>();
			if (canvas.worldCamera) {
				pos = canvas.worldCamera.WorldToScreenPoint(pos);
			}

			return camera.ScreenToWorldPoint(pos).NewVectorWithZMovedBy(camera.nearClipPlane);
		}

		public static Vector3 CanvasWorldPositionForWorldPositionInOtherCanvas (this Canvas canvas, Vector2 position, Canvas otherCanvas)
		{
			var screenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, position);
			Vector3 worldPosForOtherCanvas;
			RectTransformUtility.ScreenPointToWorldPointInRectangle(otherCanvas.GetRectTransform(), screenPos, otherCanvas.worldCamera, out worldPosForOtherCanvas);
			return worldPosForOtherCanvas;
		}

		public static void SetAlpha (this Graphic graphic, float alpha)
		{
			var color = graphic.color;
			color.a = alpha;
			graphic.color = color;
		}

		public static Vector3[] GetWorldToUIScreenCorners (GameObject target, Camera camera, ref Vector3[] result)
		{
			RectTransform targetRectTranform = target.GetComponent<RectTransform>();
			targetRectTranform.GetWorldCorners(result);

			for (var i=0; i<4; i++) {
				result[i] = RectTransformUtility.WorldToScreenPoint(camera, result[i]);
			}
			return result;
		}
	}
}
