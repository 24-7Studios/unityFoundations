using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponClass : MonoBehaviour
{
    public string weaponName;
    public Sprite weaponIcon;
    public Canvas weaponInfo;

    public Player player;

    public LayerMask Shootable;

    public string draw;


    public bool Dual;
    public bool isBeingDual;
    public bool hand; //false for left true for right
    public weaponClass otherHand;

    public weaponPickup itemClass;


    public Transform viewModel;
    public Vector3 viewModelOffset;



    public bool displayHudInfo;
    public Sprite reticle;
    public Vector3 reticleScale;


    public bool isItem; 
    public Animator anim;
    public Transform cameraBone;
    public GameObject weapons;
    public PlayerWeaponFunctions controller;

    public bool InputFire1Down;
    public bool InputFire1Hold;
    public bool InputFire1Up;
    public bool InputFire2Down;
    public bool InputFire2Hold;
    public bool InputFire2Up;
    public bool InputReloadDown;
    public bool InputReloadHold;
    public bool InputRealodUp;
    //public bool InputDropDown;


    public virtual void Start()
	{
        

        if (!isItem)
        {
            systemPickup(transform.gameObject);
        }
        else
		{

            itemClass.gameObject.SetActive(true);
            itemClass.enabled = true;

		}

        

        StartCommands();

    }


	public virtual void Update()
	{


        if(!hand && isBeingDual && Dual)
		{
            viewModel.localScale = new Vector3(-Mathf.Abs(viewModel.localScale.x), viewModel.localScale.y, viewModel.localScale.z);
		}
        else
		{
            viewModel.localScale = new Vector3(Mathf.Abs(viewModel.localScale.x), viewModel.localScale.y, viewModel.localScale.z);
        }
        
        

        


        

    }



	public virtual void PrimaryAction()
	{


        Debug.Log("Primary Action has not been set!");

    }


    public virtual void SecondaryAction()
	{


        Debug.Log("Secondary Action has not been set!");

    }

    

    public virtual void StartCommands()
	{



	}






    public virtual void Pickup(GameObject parent)
	{

        Debug.Log("Player picked up " + weaponName);

        transform.SetParent(parent.transform);
        
       
        anim = viewModel.GetComponent<Animator>();
        player = GetComponentInParent<Player>();
        controller = GetComponentInParent<PlayerWeaponFunctions>();
        
        itemClass = transform.GetComponentInChildren<weaponPickup>();
        
        anim.transform.localPosition = viewModelOffset;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        itemClass.transform.localPosition = Vector3.zero;
        itemClass.transform.localRotation = Quaternion.Euler(Vector3.zero);
        anim.gameObject.SetActive(true);

        isItem = false;
        itemClass.gameObject.SetActive(false);
        anim.Play(draw);


        

    }

    public virtual void Drop()
	{

        Debug.Log("Player dropped " + weaponName);

		if (isBeingDual)
		{
            isBeingDual = false;
            otherHand.isBeingDual = false;
            otherHand.otherHand = null;
            otherHand = null;
		}
        
        isItem = true;
        itemClass.gameObject.SetActive(true);
        viewModel.gameObject.SetActive(false);
        transform.parent = null;
        transform.position = player.playerBody.transform.position + player.playerBody.transform.forward * 1;
        itemClass.enabled = true;
        itemClass.hasDropped();
        controller.setWeapons();


    }

  
    public virtual void systemDrop()
	{

        isItem = true;
        itemClass.gameObject.SetActive(true);


        viewModel.gameObject.SetActive(false);
        transform.parent = null;
        itemClass.enabled = true;
        itemClass.hasDropped();
        controller.setWeapons();



    }


    public virtual void systemPickup(GameObject parent)
	{


        transform.SetParent(parent.transform);

       
        anim = viewModel.GetComponent<Animator>();
        player = GetComponentInParent<Player>();
        controller = GetComponentInParent<PlayerWeaponFunctions>();
        weapons = player.weaponHolder;
        itemClass = transform.GetComponentInChildren<weaponPickup>(true);
        transform.SetParent(weapons.transform);
        anim.transform.localPosition = viewModelOffset;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        itemClass.transform.localPosition = Vector3.zero;
        itemClass.transform.localRotation = Quaternion.Euler(Vector3.zero);
        anim.gameObject.SetActive(true);

        isItem = false;
        itemClass.gameObject.SetActive(false);


    }








}
