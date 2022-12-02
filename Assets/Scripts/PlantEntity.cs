using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEntity : MonoBehaviour
{
    [SerializeField]
    private DayNightManager dayTracker;

    //public SpriteRenderer spriteRenderer;

    public int age, growthStage;
    //public int[] growthStageTimes;
    //public Sprite[] growthStageSprites;

    private void Start()
    {
        age = growthStage = 0;
        //dayPlanted = dayTracker.days;
    }

    private void Update()
    {
        /*age = dayTracker.days - dayPlanted;
        for(int i = growthStage; i < growthStageTimes.Length; i++)
        {
            if (age >= growthStageTimes[i]) growthStage = i;
        }

        spriteRenderer.sprite = growthStageSprites[growthStage];*/
    }

    public IEnumerator StartGrowing(int dayPlanted, int[] growthStageTimes, Sprite[] growthStageSprites, SpriteRenderer spriteRenderer)
    {
        while (growthStage < growthStageSprites.Length - 1)
        {
            age = dayTracker.days - dayPlanted;
            for (int i = growthStage; i < growthStageTimes.Length; i++)
            {
                if (age >= growthStageTimes[i]) growthStage = i;
            }

            spriteRenderer.sprite = growthStageSprites[growthStage];
            yield return null;
        }
    }
}
