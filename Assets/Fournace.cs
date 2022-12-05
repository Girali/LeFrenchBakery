using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Fournace : Machine
{
    [SerializeField]
    private float cookingTime;
    [SerializeField]
    private float overCookingTime;

    private bool started = false;
    private bool success = false;
    private float timerStart;
    private float timerEnd1;
    private float timerEnd2;
    private float timerDuration1;
    private float timerDuration2;

    private float t1;
    private float t2;

    DUI_Timer timer;

    private void Update()
    {
        if (started)
        {
            t1 = (Time.time - timerStart) / (timerEnd1 - timerStart);
            t2 = (Time.time - timerEnd1) / (timerEnd2 - timerEnd1);

            if(t1 > 1 && !success)
            {
                success = true;
                interactable = true;
            }

            if (t2 > 1)
            {
                recipeObject.Fail();
                started = false;
            }
        }
    }

    public override void OnEnter(RecipeObject r, GameObject p)
    {
        base.OnEnter(r, p);

        if (!started && !success)
        {
            interactable = false;
            started = true;
            success = false;
            timerStart = Time.time;
            timerDuration1 = cookingTime;
            timerDuration2 = overCookingTime;
            timerEnd1 = timerStart + timerDuration1;
            timerEnd2 = timerStart + timerDuration1 + timerDuration2;

            user.GetComponent<PlayerObjectController>().PauseInteractableObject();
            timer = DGUI_Controller.Insatance.StartTimer(transform, cookingTime, overCookingTime);
        }
    }

    public override RecipeObject OnExit()
    {
        started = false;
        success = false;
        Destroy(timer.gameObject);
        return base.OnExit();
    }

    public override void Interact(bool leftClick, bool leftClickDown, bool rightClickDown)
    {
        base.Interact(leftClick, leftClickDown, rightClickDown);
    }

    public override Interactable InteractFirst(bool leftClick, bool leftClickDown, bool rightClickDown)
    {
        base.InteractFirst(leftClick, leftClickDown, rightClickDown);

        if(success)
        {
            user.GetComponent<PlayerObjectController>().UnPauseInteractableObject(recipeObject);
            recipeObject.UpdateStep(this);
            OnExit();
        }

        return this;
    }
}
