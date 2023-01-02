using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class GetRandomSpritLib : MonoBehaviour
{
    [SerializeField]
    private SpriteLibrary spriteLibrary;
    [SerializeField]
    private SpriteLibraryAsset[] spriteLibraryAssets;

    private void Awake()
    {
        spriteLibrary.spriteLibraryAsset= spriteLibraryAssets[Random.Range(0, spriteLibraryAssets.Length)];
    }
}
