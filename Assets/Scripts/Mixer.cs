using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixer : Machine
{
    private DUI_Mixer mixer;

    [SerializeField]
    private float miniGameProgress = 0;
    [SerializeField]
    private float progressAmount = 0;
    [SerializeField]
    private float reprogressAmount = 0;
    [SerializeField]
    private float miniGameGoal = 0;

    private bool success = false;

    public override bool CanInteract(PlayerInteractionController pic, PlayerObjectController poc)
    {
        if (inUse)
            return false;

        if (poc.InteractableObject == null)
        {
            return false;
        }
        else
        {
            RecipeObject recipeObject = poc.InteractableObject.GetComponent<RecipeObject>();
            if (recipeObject != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public override void Interact(bool leftClick, bool leftClickDown, bool rightClickDown, bool escape, bool left, bool right)
    {
        base.Interact(leftClick, leftClickDown, rightClickDown, escape, left, right);

        if (rightClickDown)
            Quit();


        if (!success)
        {
            if (leftClickDown)
            {
                miniGameProgress += (Time.deltaTime * progressAmount);
                SoundController.Instance.Mix();
                mixer.Click();
            }

            miniGameProgress -= (Time.deltaTime * reprogressAmount);

            miniGameProgress = Mathf.Clamp(miniGameProgress, 0, miniGameGoal);
            if (miniGameProgress == miniGameGoal)
            {
                mixer.Success();
                success = true;
            }
        }
        mixer.UpdateView(miniGameProgress / miniGameGoal);
    }

    public void Quit()
    {
        mixer.finished -= Finished;
        Destroy(mixer.gameObject);
        OnExit();
    }

    public override InteractableObject OnExit()
    {
        user.StartStopMove(true, this);
        inUse = false;
        return base.OnExit();
    }

    public override void OnEnter(InteractableObject r, GameObject p)
    {
        base.OnEnter(r, p);
        user.StartStopMove(false, this);
        inUse = true;
    }

    public void Finished()
    {
        mixer.finished -= Finished;
        Destroy(mixer.gameObject);
        ((RecipeObject)interactableObject).UpdateStep(this);
        OnExit();
    }

    public override Interactable InteractFirst(bool leftClick, bool leftClickDown, bool rightClickDown)
    {
        base.InteractFirst(leftClick, leftClickDown, rightClickDown);
        mixer = DGUI_Controller.Insatance.StartMixer(transform);
        mixer.finished += Finished;
        miniGameProgress = 0;
        success = false;
        return (Interactable)this;
    }
}
