using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	[SerializeField] private Weapon currentWeapon;
	[SerializeField] private WeaponHandler weaponHandler;
	[SerializeField] private Animator animator;
	[SerializeField] private AudioSource slashSound;

	private float sliceTimer;

	private void Awake()
	{

	}

	// Start is called before the first frame update
	void Start()
	{
		currentWeapon = ItemManager.CreateWeapon("Killer bill", 15, 100, ItemRarity.Rare, "Katana", 100, new Enchantment(EnchantmentType.Fire));
		weaponHandler.SetupWeapon(currentWeapon);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			SliceAttack();
		}

		if(sliceTimer <= 0)
		{
			animator.SetBool("Sliceing", false);
			//animator.applyRootMotion = false;
			weaponHandler.EndAttack();
		}
		sliceTimer -= Time.deltaTime;
	}

	public static float GetExpForLevel(int level)
	{
		float returnValue = (float)level * 1.2f * 100f;
		return returnValue;
	}

	public void SliceAttack()
	{
		//slashSound.Play();
		weaponHandler.StartAttack();
		animator.SetBool("Sliceing", true);
		animator.applyRootMotion = true;
		if(sliceTimer <= 0)
		{
			
		}
		animator.SetTrigger("Slice");
		sliceTimer = 2f;
	}

	void Slice()
	{

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Loot"))
		{
			LootDrop lootDrop = other.GetComponent<LootDrop>();
			Debug.Log(lootDrop.item.name);
			GameManager.current.PickupItem(lootDrop.item);
			other.gameObject.SetActive(false);
			Destroy(other.gameObject, 0.01f);
		}
	}
}
