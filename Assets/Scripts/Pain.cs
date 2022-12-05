using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TypePain
{
    Baguette,
    PainLevain,
    PainCereales,
    PainOlives,
    PainNoix,
    PainMais
}
[CreateAssetMenu(fileName = "nouveau pain", menuName = "Article/pain")]
public class Pain : Article
{
    public TypePain genrepain;
    

}
