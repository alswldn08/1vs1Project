using UnityEngine;
using UnityEngine.UI;


public class PlayerEnergySlider : MonoBehaviour
{
    private Player player;

    public Slider energy;
    public Text countEnergy;
    //public Image fillColor; //슬라이더 색상 이미지
    //private Color targetColor;


    void Start()
    {
        player = GetComponent<Player>();
        energy.maxValue = 100f;
        energy.value = 100f;
        energy.interactable = false;
    }


    private void Update()
    {
        if (energy.value < energy.maxValue)
        {
            player.playerEnergy += 10f * Time.deltaTime;
        }
        else if (player.playerEnergy > energy.maxValue)
        {
            player.playerEnergy = energy.maxValue;
        }

        energy.value = player.playerEnergy;
        countEnergy.text = energy.value.ToString("0") + "/" + energy.maxValue.ToString("0");

        //if (energy.value < 20f)
        //{
        //    targetColor = Color.red;
        //}
        //else
        //{
        //    targetColor = new Color(1f, 0.6f, 0.2f);
        //}
        //fillColor.color = Color.Lerp(fillColor.color, targetColor, Time.deltaTime * 5f);
    }
}
