using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using discount.Domain.Interface;

namespace discount.Domain
{
    
    public class Cart:ICart
    {
        public string Id{get;}
        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();
        private List<Product> _products;
        public IReadOnlyCollection<ISpecification> Specifications => _specifications.AsReadOnly();
        private List<ISpecification> _specifications;
        public IReadOnlyCollection<Discount> Discounts => _discounts.AsReadOnly();
        private List<Discount> _discounts;
        public decimal BeforeDiscountTotalPrice{get;private set;}
        public decimal TotalPrice{get;private set;}
        public decimal TotalDiscount{get;private set;}

        internal Cart()
        {
            _products=new List<Product>();
            _specifications= new List<ISpecification>();
            _discounts= new List<Discount>();
        }
        internal Cart(string id):this()
        {
            Id=id;
        }

        public static Cart CreateNew()
            => new Cart();

        public static Cart CreateNew(string id)
            => new Cart(id);

        public void AddItem(Product item)
        {
            _products.Add(item);
            // if(_products.Any(x=>x.sku==product.sku))
            // {
            //     var item=_products.Where(x=>x.sku==product.sku).FirstOrDefault();
            //     item.number=item.number+1;
            // }
            // else
            // {
            //     _products.Add(product);
            //     product.number=1;
            // }
                
        }

        public void AddItems(IEnumerable<Product> items)
        {
            foreach(var item in items)
            {
                AddItem(item);
            }
        }

        public void CheckOut()
        {
            CalculateDiscount();
            this.BeforeDiscountTotalPrice=_products.Sum(x=> x.Price);
            this.TotalDiscount=_discounts.Sum(x=>x.Amount);
            this.TotalPrice=BeforeDiscountTotalPrice-TotalDiscount;
        }

        public void AddSpecification(ISpecification spec)
        {
            _specifications.Add(spec);
        }

        private void CalculateDiscount()
        {
            foreach(ISpecification spec in _specifications)
            {
                _discounts.AddRange(spec.Process(_products));
            }
        }
  }
}