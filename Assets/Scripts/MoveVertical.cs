using UnityEngine;

public class MoveVertical : MonoBehaviour
{
	[SerializeField]
	private	float	speed = 1;

	private void Update()
	{
		transform.position = Vector3.Lerp(new Vector3(6, -3, 0), new Vector3(6, 3, 0), Mathf.PingPong(Time.time * speed, 1));
	}
}

