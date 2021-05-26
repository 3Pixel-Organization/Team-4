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

	private float comboTimer;

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
			//SliceAttack();
		}

		if(comboTimer <= 0)
		{
			//weaponHandler.EndAttack();
			comboTimer = 0;
		}
		animator.SetFloat("ComboTimer", comboTimer);
		if (comboTimer > 0)
		{
			comboTimer -= Time.deltaTime;
		}
	}

	public void SliceAttack()
	{
		//slashSound.Play();
		//weaponHandler.StartAttack();
		animator.SetTrigger("Slice");
		comboTimer = 0.5f;
	}

	void Slice()
	{

	}

	private void PlaySound()
	{
		
	}

	private void StartAttack()
	{
		//weaponHandler.StartAttack();
	}

	private void EndAttack()
	{
		//weaponHandler.EndAttack();
	}

	private void OnTriggerEnter(Collider other)
	{
		// Old loot code
		/*
		if (other.CompareTag("Loot"))
		{
			LootDrop lootDrop = other.GetComponent<LootDrop>();
			Debug.Log(lootDrop.item.name);
			GameManager.current.PickupItem(lootDrop.item);
			other.gameObject.SetActive(false);
			Destroy(other.gameObject, 0.01f);
		}
		*/
	}
}
