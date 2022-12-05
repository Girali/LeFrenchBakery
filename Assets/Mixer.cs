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
    private bool success = false;

    private void OnEnable()
    {
        mixer.finished += Finished;
    }

    private void OnDisable()
    {
        mixer.finished -= Finished;
    }

    public override void Interact(bool leftClick, bool leftClickDown, bool rightClickDown)
    {
        base.Interact(leftClick, leftClickDown, rightClickDown);

        if (!success)
        {
            if (isLeft && leftClickDown)
            {
                miniGameProgress += (Time.deltaTime * progressAmount);
                isLeft = false;
            }
            else if (!isLeft && rightClickDown)
            {
                miniGameProgress += (Time.deltaTime * progressAmount);
                isLeft = true;
            }

            miniGameProgress -= (Time.deltaTime * reprogressAmount);

            miniGameProgress = Mathf.Clamp(miniGameProgress, 0, miniGameGoal);
            if (miniGameProgress == miniGameGoal)
            {
                mixer.Success();
                success = true;
            }
        }
        mixer.UpdateView(miniGameProgress / miniGameGoal, isLeft);
    }

    public override RecipeObject OnExit()
    {
        user.StartStopMove(true, this);
        return base.OnExit();
    }

    public override void OnEnter(RecipeObject r, GameObject p)
    {
        base.OnEnter(r, p);
        user.StartStopMove(false, this);
    }

    public void Finished()
    {
        mixer.Show(false);
        recipeObject.UpdateStep(this);
        OnExit();
    }

    public override Interactable InteractFirst(bool leftClick, bool leftClickDown, bool rightClickDown)
    {
        base.InteractFirst(leftClick, leftClickDown, rightClickDown);
        mixer.Show(true);
        miniGameProgress = 0;
        success = false;
        return (Interactable)this;
    }
}
