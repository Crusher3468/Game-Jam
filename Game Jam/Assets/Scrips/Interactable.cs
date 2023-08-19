using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
	[SerializeField] public GameObject interactFX;
	[SerializeField] public bool destroyOnInteract = true;
	[SerializeField] protected Condition condition;
	[SerializeField] public AudioSource sounds;

	public abstract void OnInteract(GameObject target);
}
