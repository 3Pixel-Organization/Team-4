using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	[SerializeField] private Weapon currentWeapon;
	[SerializeField] private WeaponHandler weaponHandler;
	[SerializeField] private Animator animator;

	private float sliceTimer;

	float expForNextLvl;
	private void Awake()
	{
		Player.Load();
	}

	// Start is called before the first frame update
	void Start()
	{
		currentWeapon = ItemManager.CreateWeapon("Killer bill", 15, 100, ItemRarity.Rare, "Katana", 100, new Enchantment(EnchantmentType.Fire));
		weaponHandler.SetupWeapon(currentWeapon);
		expForNextLvl = GetExpForLevel(Player.level + 1);
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
			weaponHandler.EndAttack();
		}
		sliceTimer -= Time.deltaTime;
		if(Player.expPoints >= expForNextLvl)
		{
			PlayerLevelUp();
		}
	}

	public void PlayerLevelUp()
	{
		Player.level++;
		GameEvents.current.PlayerLevelUp();
		expForNextLvl = GetExpForLevel(Player.level + 1);
	}

	public static float GetExpForLevel(int level)
	{
		float returnValue = (float)level * 1.2f * 100f;
		return returnValue;
	}

	public void SliceAttack()
	{
		weaponHandler.StartAttack();
		animator.SetBool("Sliceing", true);
		if(sliceTimer <= 0)
		{
			animator.SetTrigger("Slice");
		}
		sliceTimer = 0.3f;
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
