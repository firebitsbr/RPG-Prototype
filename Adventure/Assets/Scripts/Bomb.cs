using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour 
{
	public float duration = 5f;
	public float radius = 3f;
	public float explosionDuraion = 3f;
	public GameObject explosionModel;

	private float explosionTimer;
	private bool exploded;


	void Start()
	{
		explosionTimer = duration;
		explosionModel.transform.localScale = Vector3.one * radius;
		explosionModel.SetActive(false);
	}

	void Update()
	{
		explosionTimer -= Time.deltaTime;
		if(explosionTimer <= 0f && exploded == false)
		{
			exploded = true;
			Collider[] hitObjects = Physics.OverlapSphere(transform.position, radius);
			foreach(Collider collider in hitObjects)
			{
				if(collider.GetComponent<Enemy>() != null)
				{
					collider.GetComponent<Enemy>().Hit();
				}
			}

			foreach(Collider collider in hitObjects)
			{
				Debug.Log(collider.name + "was hit");
			}
			StartCoroutine(Explode());


		}
	}
	private IEnumerator Explode()
	{
		explosionModel.SetActive(true);

		yield return new WaitForSeconds(explosionDuraion);
		Destroy(gameObject);
	}

} // Main Class
