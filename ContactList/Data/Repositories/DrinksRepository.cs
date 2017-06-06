using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactList.Controllers;
using ContactList.Infrastructure;
using ContactList.Models;
using Dapper;
using Slapper;

namespace ContactList.Data.Repositories
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

        public Drink Add(AddDrinkModel drink)
        {
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                var drinkExists = connection.Query<int?>($@"
                    SELECT
                        id
                    FROM
                        drink
                    WHERE
                        name = {drink.Name}
                ").FirstOrDefault();

                if (drinkExists != null)
                {
                    return null;
                }

                var sql = @"
                    INSERT INTO [Drink] () VALUES (@Stuff);
                    SELECT CAST(SCOPE_IDENTITY() as int)";


            }

            return null;
        }
    }
}