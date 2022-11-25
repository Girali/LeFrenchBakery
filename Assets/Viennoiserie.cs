using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeViennoiserie
{
    PainAuChocolat,
    Religieuse,
    Eclair,
    Croissant
}
[CreateAssetMenu(fileName = "nouvelle viennoiserie", menuName = "Article/viennoiserie")]

public class Viennoiserie : Article
{
    public TypeViennoiserie genreviennoiserie;

}