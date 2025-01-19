using ZTP_projekt.Data.Enum;
using ZTP_projekt.Model;
using IObserver = ZTP_projekt.Interface.IObserver;

internal class ShoppingListObserver : IObserver
{
    private readonly MealPlan _mealPlan;
    public Dictionary<CategoryIngredientEnum, List<Ingredient>> Ingredients { get; private set; } = new();
    public List<string> ShoppingList { get; private set; } = new();

    public ShoppingListObserver(MealPlan mealPlan)
    {
        _mealPlan = mealPlan;
        _mealPlan.Attach(this);
    }

    public void Update()
    {
        GenerateShoppingList();
    }

    public void ClearShoppingList()
    {
        ShoppingList.Clear();
    }

    private void GenerateShoppingList()
    {
        Ingredients.Clear();
        var groupedIngredients = _mealPlan.MealDays
            .SelectMany(mealDay => mealDay.Meals)
            .SelectMany(meal => meal.Recipes)
            .SelectMany(recipe => recipe.Ingredients)
            .GroupBy(ingredient => ingredient.CategoryEnum);

        foreach (var category in groupedIngredients)
        {
            Ingredients[category.Key] = category.ToList();
        }

        ShoppingList = Ingredients
            .SelectMany(kvp => kvp.Value)
            .GroupBy(ingredient => ingredient.Name)
            .Select(group => $"{group.Key} ({group.Sum(ingredient => ingredient.Quantity)}g)")
            .ToList();
    }

    public void DisplayShoppingList()
    {
        Console.WriteLine("\nShopping List:");
        var shoppingList = new Dictionary<string, int>();

        foreach (var mealDay in _mealPlan.MealDays)
        {
            foreach (var meal in mealDay.Meals)
            {
                foreach (var recipe in meal.Recipes)
                {
                    foreach (var ingredient in recipe.Ingredients)
                    {
                        if (shoppingList.ContainsKey(ingredient.Name))
                        {
                            shoppingList[ingredient.Name] += ingredient.Quantity;
                        }
                        else
                        {
                            shoppingList[ingredient.Name] = ingredient.Quantity;
                        }
                    }
                }
            }
        }

        if (shoppingList.Count == 0)
        {
            Console.WriteLine("No ingredients in the shopping list.");
        }
        else
        {
            foreach (var item in shoppingList)
            {
                Console.WriteLine($"- {item.Key} ({item.Value}g)");
            }
        }
    }
}
