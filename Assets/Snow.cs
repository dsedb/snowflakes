using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class Snow : MonoBehaviour
{
	const int SNOW_NUM = 16000;
	private Vector3[] vertices_;
	private int[] triangles_;
	private Vector2[] uvs_;
	private float range_;
	private float rangeR_;
	private Vector3 move_ = Vector3.zero;

	void Start ()
	{
		range_ = 16f;
		rangeR_ = 1.0f/range_;
		vertices_ = new Vector3[SNOW_NUM*4];
		for (var i = 0; i < SNOW_NUM; ++i) {
			float x = Random.Range (-range_, range_);
			float y = Random.Range (-range_, range_);
			float z = Random.Range (-range_, range_);
			var point = new Vector3(x, y, z);
			vertices_ [i*4+0] = point;
			vertices_ [i*4+1] = point;
			vertices_ [i*4+2] = point;
			vertices_ [i*4+3] = point;
		}

		triangles_ = new int[SNOW_NUM * 6];
		for (int i = 0; i < SNOW_NUM; ++i) {
			triangles_[i*6+0] = i*4+0;
			triangles_[i*6+1] = i*4+1;
			triangles_[i*6+2] = i*4+2;
			triangles_[i*6+3] = i*4+2;
			triangles_[i*6+4] = i*4+1;
			triangles_[i*6+5] = i*4+3;
		}

		uvs_ = new Vector2[SNOW_NUM*4];
		for (var i = 0; i < SNOW_NUM; ++i) {
			uvs_ [i*4+0] = new Vector2 (0f, 0f);
			uvs_ [i*4+1] = new Vector2 (1f, 0f);
			uvs_ [i*4+2] = new Vector2 (0f, 1f);
			uvs_ [i*4+3] = new Vector2 (1f, 1f);
		}
		Mesh mesh = new Mesh ();
		mesh.name = "MeshSnowFlakes";
		mesh.vertices = vertices_;
		mesh.triangles = triangles_;
		mesh.uv = uvs_;
		mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 99999999);
		var mf = GetComponent<MeshFilter> ();
		mf.sharedMesh = mesh;
	}
	
	void LateUpdate ()
	{
		var target_position = Camera.main.transform.TransformPoint(Vector3.forward * range_);
		var mr = GetComponent<Renderer> ();
		mr.material.SetFloat("_Range", range_);
		mr.material.SetFloat("_RangeR", rangeR_);
		mr.material.SetFloat("_Size", 0.1f);
		mr.material.SetVector("_MoveTotal", move_);
		mr.material.SetVector("_CamUp", Camera.main.transform.up);
		mr.material.SetVector("_TargetPosition", target_position);
		float x = (Mathf.PerlinNoise(0f, Time.time*0.1f)-0.5f) * 10f;
		float y = -2f;
		float z = (Mathf.PerlinNoise(Time.time*0.1f, 0f)-0.5f) * 10f;
		move_ += new Vector3(x, y, z) * Time.deltaTime;
		move_.x = Mathf.Repeat(move_.x, range_ * 2f);
		move_.y = Mathf.Repeat(move_.y, range_ * 2f);
		move_.z = Mathf.Repeat(move_.z, range_ * 2f);
	}
}
