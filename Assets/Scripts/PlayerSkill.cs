using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField]
    PlayerUI playerUI;
    Skill currentSkill = Skill.None;

    public enum Skill
    {
        C = 0,
        Q = 1,
        E = 2,
        X = 3,
        None
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Input Q");
            currentSkill = playerUI.UseSkill(Skill.Q);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Input E");
            currentSkill = playerUI.UseSkill(Skill.E);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Input C");
            currentSkill = playerUI.UseSkill(Skill.C);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Input X");
            currentSkill = playerUI.UseSkill(Skill.X);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Input MouseButton");
        }
    }
}
