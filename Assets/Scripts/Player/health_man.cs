using System;
using Extensions;
using R3;
using UnityEngine;

public class health_man : MonoBehaviour
{
    public HealthScript player;
    public GameObject[] healthIcons;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player.Health.Subscribe(Awesome).AddTo(this);
    }

    //heh this is prettys wag ;3
    void Awesome(int currenthealth)
    {
        for (int i = 0; i < healthIcons.Length; i++) 
        {
            healthIcons[i].SetActive(currenthealth>=i+1);
        }
    }
}
