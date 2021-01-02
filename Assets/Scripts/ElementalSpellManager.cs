using System.Collections.Generic;
using UnityEngine;

public class ElementalSpellManager : MonoBehaviour {

    [SerializeField] GameObject[] spellsToEquip;
    List<GameObject> equippableSpells = new List<GameObject>();
    GameObject equippedSpell;
    CastBehavior castScript;
    int equippedSlot;

    void Start() {
        foreach (GameObject spellEmitter in spellsToEquip) {
            GameObject newSpell = GameObject.Instantiate(spellEmitter, transform.position, transform.rotation);
            newSpell.SetActive(false);
            newSpell.transform.parent = transform;
            equippableSpells.Add(newSpell);
        }
    }

    public void Fire() {
        if (castScript != null) {
            castScript.Cast();
        }
    }

    void Unequip(GameObject spell) {
        if (spell != null) {
            spell.SetActive(false);
        }
    }

    public void Equip(int slot) {
        if (equippedSlot != slot) {
            Unequip(equippedSpell);
            equippedSlot = slot;
            int equipSlotZeroed = equippedSlot - 1;
            if (equippableSpells[equipSlotZeroed] != null) {
                equippedSpell = equippableSpells[equipSlotZeroed];
                equippedSpell.SetActive(true);
                castScript = equippedSpell.GetComponent<CastBehavior>();
            }
        }
    }
}