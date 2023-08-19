using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class Timer : Interactable
{
    [SerializeField] private float time = 0;
    // Start is called before the first frame update
    void Start()
	{
		GetComponent<CollisionEvent>().onEnter += OnInteract;
	}

	public override void OnInteract(GameObject go)
	{
			GameManager.Instance.AddTime(time);
		    if (interactFX != null) Instantiate(interactFX, transform.position, Quaternion.identity);
			if (destroyOnInteract) Destroy(gameObject);
	}
}
