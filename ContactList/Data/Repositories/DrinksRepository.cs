using System.Collections.Generic;
using System.Linq;
using Dapper;
using DrinkAPI.Controllers;
using DrinkAPI.Infrastructure;
using DrinkAPI.Models;
using Slapper;

namespace DrinkAPI.Data.Repositories
{
    public class DrinksRepository
    {
        public IList<Drink> ListDrinks()
        {
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                var query = connection.Query<dynamic>(@"
                    SELECT
	                    d.id as Id, 
                        d.name as Name, 
                        d.glass as Glass,
                        d.instructions as Instructions,
                        d.Modified as ModifiedUtc,
                        di.size as Ingredients_Size,
                        di.unit as Ingredients_Unit,
                        i.name as Ingredients_Name,
                        i.id as Ingredients_Id
                    FROM drink d
                    inner join drink_ingredient di on d.id = di.drink_id
                    inner join ingredient i on i.id = di.ingredient_id
                    ");

                AutoMapper.Configuration.AddIdentifiers(typeof (Drink), new List<string> {"Id"});

                return AutoMapper.MapDynamic<Drink>(query).ToList();
            }
        }

        public bool Add(AddDrinkModel drink)
        {
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                var drinkExists = connection.Query<int?>($@"
                    SELECT
                        id
                    FROM
                        drink
                    WHERE
                        name = @Name
                ", new {Name = drink.Name.Trim().ToLower() }).FirstOrDefault();

                if (drinkExists != null)
                {
                    return false;
                }

                var insertedDrinkId = connection.Query<int>($@"
                    INSERT INTO [Drink] (Name, DisplayName, Glass, Instructions, Modified, Created) VALUES (@name, @displayName, @glass, @instructions, GETUTCDATE(), GETUTCDATE());
                    SELECT CAST(SCOPE_IDENTITY() as int)", new {name = drink.Name.Trim().ToLower(), displayName = drink.Name.Trim(), glass = drink.Glass.ToLower().Trim(), instructions = drink.Instructions}).Single();

                foreach (var ingredient in drink.Ingredients)
                {
                    var existingIngredientId = connection.Query<int?>($"SELECT id FROM ingredient WHERE name = @Name",
                        new {ingredient.Name}).FirstOrDefault();

                    int ingredientId;

                    if (existingIngredientId != null)
                    {
                        ingredientId = existingIngredientId.Value;
                    }
                    else
                    {
                        ingredientId = connection
                            .Query<int>(
                                "INSERT INTO Ingredient (Name, DisplayName) VALUES(@Name, @DisplayName);SELECT CAST(SCOPE_IDENTITY() as int)",
                                new {Name = ingredient.Name.Trim().ToLower(), DisplayName = ingredient.Name.Trim()})
                            .Single();
                    }

                    connection.Query(
                        "INSERT INTO drink_ingredient(drink_id, ingredient_id, size, unit) VALUES(@DrinkId, @IngredientId, @Size, @Unit)",
                        new {DrinkId = insertedDrinkId, IngredientId = ingredientId, ingredient.Size, ingredient.Unit});
                }
            }
            return true;
        }
    }
}