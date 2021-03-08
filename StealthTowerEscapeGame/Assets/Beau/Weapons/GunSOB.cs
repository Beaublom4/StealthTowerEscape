using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGun", menuName = "Gun")]
public class GunSOB : ScriptableObject
{
	public string name;
	public float damage;
	public float fireRate;
	public float range;
	public GameObject gunObj;
	public AudioClip shotSound;
}