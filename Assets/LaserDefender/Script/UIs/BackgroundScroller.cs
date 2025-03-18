using UnityEngine;

namespace Assets.LaserDefender.Script.UIs
{
    public class BackgroundScroller : MonoBehaviour
    {
		[SerializeField] private float scrollSpeed = 2f; // Tốc độ cuộn
		[SerializeField] private float backgroundHeight = 10f; // Chiều cao của 1 background
		[SerializeField] private GameObject[] backgrounds; // Các background

		private int currentIndex = 0;
		private Vector3 startPosition;

		private void Start()
		{
			if (backgrounds.Length == 0)
			{
				Debug.LogError("Chưa gán background trong Inspector!");
				return;
			}

			startPosition = backgrounds[0].transform.position;
		}

		private void Update()
		{
			// Di chuyển tất cả các background lên trên
			foreach (GameObject bg in backgrounds)
			{
				bg.transform.position += Vector3.up * scrollSpeed * Time.deltaTime;
			}

			// Kiểm tra nếu background đầu tiên đi qua một giới hạn, thì reset vị trí
			if (backgrounds[currentIndex].transform.position.y >= startPosition.y + backgroundHeight)
			{
				// Đưa background xuống phía dưới để tạo hiệu ứng cuộn vô hạn
				int nextIndex = (currentIndex + 1) % backgrounds.Length;
				backgrounds[currentIndex].transform.position = backgrounds[nextIndex].transform.position - new Vector3(0, backgroundHeight, 0);

				// Cập nhật chỉ mục
				currentIndex = nextIndex;
			}
		}
	}
}
