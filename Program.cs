using System;

class Program
{
    public static void Main(string[] args)
    {
    }
}
class Product
{
    private string description;
    private double price;
    public double Price
    {
        get
        {
            return price;
        }
        set
            {
            if (value > 0) 
            {
                price = value;
            }
        }
    }
    public Product()
    {
        Console.WriteLine("Введите название товара");
        description = Console.ReadLine();
        Console.WriteLine("Введите цену товара");
        price = double.Parse(Console.ReadLine());
    }
    public Product(string _description, double _price)
    {
        description = _description;
        price = _price;
    }
    public override string ToString()
    {
        return "Товар - " + description + "\nЦена - " + price;
    }
}
abstract class Delivery
{
    protected string address;
    public string Address 
    {
        get 
        {
            return address;
        }
        set 
        {
            if (value is string)
            {
                address = value;
            }
            else 
            {
                address = value.ToString();
            }
        }
    }
    public Delivery() 
    {
        address = "адрес неустановлен";
    }
    public abstract void Deliver();
}

class HomeDelivery : Delivery
{
    public HomeDelivery() 
    {
        string str;
        Console.WriteLine("Введите ваш домашний адрес в формате <улица, номер дома>");
        str = Console.ReadLine();
        Address = str;
    }
    public override void Deliver()
    {
        Console.WriteLine($"Товар доставлен к вам домой по адресу {Address}"); ;
    }
}

class PickPointDelivery : Delivery
{
    public PickPointDelivery()
    {
        int key;
        string str;
        Console.WriteLine("Введите номер ближайшего к вам пункта выдачи\n1. Мичурина, 82\n2. Рабочая, 29\n3. Вольская, 77");
        key = int.Parse(Console.ReadLine());
        switch (key)
        {
            case 1:
                str = "Мичурина, 82";
                break;
            case 2:
                str = "Рабочая, 29";
                break;
            case 3:
                str = "Вольская, 77";
                break;
            default:
                Console.WriteLine("Введен некорректный номер, товар будет доставлен по адресу нахождения магазина");
                str = "Мичурина, 82";
                break;
        }
        Address = str;
    }
    public override void Deliver()
    {
        Console.WriteLine($"Товар доставлен в ближайший к вам пункт выдачи, находящийся по адресу {Address}"); ;
    }
}
class ShopDelivery : Delivery
{
    public ShopDelivery()
    {
        Address = "Мичурина, 82";
    }
    public override void Deliver()
    {
        Console.WriteLine($"Товар доставлен по адресу нахождения магазина {Address}"); ;
    }
}


class Order<TDelivery,T> where TDelivery : Delivery
{
    public TDelivery Delivery;

    private T Number;
    public T number
    {
        get {
            return Number;
        }
    }

    private Product Goods;

    public Order(TDelivery Del, Product prod, T Number)
    {
        Goods = prod;
        this.Number = Number;
        Delivery = Del;
    }
    public Order(TDelivery Del, T Number)
    {
        Goods = new Product();
        this.Number = Number;
        Delivery = Del;
    }
    public void DisplayAddress()
    {
        Console.WriteLine(Delivery.Address);
    }
    public T DisplayNumber() 
    {
        Console.WriteLine($"Номер заказа {Number}");
        return Number;
    }
    public static bool operator >(Order<TDelivery,T> order1, Order<TDelivery,T> order2)
    {
        int temp = order1.Number.ToString().CompareTo(order2.Number.ToString());
        if (temp > 0)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    public static bool operator <(Order<TDelivery, T> order1, Order<TDelivery, T> order2)
    {
        int temp = order1.Number.ToString().CompareTo(order2.Number.ToString());
        if (temp < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
class PrivilegedOrder<TDelivery,T>: Order<TDelivery, T> where TDelivery : Delivery
{
    private static bool privilege = true;
    public PrivilegedOrder(TDelivery Del, T Number) : base(Del, Number)
    {
    }
}

class MyOrders<TDelivery,T> where TDelivery : Delivery
{
    private Order<TDelivery, T>[] OrderList;

    public MyOrders(Order<TDelivery, T>[] OrderList)
    {
        this.OrderList = OrderList;
    }
    public Order<TDelivery, T> this[int index]
    {
        get
        {
            if (index >= 0 && index < OrderList.Length)
            {
                return OrderList[index];
            }
            else
            {
                return null;
            }
        }
        private set
        {
            if (index >= 0 && index < OrderList.Length)
            {
                OrderList[index] = value;
            }
        }
    }
    public Order<TDelivery, T> this[string name]
    {
        get
        {
            for (int i = 0; i < OrderList.Length; i++)
            {
                if (OrderList[i].number.ToString() == name)
                {
                    return OrderList[i];
                }
            }
            return null;
        }
    }
    public void BubbleSort()
    {
        Order<TDelivery, T> temp = null;

        for (int i = 0; i < OrderList.Length; i++)
        {
            for (int j = 0; j < OrderList.Length - 1; j++)
            {
                if (OrderList[j] > OrderList[j + 1])
                {
                    temp = OrderList[j + 1];
                    OrderList[j + 1] = OrderList[j];
                    OrderList[j] = temp;
                }
            }

        }
    }
}
static class ProductExtensions
{
    public static double GetPriceOfProduct(this Product temp)
    {
        Console.WriteLine($"Цена товара {temp.Price} рублей");
        double result = temp.Price;
        return result;
    }
}
