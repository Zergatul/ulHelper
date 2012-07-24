using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = D0
    /// </summary>
    public class MultiSellList : ServerPacket
    {
        public int ListID { get; set; }
        public int Page { get; set; }
        public int Finished { get; set; }
        public int PageSize { get; set; }
        public List<Entry> Entries { get; set; }

        public MultiSellList(ServerPacket pck)
            : base(pck)
        {
            ListID = ReadInt();
            Page = ReadInt();
            Finished = ReadInt();
            PageSize = ReadInt();
            int entryCount = ReadInt();
            Position++;
            Entries = new List<Entry>(entryCount);
            for (int i = 0; i < entryCount; i++)
                Entries.Add(ReadEntry());
        }

        Entry ReadEntry()
        {
            var entry = new Entry();
            entry.EntryID = ReadInt();
            entry.IsStackable = ReadByte();
            Position += 10;
            Position += 2; //h -2
            Position += 14;
            var productCount = ReadShort();
            var ingredientCount = ReadShort();
            entry.Products = new List<Product>(productCount);
            entry.Ingredients = new List<Ingredient>(ingredientCount);
            for (int i = 0; i < productCount; i++)
                entry.Products.Add(ReadProduct());
            for (int i = 0; i < ingredientCount; i++)
                entry.Ingredients.Add(ReadIngredient());            
            return entry;
        }

        Product ReadProduct()
        {
            var product = new Product();
            product.ItemID = ReadInt();
            product.BodyPart = ReadInt();
            product.Type2 = ReadShort();
            product.Count = ReadLong();
            Position += 2;
            Position += 4;
            Position += 4;
            Position += 4;
            Position += 2; // h -2
            Position += 14;
            return product;
        }

        Ingredient ReadIngredient()
        {
            var ingredient = new Ingredient();
            ingredient.ItemID = ReadInt();
            ingredient.Type2 = ReadShort();
            ingredient.Count = ReadLong();
            Position += 10;
            Position += 2; // h -2
            Position += 14;
            return ingredient;
        }

        public class Entry
        {
            public int EntryID { get; set; }
            public byte IsStackable { get; set; }            
            public List<Ingredient> Ingredients { get; set; }
            public List<Product> Products { get; set; }
        }

        public class Ingredient
        {
            public int ItemID { get; set; }
            public short Type2 { get; set; }
            public long Count { get; set; }
        }

        public class Product
        {
            public int ItemID { get; set; }
            public int BodyPart { get; set; }
            public short Type2 { get; set; }
            public long Count { get; set; }
        }
    }
}