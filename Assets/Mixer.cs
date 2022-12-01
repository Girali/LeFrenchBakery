using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixer : Machine
{
    public UI_Mixer mixer;

    [SerializeField]
    private float miniGameProgress = 0;
    [SerializeField]
    private float progressAmount = 0;
    [SerializeField]
    private float reprogressAmount = 0;
    [SerializeField]
    private float miniGameGoal = 0;

    private bool isLeft = false;


    public override void Interact(bool leftClick, bool leftClickDown, bool rightClickDown)
    {
        base.Interact(leftClick, leftClickDown, rightClickDown);

        if (isLeft && leftClickDown)
        {
            miniGameProgress += Time.deltaTime * progressAmount;
            isLeft = false;
        }
        else if(!isLeft && rightClickDown)
        {
            miniGameProgress += Time.deltaTime * progressAmount;
            isLeft = true;
        }

        miniGameProgress -= Time.deltaTime * reprogressAmount;

        miniGameProgress = Mathf.Clamp(miniGameProgress, 0, miniGameGoal);
        if(miniGameProgress == miniGameGoal)
        {
            //finish
            mixer.Show(false);
            inUse = false;
        }

        mixer.UpdateView(miniGameProgress / miniGameGoal, isLeft);
    }

    public override Interactable InteractFirst(bool leftClick, bool leftClickDown, bool rightClickDown)
    {
        base.InteractFirst(leftClick, leftClickDown, rightClickDown);
        mixer.Show(true);
        inUse = true;
        miniGameProgress = 0;
        return (Interactable)this;
    }
}
