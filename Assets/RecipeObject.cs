using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeObject : Interactable
{
    [SerializeField]
    private MeshRenderer render;
    private Gradient colorOverReciepe;
    private Recipe recipe;
    private int currentStep;
    private int stepCount;

    public void Init(Recipe r)
    {
        recipe = r;
        currentStep = 0;
        stepCount = recipe.steps.Length - 1;
    }

    public void UpdateStep(Machine m)
    {
        if (m.GetMachineType == recipe.steps[currentStep].machineToUse)
        {
            currentStep++;
            UpdateView();
        }
    }

    public void UpdateStep(Ingredient i)
    {
        if (i.ingredient == recipe.steps[currentStep].ingredientToUse)
        {
            currentStep++;
            UpdateView();
        }
    }

    public void UpdateView()
    {
        render.material.color = colorOverReciepe.Evaluate(currentStep / (float)stepCount);
    }
}
