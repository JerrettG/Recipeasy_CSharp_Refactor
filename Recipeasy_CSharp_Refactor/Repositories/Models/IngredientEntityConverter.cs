using System;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace Recipeasy_CSharp_Refactor.Repositories.Models
{
	public class IngredientEntityConverter : IPropertyConverter
	{
        public DynamoDBEntry ToEntry(object value)
        {
            if (value is IngredientEntity entity)
            {
                return new List<Primitive>
            {
                new Primitive(entity.Id.ToString()),
                new Primitive(entity.Name),
                new Primitive(entity.Quantity.ToString()),
                new Primitive(entity.UnitMeasurement),
                new Primitive(entity.ImageFileName)
            };
            }

            throw new InvalidOperationException("Invalid type for conversion.");
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            if (entry is PrimitiveList primitiveList && primitiveList.Entries.Count == 2)
            {
                int id = primitiveList.Entries[0].AsInt();
                String name = primitiveList.Entries[1].AsString();
                double quantity = primitiveList.Entries[2].AsDouble();
                String unitMeasurement = primitiveList.Entries[3].AsString();
                String imageFileName = primitiveList.Entries[4].AsString();

                return new IngredientEntity(id, name, quantity, unitMeasurement, imageFileName);
            }

            throw new InvalidOperationException("Invalid entry for conversion.");
        }
    }
}

