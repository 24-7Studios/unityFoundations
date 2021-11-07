using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    private PlayerScript player;

    [SerializeField]
    private Slider slider;

    private void Start()
    {
        player = GetComponentInParent<PlayerScript>();
    }

    private void Update()
    {
        slider.value = player.getHealth();
    }
}
