using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCollector : MonoBehaviour
{
    private PlayerSystem playerSystem;

    private void Start()
    {
        FindSystem();
    }

    private void FindSystem()
    {
        playerSystem = FindObjectOfType<PlayerSystem>();
    }

    
}
