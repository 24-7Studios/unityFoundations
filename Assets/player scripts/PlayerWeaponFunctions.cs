using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class PlayerWeaponFunctions : MonoBehaviour
{


    public Rigidbody body;
    public Transform cam;
    public Animator anim;
    public Canvas hud;
    public Image crosshair;
    public Text hud_weaponName;
    public Text hud_weaponInfo;
    public GameObject weaponHolder;
    public weaponClass equippedWeapon;
    public weaponClass noWeapon;
    public List<weaponClass> weapons;
    
    public int weaponSystem = 0; // 0 = 3 1/2 default | 1 = 2 1/1 halo like | 3 = [] source like

    //public int i;

    [HideInInspector] 
    public bool canFire;
    public bool canPick;
    public int inventoryIndex;
    private int grabDistance = 5;
    private weaponClass lastWeapon;



    // Start is called before the first frame update
    void Start()
    {

        setWeapons();

    }

    // Update is called once per frame
    void Update()
    {




        if (weapons.Count == 0)
        {
            inventoryIndex = -1;
            equippedWeapon = noWeapon;


            noWeapon.gameObject.SetActive(true);
        }
        else
        {


            noWeapon.gameObject.SetActive(false);

            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            {
                inventoryIndex++;


            }

            if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            {
                inventoryIndex--;


            }



            if (inventoryIndex > weapons.Count - 1)
            {
                inventoryIndex = 0;

            }
            else if (inventoryIndex < 0)
            {
                inventoryIndex = weapons.Count - 1;
            }


            if (weapons.IndexOf(lastWeapon) != weapons.IndexOf(equippedWeapon))
            {
                lastWeapon = equippedWeapon;
            }


            equippedWeapon = weapons[inventoryIndex];



        }

        setWeapons();
        foreach (weaponClass e in weapons)
        {
            if(e == noWeapon)
			{
                weapons.RemoveAt(weapons.IndexOf(e));
            }
                
            
        }


        foreach (weaponClass e in weapons)
        {

            e.gameObject.SetActive(true);
            if (equippedWeapon == e && equippedWeapon.isBeingDual && equippedWeapon.otherHand != null)
            {
                e.viewModel.gameObject.SetActive(true);
                e.otherHand.viewModel.gameObject.SetActive(true);
            }
            else if (equippedWeapon == e && !equippedWeapon.isBeingDual)
			{
                e.viewModel.gameObject.SetActive(true);
			}
            else if (e != equippedWeapon.otherHand)
			{

                e.viewModel.gameObject.SetActive(false);

			}


        }


        hud_weaponName.text = equippedWeapon.weaponName;
        hud_weaponInfo.text = equippedWeapon.hud_ammo();
        if(equippedWeapon.reticle != null)
		{
            crosshair.gameObject.SetActive(true);
            crosshair.sprite = equippedWeapon.reticle;
            crosshair.rectTransform.localScale = equippedWeapon.reticleScale;
		}
        else
		{
            crosshair.gameObject.SetActive(false);
		}
       


        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {
            if(equippedWeapon.isBeingDual && (lastWeapon != equippedWeapon.otherHand || lastWeapon.otherHand != equippedWeapon))
			{

                equippedWeapon.anim.Play(equippedWeapon.draw);
                equippedWeapon.otherHand.anim.Play(equippedWeapon.otherHand.draw);

			}
			else if (!equippedWeapon.isBeingDual)
			{

                equippedWeapon.anim.Play(equippedWeapon.draw);

            }

        }




        if (!equippedWeapon.isBeingDual)
		{
            equippedWeapon.InputFire1Down = Input.GetButtonDown("Fire1");
            equippedWeapon.InputFire1Hold = Input.GetButton("Fire1");
            equippedWeapon.InputFire1Up = Input.GetButtonUp("Fire1");
        }
        else if(equippedWeapon.isBeingDual)
		{
            if(equippedWeapon.hand)
			{
                equippedWeapon.otherHand.InputFire1Down = Input.GetButtonDown("Fire1");
                equippedWeapon.otherHand.InputFire1Hold = Input.GetButton("Fire1");
                equippedWeapon.otherHand.InputFire1Up = Input.GetButtonUp("Fire1");
            }
            else
			{
                equippedWeapon.InputFire1Down = Input.GetButtonDown("Fire1");
                equippedWeapon.InputFire1Hold = Input.GetButton("Fire1");
                equippedWeapon.InputFire1Up = Input.GetButtonUp("Fire1");
            }
        }


        if (!equippedWeapon.isBeingDual)
        {
            equippedWeapon.InputFire2Down = Input.GetButtonDown("Fire2");
            equippedWeapon.InputFire2Hold = Input.GetButton("Fire2");
            equippedWeapon.InputFire2Up = Input.GetButtonUp("Fire2");
        }
        else if (equippedWeapon.isBeingDual)
        {
            if (equippedWeapon.hand)
            {
                equippedWeapon.InputFire1Down = Input.GetButtonDown("Fire2");
                equippedWeapon.InputFire1Hold = Input.GetButton("Fire2");
                equippedWeapon.InputFire1Up = Input.GetButtonUp("Fire2");
            }
            else
            {
                equippedWeapon.otherHand.InputFire1Down = Input.GetButtonDown("Fire2");
                equippedWeapon.otherHand.InputFire1Hold = Input.GetButton("Fire2");
                equippedWeapon.otherHand.InputFire1Up = Input.GetButtonUp("Fire2");
            }
        }



        if (Input.GetKeyDown("g") && equippedWeapon != noWeapon)
        {   
            if(equippedWeapon.isBeingDual)
			{
                
                lastWeapon = equippedWeapon.otherHand;
                equippedWeapon.Drop();
                inventoryIndex = weapons.IndexOf(lastWeapon);

			}
            else
			{
                equippedWeapon.Drop();
                inventoryIndex = weapons.IndexOf(lastWeapon);
            }
            

        }
       

        if(Input.GetKeyDown("e") || Input.GetKeyDown("c"))
		{
            RaycastHit r;
         
            
            if(Physics.Raycast(cam.transform.position, cam.transform.forward, out r, grabDistance))
			{
               

                Debug.Log(r.transform.gameObject.name);

                if (r.transform.parent.GetComponent<weaponClass>())
				{
                    weaponClass pickedUpWeapon = r.transform.parent.GetComponent<weaponClass>();
                    
                    pickedUpWeapon.enabled = true;
                    
                    
                    pickedUpWeapon.Pickup(weaponHolder);
                    
                    anim = pickedUpWeapon.GetComponentInChildren<Animator>();
                    pickedUpWeapon.gameObject.SetActive(true);



                    if((equippedWeapon.Dual && pickedUpWeapon.Dual && equippedWeapon.otherHand == null) && Input.GetKeyDown("c"))
					{


                        equippedWeapon.isBeingDual = true;
                        pickedUpWeapon.isBeingDual = true;
                        equippedWeapon.hand = true;
                        pickedUpWeapon.hand = false;
                        equippedWeapon.otherHand = pickedUpWeapon;
                        pickedUpWeapon.otherHand = equippedWeapon;
                        pickedUpWeapon.transform.SetSiblingIndex(inventoryIndex + 1);
                        equippedWeapon = pickedUpWeapon;
                        setWeapons();
                        inventoryIndex = weapons.IndexOf(equippedWeapon);
                        

                        setWeapons();
                    }
					else
					{
                        equippedWeapon = pickedUpWeapon;
                        inventoryIndex = weapons.IndexOf(equippedWeapon);
                        setWeapons();
                    }

                        
                    


				}
               
                
			}
            
                

		}


    }



    public void setWeapons()
	{

        
        weapons = getWeapons();


	}






    public List<weaponClass> getWeapons()
	{

		

        




        return cam.GetComponentsInChildren<weaponClass>(false).ToList();




        


	}



}
