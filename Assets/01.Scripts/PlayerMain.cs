using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
	[SerializeField] private List<KeyCode> MoveKeys;

	[Range(0,20)][SerializeField] private float MinSpeed;
	[Range(0,20)][SerializeField] private float MaxSpeed;
	private float CurSpeed { get; set; }

	private float KeyPressTime = 0f;

	[HideInInspector] public Rigidbody PlayerRB { get; private set; }
	[HideInInspector] public Vector3 MoveDirection { get; private set; }

	private void Awake()
	{
		PlayerRB = GetComponent<Rigidbody>();
		MoveDirection = Vector3.zero;
		CurSpeed = MinSpeed;
	}

	private void Update()
	{
		float moveX = 0;
		if (Input.GetKey(MoveKeys[0])) moveX = 1f;
		else if (Input.GetKey(MoveKeys[1])) moveX = -1f;
		
		float moveZ = 0;
		if (Input.GetKey(MoveKeys[2])) moveZ = -1f;
		else if (Input.GetKey(MoveKeys[3])) moveZ = 1f;
		
		MoveDirection = new Vector3(moveX, 0, moveZ);
		if(MoveDirection == Vector3.zero)
		{
			KeyPressTime += Time.deltaTime;
			Mathf.Clamp(KeyPressTime, 0f, 1f);
		}
		else
		{
			KeyPressTime = 0f;
		}

		Mathf.Lerp(MinSpeed, MaxSpeed, KeyPressTime);
		PlayerRB.velocity = MoveDirection * (CurSpeed * Time.deltaTime);
	}

}
