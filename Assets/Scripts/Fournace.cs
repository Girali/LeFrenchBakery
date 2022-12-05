using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class Fournace : Machine
{
    [SerializeField]
    private float cookingTime;
    [SerializeField]
    private float overCookingTime;

    [SerializeField]
    private ParticleSystem smoke;

    private bool started = false;
    private bool cooked = false;
    private float timerStart;
    private float timerEnd1;
    private float timerEnd2;
    private float timerDuration1;
    private float timerDuration2;

    private float t1;
    private float t2;

    private bool retrivedThisFrame = false;

    DUI_Timer timer;

    public override bool CanInteract(PlayerInteractionController pic, PlayerObjectController poc)
    {
        if (poc.InteractableObject == null)
        {
            if (cooked == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            RecipeObject recipeObject = poc.InteractableObject.GetComponent<RecipeObject>();
            if (recipeObject != null)
            {
                if (started || cooked)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }

    private void Update()
    {
        if (started)
        {
            t1 = (Time.time - timerStart) / (timerEnd1 - timerStart);
            t2 = (Time.time - timerEnd1) / (timerEnd2 - timerEnd1);

            if(t1 > 1 && !cooked)
            {
                cooked = true;
                interactable = true;
            }

            if (t2 > 1)
            {
                ((RecipeObject)interactableObject).Fail();
                started = false;
                smoke.Play(true);
                Destroy(timer.gameObject);
            }
        }
    }

    public override void OnEnter(InteractableObject r, GameObject p)
    {
        if (!retrivedThisFrame)
        {
            base.OnEnter(r, p);

            if (!started && !cooked)
            {
                interactable = false;
                started = true;
                cooked = false;
                timerStart = Time.time;
                timerDuration1 = cookingTime;
                timerDuration2 = overCookingTime;
                timerEnd1 = timerStart + timerDuration1;
                timerEnd2 = timerStart + timerDuration1 + timerDuration2;

                user.GetComponent<PlayerObjectController>().PauseInteractableObject();
                timer = DGUI_Controller.Insatance.StartTimer(transform, cookingTime, overCookingTime);
            }
        }
        else
        {
            retrivedThisFrame = false;
        }
    }

    public override InteractableObject OnExit()
    {
        started = false;
        cooked = false;
        smoke.Stop(true);
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

        if(cooked)
        {
            user.GetComponent<PlayerObjectController>().UnPauseInteractableObject(interactableObject);
            ((RecipeObject)interactableObject).UpdateStep(this);
            OnExit();
            retrivedThisFrame = true;
        }

        return this;
    }
}
