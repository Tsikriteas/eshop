using System;
using System.Collections.Generic;
using System.ComponentModel.Design;


public class User
{
    public string Username { get; set; }
    public int Password { get; set; }

    public User(string username, int password)
    {
        Username = username;
        Password = password;
    }
}
class Cart
{
    public int Number {get; set;}
    public string Name { get; set; }
    public int Price { get; set; }
    static List<Cart> Carts = new List<Cart>();
    //list cart new cart
    //public Cart cart = new Cart();

    public void addToCart(Product product)
    {
        Carts.Add(new Cart{Number = product.Number, Name = product.Name, Price = product.Price});
        double totalBill = Carts.Sum(Cart => Cart.Price);
        Console.WriteLine("Το συνολικό ποσό είναι: " + totalBill);
    }

        public void checkOut()
    {
        Console.WriteLine("Επιλέξτε τρόπο πληρωμής.");
        Console.WriteLine("1.Apple Pay    2.Αντικαταβολή");
        int payChoice = int.Parse(Console.ReadLine());
        switch(payChoice)
        {
            case 1:
                applePay();
                break;
            case 2:
                cash();
                break;
            default:
                Console.WriteLine("Μη έγγυρoς τρόπος πληρωμής.");
                break;
        }
    }

    public void applePay()
    {
        Console.WriteLine("Πληρωμή μέσω Apple Pay.");
    }
    public void cash()
    {
        Console.WriteLine("Πληρωμή με αντικαταβολή.");
    }
}
public class Product
{
    public int Number {get; set;}
    public string Name { get; set; }
    public int Price { get; set; }

    public Product(int number,string name, int price)
    {
        Number = number;
        Name = name;
        Price = price;
    }
}

public class Eshop
{
    string? LongedInUser = null;

    public List<User> Users = new List<User>()
    {
        new User("fanis", 123 )
    };

    public List<Product> Products = new List<Product>()
    {
        new Product(1,"laptop", 1000),
        new Product(2,"desktop", 1500 ),
        new Product(3,"mobile", 800),
        new Product(4,"bike", 500)
    };

    public static void Main(string[] args)
    {
        var eshop = new Eshop();

        while (true)
        {
            Console.WriteLine("1. Κάνε σύνδεση.");
            Console.WriteLine("2. Αν δεν έχεις λογαριασμό κάνε εγγραφή.");
            Console.WriteLine("3. Έξοδος");
            Console.Write("Επιλογή ");

            bool breakWhile = false;

            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    if (eshop.Login())
                    {
                        eshop.ShowProducts();
                        var item = new Cart();
                        //αντι για αριθμό να είναι το ιντεξ
                        Console.WriteLine("1. Συμπλήρωσε το όνομα του προιόντος που θα ήθελες να αγοράσεις με τον αριθμό του προιόντος: ");
                        int cartProduct = int.Parse(Console.ReadLine());
                        if (cartProduct >= 0 && cartProduct < eshop.Products.Count)
                        {
                            Product selectedProduct = eshop.Products[cartProduct];
                            item.addToCart(selectedProduct);
                            item.checkOut();
                        }
                        else
                        {
                            Console.WriteLine("Μη έγκυρος κωδικός.");
                        }
                    }
                    breakWhile = true;
                    break;
                case 2:
                    eshop.Register();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Μη έγγυρη δοκιμασία, δοκιμάστε ξανά.");
                    break;
            }
            if(breakWhile)
            {
                break;
            }
        }
    }

    void Register()
    {
        Console.WriteLine("Δώσε ένα όνομα χρήστη: ");
        string username = Console.ReadLine();
        Console.WriteLine("Δώσε κωδικό χρήστη: ");
        int password = int.Parse(Console.ReadLine());

        Users.Add(new User(username, password));
        Console.WriteLine("Επιτυχής εγγραφή");

    }

    bool Login()
    {
        Console.WriteLine("Όνομα χρήστη: ");
        string username = Console.ReadLine();
        Console.WriteLine("Κωδικός χρήστη: ");
        int password = int.Parse(Console.ReadLine());

        User user = Users.Find(u => u.Username == username && u.Password == password);

        if (user != null)
        {
            Console.WriteLine("Επιτυχής σύνδεση!");
            LongedInUser = username;
            return true;
        }
        else
        {
            Console.WriteLine("Αποτυχία σύνδεσης. Ελέγξτε τα στοιχεία σας.");
            return false;
        }
    }
    void ShowProducts()
    {
        Console.WriteLine("Τα διαθέσιμα προϊόντα είναι: ");
        foreach (Product product in Products)
        {
            Console.WriteLine(product.Name + " - " + product.Price);
        }
    }
}