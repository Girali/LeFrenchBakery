using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RecipeObject : InteractableObject
{
    [SerializeField]
    private MeshRenderer render;
    [SerializeField]
    private Gradient colorOverReciepe;


    private Recipe recipe;
    private int currentStep;
    private int stepCount;
    private bool failed = false;
    [SerializeField]
    private GameObject articlePrefab;

    public Recipe Recipe { get => recipe; }
    public int CurrentStep { get => currentStep; }
    public bool Failed { get => failed; }

    public void Fail()
    {
        failed = true;
    }

    public override void Init(Object r)
    {
        base.Init(r);
        recipe = (Recipe)r;
        currentStep = 0;
        stepCount = recipe.steps.Length - 1;

        SuccessCheck();
        UpdateView();
    }

    public void SuccessCheck()
    {
        if (!failed)
        {
            if (currentStep == stepCount + 1)
            {
                InteractableObject g = Instantiate(articlePrefab).GetComponent<InteractableObject>();
                g.Init(recipe.result);
                GameObject h = holder.GetComponent<PlayerObjectController>().DestroyInteractableObject();
                h.GetComponent<PlayerObjectController>().AddInteractableObject(g);
            }
        }
    }

    public void UpdateStep(Machine m)
    {
        if (m.GetMachineType == recipe.steps[currentStep].machineToUse)
        {
            currentStep++;
        }
        else
        {
            failed = true;
        }

        SuccessCheck();
        UpdateView();
    }

    public void UpdateStep(Ingredient i)
    {
        if (i.ingredient == recipe.steps[currentStep].ingredientToUse)
        {
            currentStep++;
        }
        else
        {
            failed = true;
        }

        SuccessCheck();
        UpdateView();
    }

    public override void Hold(GameObject g)
    {
        base.Hold(g);
        UpdateView();
    }

    public void UpdateView()
    {
        if (failed)
            render.material.color = Color.black;
        else
            render.material.color = colorOverReciepe.Evaluate(currentStep / (float)stepCount);

        GUI_Controller.Insatance.recipeFollower.UpdateView(this);
    }
}
