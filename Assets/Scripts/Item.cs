using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType 
{
    Empty,
    Ball,
    Barrel,
    Stone,
    Box,
    Dynamite,
    Star,
    SilverStar
}
public class Item : MonoBehaviour
{
    public ItemType ItemType;
}
