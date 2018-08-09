using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class SMSShoppingData
    {

        public static void Seed(ApplicationDBContext db)
        {
            if (!db.Products.Any())
            {
                //if the database table for products is empty then seed
                string details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.";
                var products = new List<Product>
                {
                    new Product{ProductName = "Women's Handbag", ProductPrice=10000, ProductImageUrl ="https://images-na.ssl-images-amazon.com/images/I/71t4DfxMoaL._UL1500_.jpg",ProductDescription=details},
                    new Product{ProductName = "Laptop", ProductPrice=70000,ProductImageUrl="https://img.purch.com/o/aHR0cDovL3d3dy5sYXB0b3BtYWcuY29tL2ltYWdlcy93cC9wdXJjaC1hcGkvaW5jb250ZW50LzIwMTYvMTEvMTQ3ODIwNzE4N182NzU0MDMuanBn",ProductDescription=details},
                    new Product{ProductName = "Leather Wallet", ProductPrice=5000,ProductImageUrl="https://rukminim1.flixcart.com/image/704/704/wallet-card-wallet/6/b/8/14023-hidelink-wallet-men-women-original-imae9hhmhsfc9djt.jpeg?q=70",ProductDescription=details},
                    new Product{ProductName="Samsung Galaxy S5",ProductPrice=25000,ProductImageUrl="https://drop.ndtv.com/TECH/product_database/images/2252014124325AM_635_samsung_galaxy_s5.jpeg",ProductDescription=details},
                    new Product{ProductName="Stud Earring",ProductPrice=12000,ProductImageUrl="http://www.americanswiss.co.za/images/size1/36202861.jpg",ProductDescription=details},
                    new Product{ProductName="External Monitor",ProductPrice=30000,ProductImageUrl="https://www.bhphotovideo.com/images/images2500x2500/asus_vp239h_p_23_widescreen_led_1204609.jpg",ProductDescription=details},
                    new Product{ProductName="Nerf Elite Gun",ProductPrice=1500,ProductImageUrl="https://images-na.ssl-images-amazon.com/images/I/71TgBgvJoNL._SL1500_.jpg",ProductDescription=details},
                    new Product{ProductName="Vinyl Player",ProductPrice=7600,ProductImageUrl="http://media.thisisitstores.co.uk/media/catalog/product/cache/1/image/1500x/9df78eab33525d08d6e5fb8d27136e95/b/r/brown-turntable.jpg",ProductDescription=details},
                    new Product{ProductName="Beats By Dre Headphones",ProductPrice=30000,ProductImageUrl="http://www.xxlmag.com/files/2017/09/beats-by-dre-studio3.jpg",ProductDescription=details},
                    new Product{ProductName="Amazon Dot",ProductPrice=12500,ProductImageUrl="https://images-na.ssl-images-amazon.com/images/I/41-v1fozy0L.jpg",ProductDescription=details},
                    new Product{ProductName="Amazon Kindle",ProductPrice=7900,ProductImageUrl="http://www.pcquest.com/wp-content/uploads/2015/05/Amazon-Kindle-Voyage.jpg",ProductDescription=details},
                    new Product{ProductName="Men's Running Shoes",ProductPrice=5600,ProductImageUrl="http://www.telegraph.co.uk/content/dam/men/2017/03/09/free-rn-motion-flyknit-running-shoe_trans_NvBQzQNjv4BqFw5DrrLYlyZ-K7XHa1fcScg7-G-O9ruFYe2E9rF5gio.jpg",ProductDescription=details},
                    new Product{ProductName="Women's Running Shoes",ProductPrice=5600,ProductImageUrl="https://media.kohlsimg.com/is/image/kohls/2992951_Black_White?wid=1000&hei=1000&op_sharpen=1",ProductDescription=details},
                    new Product{ProductName="Manicure Set",ProductPrice=1800,ProductImageUrl="https://imagehost.vendio.com/preview/ba/batterygallery/nailmanicureset.jpg",ProductDescription=details},
                    new Product{ProductName="Thor Keychain",ProductPrice=900,ProductImageUrl="https://images-na.ssl-images-amazon.com/images/I/61V-fuAWPVL._SL1500_.jpg",ProductDescription=details},
                    new Product{ProductName="Womens Winter Glove",ProductPrice=1600,ProductImageUrl="https://images-na.ssl-images-amazon.com/images/I/81DxQmUFCmL._UL1500_.jpg",ProductDescription=details},
                    new Product{ProductName="Umbrella",ProductPrice=1200,ProductImageUrl="https://cdn.shopify.com/s/files/1/0227/0033/products/Davek_Umbrella_Elite_Open_Straight.jpg?v=1487711086",ProductDescription=details},
                    new Product{ProductName="Laptop Backpack",ProductPrice=2900,ProductImageUrl="https://www.incase.com/media/catalog/product/i/n/incase-icon-slim-laptop-backpack-7.jpg",ProductDescription=details},
                    new Product{ProductName="Men's Socks",ProductPrice=990,ProductImageUrl="https://www.goodsamaritan.ms/uploads/1/2/7/7/12777965/s736176615899143325_p348_i1_w900.jpeg",ProductDescription=details},
                    new Product{ProductName="G-Shock Watch",ProductPrice=5700,ProductImageUrl="https://images-eu.ssl-images-amazon.com/images/I/81UQEHCDUGL._UL1500_.jpg",ProductDescription=details},
                    new Product{ProductName="Men's Sunglasses",ProductPrice=3000,ProductImageUrl="https://cdn.shopify.com/s/files/1/0193/6253/products/C005-01a_900x.jpg?v=1477344974",ProductDescription=details},

                };

                products.ForEach(p => db.Products.Add(p));
                db.SaveChanges();


            }

        }
    }
}
