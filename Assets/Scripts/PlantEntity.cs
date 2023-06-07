using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEntity : MonoBehaviour
{
    [SerializeField]
    private DayNightManager dayTracker;

    //public SpriteRenderer spriteRenderer;

    public SeedData seed;
    public int age, growthStage;
    //public int[] growthStageTimes;
    //public Sprite[] growthStageSprites;

    public void Initialize(SeedData inputSeed)
    {
        seed = inputSeed;
        age = growthStage = 0;
    }

    /*public IEnumerator StartGrowing(int dayPlanted, int[] growthStageTimes, Sprite[] growthStageSprites, SpriteRenderer spriteRenderer)
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
    */

    public void Grow(SpriteRenderer spriteRenderer)
    {
        age++;
        for (int i = growthStage; i < seed.maxGrowthStage; i++)
        {
            if (age >= seed.growthStageTimes[i]) growthStage = i;
        }

        spriteRenderer.sprite = seed.growthStageSprites[growthStage];
    }
}
