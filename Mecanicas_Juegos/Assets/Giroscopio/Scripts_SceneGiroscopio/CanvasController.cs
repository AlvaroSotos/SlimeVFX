using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public GameObject directionalLight;
    public Image background;
    public Sprite[] backgroundImages;  
    [SerializeField]
    public LightConfig[] lightConfig;
    // Start is called before the first frame update
    void Start()
    {
        int randomNumber = UnityEngine.Random.Range(0, backgroundImages.Length);
        background.sprite = backgroundImages[randomNumber];

        directionalLight.transform.Rotate(lightConfig[randomNumber].direction);
        directionalLight.GetComponent<Light>().color = lightConfig[randomNumber].color;
        directionalLight.GetComponent<Light>().intensity = lightConfig[randomNumber].intensity;
    }


    [Serializable]
    public class LightConfig
    {
        public string name;
        public Color color;
        public Vector3 direction;
        public float intensity;

    }
}



