using UnityEngine;
using System.Collections;

public class SimpleMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		Input.ResetInputAxes();
	}
	
	// Update is called once per frame
	void Update () {
		{
			float input_v = Input.GetAxis ("Vertical");
			float input_h = Input.GetAxis ("Horizontal");
			const float sensitivity = 4f;
			if (input_v != 0) {
				transform.rotation *= Quaternion.Euler (-input_v * sensitivity, 0, 0);
			}
			if (input_h != 0) {
				transform.rotation *= Quaternion.Euler (0, input_h * sensitivity, 0);
			}
			// transform.position += transform.rotation * Vector3.forward * Time.deltaTime * 10.0f;
		}
		// {
		// 	float input_v = Input.GetAxis ("Mouse Y");
		// 	float input_h = Input.GetAxis ("Mouse X");
		// 	const float sensitivity = 10f;
		// 	if (input_v != 0) {
		// 		transform.rotation *= Quaternion.Euler (-input_v * sensitivity, 0, 0);
		// 	}
		// 	if (input_h != 0) {
		// 		transform.rotation *= Quaternion.Euler (0, input_h * sensitivity, 0);
		// 	}
		// }
	}
}
