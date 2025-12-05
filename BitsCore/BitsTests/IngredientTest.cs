using NUnit.Framework;
using BitsEFClasses;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BitsEFClasses.Models;

namespace BitsTests
{
    public class IngredientTests
    {
        private BitsContext context;

        [SetUp]
        public void Setup()
        {
            // Load DB configuration
            ConfigDB cfg = new ConfigDB();
            var builder = new DbContextOptionsBuilder<BitsContext>();
            builder.UseMySql(ConfigDB.GetMySqlConnectionString(),
    new MySqlServerVersion(new Version(8, 0, 0)));

            context = new BitsContext(builder.Options);
        }

        // -------------------------------
        // Test 1: Retrieve ALL ingredients
        // -------------------------------
        [Test]
        public void GetAllIngredients_ShouldReturnList()
        {
            var ingredients = context.Ingredients.ToList();

            Assert.IsNotNull(ingredients, "Ingredient list should not be null.");
            Assert.IsTrue(ingredients.Count > 0, "There should be at least one ingredient in the database.");

            // Optional: Print some values to help debug
            TestContext.WriteLine($"Found {ingredients.Count} ingredients.");
            foreach (var i in ingredients.Take(3))
            {
                TestContext.WriteLine($" -> {i.Name}");
            }
        }

        // ---------------------------------------------------
        // Test 2: Retrieve ONE ingredient by primary key
        // ---------------------------------------------------
        [Test]
        public void GetIngredientById_ShouldReturnCorrectIngredient()
        {
            // Get a known existing ID (use the first one in DB)
            var firstIngredient = context.Ingredients.FirstOrDefault();
            Assert.IsNotNull(firstIngredient, "Database should contain at least one ingredient.");

            int id = firstIngredient.IngredientId;

            var ingredient = context.Ingredients
                                    .FirstOrDefault(i => i.IngredientId == id);

            Assert.IsNotNull(ingredient, "Ingredient should be found by ID.");
            Assert.AreEqual(id, ingredient.IngredientId);

            // Optional debug output
            TestContext.WriteLine($"Retrieved ingredient: {ingredient.Name}");
        }
    }
}
