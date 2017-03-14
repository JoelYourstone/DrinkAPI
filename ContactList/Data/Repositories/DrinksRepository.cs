using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
	                    d.id as DrinkId, 
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

                AutoMapper.Configuration.AddIdentifiers(typeof (Drink), new List<string> {"DrinkId"});

                return AutoMapper.MapDynamic<Drink>(query).ToList();
            }
        } 
    }
}