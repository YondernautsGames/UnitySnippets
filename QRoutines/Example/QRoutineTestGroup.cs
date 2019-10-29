using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace STK
{
	public class QRoutineTestGroup : MonoBehaviour
	{
		[SerializeField] private Image m_Image;
		[SerializeField] private float m_MinX;
		[SerializeField] private float m_MaxX;
		[SerializeField] private float m_Wait;
		[SerializeField] private float m_Duration;

		private QRoutineController m_QRoutine;

		void Start () {
			m_QRoutine = GetComponentInParent<QRoutineController> ();
		}

		public void OnColourPress () {
			m_QRoutine.Enqueue (ChangeColour (UnityEngine.Random.ColorHSV()));
		}

		public void OnWaitPress () {
			m_QRoutine.Enqueue (WaitForSecondsUnscaled (m_Wait));
		}

		public void OnLeftPress () {
			m_QRoutine.Enqueue (MoveImage (-20));
		}

		public void OnRightPress () {
			m_QRoutine.Enqueue (MoveImage (20));
		}

		IEnumerator MoveImage (float amount) {
			yield return null;
			Vector3 position = m_Image.transform.localPosition;
			Vector3 target = new Vector3 (
				                 position.x + amount,
				                 position.y,
				                 position.z
			                 );
			float timer = 0f;
			while (timer < m_Duration) {
				timer += Time.deltaTime;
				m_Image.transform.localPosition = Vector3.Lerp (
					position, target,
					Mathf.Clamp01 (timer / m_Duration)
				);
				yield return null;
			}
		}

		IEnumerator ChangeColour (Color to) {
			yield return null;
			Color start = m_Image.color;
			float timer = 0f;
			while (timer < m_Duration) {
				timer += Time.deltaTime;
				m_Image.color = Color.Lerp (
					start, to,
					Mathf.Clamp01 (timer / m_Duration)
				);
				yield return null;
			}
		}

		IEnumerator WaitForSecondsUnscaled (float seconds) {
			yield return null;
			float timer = 0f;
			while (timer < seconds) {
				timer += Time.unscaledDeltaTime;
				yield return null;
			}
		}
	}
}