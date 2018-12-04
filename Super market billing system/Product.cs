using System;

namespace Super_market_billing_system
{
    class Product
    {
        private string product_id;
        private string product_name;
        private int quantity;
        private float price;
        private int discount;
        private DateTime expire_date;

        public string Product_id
        {
            get
            {
                return product_id;
            }
            set
            {
                product_id = value;
            }
        }
        public string Product_name
        {
            get
            {
                return product_name;
            }
            set
            {
                product_name = value;
            }
        }
        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
            }
        }
        public float Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
        public int Discount
        {
            get
            {
                return discount;
            }
            set
            {
                discount = value;
            }
        }
        public DateTime Expire_date
        {
            get
            {
                return expire_date;
            }
            set
            {
                expire_date = value;
            }
        }
    }
}
