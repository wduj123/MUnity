using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class LinqDemo : MonoBehaviour
{
    private List<Product> m_products;
	void Start ()
    {
		this.m_products = Product.GetProducts ();
	}
	
	void Update ()
    {
	
	}

    void OnGUI()
    {
        if(GUI.Button(new Rect(0,0,100,50), "SortByName"))
        {
            SortByName();
            Print(this.m_products);
        }
        if (GUI.Button(new Rect(0, 50, 100, 50), "SortByPrice"))
        {
            SortByPrice();
            Print(this.m_products);
        }
    }

    private void Print(List<Product> products)
    {
        Action<Product> print = Debug.Log;
        products.ForEach(print);
    }

    private void SortByName()
    {
        this.m_products.Sort(delegate (Product p1, Product p2)
        {
            return p1.Name.CompareTo(p2.Name);
        });
    }

    private void SortByPrice()
    {
        this.m_products.Sort((p1, p2) => p1.Price.CompareTo(p2.Price));
    }

    private List<Product> FindAll1()
    {
        Predicate<Product> condition = delegate (Product p)
        {
            return p.Price > 15m;
        };
        return this.m_products.FindAll(condition);
    }

    private List<Product> FindAll2()
    {
        var filtered = from Product p in m_products
                       where p.Price > 15m
                       select p;
        return filtered.ToList<Product>();
    }

    private List<Product> FindAll3()
    {
        var filtered = this.m_products.Where(p => p.Price > 15m);
        return filtered.ToList<Product>();
    }

	private class Product
	{
		public string Name {
			set;
			get;
		}

		public decimal Price {
			set;
			get;
		}

		public Product(string name,decimal price)
		{
			Name = name;
            Price = price;
		}

		public static List<Product> GetProducts()
		{
			return new List<Product> () {
				new Product ("a", 10m),
                new Product ("b", 19m),
                new Product ("e", 17m),
                new Product ("c", 13m),
                new Product ("d", 12m)
			};
		}

		public override string ToString ()
		{
			return string.Format ("[Product: Name={0}, Price={1}]", Name, Price);
		}
	}
}
