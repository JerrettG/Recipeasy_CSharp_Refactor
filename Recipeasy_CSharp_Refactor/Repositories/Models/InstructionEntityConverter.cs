using System;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace Recipeasy_CSharp_Refactor.Repositories.Models
{
    public class InstructionEntityConverter : IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value)
        {
            if (value is InstructionEntity entity)
            {
                return new List<Primitive>
            {
                new Primitive(entity.Number.ToString()),
                new Primitive(entity.Step)
            };
            }

            throw new InvalidOperationException("Invalid type for conversion.");
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            if (entry is PrimitiveList primitiveList && primitiveList.Entries.Count == 2)
            {
                int number = primitiveList.Entries[0].AsInt();
                String step = primitiveList.Entries[1].AsString();

                return new InstructionEntity(number, step);
            }

            throw new InvalidOperationException("Invalid entry for conversion.");
        }
    }
}

