using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The purpose of this script is to 
 * hold enumerators for various items, such as
 * tools, seeds, crops, etc.
 * 
 * Generally, crops and seeds share the same enum to maintain
 * parity in the association of seeds and crops to a certain type
 */

public enum PlantSpecies 
{ 
    CARROT,
    CUCUMBER,
    BLUEBERRY,
    WHEAT,
    WATERMELON,
    APRICOT,
    CORN,
    PLUM,
    SQUASH,
    MANGO,
    BANANA,
    DRAGONFRUIT,
    APPLE,
    STRAWBERRY,
    POTATO,
    ORANGE,
    PUMPKIN,
    CAULIFLOWER,
    CABBAGE,
    SUNFLOWER,
    FOXTAILCARROT,
    WATERBERRY,
    PEPPERSPROUT,
    LONGTAILBEANS,
    JELLYPAD,
    PURZUL,
    MOONFRUIT,
    BLOSSOMWEED,
    POMPOMFRUIT,
    POCHIGO,
    POLKAPUM,
    STARSEEDS,
    COCOAFLOWER,
    AGUADULCE,
    SNEGGPLANT 
};

public enum PlantType
{
    VEGETABLE,
    FRUIT,
    GRAIN,
    BERRY,
    FLOWER,
};