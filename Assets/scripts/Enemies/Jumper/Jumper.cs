﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour 
{

	public float jumpTime = 0.5f;

	public float nextJump;

	Rigidbody2D rigidbody2D;
	// Use this for initialization

	private Vector2 direction;
	void Start () 
	{

		rigidbody2D = GetComponent<Rigidbody2D>();
	

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Jump();
	}

	void Jump()
	{
		
	
	}
}