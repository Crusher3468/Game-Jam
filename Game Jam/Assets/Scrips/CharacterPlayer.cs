using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(CharacterController))]

public class CharacterPlayer : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Animator animator;
	//[SerializeField] private InputRouter inputRouter;

    CharacterController characterController;
	Camera mainCamera;

	Vector2 inputAxis;
	Vector3 velocity = Vector3.zero;
	float inAirTime = 0;
    private int score;

	void Start()
    {
        characterController = GetComponent<CharacterController>();
		mainCamera = Camera.main;

		/*inputRouter.jumpEvent += OnJump;
		inputRouter.moveEvent += OnMove;
		inputRouter.fireEvent += OnFire;
		inputRouter.fireStopEvent += OnFireStop;
		inputRouter.nextItemEvent += OnNextItem;*/

        //GetComponent<Health>().onDamage += OnDamage;
        //GetComponent<Health>().onHeal += OnHeal;
        //GetComponent<Health>().onDeath += OnDeath;

        UIManager.Instance.SetHealth((int)GetComponent<Health>().health);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

		direction.x = inputAxis.x;
		direction.z = inputAxis.y;

		direction = mainCamera.transform.TransformDirection(direction);

		if (characterController.isGrounded)
		{
			velocity.x = direction.x * playerData.speed;
			velocity.y = (velocity.y < 0) ? 0 : velocity.y;
			velocity.z = direction.z * playerData.speed;
			inAirTime = 0;
		}
		else
		{
			velocity.x = direction.x * playerData.speed / 2;
			velocity.z = direction.z * playerData.speed / 2;

			inAirTime += Time.deltaTime;
			velocity.y += playerData.gravity * Time.deltaTime;
		}

		characterController.Move(velocity * Time.deltaTime);
		Vector3 look = direction;
		look.y = 0;
		if (look.magnitude > 0)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look), playerData.turnRate * Time.deltaTime);
		}

		//animator.
		animator.SetFloat("Speed", characterController.velocity.magnitude);
		animator.SetFloat("VelocityY", characterController.velocity.y);
		animator.SetFloat("inAirTime", inAirTime);
		animator.SetBool("isGrounded", characterController.isGrounded);
    
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		Rigidbody body = hit.collider.attachedRigidbody;

		// no rigidbody
		if (body == null || body.isKinematic)
		{
			return;
		}

		// We dont want to push objects below us
		if (hit.moveDirection.y < -0.3)
		{
			return;
		}

		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

		body.velocity = pushDir * playerData.hitForce;
	}

	void OnJump()
	{
        animator.SetTrigger("Jump");
        velocity.y = Mathf.Sqrt(playerData.jumpHeight * -3 * playerData.gravity);
    }

	void OnFire()
	{
//		inventory.Use();
	}

	void OnFireStop()
	{
	//	inventory.StopUse();
	}

	public void OnNextItem()
	{
	//	inventory.EquipNextItem();
	}

	public void OnMove(Vector2 axis)
	{
		inputAxis = axis;
	}

	public void OnAnimEventItemUse()
	{
		Debug.Log("item use");
		//inventory.OnAnimEventItemUse();
	}


	public void OnLeftFootSpawn(GameObject go)
	{
		Transform bone = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
		Instantiate(go, bone.position, bone.rotation);
	}

	public void OnRightFootSpawn(GameObject go)
	{
		Transform bone = animator.GetBoneTransform(HumanBodyBones.RightFoot);
		Instantiate(go, bone.position, bone.rotation);
	}

	public void OnDeath()
	{
		GameManager.Instance.SetPlayerDead();
        Destroy(gameObject);
    }

    public void OnDamage()
    {
        UIManager.Instance.SetHealth((int)GetComponent<Health>().health);
    }

    public void OnHeal()
    {
        UIManager.Instance.SetHealth((int)GetComponent<Health>().health);
    }

    public void AddPoints(int point)
	{
		score += point;
        UIManager.Instance.SetScore(score);

        if (score == 2000)
        {
            GameManager.Instance.SetVictory();
            Destroy(gameObject);
        }
    }
}
