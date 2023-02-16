using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookBook : Machine
{
    [SerializeField]
    private RecipeController recipeController;

    [SerializeField]
    private DUI_Recipe reciepe;
    private int currentRecipe = 0;

    [SerializeField]
    private Article[] articles;
    private bool firstFrame = false;

    public override bool CanInteract(PlayerInteractionController pic, PlayerObjectController poc)
    {
        if (inUse)
            return false;
        
        if (poc.InteractableObject != null)
        {
            return false;
        }

        return true;
    }

    public override void OnEnter(InteractableObject r, GameObject p)
    {
        base.OnEnter(r,p);
        user.StartStopMove(false, this);
        reciepe.gameObject.SetActive(true);
        reciepe.UpdateView(articles[currentRecipe]);
        inUse = true;
    }


    public override Interactable InteractFirst(bool leftClick, bool leftClickDown, bool rightClickDown)
    {
        firstFrame = true;
        return base.InteractFirst(leftClick, leftClickDown, rightClickDown);
    }

    public override void Interact(bool leftClick, bool leftClickDown, bool rightClickDown, bool escape, bool left, bool right)
    {
        base.Interact(leftClick, leftClickDown, rightClickDown, escape, left, right);
        if (firstFrame)
        {
            firstFrame = false;
        }
        else
        {
            if (rightClickDown)
                Quit();

            if (left)
            {
                reciepe.Prev();
                currentRecipe--;
                currentRecipe = Mathf.Clamp(currentRecipe,0 , articles.Length - 1);
                reciepe.UpdateView(articles[currentRecipe]);
            }

            if (right)
            {
                reciepe.Next();
                currentRecipe++;
                currentRecipe = Mathf.Clamp(currentRecipe,0 , articles.Length - 1);
                reciepe.UpdateView(articles[currentRecipe]);
            }

            if (leftClickDown)
            {
                reciepe.Buy();
            }
        }
    }

    public void Quit()
    {
        SoundController.Instance.CloseUI();
        reciepe.gameObject.SetActive(false);
        OnExit();
    }

    public override InteractableObject OnExit()
    {
        inUse = false;
        user.StartStopMove(true, this);
        return base.OnExit();
    }

    public void StartRecipe(Article i)
    {
        interactableObject = recipeController.StartReciepe(i);
        user.GetComponent<PlayerObjectController>().AddInteractableObject(interactableObject);
        OnExit();
    }
}
