﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public enum EnumFacing
	{
		LEFT,RIGHT
	}

	public static EnumFacing getOpposite(EnumFacing facing){
		if (facing == EnumFacing.LEFT) {
			return EnumFacing.RIGHT;
		} else {
			return EnumFacing.LEFT;
		}
	}

	public custom_inputs inputManager;
	public Feet feet;
	public PlayerArt art;
	public bool allowMovement;
	private bool grounded;

	private EnumFacing facing;
	private EnumFacing prevFacing;

	public float moveSpeed;
	public float jumpSpeed;

	private Rigidbody rigidbody;

	void Start () {
		facing = EnumFacing.RIGHT;
		prevFacing = EnumFacing.RIGHT;
		grounded = true;
		rigidbody = gameObject.GetComponent<Rigidbody> ();
	}
		
	void FixedUpdate () {
		if (allowMovement) {
			doMovement ();
			updateArt ();
		}
	}

	void doMovement(){
		if (inputManager.isInput [0]) {
			rigidbody.velocity += new Vector3 (0f,0f,moveSpeed);
		}

		if(inputManager.isInput [1]){
			rigidbody.velocity -= new Vector3 (0f,0f,moveSpeed);
		}

		if (inputManager.isInput [2]) {
			rigidbody.velocity -= new Vector3 (moveSpeed,0f,0f);
		}

		if(inputManager.isInput [3]) {
			rigidbody.velocity += new Vector3 (moveSpeed,0f,0f);
		}

		if (inputManager.isInput [4] && feet.CheckGroundStatus()) {
			rigidbody.velocity = new Vector3 (rigidbody.velocity.x, jumpSpeed, rigidbody.velocity.z);
		}
	}

	void updateArt(){
		if (rigidbody.velocity == Vector3.zero) {
			art.Idle (getFacing ());
		} else if (rigidbody.velocity.y == 0) {
			art.Walk (getFacing ());
		} else if (rigidbody.velocity.y > 0.1f || rigidbody.velocity.y < -0.1f) {
			art.Jump (getFacing ());
		}
	}

	public EnumFacing getFacing(){
		if (rigidbody.velocity.x > 0) {
			prevFacing = EnumFacing.RIGHT;
			return EnumFacing.RIGHT;
		} else if (rigidbody.velocity.x < 0) {
			prevFacing = EnumFacing.LEFT;
			return EnumFacing.LEFT;
		} else {
			return prevFacing;
		}
	}
}