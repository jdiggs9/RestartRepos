using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class HUD : MonoBehaviour
{

    private float[] hp = new float[10];

    public Sprite full;
    public RuntimeAnimatorController fullAni;
    public Sprite half;
    public RuntimeAnimatorController halfAni;
    public Sprite empty;
    public RuntimeAnimatorController emptyAni;
    public Sprite shield;
    public RuntimeAnimatorController shieldAni;

    public GameObject[] slotArray;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        //Sets everything to null
        for (int i = 0; i < 10; i++) {
                hp[i] = 0;
        }
        //Sets beginning 3 hearts to full
        hp[0] = 3;
        hp[1] = 3;
        hp[2] = 3;
        UpdateHP(); 
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Damaged() {
        int temp = FindLastNull() -1;
        
        switch (hp[temp]) {
            case 4:
                hp[temp] = 0; 
                break;
            case 1:
                while (hp[temp] == 1) {
                    temp--;
                }
                hp[temp]--; 
                break;
            default:
                hp[temp]--;
                break;
        }
        UpdateHP();
        if (hp[0] == 1) {
            Death();
        }
    }
    public void HealHalf() {
        int i = 0;
        while (hp[i] == 3) {
            i++;
        }
        hp[i]++;
        UpdateHP();
    }
    public void HealFull() {
        if (!IsFullHP()) {
            int i = 0;
            while (hp[i] == 3) {
                i++;
            }
            hp[i] = 3;
            if (hp[i + 1] == 1) {
                hp[i + 1] = 2;
            }
            UpdateHP();
        }
    }
    public void HealComplete() {
        for (int i = 0; i < 10; i++) {
            if (hp[i] == 1 || hp[i] == 2) {
                hp[i] = 3;
            }
        }
        UpdateHP();
    }
    public void IncreaseHP() {
        if (!IsMaxHP()) {
            int i = FindLastHeart();
            if (hp[i] == 4 && !IsFullSlots()) {
                hp[FindLastNull()] = 4;
            }
            hp[i] = 1;
            HealFull();
        }
        UpdateHP();
    }
    public void AddShield() {
        if (!IsFullSlots()) {
            hp[FindLastNull()] = 4;
        }
        UpdateHP();
    }
    private void UpdateHP() {
        for (int i = 0; i < 10; i++) {
            if (hp[i] == 0)
            {              //Null
                slotArray[i].GetComponent<Renderer>().enabled = false;
                slotArray[i].GetComponent<Animator>().enabled = false;
            } else {
                if (hp[i] == 1) {       //Empty Heart
                    slotArray[i].GetComponent<SpriteRenderer>().sprite = empty;
                    slotArray[i].GetComponent<Animator>().runtimeAnimatorController = emptyAni;
                }
                else if (hp[i] == 2) {       //Half Heart
                    slotArray[i].GetComponent<SpriteRenderer>().sprite = half;
                    slotArray[i].GetComponent<Animator>().runtimeAnimatorController = halfAni;
                }
                else if (hp[i] == 3) {      //Full Heart
                    slotArray[i].GetComponent<SpriteRenderer>().sprite = full;
                    slotArray[i].GetComponent<Animator>().runtimeAnimatorController = fullAni;
                }
                else if (hp[i] == 4) {      //Shield
                    slotArray[i].GetComponent<SpriteRenderer>().sprite = shield;
                    slotArray[i].GetComponent<Animator>().runtimeAnimatorController = shieldAni;
                }
                slotArray[i].GetComponent<Renderer>().enabled = true;
                slotArray[i].GetComponent<Animator>().enabled = true;
            }
        }
    }

    //Finds the first null position in the 2D array
    private int FindLastNull() {
        for (int i = 0; i < 10; i++) {
               if (hp[i] == 0) { 
                    return i;
               }
        }
        return 9;
    }
    //Finds the last non heart position in the 2D array
    private int FindLastHeart() {
        for (int i = 0; i < 10; i++) {
            if (hp[i] == 0 || hp[i] == 4) {
                return i;
            }
        }
        return 9;
    }
    private void Death() {

    }
    public bool IsFullHP() {
        int temp = FindLastNull() - 1;
        
        if (hp[temp] == 4) {
            while (hp[temp] == 4) {
                temp--;
            }
        } if (hp[temp] == 3) {
            return true;
        } else {
            return false;
        }
    }
    public bool IsFullSlots() {
        for (int i = 0;i < 10;i++) {
            if (hp[i] == 0) {
                return false;
            }
        }
        return true;
    }
    public bool IsMaxHP() {
        for (int i = 0; i < 10; i++) {
            if (hp[i] == 0 || hp[i] == 4) {
                return false;
            }
        }
        return true;
    }
}
