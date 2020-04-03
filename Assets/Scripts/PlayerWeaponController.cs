using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the functionality and effects of the flashlight and the shotgun.
/// </summary>
public class PlayerWeaponController : MonoBehaviour
{
    public enum Weapon {
        flashlight,
        shotgun,
    }

    public Weapon curWeapon;

    [SerializeField]
    private GameObject flashLightObj;

    [SerializeField]
    private GameObject flashLightFlasher;

    private bool flashlightOn = false;

    [SerializeField]
    private Image flashLightImage;

    [SerializeField]
    private GameObject shotgunObj;

    [SerializeField]
    private GameObject shotgunObj2;

    [SerializeField]
    private Light shotgunFlasher;

    [SerializeField]
    private Image shotgunBangThing;

    [SerializeField]
    private Image shotgunImage;

    [SerializeField]
    private GameObject shotgunMovable;

    [SerializeField]
    private Image[] ammoIcons;

    public int ammo = 10;

    [SerializeField]
    private Text noAmmoIndicator;

    // Start is called before the first frame update
    void Start()
    {
        curWeapon = Weapon.flashlight;
        turnOffFlashLight();
        shotgunImage.gameObject.SetActive(false);
        shotgunObj2.SetActive(false);
        Hashtable ht = iTween.Hash("from", -1000, "to", -500, "time", 0, "onupdate", "changeShotgunPosition");
        iTween.ValueTo(gameObject, ht);
        noAmmoIndicator.enabled = false;
        flashLightImage.enabled = true;
        Color colour = shotgunBangThing.color;
        colour.a = 0;
        shotgunBangThing.color = colour;
    }

    public void switchToShotGun ()
    {
        if (curWeapon == Weapon.shotgun) return;
        curWeapon = Weapon.shotgun;
        shotgunObj.SetActive(true);
        shotgunObj2.SetActive(true);
        flashLightObj.SetActive(false);
        shotgunImage.gameObject.SetActive(true);
        moveShotgunIntoPosition();
        flashLightImage.enabled = false;
    }

    public void switchToFlashLight()
    {
        if (curWeapon == Weapon.flashlight) return;
        curWeapon = Weapon.flashlight;
        shotgunObj.SetActive(false);
        shotgunObj2.SetActive(false);
        flashLightObj.SetActive(true);
        shotgunImage.gameObject.SetActive(false);
        moveShotgunOutOfPosition();
        flashLightImage.enabled = true;

    }

    public void attemptFire ()
    {
        if(curWeapon == Weapon.flashlight)
        {
            flashlightOn = true;
            MusicPlayer.Instance.playClick();
            flashLightFlasher.SetActive(true);

        }

        if(curWeapon == Weapon.shotgun)
        {
            if (ammo == 0) return;

            Hashtable ht = iTween.Hash("from",2, "to", 0, "time", .25f, "onupdate", "changeShotgunFlash");
            iTween.ValueTo(gameObject, ht);

            Ray mseRay = new Ray(Camera.main.gameObject.transform.position,Camera.main.gameObject.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(mseRay, out hit))
            {
                if(hit.transform.gameObject.tag == "Vampire")
                {
                    hit.transform.gameObject.GetComponent<_Vampire>().hit();
                }
            }
            MusicPlayer.Instance.playShotgun();
            ammo--;
            ammoIcons[ammo].enabled = false;
            if(ammo == 0)
            {
                noAmmoIndicator.enabled = true;
            }
        }
    }

    [ContextMenu("add 5 ammo")]
    private void testAmmo ()
    {
        addAmmo(5);
    }

    public void addAmmo (int number)
    {
        noAmmoIndicator.enabled = false;
        for (int i =0; i < number;i++)
        {
            if (ammo == 5) return;
            ammo++;
            ammoIcons[ammo - 1].enabled = true;
        }
    }

    public void unFire ()
    {
        //MusicPlayer.Instance.playClick();
        turnOffFlashLight();
    }

    private void turnOffFlashLight ()
    {
        flashlightOn = false;
        flashLightFlasher.SetActive(false);
    }

    void changeShotgunFlash(float newValue)
    {
        shotgunFlasher.intensity = newValue;
        Color colour = shotgunBangThing.color;
        colour.a = newValue / 2;
        shotgunBangThing.color = colour;
    }

    void changeShotgunPosition (float newVal)
    {
        Vector3 oldPos = shotgunMovable.transform.localPosition;
        float yVal = Mathf.Lerp(-1000,-500,newVal);
        oldPos.y = yVal;
        shotgunMovable.transform.localPosition = oldPos;
    }

    private void moveShotgunIntoPosition ()
    {
        Hashtable ht = iTween.Hash("from", 0, "to", 1, "time", .5f, "onupdate", "changeShotgunPosition");
        iTween.ValueTo(gameObject, ht);
    }

    private void moveShotgunOutOfPosition()
    {
        Hashtable ht = iTween.Hash("from", 1, "to", 0, "time", .5f, "onupdate", "changeShotgunPosition");
        iTween.ValueTo(gameObject, ht);
    }
}
