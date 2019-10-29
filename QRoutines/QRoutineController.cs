using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace STK
{
	public class QRoutineController : MonoBehaviour
	{
		[SerializeField] private string m_Title;
		[SerializeField] private int m_MaxQueueLength = 64;
		
		private int m_Handle = -1;
		private IEnumerator[] m_Enqueued;
		private int m_Head;
		private int m_Length;
		private bool m_Running;

		public string title {
			get { return m_Title; }
		}

		public int handle {
			get { return m_Handle; }
		}

		private int head {
			get { return m_Head; }
			set {
				m_Head = value;
				while (m_Head > m_Enqueued.Length)
					m_Head -= m_Enqueued.Length;
			}
		}

		void Start () {
			m_Enqueued = new IEnumerator[m_MaxQueueLength];
		}

		public void Enqueue (IEnumerator coroutine) {
			// Check for null
			if (coroutine == null) {
				Debug.LogError ("QRoutine: Attempting to enqueue null coroutine.");
				return;
			}
			// Check for length
			if (m_Length >= m_MaxQueueLength) {
				Debug.LogError ("QRoutine: Queue limit reached. You can set the max length from the prefab or object in your scene.");
				return;
			}

			if (!m_Running)
				StartCoroutine (ProcessQueue (coroutine));
			else {
				int index = m_Head + m_Length;
				if (index >= m_Enqueued.Length)
					index -= m_Enqueued.Length;
				m_Enqueued [index] = coroutine;
				++m_Length;
			}
		}

		IEnumerator Dequeue () {
			if (m_Length == 0) {
				Debug.LogError ("QRoutine: Attempting to dequeue with zero length.");
				return null;
			}

			IEnumerator result = m_Enqueued [m_Head];
			m_Enqueued [m_Head] = null;
			--m_Length;
			++m_Head;
			if (m_Head >= m_Enqueued.Length)
				m_Head -= m_Enqueued.Length;
			return result;
		}

		IEnumerator ProcessQueue (IEnumerator start) {
			m_Running = true;
			// Yield for starting coroutine
			yield return start;
			// Loop through queue
			while (m_Length > 0)
				yield return Dequeue ();
			// Finish
			m_Running = false;
		}

		// =====================================================
		// TODO: Complete "Clear" functions
		// =====================================================
		public void Clear () {
		}
	}
}